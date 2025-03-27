package ubb.scs.socialnetworkgui.domain.validator;

import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.utils.StringUtility;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/*
    Folosesc sablonul Singleton pentru clasa UserValidator, deoarece nu este nevoie de existenta mai multor instante
*/

public class UserValidator implements Validator<User> {

    private static String emailRegex = "[a-zA-Z][a-zA-Z0-9]+@[a-z]+\\.com";
    private static UserValidator instance = null;
    private UserValidator() {
    }

    public static UserValidator getInstance() {
        if (instance == null) {
            instance = new UserValidator();
        }
        return instance;
    }

    private boolean validateEmail(String email) {
        Pattern pattern = Pattern.compile(emailRegex);
        Matcher matcher = pattern.matcher(email);
        return matcher.matches();
    }

    private boolean validatePassword(String password) {
        return password.length() >= 8 && StringUtility.hasAlpha(password) && StringUtility.hasDigit(password) && StringUtility.hasSpecial(password);
    }

    @Override
    public void validate(User user) throws RepositoryException {
        String errors = "";
        if (user.getUsername().isEmpty()) {
            errors += "Username is invalid! ";
        }
        if (!validateEmail(user.getEmail())) {
            errors += "Email is invalid! ";
        }
        if (!validatePassword(user.getPassword())) {
            errors += "Password must contain at least 8 characters and include at least 1 letter, 1 digit and 1 special character. ";
        }
        if (!errors.isEmpty()) {
            throw new RepositoryException(errors);
        }
    }
}
