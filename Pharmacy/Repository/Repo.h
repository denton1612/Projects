//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_REPO_H
#define LAB13_14_REPO_H
#include "RepoAbstract.h"
#include "../Validare/Error.h"
#include <algorithm>

class Repo : public RepoAbstract{
        friend class Reteta;

        private:
        vector<Medicament> medicamente;

        public:

        Repo() = default;

        // adauga un nou medicament in vectorul de medicamente
        // param newMedicament(Medicament): medicamentul ce va fi adaugat
        void addMedicament(const Medicament& newMedicament) override;

        // sterge un medicament din farmacie
        // param medicament(Medicament): medicamentul ce va fi sters
        void deleteMedicament(const Medicament& medicament) override;

        // cauta un medicament in farmacie
        // param denumire(string): denumriea medicamentului cautat
        // param prod(string): producatorul medicamentului cautat
        // returneaza adevarat daca medicamentul exista in farmacie sau fals in caz contrar
        bool findMedicament(const string& denumire, const string& prod) override;

        // modifica un medicament din farmacie
        // param oldMedicament(Medicament): medicamentul inainte de a fi modificat
        // param newMedicament(Medicament): medicamentul dupa modificare
        void updateMedicament(const Medicament& oldMedicament, const Medicament& newMedicament) override;

        // returneaza medicamentele stocate
        vector<Medicament>& getAll() override;

        vector<Medicament> getAllCopy() override;

        // returneaza numarul medicamentelor stocate
        [[nodiscard]] unsigned int getSize() const override{
            return medicamente.size();
        }

};


#endif //LAB13_14_REPO_H
