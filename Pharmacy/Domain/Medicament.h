//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_MEDICAMENT_H
#define LAB13_14_MEDICAMENT_H
#include <string>
#include <utility>
#include <iostream>
using std::cout;
using std::string;

class Medicament {
private:
    string denumire;
    double pret;
    string producator;
    string subst;

public:
    Medicament() = default;

    Medicament(const string& den, const string& prod): denumire{den}, producator{prod}{

    }

    Medicament (const Medicament& med): denumire{med.denumire}, pret{med.pret}, producator{med.producator}, subst{med.subst}{

    }

    Medicament(string den, double pre, string pro, string sub): denumire{std::move(den)}, pret{pre}, producator{std::move(pro)}, subst{std::move(sub)}{

    }

    [[nodiscard]] string getDenumire() const;

    [[nodiscard]] double getPret() const;

    [[nodiscard]] string getProducator() const;

    [[nodiscard]] string getSubst() const;

    void setPret(const double& newPret);

    void setSubst(const string& newSubst);

    bool operator==(const Medicament& med) const;
};


#endif //LAB13_14_MEDICAMENT_H
