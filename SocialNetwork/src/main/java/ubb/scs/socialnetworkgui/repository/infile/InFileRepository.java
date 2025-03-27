package ubb.scs.socialnetworkgui.repository.infile;

import ubb.scs.socialnetworkgui.domain.Entity;
import ubb.scs.socialnetworkgui.domain.validator.Validator;
import ubb.scs.socialnetworkgui.repository.inmemory.InMemoryRepository;

import java.io.*;
import java.util.Optional;
import java.util.function.Function;
import java.util.function.Predicate;

public abstract class InFileRepository<ID, E extends Entity<ID>> extends InMemoryRepository<ID, E> {
    private final String filePath;
    protected Function<String, E> createEntityFunction;
    protected Function<E, String> createLineFunction;
    private final Predicate<Optional<E>> isEmptyPredicate;

    protected abstract void initCreateEntityFunction();
    protected abstract void initCreateLineFunction();

    public InFileRepository(String filePath, Validator<E> validator) {
        super(validator);
        this.filePath = filePath;
        initCreateEntityFunction();
        initCreateLineFunction();
        loadFromFile();
        isEmptyPredicate = Optional::isEmpty;}
    
    protected E createEntity(String line) {
        return createEntityFunction.apply(line);
    }
    
    protected String createLine(E entity) {
        return createLineFunction.apply(entity);
    }
    
    private void loadFromFile() {
        try (BufferedReader bufferedReader = new BufferedReader(new FileReader(filePath))) {
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                E entity = createEntity(line);
                super.add(entity);
            }
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    private void saveToFile() {
        try (BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(filePath))) {
            for (E entity : super.findAll()) {
                bufferedWriter.write(createLine(entity));
                bufferedWriter.newLine();
            }
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public Optional<E> add(E entity) {
        Optional<E> optional = super.add(entity);
        if (isEmptyPredicate.test(optional)) {
            saveToFile();
        }
        return optional;
    }

    @Override
    public Optional<E> update(E entity) {
        Optional<E> optional = super.update(entity);
        if (isEmptyPredicate.test(optional)) {
            saveToFile();
        }
        return optional;
    }

    @Override
    public Optional<E> delete(ID id) {
        Optional<E> optional = super.delete(id);
        if (isEmptyPredicate.negate().test(optional)) {
            saveToFile();
        }
        return optional;
    }

}
