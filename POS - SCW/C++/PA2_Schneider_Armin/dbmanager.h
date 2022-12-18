#ifndef DBMANAGER_H
#define DBMANAGER_H

#include <QString>
#include <QSqlDatabase>
#include <QSqlQuery>
#include <string>


class DbManager
{
public:
    DbManager(const QString& path);
    ~DbManager();
    QStringList getStatebyName(std::string name);
    QSqlQuery getQuakesbyName(std::string name);
private:
    QSqlDatabase m_db;
};

#endif // DBMANAGER_H
