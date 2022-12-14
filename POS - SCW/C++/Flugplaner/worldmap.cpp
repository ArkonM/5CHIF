#include "worldmap.h"
#include <QPainter>
#include <iostream>

using namespace std;

worldmap::worldmap(QWidget *parent)
    : QWidget{parent}
{
}

void worldmap::paintEvent(QPaintEvent* event)
{
    painter = new QPainter(this);
    QSize size = this->size();
    QImage* map = new QImage();
    map->load("..\\Flugplaner\\Earthmap.jpg");
    QRectF target(0.0, 0.0, size.width(), size.height());
    QRectF source(0.0,0.0,720.0,360.0);
    painter->drawImage(target, *map, source);
    drawAirport(size);
    painter->end();
}


void worldmap::addAirport(Airport x) {
    cout << "x " << x.longi << " y " << x.lati << endl;
    this->Airports.append(x);
}

void worldmap::setAirline(int airline) {
    this->airline = airline;
}

void worldmap::drawAirport(QSize size) {
    for (int i = 0; i < Airports.size(); i++) {
        double x = (Airports[i].longi + 180) * size.width() / 360;
        double y = (90 - Airports[i].lati) * size.height() / 180;

        cout << Airports[i].iata << endl;

        painter->setPen(Qt::white);
        painter->drawText(x - 10, y - 10, QString::fromStdString(Airports[i].iata));

        painter->setPen(Qt::red);
        painter->drawEllipse(x - 5, y - 5, 10, 10); // -5 -> Punkt soll in der Mitte vom Kreis sein, Kreislänge 10

        if(i != 0) {
            double xbefore = (Airports[i-1].longi + 180) * size.width() / 360;
            double ybefore = (90 - Airports[i-1].lati) * size.height() / 180;
            if(Airports[i-1].airline != -1 /*&& Airports[i-1].alliance != -1*/ && Airports[i-1].airline == this->airline) {
                painter->setPen(Qt::red);
            } else if(Airports[i-1].airline == -1 && Airports[i-1].alliance != -1) {
                painter->setPen(Qt::blue);
            } else {
                painter->setPen(Qt::gray);
            }
            if((x-xbefore) < -360) {
                painter->drawLine(x,y,0, ((y + ybefore) / 2));
                painter->drawLine(size.width(), ((y + ybefore) / 2),xbefore, ybefore);
            } else if((x-xbefore) > 360) {
                painter->drawLine(x,y, size.width(), ((y + ybefore) / 2));
                painter->drawLine(0, ((y + ybefore) / 2), xbefore, ybefore);
            } else {
                painter->drawLine(x, y, xbefore, ybefore);
            }
        }
    }
}
