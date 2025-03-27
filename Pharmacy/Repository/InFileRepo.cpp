//
// Created by Știube Denis on 26.05.2024.
//

#include "InFileRepo.h"
InFileRepo::InFileRepo(const string& filename) : Repo(), filename{filename}{
    loadFromFile();
}

void InFileRepo::loadFromFile() {
    ifstream in(filename);
    if (!in.is_open())
        throw Error("Nu s-a putut deschide fisierul!");
    string denumire, producator, subst;
    double pret;
    in >> denumire;
    in >> pret;
    in >> producator;
    in >> subst;
    while (!in.eof()) {
        Medicament m1{denumire, pret, producator, subst};
        Repo::addMedicament(m1);
        in >> denumire;
        in >> pret;
        in >> producator;
        in >> subst;
    }
    in.close();
}

void InFileRepo::saveToFile() {
    ofstream out(filename);
    for (auto& m : getAll()){
        out << m.getDenumire() << " " << m.getPret() << " " << m.getProducator() << " " << m.getSubst() << '\n';
    }
    out.close();
}