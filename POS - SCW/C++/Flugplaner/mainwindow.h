#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "airport.h"
#include "dbmanager.h"

#include <QMainWindow>
#include <list>

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
    void on_searchbutton_clicked();

    void on_setAsTarget_clicked();

    void on_setAsStart_clicked();

    void on_airportSearch_textChanged();

    void on_airportComboBox_currentIndexChanged();

    void on_airlineComboBox_currentIndexChanged();

    void on_findRoute_clicked();

private:
    Ui::MainWindow *ui;
    std::list<Airport> search(std::string start);
    void buildtree(std::string start, std::string end);
    DBManager* m;
};
#endif // MAINWINDOW_H
