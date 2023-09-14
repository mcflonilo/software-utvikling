module com.example.lecture1 {
    requires javafx.controls;
    requires javafx.fxml;


    opens com.example.lecture1 to javafx.fxml;
    exports com.example.lecture1;
}