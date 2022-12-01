/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 6.3.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QDateTimeEdit>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTreeWidget>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QLineEdit *AufgabenName;
    QDateTimeEdit *SetFinishDate;
    QPushButton *AddToCalnd;
    QLabel *label;
    QLabel *label_2;
    QLabel *label_3;
    QComboBox *DatumsFormat;
    QLabel *label_4;
    QLabel *Show_FinishDate;
    QPushButton *choosFile;
    QLabel *show_FilePath;
    QTreeWidget *ShowWork;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QString::fromUtf8("MainWindow"));
        MainWindow->resize(800, 600);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        AufgabenName = new QLineEdit(centralwidget);
        AufgabenName->setObjectName(QString::fromUtf8("AufgabenName"));
        AufgabenName->setGeometry(QRect(330, 60, 231, 26));
        SetFinishDate = new QDateTimeEdit(centralwidget);
        SetFinishDate->setObjectName(QString::fromUtf8("SetFinishDate"));
        SetFinishDate->setGeometry(QRect(590, 60, 194, 26));
        AddToCalnd = new QPushButton(centralwidget);
        AddToCalnd->setObjectName(QString::fromUtf8("AddToCalnd"));
        AddToCalnd->setGeometry(QRect(330, 110, 231, 29));
        label = new QLabel(centralwidget);
        label->setObjectName(QString::fromUtf8("label"));
        label->setGeometry(QRect(330, 30, 63, 20));
        label_2 = new QLabel(centralwidget);
        label_2->setObjectName(QString::fromUtf8("label_2"));
        label_2->setGeometry(QRect(590, 20, 151, 20));
        label_3 = new QLabel(centralwidget);
        label_3->setObjectName(QString::fromUtf8("label_3"));
        label_3->setGeometry(QRect(340, 200, 201, 20));
        DatumsFormat = new QComboBox(centralwidget);
        DatumsFormat->setObjectName(QString::fromUtf8("DatumsFormat"));
        DatumsFormat->setGeometry(QRect(340, 230, 191, 26));
        label_4 = new QLabel(centralwidget);
        label_4->setObjectName(QString::fromUtf8("label_4"));
        label_4->setGeometry(QRect(340, 270, 311, 20));
        Show_FinishDate = new QLabel(centralwidget);
        Show_FinishDate->setObjectName(QString::fromUtf8("Show_FinishDate"));
        Show_FinishDate->setGeometry(QRect(340, 310, 161, 20));
        choosFile = new QPushButton(centralwidget);
        choosFile->setObjectName(QString::fromUtf8("choosFile"));
        choosFile->setGeometry(QRect(20, 310, 181, 29));
        show_FilePath = new QLabel(centralwidget);
        show_FilePath->setObjectName(QString::fromUtf8("show_FilePath"));
        show_FilePath->setGeometry(QRect(20, 350, 721, 20));
        ShowWork = new QTreeWidget(centralwidget);
        QTreeWidgetItem *__qtreewidgetitem = new QTreeWidgetItem();
        __qtreewidgetitem->setText(0, QString::fromUtf8("1"));
        ShowWork->setHeaderItem(__qtreewidgetitem);
        ShowWork->setObjectName(QString::fromUtf8("ShowWork"));
        ShowWork->setGeometry(QRect(10, 20, 291, 261));
        MainWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName(QString::fromUtf8("menubar"));
        menubar->setGeometry(QRect(0, 0, 800, 26));
        MainWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(MainWindow);
        statusbar->setObjectName(QString::fromUtf8("statusbar"));
        MainWindow->setStatusBar(statusbar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "MainWindow", nullptr));
        AddToCalnd->setText(QCoreApplication::translate("MainWindow", "Neue Aufgabe einf\303\274gen", nullptr));
        label->setText(QCoreApplication::translate("MainWindow", "Aufgabe", nullptr));
        label_2->setText(QCoreApplication::translate("MainWindow", "Fertigstellungstermin", nullptr));
        label_3->setText(QCoreApplication::translate("MainWindow", "Zeit- / Datumsformatierung", nullptr));
        label_4->setText(QCoreApplication::translate("MainWindow", "Fertigstellungstermin der gew\303\244hlten Aufgabe", nullptr));
        Show_FinishDate->setText(QCoreApplication::translate("MainWindow", "Datum", nullptr));
        choosFile->setText(QCoreApplication::translate("MainWindow", "Logfile w\303\244hlen", nullptr));
        show_FilePath->setText(QCoreApplication::translate("MainWindow", "Current file Path", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
