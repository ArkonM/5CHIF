#include "dbmanager.h"
#include <QSqlDatabase>
#include <QSqlQuery>
#include <QString>
#include <iostream>

using namespace std;

DbManager::DbManager(const QString& path)
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
}

DbManager::~DbManager() {
    m_db.close();
    cout << "close connection" << endl;
}


QStringList DbManager::getStatebyName(string start) {
    QSqlQuery query;
    string q = "select place from quakes where upper(place) like '%"+start+"%'";
    query.exec(QString::fromStdString(q));
    QStringList res;
    while(query.next()) {
        res.push_back(query.value(0).toString());
    }
    return res;
}



QSqlQuery DbManager::getQuakesbyName(string start) {
    QSqlQuery query;
    string q = "select time, latitude, longitude, mag from quakes where upper(place) like '%"+start+"%'";
    query.exec(QString::fromStdString(q));
    return query;
}
