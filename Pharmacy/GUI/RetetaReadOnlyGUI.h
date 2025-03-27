//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_RETETAREADONLYGUI_H
#define LAB13_14_RETETAREADONLYGUI_H
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
#include <QPaintEvent>
#include <QPainter>

class RetetaReadOnlyGUI : public Observer, public QWidget{
private:
    Service & service;
    void paintEvent(QPaintEvent*) override;
public:
    RetetaReadOnlyGUI(Service & s): service{s} {
        service.addObserver(this);
    }

    ~RetetaReadOnlyGUI() {
        service.removeObserver(this);
    }

    void update() {
        repaint();
    }
};


#endif //LAB13_14_RETETAREADONLYGUI_H
