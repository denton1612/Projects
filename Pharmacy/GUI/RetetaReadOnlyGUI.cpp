//
// Created by Știube Denis on 26.05.2024.
//

#include "RetetaReadOnlyGUI.h"

void RetetaReadOnlyGUI::paintEvent(QPaintEvent *) {
    QPainter p{this};
    for (int i = 0; i < service.getSizeReteta(); i++) {
        int y = random() % height();
        int x = random() % width();
        int pr = random() % 2;
        if (pr)
            p.drawRect(0, 0, x, y);
        else
            p.drawEllipse(0, 0, x, y);
    }
}