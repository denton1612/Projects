//
// Created by Știube Denis on 26.05.2024.
//

#include "FarmacieGUI.h"

void FarmacieGUI::initGUI() {
    setLayout(lymain); // setarea layout ului principal

    // lab_14 ------------------------------------------------------------------------------------------------

    vw->setModel(lista);
    left->addWidget(vw);

    // ----------------------------------------------------------------------------------------------------------------

//    left->addWidget(lista);
    auto* btF = new QHBoxLayout;
    btF->addWidget(btAdauga);
    btF->addWidget(btSterge);
    btF->addWidget(btModifica);
    left->addLayout(btF);
    btly1->addWidget(btSortDen);
    btly2->addWidget(btSortProd);
    btly2->addWidget(btSortSubstPret);
    btly1->addWidget(btUndo);
    btly2->addWidget(btExit);
    fly->addRow("Denumire", denText);
    fly->addRow("Pret", pretText);
    fly->addRow("Producator", prodText);
    fly->addRow("Substanta", substText);
    fly->addRow(btFiltruPret, filtruPret);
    fly->addRow(btFiltruSubst, filtruSubstText);
    filtruPret->setMaximum(999);
    filtruPret->setWrapping(true);
    middle->addLayout(fly);
    middle->addLayout(btly1);
    middle->addLayout(btly2);
    right->addWidget(btAdaugaReteta);
    right->addWidget(btAdaugaRandom);
    right->addWidget(btGoleste);
    right->addWidget(btRetetaGUI);
    right->addWidget(btRetetaRDonly);
    sl->setMinimum(0);
    sl->setMaximum(10);
    slider->addWidget(sl);
    value->setText("0");
    slider->addWidget(value);
    lymain->addLayout(left);
    lymain->addLayout(middle);
    lymain->addLayout(right);
    lymain->addLayout(slider);

}

void FarmacieGUI::loadFarmacie() {
//    for (Medicament& m : service.getAll()){
//        lista->addItem(QString::fromStdString(m.getDenumire() + ", " + std::to_string(int(m.getPret())) + ", " + m.getProducator() + ", " + m.getSubst()));
//    }

}


void FarmacieGUI::loadData() {
    loadFarmacie();
}

void FarmacieGUI::reload() {
//    lista->clear();
//    loadData();
    lista->setMeds(service.getAll()); // lab_14 - se actualizeaza datele modelului doar (service ul dupa orice modficare)
}

    // lab_13 ------------------------------------------------------------------------------------------------

//void FarmacieGUI::ConnectList() {
//    QObject::connect(lista, &QListWidget::itemClicked, [&](){
//        auto item = lista->currentItem();
//        auto text = item->text().toStdString();
//        auto denumire = text.substr(0, text.find(',')); // tot pana la prima virgula
//        denText->setText(QString::fromStdString(denumire));
//        int poz = -1;
//        // gasesc poztita celui de al 2 lea spatiu in text
//        for (int i = 0; i < 2; i++){
//            poz = text.find_first_of(' ', poz+1);
//        }
//        poz++;
//        // producatorul este tot pana la prima virgula intalnita
//        auto producator = text.substr(poz, text.find(',', poz)-poz);
//        prodText->setText(QString::fromStdString(producator));
//        pretText->setText("");
//        substText->setText("");
//    });
//}
    // ----------------------------------------------------------------------------------------------------------------
void FarmacieGUI::ConnectModelList() {
    QObject::connect(vw->selectionModel(), &QItemSelectionModel::selectionChanged, [&]() {
       if (vw->selectionModel()->selectedIndexes().isEmpty())
           return ;
       auto selIndex = vw->selectionModel()->selectedIndexes().at(0);
       auto text = selIndex.data(Qt::DisplayRole).toString().toStdString();
       auto denumire = text.substr(0, text.find(',')); // tot pana la prima virgula
       denText->setText(QString::fromStdString(denumire));
       int poz = -1;
       // gasesc poztita celui de al 2 lea spatiu in text
       for (int i = 0; i < 2; i++){
           poz = text.find_first_of(' ', poz+1);
       }
       poz++;
       // producatorul este tot pana la prima virgula intalnita
       auto producator = text.substr(poz, text.find(',', poz)-poz);
       prodText->setText(QString::fromStdString(producator));
       pretText->setText("");
       substText->setText("");
    });
}

