/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 6.4.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTableWidget>
#include <QtWidgets/QWidget>
#include "worldmap.h"

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    worldmap *mapWidget;
    QLabel *start;
    QLineEdit *airportSearch;
    QLabel *target;
    QLabel *label;
    QComboBox *airportComboBox;
    QPushButton *searchbutton;
    QPushButton *setAsStart;
    QPushButton *setAsTarget;
    QTableWidget *RouteDetailTable;
    QLabel *start_2;
    QComboBox *airlineComboBox;
    QLabel *startairport;
    QLabel *targetairport;
    QPushButton *findRoute;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName("MainWindow");
        MainWindow->resize(1199, 606);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName("centralwidget");
        mapWidget = new worldmap(centralwidget);
        mapWidget->setObjectName("mapWidget");
        mapWidget->setGeometry(QRect(0, 100, 841, 451));
        start = new QLabel(centralwidget);
        start->setObjectName("start");
        start->setGeometry(QRect(10, 10, 91, 20));
        airportSearch = new QLineEdit(centralwidget);
        airportSearch->setObjectName("airportSearch");
        airportSearch->setGeometry(QRect(610, 10, 191, 28));
        target = new QLabel(centralwidget);
        target->setObjectName("target");
        target->setGeometry(QRect(210, 10, 101, 20));
        label = new QLabel(centralwidget);
        label->setObjectName("label");
        label->setGeometry(QRect(460, 10, 131, 20));
        airportComboBox = new QComboBox(centralwidget);
        airportComboBox->setObjectName("airportComboBox");
        airportComboBox->setGeometry(QRect(940, 10, 231, 28));
        searchbutton = new QPushButton(centralwidget);
        searchbutton->setObjectName("searchbutton");
        searchbutton->setEnabled(false);
        searchbutton->setGeometry(QRect(810, 10, 101, 29));
        setAsStart = new QPushButton(centralwidget);
        setAsStart->setObjectName("setAsStart");
        setAsStart->setEnabled(false);
        setAsStart->setGeometry(QRect(610, 50, 101, 29));
        setAsTarget = new QPushButton(centralwidget);
        setAsTarget->setObjectName("setAsTarget");
        setAsTarget->setEnabled(false);
        setAsTarget->setGeometry(QRect(720, 50, 101, 29));
        RouteDetailTable = new QTableWidget(centralwidget);
        RouteDetailTable->setObjectName("RouteDetailTable");
        RouteDetailTable->setGeometry(QRect(860, 160, 311, 391));
        start_2 = new QLabel(centralwidget);
        start_2->setObjectName("start_2");
        start_2->setGeometry(QRect(860, 120, 231, 20));
        airlineComboBox = new QComboBox(centralwidget);
        airlineComboBox->setObjectName("airlineComboBox");
        airlineComboBox->setGeometry(QRect(940, 60, 231, 28));
        startairport = new QLabel(centralwidget);
        startairport->setObjectName("startairport");
        startairport->setGeometry(QRect(110, 10, 91, 20));
        targetairport = new QLabel(centralwidget);
        targetairport->setObjectName("targetairport");
        targetairport->setGeometry(QRect(320, 10, 121, 20));
        findRoute = new QPushButton(centralwidget);
        findRoute->setObjectName("findRoute");
        findRoute->setGeometry(QRect(460, 40, 83, 29));
        MainWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName("menubar");
        menubar->setGeometry(QRect(0, 0, 1199, 21));
        MainWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(MainWindow);
        statusbar->setObjectName("statusbar");
        MainWindow->setStatusBar(statusbar);

        retranslateUi(MainWindow);

        airportComboBox->setCurrentIndex(-1);


        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "MainWindow", nullptr));
        start->setText(QCoreApplication::translate("MainWindow", "Start Airport:", nullptr));
        target->setText(QCoreApplication::translate("MainWindow", "Target Airport:", nullptr));
        label->setText(QCoreApplication::translate("MainWindow", "Search for Airport:", nullptr));
        searchbutton->setText(QCoreApplication::translate("MainWindow", "Search", nullptr));
        setAsStart->setText(QCoreApplication::translate("MainWindow", "Set as Start", nullptr));
        setAsTarget->setText(QCoreApplication::translate("MainWindow", "Set as Target", nullptr));
        start_2->setText(QCoreApplication::translate("MainWindow", "Route details", nullptr));
        startairport->setText(QString());
        targetairport->setText(QString());
        findRoute->setText(QCoreApplication::translate("MainWindow", "Find Route", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
