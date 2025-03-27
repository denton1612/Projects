//
// Created by Știube Denis on 26.05.2024.
//

#include "Observer.h"

void Observable::addObserver(Observer *obs) {
    interesati.push_back(obs);
}

void Observable::removeObserver(Observer *obs) {
    auto it = std::find(interesati.begin(), interesati.end(), obs);
    interesati.erase(it);
}

void Observable::notify() {
    for (auto obs : interesati)
        obs->update();
}