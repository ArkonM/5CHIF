#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_World.h"
#include "DBManager.h"

class World : public QMainWindow
{
    Q_OBJECT

public:
    World(QWidget *parent = nullptr);
    ~World();

private slots:

    void on_continentBox_currentTextChanged(const QString& arg1);

    void on_countryBox_currentTextChanged(const QString& arg1);

    void on_tableWidget_cellClicked(int row, int column);

    void on_cityBox_currentTextChanged(const QString& arg1);

    void on_pushButton_clicked();

    void on_pushButton_2_clicked();

private:
    Ui::WorldClass ui;
    DBManager* db;

    int currentSelectedIndex = -1;

    QString currentCity = "";

};
