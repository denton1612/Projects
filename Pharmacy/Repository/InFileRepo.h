//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_INFILEREPO_H
#define LAB13_14_INFILEREPO_H
#include "Repo.h"
#include <fstream>
using std::ifstream;
using std::ofstream;

class InFileRepo : public Repo{
private:
    string filename;
    void loadFromFile();
    void saveToFile();

public:
    explicit InFileRepo(const string& filename);

    InFileRepo() = delete;

    void addMedicament(const Medicament& newMedicament) override{
        Repo::addMedicament(newMedicament);
        saveToFile();
    }

    void deleteMedicament(const Medicament& medicament) override{
        Repo::deleteMedicament(medicament);
        saveToFile();
    }

    void updateMedicament(const Medicament& oldMedicament, const Medicament& newMedicament) override{
        Repo::updateMedicament(oldMedicament, newMedicament);
        saveToFile();
    }

};


#endif //LAB13_14_INFILEREPO_H
