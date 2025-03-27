//
// Created by Știube Denis on 26.05.2024.
//

#include "Service.h"

void Service::addMedicamentFarmacie(const string& denumire, const double& pret, const string& producator,
                                    const string& subst) {
    Medicament newMedicament{denumire, pret, producator, subst};
    repo.addMedicament(newMedicament);
    undoList.push(new UndoAdauga(repo, newMedicament));
}

void Service::deleteMedicamentFarmacie(const std::string &denumire, const std::string &prod) {
    auto it = find_if(getAll().begin(), getAll().end(), [&](Medicament& med){
        return (med.getDenumire() == denumire && med.getProducator() == prod);
    });
    Medicament m{denumire, prod};
    if (it == getAll().end())
        repo.deleteMedicament(m);
    undoList.push(new UndoSterge(repo, *it));
    repo.deleteMedicament(*it);
}

void Service::updatePret(const std::string &denumire, const std::string &prod, const double &newPret) {
    if (newPret <= 0 || denumire.empty() || prod.empty())
        throw Error("Medicamentul nu exista sau nu este valid!");
    auto it = find_if(getAll().begin(), getAll().end(), [&](Medicament& med){
        return (med.getDenumire() == denumire && med.getProducator() == prod);
    });
    Medicament vechi{denumire, prod};
    Medicament nou = *it;
    nou.setPret(newPret);
    if (it == getAll().end())
        repo.updateMedicament(vechi, nou);
    vechi = *it;
    repo.updateMedicament(*it, nou);
    if (vechi.getPret() != nou.getPret())
        undoList.push(new UndoModifica(repo, vechi, nou));
}

void Service::updateSubst(const std::string &denumire, const std::string &prod, const std::string &newSubst) {
    if (newSubst.empty() || denumire.empty() || prod.empty())
        throw Error("Medicamentul nu exista sau nu este valid!");
    auto it = find_if(getAll().begin(), getAll().end(), [&](Medicament& med){
        return (med.getDenumire() == denumire && med.getProducator() == prod);
    });
    Medicament vechi{denumire, prod};
    Medicament nou = *it;
    nou.setSubst(newSubst);
    if (it == getAll().end())
        repo.updateMedicament(vechi, nou);
    vechi = *it;
    repo.updateMedicament(*it, nou);
    if (vechi.getSubst() != nou.getSubst())
        undoList.push(new UndoModifica(repo, vechi, nou));
}

void Service::updateGUI(const std::string &denumire, const std::string &prod, const double &newPret,
                        const std::string &newSubst) {
    auto it = find_if(getAll().begin(), getAll().end(), [&](Medicament& med){
        return (med.getDenumire() == denumire && med.getProducator() == prod);
    });
    Medicament vechi{denumire, prod};
    Medicament nou = *it;
    if (it == getAll().end())
        repo.updateMedicament(vechi, nou);
    vechi = *it;
    if (newPret != 0)
        nou.setPret(newPret);
    if (!newSubst.empty())
        nou.setSubst(newSubst);
    repo.updateMedicament(*it, nou);
    if (vechi.getSubst() != nou.getSubst() || vechi.getPret() != nou.getPret())
        undoList.push(new UndoModifica(repo, vechi, nou));
}

bool Service::findMedicament(const string& denumire, const string& producator) const {
    return repo.findMedicament(denumire, producator);
}

void Service::addMedicamentReteta(const std::string &denumire, const std::string& producator) {
    auto it = find_if(getAll().begin(), getAll().end(), [&](Medicament& m){return (m.getDenumire() == denumire and m.getProducator() == producator);});
    if (it == getAll().end())
        throw Error("Medicament indisponibil in farmacie!");
    auto newMedicament = *it;
    reteta.addMedicament(newMedicament);
    notify();
}

void Service::deleteAllReteta() {
    reteta.deleteAll();
    notify();
}

