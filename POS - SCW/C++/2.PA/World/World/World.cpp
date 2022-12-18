#include "World.h"
#include <QSqlDatabase>
#include <QString>
#include <iostream>


using namespace std;


World::World(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);
    db = new DBManager("world.db");

    QStringList continents = db->getContinentNames();


    for (int i = 0; i < continents.count(); i++) {
        ui.continentBox->addItem(continents[i]);
    }

    QStringList headers;
    headers << "Name" << "Lat" << "Lon";

    ui.tableWidget->setColumnCount(3);
    ui.tableWidget->setShowGrid(true);
    ui.tableWidget->setSelectionMode(QAbstractItemView::SingleSelection);
    ui.tableWidget->setSelectionBehavior(QAbstractItemView::SelectRows);
    ui.tableWidget->setHorizontalHeaderLabels(headers);
    ui.tableWidget->horizontalHeader()->setStretchLastSection(true);
}


World::~World()
{}


void World::on_continentBox_currentTextChanged(const QString& arg1) {
    ui.countryBox->clear();
    ui.cityBox->clear();
    
    QStringList countries = db->getCountries(QString::number(ui.continentBox->currentIndex() + 1));


    for (int i = 0; i < countries.count(); i++) {
        ui.countryBox->addItem(countries[i]);
    }
}


void World::on_countryBox_currentTextChanged(const QString& arg1) {
    ui.cityBox->clear();

    QStringList cities = db->getCities(QString::number(ui.countryBox->currentIndex() + 1));


    for (int i = 0; i < cities.count(); i++) {
        ui.cityBox->addItem(cities[i]);
    }
}


void World::on_tableWidget_cellClicked(int row, int column)
{
    currentSelectedIndex = row;
}


void World::on_cityBox_currentTextChanged(const QString& arg1) {
    currentCity = arg1;
}

void World::on_pushButton_clicked() {
    if (currentCity != "") {
        double lat = db->getLat(currentCity);
        double lon = db->getLon(currentCity);
        int rowCount = ui.tableWidget->rowCount();

        ui.tableWidget->insertRow(rowCount);

        ui.tableWidget->setItem(rowCount, 0, new QTableWidgetItem(currentCity));

        std::cout << lat << " " << lon << std::endl;

        if (lat != -1 && lon != -1) {
            ui.tableWidget->setItem(rowCount, 1, new QTableWidgetItem(QString::number(lat)));
            ui.tableWidget->setItem(rowCount, 2, new QTableWidgetItem(QString::number(lon)));

        }
    }
}

void World::on_pushButton_2_clicked() {
    if (currentSelectedIndex != -1) {
        ui.tableWidget->removeRow(currentSelectedIndex);
    }
    currentSelectedIndex = -1;
}