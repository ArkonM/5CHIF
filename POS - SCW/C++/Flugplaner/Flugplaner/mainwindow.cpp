#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "dbmanager.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QString path = "D:/Github/5CHIF/POS - SCW/C++/Flugplaner/Flugplaner/Flugplaner_Ressource/AirlineRoutes.db";
    DbManager db = DbManager(path);
}

MainWindow::~MainWindow()
{
    delete ui;
}
