#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "dynamic_lib_test.h"
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QMessageBox* msg = new QMessageBox();
    msg->setText(QString::fromStdString(String_Plus("Du", "HS")));
    msg->exec();
}

MainWindow::~MainWindow()
{
    delete ui;
}

