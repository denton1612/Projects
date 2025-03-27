//
// Created by Știube Denis on 26.05.2024.
//

#include "utils.h"

#include "../Validare/Error.h"

void clearFile(const string& filename){
    ofstream out(filename);
    if (!out.is_open())
        throw Error("Nu s-a putut deschide fisierul!");
    out.close();
}