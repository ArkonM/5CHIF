#ifndef CUSTOMWIDGET_H
#define CUSTOMWIDGET_H

#include <QPainter>
#include <QWidget>

class CustomWidget : public QWidget
{
    Q_OBJECT
public:
    std::vector<QPoint> clickedPositions;
    QPainter *painter = new QPainter(this);
    explicit CustomWidget(QWidget *parent = nullptr);
    void paintEvent(QPaintEvent *);
    void mousePressEvent(QMouseEvent* event);

signals:

};

#endif // CUSTOMWIDGET_H
