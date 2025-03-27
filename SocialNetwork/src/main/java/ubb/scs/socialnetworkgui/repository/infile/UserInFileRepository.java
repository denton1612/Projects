package ubb.scs.socialnetworkgui.repository.infile;

import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.domain.validator.Validator;

import java.util.Optional;

public class UserInFileRepository extends InFileRepository<Long, User> {

    @Override
    protected void initCreateEntityFunction() {
        createEntityFunction = (s) -> {
            String [] attributes = s.split(",");
            return new User(Long.parseLong(attributes[0]), attributes[1], attributes[2], attributes[3]);
        };
    }

    @Override
    protected void initCreateLineFunction() {
        createLineFunction = (u) -> u.getUsername() + "," + u.getPassword();
    }

    public UserInFileRepository(String filePath, Validator<User> validator) {
        super(filePath, validator);
    }

    @Override
    public Optional<User> delete(Long id) {
        Optional<User> optional = super.delete(id);
        if (optional.isPresent()) {
            User user = optional.get();
            user.getFriends().forEach(friend -> {find(friend).get().removeFriend(user.getId());});
        }
        return optional;
    }
}
