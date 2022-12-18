#include "earthmap.h"

#include <QPainter>

earthMap::earthMap(QWidget *parent)
    : QWidget{parent}
{

}

void earthMap::paintEvent(QPaintEvent* event)
{
    /*painter = new QPainter(this);
    QSize size = this->size();
    QImage* map = new QImage();
    map->load("../PA2_Schneider_Armin/USA.jpg");
    QRectF target(0.0, 0.0, size.width(), size.height());
    QRectF source(0.0,0.0,720.0,360.0);
    painter->drawImage(target, *map, source);
    painter->end();*/
}
