//
// Created by Știube Denis on 26.05.2024.
//

#include "Medicament.h"
#include <iostream>
using namespace std;

string Medicament::getDenumire() const {
    return denumire;
}

double Medicament::getPret() const {
    return pret;
}

string Medicament::getProducator() const {
    return producator;
}

string Medicament::getSubst() const {
    return subst;
}

void Medicament::setPret(const double &newPret) {
    this->pret = newPret;
}

void Medicament::setSubst(const string& newSubst) {
    this->subst = newSubst;
}

bool Medicament::operator==(const Medicament &med) const {
    if (this->getDenumire() == med.denumire and this->getProducator() == med.producator)
        return true;
    return false;
}

