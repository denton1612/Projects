package ubb.scs.socialnetworkgui.controller;

import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import ubb.scs.socialnetworkgui.service.MessageService;
import ubb.scs.socialnetworkgui.service.UserFriendshipService;

import java.io.IOException;

public class Controller {
    private UserFriendshipService userFriendshipService;
    private MessageService messageService;
    private Stage stage;

    public UserFriendshipService getService() {
        return userFriendshipService;
    }
    public MessageService getMessageService() {
        return messageService;
    }

    public void setServices(UserFriendshipService userFriendshipService, MessageService messageService) {
        this.userFriendshipService = userFriendshipService;
        this.messageService = messageService;
    }

    public Stage getStage() {
        return stage;
    }

    public void setStage(Stage stage) {
        this.stage = stage;
    }

    protected void changeScene(String pathFile) throws IOException {
        try {
            FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource(pathFile));
            Scene scene = new Scene(fxmlLoader.load());
            Controller controller = fxmlLoader.getController();
            controller.setServices(userFriendshipService, messageService);
            controller.setStage(stage);
            stage.setScene(scene);
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
}
