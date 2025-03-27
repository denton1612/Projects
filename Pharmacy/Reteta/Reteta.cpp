//
// Created by Știube Denis on 26.05.2024.
//

#include "Reteta.h"

void Reteta::addMedicament(const Medicament &med) {
    lista.push_back(med);
}

void Reteta::deleteAll() {
    lista.clear();
}