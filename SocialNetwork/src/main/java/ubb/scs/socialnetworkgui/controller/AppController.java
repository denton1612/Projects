package ubb.scs.socialnetworkgui.controller;

import javafx.application.Platform;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import ubb.scs.socialnetworkgui.domain.Friendship;
import ubb.scs.socialnetworkgui.domain.Status;
import ubb.scs.socialnetworkgui.domain.User;
import ubb.scs.socialnetworkgui.domain.dto.FriendshipDTOFilter;
import ubb.scs.socialnetworkgui.utils.event.DataChangedEvent;
import ubb.scs.socialnetworkgui.utils.observer.Observer;
import ubb.scs.socialnetworkgui.utils.paging.Page;
import ubb.scs.socialnetworkgui.utils.paging.Pageable;

import java.io.IOException;
import java.util.*;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class AppController extends Controller implements Observer<DataChangedEvent<Long, User>> {

    private User user;

    @FXML
    private TabPane tabPane;

    // About tab
    @FXML
    private TextField usernameField;
    @FXML
    private TextField emailField;
    @FXML
    private TextField passwordField;
    @FXML
    private TextField visiblePasswordField;
    @FXML
    private CheckBox visibleCheckBox;
    //

    // Friends tab
    @FXML
    private TableView<User> tableViewFriends;
    @FXML
    private TableColumn<User, String> tableColumnUsernameFriends;
    @FXML
    private TableColumn<User, String> tableColumnEmailFriends;
    @FXML
    private TableColumn<User, String> tableColumnDateFriends;
    @FXML
    private TextField usernameFieldFriends;
    private final ObservableList<User> friends = FXCollections.observableArrayList();
    //

    // Requests tab
    @FXML
    private TableView<User> tableViewRequests;
    @FXML
    private TableColumn<User, String> tableColumnUsernameRequests;
    @FXML
    private TableColumn<User, String> tableColumnEmailRequests;
    @FXML
    private TableColumn<User, String> tableColumnDateRequests;
    @FXML
    private TextField usernameFieldRequests;
    @FXML
    private Button prevButtonRequests;
    @FXML
    private Button nextButtonRequests;
    @FXML
    private Label pageLabelRequests;
    private final FriendshipDTOFilter friendshipDTOFilter = new FriendshipDTOFilter();
    private int currentPage = 1;
    private int maxPage = 1;
    private final int onPage = 5;
    private final ObservableList<User> requests = FXCollections.observableArrayList();
    //

    // Others tab
    @FXML
    private TableView<User> tableViewOthers;
    @FXML
    private TableColumn<User, String> tableColumnUsernameOthers;
    @FXML
    private TableColumn<User, String> tableColumnEmailOthers;
    @FXML
    private TextField usernameFieldOthers;
    private final ObservableList<User> others = FXCollections.observableArrayList();
    //

    @FXML
    private void initialize() {
        tableColumnUsernameFriends.setCellValueFactory(new PropertyValueFactory<>("username"));
        tableColumnEmailFriends.setCellValueFactory(new PropertyValueFactory<>("email"));
        tableColumnDateFriends.setCellValueFactory(cellData -> {
            User user = cellData.getValue();
            Friendship friendship = getService().findFriendship(this.user.getId(), user.getId());
            return new SimpleStringProperty(friendship.getCreatedAt().toString());
        });
        tableViewFriends.setItems(friends);
        tableViewFriends.getSelectionModel().setSelectionMode(SelectionMode.MULTIPLE);
        usernameFieldFriends.textProperty().addListener(o -> handleUsernameFieldChangedFriends());

        tableColumnUsernameRequests.setCellValueFactory(new PropertyValueFactory<>("username"));
        tableColumnEmailRequests.setCellValueFactory(new PropertyValueFactory<>("email"));
        tableColumnDateRequests.setCellValueFactory(cellData -> {
            User user = cellData.getValue();
            Friendship friendship = getService().findFriendship(this.user.getId(), user.getId());
            return new SimpleStringProperty(friendship.getCreatedAt().toString());
        });
        tableViewRequests.setItems(requests);
        tableViewRequests.getSelectionModel().setSelectionMode(SelectionMode.SINGLE);
        usernameFieldRequests.textProperty().addListener(o -> handleUsernameFieldChangedRequests());

        tableColumnUsernameOthers.setCellValueFactory(new PropertyValueFactory<>("username"));
        tableColumnEmailOthers.setCellValueFactory(new PropertyValueFactory<>("email"));
        tableViewOthers.setItems(others);
        usernameFieldOthers.textProperty().addListener(o -> handleUsernameFieldChangedOthers());
    }

    private void handleUsernameFieldChangedFriends() {
        Predicate<User> startsWith = (user -> user.getUsername().startsWith(usernameFieldFriends.getText()));
        friends.clear();
        Set<User> users = getService().getFriends(user.getId()).stream()
                .filter(startsWith)
                .collect(Collectors.toSet());
        friends.setAll(users);
    }

    private void handleUsernameFieldChangedRequests() {
        Predicate<Friendship> startsWith = (friendship -> getService().findUser(friendship.getFirst()).getUsername().startsWith(usernameFieldRequests.getText()));
        this.requests.clear();
        Set<Friendship> requests = getService().getReceivedRequests(user.getId()).stream()
                .filter(startsWith)
                .collect(Collectors.toSet());
        this.requests.setAll(requests.stream().map(request -> getService().findUser(request.getFirst())).collect(Collectors.toSet()));
    }

    private void handleUsernameFieldChangedOthers() {
        Predicate<User> startsWith = (user -> user.getUsername().startsWith(usernameFieldOthers.getText()));
        others.clear();
        Set<User> others = getService().getOthers(user.getId()).stream()
                .filter(startsWith)
                .collect(Collectors.toSet());
        this.others.setAll(others);
    }

    private void initFriendsTab() {
        Set<User> friends = getService().getFriends(user.getId());
        this.friends.setAll(friends);
    }

    private void initRequestsTab() {
        friendshipDTOFilter.setStatus(Optional.of(Status.WAITING));
        friendshipDTOFilter.setReceiverID(Optional.of(user.getId()));
        if (currentPage > maxPage)
            currentPage = maxPage;
        Page<Friendship> requestPage = getService().findAllOnPage(new Pageable(onPage, currentPage), friendshipDTOFilter);
        maxPage = (int) Math.ceil(((double) requestPage.getTotalNumberOfElements() / onPage));
        if (maxPage == 0)
            maxPage = 1;
        prevButtonRequests.setDisable(currentPage == 1);
        nextButtonRequests.setDisable(currentPage == maxPage);
        Set<Friendship> requests = StreamSupport.stream(requestPage.getElementsOnPage().spliterator(), false)
                .collect(Collectors.toSet());
        this.requests.setAll(requests.stream().map(request -> getService().findUser(request.getFirst())).collect(Collectors.toSet()));
        pageLabelRequests.setText("Page " + currentPage + " of " + maxPage);
    }

    private void initOthersTab() {
        this.others.setAll(getService().getOthers(user.getId()));
    }

    private void initAboutTab() {
        usernameField.setText(user.getUsername());
        emailField.setText(user.getEmail());
        passwordField.setText(user.getPassword());
        visiblePasswordField.setText(user.getPassword());
        visiblePasswordField.visibleProperty().set(false);
        visibleCheckBox.setOnAction(e -> {
            if (visibleCheckBox.isSelected()) {
                passwordField.visibleProperty().set(false);
                visiblePasswordField.visibleProperty().set(true);
            }
            else {
                passwordField.visibleProperty().set(true);
                visiblePasswordField.visibleProperty().set(false);
            }
        });
    }

    private void initAllTabs() throws IOException {
        initAboutTab();
        initFriendsTab();
        initRequestsTab();
        initOthersTab();
    }

    public void setUser(User user) throws IOException {
        this.user = user;
        initAllTabs();
        getService().addObserver(this);
    }

    public void openTab(int tabIndex) {
        Platform.runLater(() -> tabPane.getSelectionModel().select(tabIndex));
    }

    @FXML
    private void handleLogoutButton() throws IOException {
        changeScene("/ubb/scs/socialnetworkgui/login-view.fxml");
    }

    @FXML
    private void handleSendMessage() {
        ObservableList<User> selectedFriends = tableViewFriends.getSelectionModel().getSelectedItems();
        try {
            FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("/ubb/scs/socialnetworkgui/message-view.fxml"));
            Scene scene = new Scene(fxmlLoader.load());
            MessageController controller = fxmlLoader.getController();
            controller.setServices(getService(), getMessageService());
            controller.setUsers(user, selectedFriends.stream().toList());
            controller.setStage(getStage());
            getStage().setScene(scene);
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }

    @FXML
    private void handleDeleteFriend() {
        User selectedUser = tableViewFriends.getSelectionModel().getSelectedItem();
        getService().deleteFriendship(user.getId(), selectedUser.getId());
    }

    @FXML
    private void handleSendRequest() {
        User selectedUser = tableViewOthers.selectionModelProperty().get().getSelectedItem();
        getService().sendFriendRequest(user.getId(), selectedUser.getId());
    }

    @FXML
    private void handleAcceptRequest() {
        User selectedUser = tableViewRequests.selectionModelProperty().get().getSelectedItem();
        getService().acceptFriendRequest(user.getId(), selectedUser.getId());
    }

    @FXML
    private void handleDeclineRequest() {
        User selectedUser = tableViewRequests.selectionModelProperty().get().getSelectedItem();
        getService().declineFriendRequest(user.getId(), selectedUser.getId());
    }

    @Override
    public void update(DataChangedEvent<Long, User> event) {
        switch (event.getType()) {
            case ADD_USER -> others.add(event.getData());
            case ADD_REQUEST -> {
                if (Objects.equals(user.getId(), event.getOldData().getId())) {
                    Alert alert = new Alert(Alert.AlertType.INFORMATION);
                    alert.setTitle("Add Request");
                    alert.setHeaderText(null);
                    alert.setContentText("You received a request from " + event.getData().getUsername());
                    alert.initOwner(getStage());
                    alert.showAndWait();
                    initRequestsTab();
                    others.remove(event.getData());
                }
                if (Objects.equals(user.getId(), event.getData().getId())) {
                    others.remove(event.getOldData());
                }
            }
            case UPDATE_FRIENDSHIP -> {
                if (Objects.equals(user.getId(), event.getData().getId())) {
                    initRequestsTab();
                    friends.add(event.getOldData());
                }
                if (Objects.equals(user.getId(), event.getOldData().getId())) {
                    friends.add(event.getData());
                }
            }
            case DELETE_REQUEST -> {
                if (Objects.equals(user.getId(), event.getData().getId())) {
                    initRequestsTab();
                    others.add(event.getOldData());
                }
                if (Objects.equals(user.getId(), event.getOldData().getId())) {
                    others.add(event.getData());
                }
            }
            case DELETE_FRIENDSHIP -> {
                if (Objects.equals(user.getId(), event.getData().getId())) {
                    friends.remove(event.getOldData());
                    others.add(event.getOldData());
                }
                if (Objects.equals(user.getId(), event.getOldData().getId())) {
                    friends.remove(event.getData());
                    others.add(event.getData());
                }
            }
        }
    }

    @FXML
    public void handlePrevButtonRequests() {
        currentPage--;
        initRequestsTab();
    }

    @FXML
    public void handleNextButtonRequests() {
        currentPage++;
        initRequestsTab();
    }
}
