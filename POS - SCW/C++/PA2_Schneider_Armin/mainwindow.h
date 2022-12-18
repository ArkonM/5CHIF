#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "dbmanager.h"
#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_searchBtn_clicked();

    void on_citySelector_currentTextChanged(const QString &arg1);

    void on_magSlider_valueChanged(int value);

private:
    Ui::MainWindow *ui;
    DbManager* db;
};
#endif // MAINWINDOW_H
