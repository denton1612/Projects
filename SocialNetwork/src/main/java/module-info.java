module ubb.scs.socialnetwork {
    requires javafx.controls;
    requires javafx.fxml;


    opens ubb.scs.socialnetwork to javafx.fxml;
    exports ubb.scs.socialnetwork;
}