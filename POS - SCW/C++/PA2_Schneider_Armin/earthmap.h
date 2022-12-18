#ifndef EARTHMAP_H
#define EARTHMAP_H

#include <QWidget>

class earthMap : public QWidget
{
    Q_OBJECT
public:
    explicit earthMap(QWidget *parent = nullptr);
    void paintEvent(QPaintEvent *event) override;
private:
    QPainter* painter;
signals:

};

#endif // EARTHMAP_H
