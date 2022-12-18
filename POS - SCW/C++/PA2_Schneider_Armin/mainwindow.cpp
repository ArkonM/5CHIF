#include "mainwindow.h"
#include "dbmanager.h"
#include <iostream>
#include <string>
#include "ui_mainwindow.h"

using namespace std;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    QStringList headers;
    headers << "Zeit" << "Magnitude" << "Latitude" << "Lingitude";

    ui->earthquakes->setColumnCount(4);
    ui->earthquakes->setHorizontalHeaderLabels(headers);
    db = new DbManager("../PA2_Schneider_Armin/earthquakes.sqlite");


    ui->magSlider->setMinimum(3);
    ui->magSlider->setMaximum(7);
    ui->magSlider->showNormal();
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_searchBtn_clicked()
{
    QStringList states = db->getStatebyName(ui->searchBox->text().toStdString());
    states.sort();
    for(int i = 0; i < states.count(); i++) {
        if(i==0 || states[i-1] != states[i]){
            ui->citySelector->addItem(states[i]);
        }
    }
}


void MainWindow::on_citySelector_currentTextChanged(const QString &arg1)
{
    QSqlQuery quakes = db->getQuakesbyName(arg1.toStdString());
    while(quakes.next()){
        int i = ui->earthquakes->rowCount();

        QTableWidgetItem* time = new QTableWidgetItem();
        time->setText(quakes.value("time").value<QString>());
        ui->earthquakes->setItem(i+1,0, time);

        QTableWidgetItem* lat = new QTableWidgetItem();
        time->setText(quakes.value("latitude").value<QString>());
        ui->earthquakes->setItem(i+1,1, lat);

        QTableWidgetItem* lon = new QTableWidgetItem();
        time->setText(quakes.value("longitude").value<QString>());
        ui->earthquakes->setItem(i+1,2, lon);

        QTableWidgetItem* mag = new QTableWidgetItem();
        time->setText(quakes.value("mag").value<QString>());
        ui->earthquakes->setItem(i+1,3, mag);

        ui->earthquakes->update();
    }
}


void MainWindow::on_magSlider_valueChanged(int value)
{
    string current = ui->citySelector->currentText().toStdString();
    QSqlQuery quakes = db->getQuakesbyName(current);
    while(quakes.next()){
        if(quakes.value("mag").value<QString>().toInt() > value){
            int i = ui->earthquakes->rowCount();

            QTableWidgetItem* time = new QTableWidgetItem();
            time->setText(quakes.value("time").value<QString>());
            ui->earthquakes->setItem(i+1,0, time);

            QTableWidgetItem* lat = new QTableWidgetItem();
            time->setText(quakes.value("latitude").value<QString>());
            ui->earthquakes->setItem(i+1,1, lat);

            QTableWidgetItem* lon = new QTableWidgetItem();
            time->setText(quakes.value("longitude").value<QString>());
            ui->earthquakes->setItem(i+1,2, lon);

            QTableWidgetItem* mag = new QTableWidgetItem();
            time->setText(quakes.value("mag").value<QString>());
            ui->earthquakes->setItem(i+1,3, mag);

            ui->earthquakes->update();
        }
    }
}

