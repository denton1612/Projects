//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_REPOABSTRACT_H
#define LAB13_14_REPOABSTRACT_H
#include "../Domain/Medicament.h"
#include <vector>
using std::vector;

class RepoAbstract {
public:

    // adauga un nou medicament in vectorul de medicamente
    // param newMedicament(Medicament): medicamentul ce va fi adaugat
    virtual void addMedicament(const Medicament &newMedicament) = 0;

    // sterge un medicament din farmacie
    // param medicament(Medicament): medicamentul ce va fi sters
    virtual void deleteMedicament(const Medicament &medicament) = 0;

    // cauta un medicament in farmacie
    // param denumire(string): denumriea medicamentului cautat
    // param prod(string): producatorul medicamentului cautat
    // returneaza adevarat daca medicamentul exista in farmacie sau fals in caz contrar
    virtual bool findMedicament(const string &denumire, const string &prod) = 0;

    // modifica un medicament din farmacie
    // param oldMedicament(Medicament): medicamentul inainte de a fi modificat
    // param newMedicament(Medicament): medicamentul dupa modificare
    virtual void updateMedicament(const Medicament &oldMedicament, const Medicament &newMedicament) = 0;

    [[nodiscard]] virtual unsigned int getSize() const = 0;

    virtual vector<Medicament>& getAll() = 0;

    virtual vector<Medicament> getAllCopy() = 0;

};


#endif //LAB13_14_REPOABSTRACT_H
