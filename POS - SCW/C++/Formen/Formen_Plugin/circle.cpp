#include "circle.h"
#include <iostream>

Circle::Circle(int x, int y, int radius) : Form("Circle", "Datenformat: x,y,radius", x, y), radius(radius)
{

}

void Circle::draw(QPainter &painter)
{
    painter.setPen(QPen(Qt::black, 2, Qt::SolidLine, Qt::RoundCap));
    painter.drawEllipse(x - radius,y - radius,2*radius,2*radius);
}

Form *Circle::parseForm(const QString &data)
{
    QStringList d = data.split(",");
    if(d.size() == 3)
    {
        return new Circle(d[0].toInt(), d[1].toInt(), d[2].toInt());
    }
    return nullptr;
}
