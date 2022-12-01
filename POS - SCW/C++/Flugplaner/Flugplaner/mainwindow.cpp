#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "dbmanager.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QString path = "./Flugplaner_Ressource/AirlineRoutes.db";
    DbManager db = DbManager(path);
    ui->lineEdit->setText();
}

MainWindow::~MainWindow()
{
    delete ui;
}
