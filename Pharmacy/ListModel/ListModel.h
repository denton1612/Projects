//
// Created by Știube Denis on 26.05.2024.
//

#ifndef LAB13_14_LISTMODEL_H
#define LAB13_14_LISTMODEL_H

#include "../Domain/Medicament.h"

#include <QAbstractListModel>
#include <vector>
using std::vector;

class ListModel : public QAbstractListModel{
    Q_OBJECT
    vector<Medicament> meds;
public:
    ListModel(vector<Medicament> meds): meds{meds} {

    }

    int rowCount(const QModelIndex& parent = QModelIndex()) const override;

    QVariant data(const QModelIndex& index, int role = Qt::DisplayRole) const override;

    void setMeds(vector<Medicament> meds);
};


#endif //LAB13_14_LISTMODEL_H
