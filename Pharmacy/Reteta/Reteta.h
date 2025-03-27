//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_RETETA_H
#define LAB13_14_RETETA_H

#include "../Domain/Medicament.h"
#include <vector>
#include <string>

using std::vector;
using std::string;

class Reteta {

private:
    vector<Medicament> lista;

public:
    // adauga un medicament in reteta
    void addMedicament(const Medicament& med);

    // goleste reteta
    void deleteAll();

    vector<Medicament>& getAll(){
        return lista;
    }

    [[nodiscard]] unsigned long getSize() const{
        return lista.size();
    }

};


#endif //LAB13_14_RETETA_H
