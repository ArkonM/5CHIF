#include "square.h"
#include <iostream>

Square::Square(int x, int y, int width) : Form("Square", "Datenformat: x,y,breite", x, y), width(width)
{

}

void Square::draw(QPainter &painter)
{
    painter.setPen(QPen(Qt::black, 2, Qt::SolidLine, Qt::RoundCap));
    painter.drawRect(x,y,width,width);
}

Form *Square::parseForm(const QString &data)
{
    QStringList d = data.split(",");
    if(d.size() == 3)
    {
        return new Square(d[0].toInt(), d[1].toInt(), d[2].toInt());
    }
    return nullptr;
}
