//
// Created by Știube Denis on 26.05.2024.
//

#include "ActiuneUndo.h"

void UndoAdauga::doUndo() {
    repo.deleteMedicament(medAdaugat);
}

void UndoSterge::doUndo() {
    repo.addMedicament(medSters);
}

void UndoModifica::doUndo() {
    repo.updateMedicament(medNou, medVechi);
}