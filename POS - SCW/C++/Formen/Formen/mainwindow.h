#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QList>
#include "form.h"
#include "factory.h"

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();
    void paintEvent(QPaintEvent *event);

private slots:
    void on_pushButton_clicked();
    void on_form_select_currentTextChanged(const QString &arg1);

private:
    Ui::MainWindow *ui;
    QList<Form*> *formen;
    Factory *factory;
    void load_plugins();
};

#endif // MAINWINDOW_H
