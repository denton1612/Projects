#include "Teste/teste.h"
#include "Service/Service.h"
#include "GUI/FarmacieGUI.h"

#include <QApplication>

int main(int argc, char* argv[]) {
    testAll();
    Repo repo;
    InFileRepo repoF{"/Users/stiubedenis/Desktop/Facultate/Faculty/Teme/An 1/Semestrul 2/OOP/lab13_14/Farmacie.txt"};
    Reteta reteta;
    Service s{repoF, reteta};
    QApplication a{argc, argv};
    FarmacieGUI farmaciegui{s};
    farmaciegui.run();
    return QApplication::exec();
}
