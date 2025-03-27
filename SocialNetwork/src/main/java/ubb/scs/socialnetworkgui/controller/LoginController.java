package ubb.scs.socialnetworkgui.controller;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.service.ServiceException;

import java.io.IOException;

public class LoginController extends Controller {
    @FXML
    private TextField usernameField;
    @FXML
    private PasswordField passwordField;
    @FXML
    private Label errorLabel;

    @FXML
    private void handleCreateAccountButton() throws IOException {
        changeScene("/ubb/scs/socialnetworkgui/create_account-view.fxml");
    }

    @FXML
    private void handleLoginButton() {
        String username = usernameField.getText();
        String password = passwordField.getText();
        try {
            User user = getService().validateAccount(username, password);
            try {
                FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/ubb/scs/socialnetworkgui/app-view.fxml"));
                Scene scene = new Scene(fxmlLoader.load());
                AppController controller = fxmlLoader.getController();
                controller.setServices(getService(), getMessageService());
                controller.setUser(user);
                controller.setStage(getStage());
                getStage().setScene(scene);
            }
            catch (IOException e) {
                e.printStackTrace();
            }
        }
        catch (ServiceException e) {
            errorLabel.setVisible(true);
        }
    }
}
