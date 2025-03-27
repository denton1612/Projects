//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_SERVICE_H
#define LAB13_14_SERVICE_H

#include "../Repository/RepoAbstract.h"
#include "../Repository/Repo.h"
#include "../Reteta/Reteta.h"
#include "../Undo/ActiuneUndo.h"
#include "../Observer/Observer.h"

#include <unordered_map>
#include <utility>
#include <algorithm>
#include <chrono>
#include <random>
#include <fstream>
#include <stack>

using std::string;
using std::unordered_map;
using std::find_if;
using std::stack;

class Service: public Observable{

private:
    RepoAbstract& repo;
    Reteta& reteta;
    stack<ActiuneUndo*> undoList;

public:
    // anulam constructorul default fara parametrii
    Service() = delete;

    Service(const Service& service) = delete;

    // constructorul ce primeste ca parametrii un repository si o reteta
    Service(RepoAbstract& repository, Reteta& reteta): repo{repository}, reteta{reteta}, Observable() {

    }

    // adauga un nou medicament in vectorul de medicamente
    // param denumire(string): denumirea
    // param pret(double): pretul
    // pararm producator(string): producatorul
    // param subst(string): substanta
    void addMedicamentFarmacie(const string& denumire, const double& pret, const string& producator, const string& subst);

    // sterge medicamentul dat din farmacie
    // param denumire(string): denumirea medicamentului ce va fi sters
    // param prod(string): producatorul medicametului ce va fi sters
    void deleteMedicamentFarmacie(const string& denumire, const string& prod);

    // modifica pretul medicamentului de pe pozitia poz din vectorul de medicamente
    // param poz(int): pozitia medicamentului din vector ce va fi modificat
    // param newPret(double): noul pret al medicamentului
    void updatePret(const string& denumire, const string& prod, const double& newPret);

    // modifica substanta medicamentului de pe pozitia poz din vectorul de medicamente
    // param poz(int): pozitia medicamentului din vector ce va fi modificat
    // param newSubst(string): noua substanta a medicamentului
    void updateSubst(const string& denumire, const string& prod, const string& newSubst);

    // modifica campurile variabile (pret, subst) ale unui medicament cu parametrii pe care ii primeste
    void updateGUI(const string& denumire, const string& prod, const double& newPret, const string& newSubst);

    // verifica daca exista un medicament in farmacie
    // param denumire(string): denumirea medicamentului cautat
    // param producator(string): producatorul medicamentului cautat
    [[nodiscard]] bool findMedicament(const string& denumire, const string& producator) const;

    // returneaza vectorul de medicamente din repository
    vector<Medicament>& getAll(){
        return repo.getAll();
    }

    // returneaza o copie a vectorului de medicamente din repository

    // returneaza medicamentele de pe reteta
    vector<Medicament>& getAllReteta(){
        return reteta.getAll();
    }

    // filtreaza vectorul de medicamente dupa pret
    // param pret(double): pretul pentru filtrare
    vector<Medicament> filtruPret(const double& pret);

    // filtreaza vectorul de medicamente dupa substanta activa
    // param subst(string): substanta pentru filtrare
    vector<Medicament> filtruSubst(const string& subst);

    // sorteaza vectorul de medicamente
    vector<Medicament> sortareMedicamente(bool (*function)(const Medicament& m1, const Medicament& m2));

    // sorteaza medicamentele dupa denumire (crescator sau descrescator)
    vector<Medicament> sortareMedicamenteDenumire(bool);

    // sorteaza medicamentele dupa producator (crescator sau descrescator)
    vector<Medicament> sortareMedicamenteProducator(bool);

    // sorteaza medicamentele dupa substanta-pret (crescator sau descrescator)
    vector<Medicament> sortareMedicamenteSubstPret(bool);

    // adauga un medicament in reteta dupa denumire
    void addMedicamentReteta(const string& denumire, const string& producator);

    // sterge toate medicamentele din reteta
    void deleteAllReteta();

    // adauga medicamente generate aleator in reteta
    void addMedicamenteRandom(const int& nr);

    // exporta reteta intr un fisier in format html
    void exportToFile(const string& filename);

    // executa operatia undo pentru farmacie
    // arunca exceptie daca nu se poate efectua operatia
    void undo();

    // returneaza un dictionar de forma (producator medicament, frecventa)
    unordered_map<string, int> get_map();

    // returneaza dimensiunea vectorului de medicamente
    unsigned long getSize(){
        return repo.getSize();
    }

    unsigned long getSizeReteta(){
        return reteta.getSize();
    }

    vector<Medicament> getAllCopy(){
        return repo.getAllCopy();
    };

    ~Service(){
        while (!undoList.empty()){
            auto actiune = undoList.top();
            undoList.pop();
            delete actiune;
        }
    }

};


#endif //LAB13_14_SERVICE_H