void FarmacieGUI::ConnectAdauga() {
    QObject::connect(btAdauga, &QPushButton::clicked, [&](){
        auto denumire = denText->text().toStdString();
        auto pret = pretText->text().toDouble();
        auto prod = prodText->text().toStdString();
        auto subst = substText->text().toStdString();
        try{
            service.addMedicamentFarmacie(denumire, pret, prod, subst);
            reload();
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectAdaugaReteta() {
    QObject::connect(btAdaugaReteta, &QPushButton::clicked, [&](){
        auto den = denText->text().toStdString();
        auto prod = prodText->text().toStdString();
        qDebug() << den << " " << prod;
        try{
            service.addMedicamentReteta(den, prod);
            qDebug() << service.getSizeReteta();
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectAdaugaRandom() {
    QObject::connect(btAdaugaRandom, &QPushButton::clicked, [&](){
        int numar = sl->value();
        try {
            service.addMedicamenteRandom(numar);
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectGolesteReteta() {
    QObject::connect(btGoleste, &QPushButton::clicked, [&](){
        service.deleteAllReteta();
    });
}

void FarmacieGUI::ConnectSlLabel() {
    QObject::connect(sl, &QSlider::valueChanged, [&](){
        int nou = sl->value();
        value->setText(QString::fromStdString(std::to_string(nou)));
    });
}

void FarmacieGUI::ConnectSterge() {
    QObject::connect(btSterge, &QPushButton::clicked, [&](){
        auto denumire = denText->text().toStdString();
        auto prod = prodText->text().toStdString();
        try{
            service.deleteMedicamentFarmacie(denumire, prod);
            reload();
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectModifica() {
    QObject::connect(btModifica, &QPushButton::clicked, [&]() {
        double newPret;
        if (!pretText->text().toStdString().empty())
            newPret = std::stod(pretText->text().toStdString());
        else
            newPret = 0;
        auto newSubst = substText->text().toStdString();
        auto denumire = denText->text().toStdString();
        auto prod = prodText->text().toStdString();
        try{
            service.updateGUI(denumire, prod, newPret, newSubst);
            reload();
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectSortDen() {
    QObject::connect(btSortDen, &QPushButton::clicked, [&](){
        lista->setMeds(service.sortareMedicamenteDenumire(true));
    });
}

void FarmacieGUI::ConnectSortProd() {
    QObject::connect(btSortProd, &QPushButton::clicked, [&](){
        lista->setMeds(service.sortareMedicamenteProducator(true));
    });
}

void FarmacieGUI::ConnectSortSubstPret() {
    QObject::connect(btSortSubstPret, &QPushButton::clicked, [&](){
        lista->setMeds(service.sortareMedicamenteSubstPret(true));
    });
}

void FarmacieGUI::ConnectFiltruPret() {
    QObject::connect(btFiltruPret, &QPushButton::clicked, [&](){
        int pret = filtruPret->value();
        vector<Medicament> filtru = service.filtruPret(pret);
        QWidget* w = new QWidget;
        QVBoxLayout* ly = new QVBoxLayout;
        w->setLayout(ly);
        QListWidget* lista = new QListWidget;
        ly->addWidget(lista);
        QPushButton* bt = new QPushButton{"Iesire"};
        ly->addWidget(bt);
        for (auto& m : filtru){
            lista->addItem(QString::fromStdString(m.getDenumire() + ", " + std::to_string(int(m.getPret())) + ", " + m.getProducator() + ", " + m.getSubst()));
        }
        w->show();
        QObject::connect(bt, &QPushButton::clicked, [w](){
            w->close();
        });
    });
}

void FarmacieGUI::ConnectFiltruSubst() {
    QObject::connect(btFiltruSubst, &QPushButton::clicked, [&](){
        string subst = filtruSubstText->text().toStdString();
        vector<Medicament> filtru = service.filtruSubst(subst);
        QWidget* w = new QWidget;
        QVBoxLayout* ly = new QVBoxLayout;
        w->setLayout(ly);
        QListWidget* lista = new QListWidget;
        for (auto& m : filtru){
            lista->addItem(QString::fromStdString(m.getDenumire() + ", " + std::to_string(int(m.getPret())) + ", " + m.getProducator() + ", " + m.getSubst()));
        }
        ly->addWidget(lista);
        QPushButton* bt = new QPushButton{"Iesire"};
        ly->addWidget(bt);
        w->show();
        QObject::connect(bt, &QPushButton::clicked, [w](){
            w->close();
        });
    });
}

void FarmacieGUI::ConnectRetetaGUINoua() {
    QObject::connect(btRetetaGUI, &QPushButton::clicked, [&](){
       auto* retetaNoua = new RetetaGUI{service};
       retetaNoua->setAttribute(Qt::WA_DeleteOnClose);
       retetaNoua->show();
    });
}

void FarmacieGUI::ConnectRetetaRDONLY() {
    QObject::connect(btRetetaRDonly, &QPushButton::clicked, [&](){
        auto* retetaNoua = new RetetaReadOnlyGUI{service};
        retetaNoua->setAttribute(Qt::WA_DeleteOnClose);
        retetaNoua->show();
    });
}

void FarmacieGUI::ConnectUndo() {
    QObject::connect(btUndo, &QPushButton::clicked, [&](){
        try{
            service.undo();
            reload();
        }
        catch (Error& err){
            QMessageBox::warning(this, "INFO!", QString::fromStdString(err.getMesaj()));
        }
    });
}

void FarmacieGUI::ConnectExit() {
    QObject::connect(btExit, &QPushButton::clicked, [&](){
        close();
    });
}

void FarmacieGUI::initConnect() {
    ConnectAdauga();
    ConnectAdaugaReteta();
    ConnectAdaugaRandom();
    ConnectGolesteReteta();
    ConnectSlLabel();
    ConnectSterge();
    ConnectModifica();
    ConnectSortDen();
    ConnectSortProd();
    ConnectSortSubstPret();
    ConnectFiltruPret();
    ConnectFiltruSubst();
    ConnectRetetaGUINoua();
    ConnectRetetaRDONLY();
    ConnectUndo();
//    ConnectList(); - pt lab_13
    ConnectModelList();
    ConnectExit();
}