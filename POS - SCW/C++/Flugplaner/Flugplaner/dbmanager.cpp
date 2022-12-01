#include "dbmanager.h"
#include "qdebug.h"
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


QSqlQuery DbManager::Db_Search(QString search){
    /*QSqlQuery query{};
    query.prepare("SELECT name FROM people WHERE name = (:name)");
    query.bindValue(":name", name);

    if (query.exec())
    {
       if (query.next())
       {
          // it exists
       }
    }*/
}
