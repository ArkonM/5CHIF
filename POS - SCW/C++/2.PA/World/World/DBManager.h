#pragma once

#include <QSqlDatabase>
#include <QSqlQuery>
#include <QSqlError>
#include <QDebug>
#include <QString>

class DBManager
{
public:
	DBManager(const QString& path);
	~DBManager();

	QStringList getContinentNames();
	QStringList getCountries(QString continent);
	QStringList getCities(QString countriy);

	double getLat(QString city);
	double getLon(QString city);
	
	bool isOpen() const;

	QSqlError lastError();

private:
	QSqlDatabase myDb;

};

