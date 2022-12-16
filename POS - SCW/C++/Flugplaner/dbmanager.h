#ifndef DBMANAGER_H
#define DBMANAGER_H

#include <QSqlDatabase>
#include <QSqlQuery>
#include <QString>
#include <string>



class DBManager
{
public:
    DBManager(const QString& path);
    ~DBManager();
    QStringList getAllAirports();
    QStringList getAllAirlines();
    QSqlQuery getAPbyName(std::string name);
    QSqlQuery getAPbyIATA(std::string start);
    QSqlQuery getResfromSQL(std::string sql);
private:
    QSqlDatabase m_db;
};

#endif // DBMANAGER_H
