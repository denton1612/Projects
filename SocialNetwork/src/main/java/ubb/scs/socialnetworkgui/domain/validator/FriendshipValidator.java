package ubb.scs.socialnetworkgui.domain.validator;

import ubb.scs.socialnetworkgui.domain.Friendship;

public class FriendshipValidator implements Validator<Friendship> {
    private static FriendshipValidator instance;
    private FriendshipValidator() {}

    public static FriendshipValidator getInstance() {
        if (instance == null) {
            instance = new FriendshipValidator();
        }
        return instance;
    }

    @Override
    public void validate(Friendship friendship) throws RepositoryException {
        String errors = "";
        if (friendship.getFirst().equals(friendship.getSecond())) {
            errors += "Users can't be friends with themselves!";
        }
        if (!errors.isEmpty()) {
            throw new RepositoryException(errors);
        }
    }
}
