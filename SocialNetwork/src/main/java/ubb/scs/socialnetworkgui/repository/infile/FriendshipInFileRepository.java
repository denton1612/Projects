package ubb.scs.socialnetworkgui.repository.infile;

import ubb.scs.socialnetworkgui.domain.Friendship;
import ubb.scs.socialnetworkgui.domain.Tuple;
import ubb.scs.socialnetworkgui.domain.validator.Validator;

import java.time.LocalDate;

public class FriendshipInFileRepository extends InFileRepository<Tuple<Long, Long>, Friendship> {

    @Override
    protected void initCreateEntityFunction() {
        createEntityFunction = (s) -> {
            String [] attributes = s.split(",");
            return new Friendship(Long.parseLong(attributes[0]), Long.parseLong(attributes[1]), LocalDate.parse(attributes[2]));
        };
    }

    @Override
    protected void initCreateLineFunction() {
        createLineFunction = (f) -> f.getFirst() + "," + f.getSecond() + "," + f.getCreatedAt();
    }

    public FriendshipInFileRepository(String filePath, Validator<Friendship> validator) {
        super(filePath, validator);
    }
}
