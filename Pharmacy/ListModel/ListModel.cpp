//
// Created by Știube Denis on 26.05.2024.
//

#include "ListModel.h"

int ListModel::rowCount(const QModelIndex &parent) const {
    return meds.size();
}

QVariant ListModel::data(const QModelIndex &index, int role) const {
    if (role == Qt::DisplayRole) {
        auto med = meds[index.row()];
        return QString::fromStdString(med.getDenumire() + ", " + std::to_string((int)med.getPret()) + ", " + med.getProducator() + ", " + med.getSubst());
    }
    return QVariant{};
}

void ListModel::setMeds(vector<Medicament> meds) {
    this->meds = meds;
    const QModelIndex& topLeft = createIndex(0, 0);
    const QModelIndex& bottomRight = createIndex(rowCount(), 0);
    emit dataChanged(topLeft, bottomRight);
}