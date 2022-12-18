#include "DBManager.h"


DBManager::DBManager(const QString& path) {
	
    myDb = QSqlDatabase::addDatabase("QSQLITE");
	myDb.setDatabaseName(path);
    
    if (!myDb.open())
    {
        qDebug() << "Error: connection with database failed";
    }
    else
    {
        qDebug() << "Database: connection ok";
    }
}


DBManager::~DBManager() {
    if (myDb.isOpen()) {
        myDb.close();
    }
}


bool DBManager::isOpen() const
{
    return myDb.isOpen();
}


QSqlError DBManager::lastError() {
    return myDb.lastError();
}


QStringList DBManager::getContinentNames() {
    QSqlQuery query("select title from continents");
    query.exec(); 
    QStringList res;
    while (query.next()) {
        res.push_back(query.value(0).toString());
    }
    return res;
}


QStringList DBManager::getCountries(QString name) {
    QSqlQuery query;
    query.prepare("select title from countries where continent_id = (:name)");
    query.bindValue(":name", name.toInt());
    QStringList res;
    if (query.exec()) {
        qDebug() << "yes";
        while (query.next()) {
            res.push_back(query.value(0).toString());
        }
    }
    return res;
}


QStringList DBManager::getCities(QString name) {
    QSqlQuery query;
    query.prepare("select title from cities where country_id = (:name)");
    query.bindValue(":name", name.toInt());
    QStringList res;
    if (query.exec()) {
        qDebug() << "yes";
        while (query.next()) {
            res.push_back(query.value(0).toString());
        }
    }
    return res;
}

double DBManager::getLat(QString city) {
    QSqlQuery query;
    query.prepare("select lat from cities where title = (:name)");
    query.bindValue(":name", city);

    if (query.exec() && query.next()) {
        return query.value(0).toDouble();
    }

    return -1;
}


double DBManager::getLon(QString city) {
    QSqlQuery query;
    query.prepare("select lng from cities where title = (:name)");
    query.bindValue(":name", city);

    if (query.exec() && query.next()) {
        return query.value(0).toDouble();
    }

    return -1;
}