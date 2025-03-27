package ubb.scs.socialnetworkgui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import ubb.scs.socialnetworkgui.controller.AppController;
import ubb.scs.socialnetworkgui.controller.Controller;
import ubb.scs.socialnetworkgui.repository.database.FriendshipDBRepository;
import ubb.scs.socialnetworkgui.repository.database.MessageDBRepository;
import ubb.scs.socialnetworkgui.repository.database.UserDBRepository;
import ubb.scs.socialnetworkgui.service.MessageService;
import ubb.scs.socialnetworkgui.service.UserFriendshipService;

import java.io.IOException;

public class HelloApplication extends Application {
    private UserFriendshipService userFriendshipService;
    private MessageService messageService;

    private void construct() {
        UserDBRepository userDBRepository = new UserDBRepository("jdbc:postgresql://localhost:5432/SocialNetwork", "postgres", "200416");
        FriendshipDBRepository friendshipDBRepository = new FriendshipDBRepository("jdbc:postgresql://localhost:5432/SocialNetwork", "postgres", "200416");
        MessageDBRepository messageDBRepository = new MessageDBRepository("jdbc:postgresql://localhost:5432/SocialNetwork", "postgres", "200416");
        userFriendshipService = new UserFriendshipService(userDBRepository, friendshipDBRepository);
        messageService = new MessageService(messageDBRepository);
    }

    @Override
    public void start(Stage stage) throws IOException {
        construct();

        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("login-view.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 452, 479);
        Controller controller = fxmlLoader.getController();
        controller.setStage(stage);
        controller.setServices(userFriendshipService, messageService);
        stage.setTitle("Social App");
        stage.setScene(scene);
        stage.show();

        Stage stage1 = new Stage();
        stage.setTitle("Social App");
        FXMLLoader fxmlLoader1 = new FXMLLoader(HelloApplication.class.getResource("login-view.fxml"));
        Scene scene1 = new Scene(fxmlLoader1.load(), 452, 479);
        Controller controller1 = fxmlLoader1.getController();
        controller1.setStage(stage1);
        controller1.setServices(userFriendshipService, messageService);
        stage1.setScene(scene1);
        stage1.show();
    }

    public static void main(String[] args) {
        launch();
    }
}