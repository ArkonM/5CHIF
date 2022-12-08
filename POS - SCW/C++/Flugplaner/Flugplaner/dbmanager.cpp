#include "dbmanager.h"
#include "qdebug.h"
#include <QSqlQuery>
#include <QSqlDatabase>
#include <QSqlRecord>
#include <QString>

DbManager::DbManager(const QString& path)
{
   m_db = QSqlDatabase::addDatabase("QSQLITE");
   m_db.setDatabaseName(path);

   if (!m_db.open())
   {
      qDebug() << "Error: connection with database fail";
   }
   else
   {
      qDebug() << "Database: connection ok";
   }
}


