package ubb.scs.socialnetworkgui.controller;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import ubb.scs.socialnetworkgui.domain.validator.RepositoryException;
import ubb.scs.socialnetworkgui.service.ServiceException;

import java.io.IOException;

public class CreateAccountController extends Controller {
    @FXML
    private TextField usernameField;
    @FXML
    private TextField emailField;
    @FXML
    private PasswordField passwordField;
    @FXML
    private PasswordField passwordConfirmField;
    @FXML
    private Label errorLabel;
    @FXML
    private Label successLabel;

    @FXML
    private void handleBackToLoginButton() throws IOException {
        changeScene("/ubb/scs/socialnetworkgui/login-view.fxml");
    }

    @FXML
    private void handleCreateAccountButton() {
        // TODO: improve validation

        successLabel.setVisible(false);
        String errors = "";
        errorLabel.setText(errors);
        String username = usernameField.getText();
        String email = emailField.getText();
        String password = passwordField.getText();
        String passwordConfirm = passwordConfirmField.getText();
        if (password.equals(passwordConfirm)) {
            try {
                getService().addUser(username, email, password);
            }
            catch (ServiceException | RepositoryException e) {
                errors += e.getMessage() + "\n";
            }
        }
        else {
            errors += "Passwords do not match!\n";
        }
        errorLabel.setText(errors);
        if (errors.isEmpty())
            successLabel.setVisible(true);
    }
}
