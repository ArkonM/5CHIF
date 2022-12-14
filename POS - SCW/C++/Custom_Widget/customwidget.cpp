#include "customwidget.h"

#include <QMouseEvent>
#include <QPainter>

CustomWidget::CustomWidget(QWidget *parent)
    : QWidget{parent}
{
}


void CustomWidget::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);
    painter.setRenderHint(QPainter::Antialiasing);

    // Draw a circle at the last clicked position
    int i = 2;
    for (const QPoint &position : clickedPositions)
    {

        painter.drawEllipse(position, i, i);
        i = i+2;
    }
}

void CustomWidget::mousePressEvent(QMouseEvent *event)
{
    clickedPositions.push_back(event->pos());
    update();
}
