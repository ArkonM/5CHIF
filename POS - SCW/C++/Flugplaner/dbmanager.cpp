#include "dbmanager.h"
#include <iostream>
#include <QSqlDatabase>
#include <QSqlQuery>


using namespace std;

DBManager::DBManager(const QString& path)
{
    m_db = QSqlDatabase::addDatabase("QSQLITE");
    m_db.setDatabaseName(path);
    if (!m_db.open())
    {
      cout << "Error: connection with database fail";
    }
    else
    {
      cout << "Database: connection ok";
    }
    QSqlQuery query;
    string al = "351";
    //query.exec(QString::fromStdString("select * from airport where name like '%Munich%'"));
    query.exec(QString::fromStdString("select * from Airline where id=1"));
    //query.exec(QString::fromStdString("select distinct(airline) from route join airport on airport.id = airport2 where airport1=4908"));
    //QStringList res;
    cout << query.size() << endl;
    while(query.next()) {
        cout << "Alliance" << query.value("name").value<QString>().toStdString() << endl;
    }
}

DBManager::~DBManager() {
    m_db.close();
    cout << "close connection" << endl;
}

QStringList DBManager::getAllAirports() {
    QSqlQuery query;
    query.exec("SELECT name FROM Airport");
    QStringList res;
    while(query.next()) {
        res.push_back(query.value(0).toString());
    }
    return res;
}

QStringList DBManager::getAllAirlines() {
    QSqlQuery query;
    query.exec("SELECT name FROM Airline");
    QStringList res;
    while(query.next()) {
        res.push_back(query.value(0).toString());
    }
    return res;
}

QSqlQuery DBManager::getAPbyName(string start) {
    QSqlQuery query;
    string q = "select * from airport where upper(name) like '%"+start+"%'";
    query.exec(QString::fromStdString(q));
    return query;
}

QSqlQuery DBManager::getAPbyIATA(string start) {
    transform(start.begin(), start.end(), start.begin(), ::toupper);
    QSqlQuery query;
    string q = "select * from airport where upper(iata) = '"+(start)+"'";
    query.exec(QString::fromStdString(q));
    return query;
}

QSqlQuery DBManager::getResfromSQL(string sql) {
    QSqlQuery query;
    cout << sql << endl;
    query.exec(QString::fromStdString(sql));
    return query;
}


