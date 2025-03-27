//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_ACTIUNEUNDO_H
#define LAB13_14_ACTIUNEUNDO_H

#include <string>
#include "../Repository/RepoAbstract.h"
using std::string;

class ActiuneUndo{
public:
    virtual void doUndo() = 0;
    virtual ~ActiuneUndo() = default;
};

class UndoAdauga : public ActiuneUndo{
    RepoAbstract& repo;
    Medicament medAdaugat;
public:
    UndoAdauga(RepoAbstract& repo, const Medicament& medAdaugat): repo{repo}, medAdaugat{medAdaugat}{

    }
    UndoAdauga() = delete;

    void doUndo() override;

};

class UndoSterge : public ActiuneUndo{
    RepoAbstract& repo;
    Medicament medSters;
public:
    UndoSterge(RepoAbstract& repo, const Medicament& medSters): repo{repo}, medSters{medSters}{

    }

    void doUndo() override;
};

class UndoModifica : public ActiuneUndo {
    RepoAbstract &repo;
    Medicament medVechi;
    Medicament medNou;
public:
    UndoModifica(RepoAbstract &repo, Medicament &medVechi, Medicament &medNou) : repo{repo}, medVechi{medVechi},
                                                                                 medNou{medNou} {

    }

    void doUndo() override;
};

#endif //LAB13_14_ACTIUNEUNDO_H