void Service::addMedicamenteRandom(const int &nr) {
    if (nr <= 0){
        throw Error("Numarul citit trebuie sa fie mai mare decat 0!");
    }
    reteta.deleteAll();
    vector<Medicament> med = getAllCopy();
    auto seed = std::chrono::system_clock::now().time_since_epoch().count();
    for (int i = 0; i < nr; i++){
        std::shuffle(med.begin(), med.end(), std::default_random_engine(seed));
        auto m = *med.begin();
        reteta.getAll().push_back(m);
    }
    notify();
}

void Service::exportToFile(const std::string &filename) {
    if(filename.empty())
        throw Error("Numele fisierului nu poate fi gol!");
    vector<Medicament> reteta = getAllReteta();
    if (!reteta.size())
        throw Error("Reteta este goala!");
    std::ofstream out(filename + ".csv");
    for (int i = 1; i <= getSizeReteta(); i++){
        out << i << ", " << "Denumire: " << reteta[i-1].getDenumire() << ", Pret: " << reteta[i-1].getPret() << ", Producator: " << reteta[i-1].getProducator() << ", Substanta: " << reteta[i-1].getSubst() << '\n';
    }
    out.close();
}

void Service::undo() {
    if (undoList.empty())
        throw Error("Nu se poate face undo!");
    auto actiune = undoList.top();
    actiune->doUndo();
    delete actiune;
    undoList.pop();
}

vector<Medicament> Service::filtruPret(const double &pret) {
    vector<Medicament> medicamenteFiltrate;
    std::copy_if(getAll().begin(), getAll().end(), std::back_inserter(medicamenteFiltrate), [&](Medicament& m){return m.getPret() == pret;});
    return medicamenteFiltrate;
}

vector<Medicament> Service::filtruSubst(const string &subst) {
    vector<Medicament> medicamenteFiltrate;
    std::copy_if(getAll().begin(), getAll().end(), std::back_inserter(medicamenteFiltrate), [&](Medicament& m){return m.getSubst() == subst;});
    return medicamenteFiltrate;
}

vector<Medicament> Service::sortareMedicamente(bool (*function)(const Medicament&, const Medicament&)) {
    vector<Medicament> medicamenteSortate;
    medicamenteSortate = getAll();
    std::sort(medicamenteSortate.begin(), medicamenteSortate.end(), (*function));
    return medicamenteSortate;
}

vector<Medicament> Service::sortareMedicamenteDenumire(bool ordine) {
    vector<Medicament> medicamenteSortate;
    medicamenteSortate = sortareMedicamente([](const Medicament &m1, const Medicament& m2){
        return m1.getDenumire() < m2.getDenumire();
    });
    if (!ordine)
        std::reverse(medicamenteSortate.begin(), medicamenteSortate.end());
    return medicamenteSortate;
}

vector<Medicament> Service::sortareMedicamenteProducator(bool ordine) {
    vector<Medicament> medicamenteSortate;
    medicamenteSortate = sortareMedicamente([](const Medicament &m1, const Medicament& m2){
        return m1.getProducator() < m2.getProducator();
    });
    if (!ordine)
        std::reverse(medicamenteSortate.begin(), medicamenteSortate.end());
    return medicamenteSortate;
}

vector<Medicament> Service::sortareMedicamenteSubstPret(bool ordine) {
    vector<Medicament> medicamenteSortate;
    medicamenteSortate = sortareMedicamente([](const Medicament &m1, const Medicament& m2){
        return std::make_pair(m1.getSubst(), m1.getPret()) < std::make_pair(m2.getSubst(), m2.getPret());
    });
    if (!ordine)
        std::reverse(medicamenteSortate.begin(), medicamenteSortate.end());
    return medicamenteSortate;
}

unordered_map<string, int> Service::get_map() {
    unordered_map<string, int> dict;
    for (Medicament& m : getAll()){
        auto it = dict.find(m.getProducator());
        if (it != dict.end()){
            (*it).second++;
        }
        else{
            dict.insert(std::make_pair(m.getProducator(), 1));
        }
    }
    return dict;
}