#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "tree.h"

#include <iostream>
#include <QMessageBox>
#include <iterator>
#include <dbmanager.h>
#include <worldmap.h>

using namespace std;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->RouteDetailTable->setColumnCount(2);
    m = new DBManager("../Flugplaner/AirlineRoutes.db");
    QStringList airports = m->getAllAirports();
    airports.sort();
    for(int i = 0; i < airports.count(); i++) {
        ui->airportComboBox->addItem(airports[i]);
    }
    QSqlQuery airlines = m->getResfromSQL("SELECT * FROM Airline order by name");
    ui->airlineComboBox->addItem("");
    while(airlines.next()) {
        QVariantList data = {airlines.value("id").value<int>(), airlines.value("alliance").value<int>()};
        ui->airlineComboBox->addItem(airlines.value("name").value<QString>(), QVariant::fromValue(data));
    }
    ui->airportComboBox->setCurrentIndex(0);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_searchbutton_clicked()
{
    search(ui->airportSearch->text().toStdString());
}


void MainWindow::on_setAsTarget_clicked()
{
    if(ui->airportComboBox->currentText() != ui->startairport->text()) {
        ui->targetairport->setText(ui->airportComboBox->currentText());
    } else {
        QMessageBox msg;
        msg.setText("Start- und Zielflughafen dürfen nicht ident sein.");
        msg.exec();
    }
}


void MainWindow::on_setAsStart_clicked()
{
    if(ui->airportComboBox->currentText() != ui->targetairport->text()) {
        ui->startairport->setText(ui->airportComboBox->currentText());
    } else {
        QMessageBox msg;
        msg.setText("Start- und Zielflughafen dürfen nicht ident sein.");
        msg.exec();
    }
}


void MainWindow::on_airportSearch_textChanged()
{
    if(ui->airportSearch->text() != "") {
        ui->searchbutton->setEnabled(true);
    } else {
        ui->searchbutton->setEnabled(false);
    }
}


void MainWindow::on_airportComboBox_currentIndexChanged()
{
    if(ui->airportComboBox->count() > 0) {
        ui->setAsStart->setEnabled(true);
        ui->setAsTarget->setEnabled(true);
    } else {
        ui->setAsStart->setEnabled(false);
        ui->setAsTarget->setEnabled(false);
    }
}

list<Airport> MainWindow::search(string start) {
        list<Airport> ap;
        int i = 0;
            if(start.length() > 3) {
                cout << "by name" << endl;
                QSqlQuery rs = m->getResfromSQL("select * from airport where upper(name) like '%"+start+"%'");
                ui->airportComboBox->clear();
                while(rs.next()) {
                    Airport temp = Airport(rs.value("id").value<int>(), nullptr, rs.value("name").value<string>(), rs.value("iata").value<string>(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), true, true);
                    ui->airportComboBox->addItem(rs.value("name").value<QString>(), QVariant::fromValue(temp));
                    ap.push_back(temp);
                    i++;
                }
                rs.clear();
                if(i > 1) {
                    ui->airportComboBox->setCurrentIndex(0);
                } else if(i == 0) {
                    QMessageBox msg;
                    msg.setText("Es wurde kein Flughafen gefunden!");
                    msg.exec();
                }
            } else {
                cout << "by iata" << endl;
                i = 0;
                QSqlQuery rsiata = m->getAPbyIATA(start);
                ui->airportComboBox->clear();
                while(rsiata.next()) {
                    cout << "iata" << endl;
                    Airport temp = new Airport(rsiata.value("id").value<int>(), nullptr, rsiata.value("name").value<string>(), rsiata.value("iata").value<string>(), rsiata.value("LONGITUDE").value<double>(), rsiata.value("LATITUDE").value<double>(), true, true);
                    ap.push_back(temp);
                    ui->airportComboBox->addItem(rsiata.value("name").value<QString>(), QVariant::fromValue(temp));
                    i++;
                }
                if(i==0) {
                    cout << "by name after iata" << endl;
                    i=0;
                    rsiata = m->getAPbyName(start);
                    ui->airportComboBox->clear();
                    while(rsiata.next()) {
                        Airport temp = new Airport(rsiata.value("id").value<int>(), nullptr, rsiata.value("name").value<string>(), rsiata.value("iata").value<string>(), rsiata.value("LONGITUDE").value<double>(), rsiata.value("LATITUDE").value<double>(), true, true);
                        ap.push_back(temp);
                        ui->airportComboBox->addItem(rsiata.value("name").value<QString>(), QVariant::fromValue(temp));
                        i++;
                    }
                    if(i > 1) {
                        ui->airportComboBox->setCurrentIndex(0);
                    } else if(i == 0) {
                        QMessageBox msg;
                        msg.setText("Es wurde kein Flughafen gefunden!");
                        msg.exec();
                    }
                }
            }
        return ap;
}

