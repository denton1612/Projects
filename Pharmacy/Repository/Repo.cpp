//
// Created by Știube Denis on 26.05.2024.
//

#include "Repo.h"

void Repo::addMedicament(const Medicament &newMedicament) {
    auto gasit = findMedicament(newMedicament.getDenumire(), newMedicament.getProducator());
    if (gasit || newMedicament.getDenumire().empty() || newMedicament.getProducator().empty() || newMedicament.getSubst().empty() || newMedicament.getPret() < 0)
        throw Error( "Medicamentul exista deja sau nu este valid!");
    medicamente.push_back(newMedicament);
}

void Repo::deleteMedicament(const Medicament& medicament) {
    auto it = std::find_if(medicamente.begin(), medicamente.end(), [&](Medicament& m){
        return m == medicament;
    });
    auto gasit = findMedicament(medicament.getDenumire(), medicament.getProducator());
    if (!gasit || medicament.getDenumire().empty() || medicament.getProducator().empty())
        throw Error("Medicamentul nu exista sau nu este valid!");
    medicamente.erase(it);
}

bool Repo::findMedicament(const std::string &denumire, const std::string &prod) {
    auto it = std::find_if(medicamente.begin(), medicamente.end(), [&](Medicament& m){
        return (m.getDenumire() == denumire && m.getProducator() == prod);
    });
    if (it == medicamente.end())
        return false;
    return true;
}

void Repo::updateMedicament(const Medicament &oldMedicament, const Medicament &newMedicament) {
    auto it = std::find_if(medicamente.begin(), medicamente.end(), [&oldMedicament](Medicament& m){return m == oldMedicament;});
    if (it == getAll().end() || oldMedicament.getDenumire().empty() || oldMedicament.getProducator().empty())
        throw Error("Medicamentul nu exista sau nu este valid!");
    medicamente.erase(it);
    medicamente.insert(it, newMedicament);
}


vector<Medicament>& Repo::getAll() {
    return medicamente;
}

vector<Medicament> Repo::getAllCopy() {
    return medicamente;
}