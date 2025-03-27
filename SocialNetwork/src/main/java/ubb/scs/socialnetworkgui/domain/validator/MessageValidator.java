package ubb.scs.socialnetworkgui.domain.validator;

import ubb.scs.socialnetworkgui.domain.Message;

public class MessageValidator implements Validator<Message> {
    private static MessageValidator instance;
    private MessageValidator() {}

    public static MessageValidator getInstance() {
        if (instance == null) {
            instance = new MessageValidator();
        }
        return instance;
    }

    @Override
    public void validate(Message message) throws RepositoryException {
        String errors = "";
        if (message.getText().isEmpty())
            errors += "Message cannot be empty!";
        if (!errors.isEmpty())
            throw new RepositoryException(errors);
    }
}