void MainWindow::buildtree(string start, string end) {
        QSqlQuery rs;
        int startid = 0;
        int endid = 0;
        Airport endAirport;
        int erg = 0;
        Tree route;
        QVariantList airlines = ui->airlineComboBox->currentData(Qt::UserRole).value<QVariantList>();
        int airline = -1;
        int alliance = -1;
        if(airlines.size() == 2) {
            airline = airlines[0].value<int>();
            ui->mapWidget->setAirline(airline);
            alliance = airlines[1].value<int>();
        }
        QList<int> airlinesInAlliance;
        rs = m->getResfromSQL("select id from airline where alliance="+to_string(alliance)+"");
        while(rs.next()) {
            cout << rs.value("id").value<int>() << endl;
            airlinesInAlliance.append(rs.value("id").value<int>());
        }
        try {
            rs = m->getResfromSQL("select * from airport where name='"+start+"'"); //get start ap
            while(rs.next()) {
                route.add(rs.value("id").value<int>(), rs.value("name").value<string>(), rs.value("iata").value<string>(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), -1, -1);
                startid = rs.value("id").value<int>();
            }
            rs.clear();
            rs = m->getResfromSQL("SELECT * FROM route WHERE airport1="+to_string(startid)+"");  //get all connections from start ap
            int count = 0;
            while(rs.next()) {      //count connections from start ap
                count++;
            }
            if(count != 0) {        //search if start ap has conncetions
                rs = m->getResfromSQL("select id from airport where name='"+end+"'");   //get end ap
                while(rs.next()) {
                    endid = rs.value("id").value<int>();
                    route.setTarget(endid);
                }
                rs = m->getResfromSQL("select * from route where airport1="+to_string(endid));  //get all connections from endap
                count = 0;
                while(rs.next()) {  //count connections from end ap
                    count++;
                }
                if(count != 0) {   //find a route
                    rs = m->getResfromSQL("select * from route join airport on airport.id = airport2 where airport1="+to_string(startid));//+" or airport2="+to_string(startid)+"");    //get all routes from start and end ap
                    int airl = -1;
                    int al = -1;
                    while(rs.next() /*&& airl == -1*/) {
                        cout << rs.value("airline").value<int>() << endl;
                        //if(airline != -1) {
                            if(rs.value("airline").value<int>() == airline /*&& rs.value("airport2").value<int>() == endid*/) {
                                al = -1;
                                airl = airline;
                            }
                            if(airlinesInAlliance.contains(rs.value("airline").value<int>()) && airl == -1) {
                                al = airlinesInAlliance.at(airlinesInAlliance.indexOf(rs.value("airline").value<int>()));
                                if(airline == -1) {
                                    airl = -1;
                                }
                            }/*
                        if(airlinesInAlliance.contains(rs.value("airline").value<int>()) && rs.value("airline").value<int>() != airline) {
                            cout << "ALLIANCE" << endl;
                            al = airlinesInAlliance.at(airlinesInAlliance.indexOf(rs.value("airline").value<int>()));
                            if(airline == -1) {
                                airl = -1;
                            }
                        } else if(!airlinesInAlliance.contains(rs.value("airline").value<int>()) && rs.value("airline").value<int>() != airline) {
                            cout << "ELSE" << endl;
                            al = -1;
                            airl = -1;
                        } else {
                            cout << "AIRLINE IS CORRECT" << rs.value("airline").value<int>() << endl;
                            al = -1;
                            airl = rs.value("airline").value<int>();
                        }*/
                        //}
                        if(erg == 0 || airl != -1 || al != -1) {
                            int temperg = 0;
                            cout << "erg == 0" << endl;
                            if(airl == -1 && al == -1) {
                                cout << "set airline if nothing is set" << endl;
                                airl = rs.value("airline").value<int>();
                            }
                            temperg = route.add(rs.value("airport2").value<int>(), rs.value("airport.name").value<string>(), rs.value("airport.iata").value<string>(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), airl, al);
                            if(temperg != 0) {
                                cout << "TEMPERG" << airl << endl;
                                erg = temperg;
                            }
                            cout <<"ERG: " <<  erg << endl;
                            if(erg == 0) {
                                airl = -1;
                                al = -1;
                            }
                        }
                        cout << "nach if" << endl;
                    }
                    if(erg != 0) {
                        ui->RouteDetailTable->setRowCount(2);
                        rs = m->getResfromSQL("select * from airport where airport.id="+to_string(erg));
                        while(rs.next()) {
                            cout << "airl: " << airl << endl;
                            ui->mapWidget->addAirport(Airport(rs.value("id").value<int>(), nullptr, rs.value("name").value<string>(), rs.value("iata").toString().toStdString(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), airl, al));
                            QTableWidgetItem* temp = new QTableWidgetItem();
                            temp->setText(rs.value("name").value<QString>());
                            ui->RouteDetailTable->setItem(1,0, temp);
                            QTableWidgetItem* temp2 = new QTableWidgetItem();
                            QSqlQuery airlinename = m->getResfromSQL("select name from airline where id="+to_string(airl));
                            while(airlinename.next()) {
                                cout << "Airline name " << airlinename.value("name").value<string>() << endl;
                                temp2->setText(airlinename.value("name").value<QString>());
                            }
                            ui->RouteDetailTable->setItem(1,1, temp2);
                        }
                        rs = m->getResfromSQL("select * from airport where id="+to_string(startid));
                        while(rs.next()) {
                            ui->mapWidget->addAirport(Airport(rs.value("id").value<int>(), nullptr, rs.value("name").value<string>(), rs.value("iata").toString().toStdString(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), airl, al));
                            ui->mapWidget->repaint();
                            QTableWidgetItem* temp = new QTableWidgetItem();
                            temp->setText(rs.value("name").value<QString>());
                            ui->RouteDetailTable->setItem(0,0, temp);
                        }
                        erg = 200;
                    }
                    while(erg == 0) {
                        for(int i = 0; i < route.getChildren()->size() && route.added != -1; i++) {
                            list<Airport*>::iterator it = route.getChildren()->begin();
                            advance(it, i);
                            route.temp = *it;
                            rs = m->getResfromSQL("select * from route join airport on airport.id = airport2 where airport1="+to_string(route.temp->id));//+" or airport2="+to_string(route.temp->id)+"");
                            int airl = -1;
                            int al = -1;
                            while(rs.next()) {
                                if(airlinesInAlliance.contains(rs.value("airline").value<int>())) {
                                    al = airlinesInAlliance.at(airlinesInAlliance.indexOf(rs.value("airline").value<int>()));
                                    if(airline == -1) {
                                        airl = -1;
                                    }
                                } else if(!airlinesInAlliance.contains(rs.value("airline").value<int>()) && rs.value("airline").value<int>() != airline) {
                                    al = -1;
                                    airl = -1;
                                } else {
                                    al = -1;
                                    airl = rs.value("airline").value<int>();
                                }
                                if(airl == -1 && al == -1 && !rs.next()) {
                                    rs.previous();
                                    airl = rs.value("airline").value<int>();
                                }
                                erg = route.addfromtemproot(rs.value("airport2").value<int>(), rs.value("airport.name").value<string>(), rs.value("airport.iata").value<string>(), rs.value("LONGITUDE").value<double>(), rs.value("latitude").value<double>(), airl, al);
                            }

                        }
                        if(route.added == 0) {
                                cout << "route.added == 0" << endl;
                                route.added = -1;
                                    rs = m->getResfromSQL("select * from airport where id="+to_string(route.minleaf->id));
                                    this->repaint();
                                        while(rs.next()) {
                                            QTableWidgetItem* temp = new QTableWidgetItem();
                                            temp->setText(rs.value("name").value<QString>());
                                            ui->RouteDetailTable->setItem(ui->RouteDetailTable->rowCount() - 1,0, temp);
                                            QTableWidgetItem* temp2 = new QTableWidgetItem();
                                            QSqlQuery airlinename = m->getResfromSQL("select * from airline where id="+to_string(route.minleaf->airline));
                                            while(airlinename.next()) {
                                                temp2->setText(airlinename.value("name").value<QString>());
                                            }
                                            ui->RouteDetailTable->setItem(ui->RouteDetailTable->rowCount() - 1,1, temp2);
                                            ui->mapWidget->addAirport(Airport(rs.value("id").value<int>(), nullptr, rs.value("name").value<string>(), rs.value("iata").toString().toStdString(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), route.minleaf->airline, route.minleaf->alliance));
                                            ui->mapWidget->repaint();
                                            ui->RouteDetailTable->setRowCount(ui->RouteDetailTable->rowCount() + 1);
                                        }
                                    cout << "Total distance:" << " " << route.minleaf->distancetoroot << endl;
                                    while((route.temp != route.root || route.temp == route.root) && route.minleaf != nullptr) {
                                        rs = m->getResfromSQL("select * from airport where id="+to_string(route.minleaf->id));
                                        this->repaint();
                                        while(rs.next()) {
                                            QTableWidgetItem* temp = new QTableWidgetItem();
                                            temp->setText(rs.value("name").value<QString>());
                                            ui->RouteDetailTable->setItem(ui->RouteDetailTable->rowCount() - 1, 0, temp);
                                            QTableWidgetItem* temp2 = new QTableWidgetItem();
                                            QSqlQuery airlinename;
                                            if(route.minleaf->airline != -1) {
                                                airlinename = m->getResfromSQL("select * from airline where id="+to_string(route.minleaf->airline));
                                            } else {
                                                airlinename = m->getResfromSQL("select * from airline where id="+to_string(route.minleaf->alliance));
                                            }
                                            while(airlinename.next()) {
                                                temp2->setText(airlinename.value("name").value<QString>());
                                            }
                                            ui->RouteDetailTable->setItem(ui->RouteDetailTable->rowCount() - 1, 1, temp2);
                                            ui->mapWidget->addAirport(Airport(rs.value("id").value<int>(), nullptr, rs.value("name").value<string>(), rs.value("iata").toString().toStdString(), rs.value("LONGITUDE").value<double>(), rs.value("LATITUDE").value<double>(), route.minleaf->airline, route.minleaf->alliance));
                                            ui->mapWidget->repaint();
                                            if(!rs.next()) {
                                                rs.previous();
                                                ui->RouteDetailTable->setRowCount(ui->RouteDetailTable->rowCount() + 1);
                                            }
                                        }
                                        route.minleaf = route.minleaf->root;
                                        erg = 100;
                                    }
                            } else {
                                route.added = 0;
                            }
                    }
                }
            }
            if(count == 0) {
                QMessageBox m;
                m.setText("Es wurden keine Routen gefunden!");
                m.exec();
                count = 0;
                /*jTextField1.setText("");
                jTextField2.setText("");
                jLabel3.setText("");
                jLabel4.setText("");*/
            }
        } catch(exception e) {
                cout << (e.what()) << endl;
            /*} finally {
                try {
                    if (rs != null) rs.close();
                    if (stmt != null) stmt.close();
                    if (con != null) con.close();
                } catch(Exception e) {
                    System.out.println(e.getMessage());
                }*/
        }
    }


void MainWindow::on_airlineComboBox_currentIndexChanged()
{

}


void MainWindow::on_findRoute_clicked()
{
    buildtree(ui->startairport->text().toStdString(), ui->targetairport->text().toStdString());
}

