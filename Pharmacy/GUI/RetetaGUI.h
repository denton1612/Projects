//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_RETETAGUI_H
#define LAB13_14_RETETAGUI_H

#include "../Service/Service.h"

#include <QApplication>
#include <QLabel>
#include <QPushButton>
#include <QWidget>
#include <QListWidget>
#include <QListWidgetItem>
#include <QTableWidget>
#include <QBoxLayout>
#include <QSpinBox>
#include <QFormLayout>
#include <QLabel>
#include <QLineEdit>
#include <QDialog>
#include <QMessageBox>

class RetetaGUI : public QWidget, public Observer{
private:
    Service & service;
    QTableWidget* reteta = new QTableWidget{10, 4};
    QPushButton* btAdaugaRandom = new QPushButton{"Adauga reteta random"};
    QPushButton* btStergeTot = new QPushButton{"Goleste reteta"};
    QPushButton* btExport = new QPushButton{"Exporta"};
    QLineEdit* denText = new QLineEdit;
    QLineEdit* prodText = new QLineEdit;
    QLineEdit* fisier = new QLineEdit; // casuta pt fisier
    QSlider* sl = new QSlider(Qt::Vertical);
    QLabel* val = new QLabel;
    void loadReteta();
    void initGUI();
    void ConnectExport();
    void ConnectAdaugaRandom();
    void ConnectGolesteReteta();
    void ConnectSlLabel();
    void initConnect();
    void update() override{
        loadReteta();
    }
public:
    explicit RetetaGUI(Service& s): service{s}, QWidget(), Observer() {
        initGUI();
        initConnect();
        loadReteta();
        service.addObserver(this);
    }
    ~RetetaGUI() override {
        service.removeObserver(this);
    }

};


#endif //LAB13_14_RETETAGUI_H
