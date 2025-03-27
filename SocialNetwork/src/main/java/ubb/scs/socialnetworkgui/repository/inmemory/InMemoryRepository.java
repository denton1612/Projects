package ubb.scs.socialnetworkgui.repository.inmemory;

import ubb.scs.socialnetworkgui.domain.Entity;
import ubb.scs.socialnetworkgui.domain.validator.Validator;
import ubb.scs.socialnetworkgui.repository.Repository;

import java.util.HashMap;
import java.util.Map;
import java.util.Objects;
import java.util.Optional;
import java.util.function.Predicate;

public class InMemoryRepository<ID, E extends Entity<ID>> implements Repository<ID, E> {
    protected final Map<ID, E> entities;
    protected final Validator<E> validator;
    protected final Predicate<Object> isNullPredicate;

    public InMemoryRepository(Validator<E> validator) {
        this.entities = new HashMap<>();
        this.validator = validator;
        this.isNullPredicate = Objects::isNull;
    }

    public Iterable<E> findAll() {
        return entities.values();
    }

    @Override
    public Optional<E> find(ID id) {
        if (isNullPredicate.test(id))
            throw new IllegalArgumentException("ID cannot be null!");
        return Optional.ofNullable(entities.get(id));
    }

    @Override
    public Optional<E> add(E entity) {
        if (isNullPredicate.test(entity))
            throw new IllegalArgumentException("Entity cannot be null!");
        validator.validate(entity);
        if (entities.containsKey(entity.getId()))
            return Optional.of(entity);
        entities.put(entity.getId(), entity);
        return Optional.empty();
    }

    @Override
    public Optional<E> update(E entity) {
        if (isNullPredicate.test(entity))
            throw new IllegalArgumentException("Entity cannot be null!");
        validator.validate(entity);
        if (!entities.containsKey(entity.getId()))
            return Optional.of(entity);
        entities.put(entity.getId(), entity);
        return Optional.empty();
    }

    @Override
    public Optional<E> delete(ID id) {
        if (isNullPredicate.test(id))
            throw new IllegalArgumentException("Id cannot be null!");
        return Optional.ofNullable(entities.remove(id));
    }
}
