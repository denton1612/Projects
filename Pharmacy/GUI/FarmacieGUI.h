//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_FARMACIEGUI_H
#define LAB13_14_FARMACIEGUI_H

#include "../Service/Service.h"
#include "RetetaGUI.h"
#include "RetetaReadOnlyGUI.h"
#include "../ListModel/ListModel.h"

#include <utility>
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

class FarmacieGUI  : public QWidget{

private:
    Service& service;

    // lab_13 ------------------------------------------------------------------------------------------------
//    QListWidget* lista = new QListWidget; // lista unde se stocheaza medicamentele
    //----------------------------------------------------------------------------------------------------------------

    // lab_14 ------------------------------------------------------------------------------------------------
    ListModel* lista = new ListModel{service.getAll()}; // model list ul facut de mine
    QListView* vw = new QListView;
    // ----------------------------------------------------------------------------------------------------------------
    QHBoxLayout* lymain = new QHBoxLayout; // layout ul principal al ferestrei
    QFormLayout* fly = new QFormLayout;
    QVBoxLayout* left = new QVBoxLayout; // layout ul stang
    QVBoxLayout* middle = new QVBoxLayout; // layout ul din mij
    QVBoxLayout* right = new QVBoxLayout; // layout ul din dreapta (pt functiile retetei
    QVBoxLayout* slider = new QVBoxLayout;
    QHBoxLayout* btly1 = new QHBoxLayout;
    QHBoxLayout* btly2 = new QHBoxLayout;
    QLineEdit* denText = new QLineEdit; // casuta pt denumire
    QLineEdit* pretText = new QLineEdit; // casuta pt pret
    QLineEdit* prodText = new QLineEdit; // casuta pt producator
    QLineEdit* substText = new QLineEdit; // casuta pt substanta
    QLineEdit* filtruSubstText = new QLineEdit; // casuta pt filtrarea medicamentelor dupa substanta

    // butoanele ferestrei
    QPushButton* btAdauga = new QPushButton{"Adauga"};
    QPushButton* btAdaugaReteta = new QPushButton{"Adauga reteta"};
    QPushButton* btAdaugaRandom = new QPushButton{"Genereaza"};
    QPushButton* btSterge = new QPushButton{"Sterge"};
    QPushButton* btGoleste = new QPushButton{"Goleste"};
    QPushButton* btModifica = new QPushButton{"Modifica"};
    QPushButton* btSortDen = new QPushButton{"Sortare dupa denumire"};
    QPushButton* btSortProd= new QPushButton{"Sortare dupa producator"};
    QPushButton* btSortSubstPret = new QPushButton{"Sortare dupa subst si pret"};
    QPushButton* btFiltruPret = new QPushButton{"Filtrare dupa pret"};
    QPushButton* btFiltruSubst = new QPushButton{"Filtrare dupa subst"};
    QPushButton* btRetetaGUI = new QPushButton{"Reteta noua"};
    QPushButton* btRetetaRDonly = new QPushButton{"Reteta RDONLY noua"};
    QPushButton* btUndo = new QPushButton{"Undo"};
    QPushButton* btExit = new QPushButton{"Exit"};

    // spinbox pt filtrarea dupa pret
    QSpinBox* filtruPret = new QSpinBox;

    QSlider* sl = new QSlider(Qt::Vertical);
    QLabel* value = new QLabel;
    void initGUI(); // initializarea aspectului ferestrei
    void loadFarmacie(); // incarcarea in lista a medicamentelor
    void loadData(); //
    void initConnect(); // initializarea conexiunilor intre actiunile user ului si metode
    void ConnectAdauga();
    void ConnectAdaugaReteta();
    void ConnectAdaugaRandom();
    void ConnectGolesteReteta();
    void ConnectSlLabel();
    void ConnectRetetaGUINoua();
    void ConnectRetetaRDONLY();
    void ConnectSterge();
    void ConnectModifica();
    void ConnectSortDen();
    void ConnectSortProd();
    void ConnectSortSubstPret();
    void ConnectFiltruPret();
    void ConnectFiltruSubst();
    void ConnectUndo();
    void ConnectExit();
    void ConnectList(); // pentru listwidget ul din qt
    void ConnectModelList(); // pentru model list ul facut de mine
    void reload();

public:
    FarmacieGUI(Service& s): service{s}, QWidget(){
//        loadData();
        initGUI();
        initConnect();
    };

    void run(){
        show();
    }

};


#endif //LAB13_14_FARMACIEGUI_H
