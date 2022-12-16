#ifndef WORLDMAP_H
#define WORLDMAP_H

#include <QWidget>
#include "airport.h"

class worldmap : public QWidget
{
    Q_OBJECT
public:
    explicit worldmap(QWidget *parent = nullptr);
    void addAirport(Airport x);
    void setAirline(int airline);
    void drawAirport(QSize size);
protected:
    void paintEvent(QPaintEvent *event) override;
    int airline;
    QPainter* painter;
    QList<Airport> Airports;
signals:

};

#endif // WORLDMAP_H
