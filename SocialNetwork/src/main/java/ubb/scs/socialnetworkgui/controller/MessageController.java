package ubb.scs.socialnetworkgui.controller;

import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.VBox;
import ubb.scs.socialnetworkgui.domain.Message;
import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.event.EventTypes;
import ubb.scs.socialnetworkgui.utils.observer.Observer;

import java.io.IOException;
import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class MessageController extends Controller implements Observer<DataChangedEvent<Long, Message>> {
    private User fromUser;
    private List<User> toUsers;

    //Messages tab
    @FXML
    private Label toLabel;
    @FXML
    private ListView<Message> listViewMessages;
    @FXML
    private TextField messageFieldMessages;
    @FXML
    private Button sendButton;
    private final ObservableList<Message> messages = FXCollections.observableArrayList();

    @FXML
    private void initialize() {
        listViewMessages.setItems(messages);
        listViewMessages.setCellFactory(lv -> new ListCell<>() {
            @Override
            protected void updateItem(Message message, boolean empty) {
                super.updateItem(message, empty);
                if (empty || message == null || toUsers.size() > 1) {
                    setText(null);
                    setGraphic(null);
                } else {
                    if (Objects.equals(message.getFrom(), fromUser.getId())) {
                        setText("me: " + message.getText());
                        setAlignment(Pos.CENTER_RIGHT);
                    } else {
                        setText(getService().findUser(message.getFrom()).getUsername() + ": " + message.getText());
                        setAlignment(Pos.CENTER_LEFT);
                    }
                }
            }
        });
        // if the textField for new message is empty, we want to disable the send button
        messageFieldMessages.textProperty().addListener(o -> sendButton.setDisable(messageFieldMessages.getText().isEmpty()));
        // at the start of the application, the textField for message is surely empty
        sendButton.setDisable(true);
        // we have to set the usernames of users who will receive the messages
    }

    private void initializeToLabel() {
        StringBuilder users = new StringBuilder();
        if (toUsers.size() > 1) {
            for (int i = 0; i < toUsers.size() - 1; i++) {
                users.append(toUsers.get(i).getUsername());
                users.append(", ");
            }
            users.append(toUsers.getLast().getUsername());
        }
        else {
            users.append(toUsers.getFirst().getUsername());
        }
        toLabel.setText(users.toString());
        toLabel.setAlignment(Pos.CENTER);
    }

    private void loadConversation() {
        if (toUsers.size() == 1) {
            List<Message> conversation = getMessageService().getConversation(fromUser.getId(), toUsers.getFirst().getId());
            messages.addAll(conversation.stream().sorted(Comparator.comparing(Message::getTimestamp)).toList());
        }
    }

    public void setUsers(User fromUser, List<User> toUsers) {
        this.fromUser = fromUser;
        this.toUsers = toUsers;
        initializeToLabel();
        loadConversation();
        getMessageService().addObserver(this);
    }

    @FXML
    private void handleSendMessage() {
        String text = messageFieldMessages.getText();
        for (User toUser : toUsers) {
            getMessageService().addMessage(fromUser.getId(), toUser.getId(), text, null);
        }
        messageFieldMessages.setText("");
    }

    @FXML
    private void handleCloseMessagesTab() {
        try {
            FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/ubb/scs/socialnetworkgui/app-view.fxml"));
            Scene scene = new Scene(fxmlLoader.load());
            AppController controller = fxmlLoader.getController();
            controller.openTab(1);
            controller.setServices(getService(), getMessageService());
            controller.setUser(fromUser);
            controller.setStage(getStage());
            getStage().setScene(scene);
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void update(DataChangedEvent<Long, Message> event) {
        if (Objects.requireNonNull(event.getType()) == EventTypes.ADD_MESSAGE) {// TODO: manage the situation of more than one toUsers !!!
            if (toUsers.size() == 1) {
                Predicate<Message> sentMessage = message -> message.getFrom().equals(fromUser.getId()) && message.getTo().equals(toUsers.getLast().getId());
                Predicate<Message> receivedMessage = message -> message.getFrom().equals(toUsers.getLast().getId()) && message.getTo().equals(fromUser.getId());
                if (sentMessage.or(receivedMessage).test(event.getData())) {
                    messages.add(event.getData());
                }
            }
        }
    }
}
