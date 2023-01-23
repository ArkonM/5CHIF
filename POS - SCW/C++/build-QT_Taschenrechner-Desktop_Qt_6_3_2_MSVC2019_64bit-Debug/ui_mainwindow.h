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
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QWidget *gridLayoutWidget;
    QGridLayout *gridLayout;
    QPushButton *B1;
    QPushButton *B8;
    QPushButton *B3;
    QPushButton *B0;
    QPushButton *BPosNeg;
    QPushButton *BRightBracket;
    QLineEdit *InputAndResult;
    QPushButton *BMulti;
    QPushButton *BResult;
    QPushButton *B4;
    QPushButton *B5;
    QPushButton *BMinus;
    QPushButton *B2;
    QPushButton *BLeftBracket;
    QPushButton *BDelete;
    QPushButton *BComma;
    QPushButton *B6;
    QPushButton *B9;
    QPushButton *B7;
    QPushButton *BPlus;
    QPushButton *BDivide;
    QLineEdit *History;
    QPushButton *BClear;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QString::fromUtf8("MainWindow"));
        MainWindow->resize(800, 600);
        MainWindow->setStyleSheet(QString::fromUtf8(""));
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        centralwidget->setStyleSheet(QString::fromUtf8(""));
        gridLayoutWidget = new QWidget(centralwidget);
        gridLayoutWidget->setObjectName(QString::fromUtf8("gridLayoutWidget"));
        gridLayoutWidget->setGeometry(QRect(140, 10, 461, 521));
        gridLayout = new QGridLayout(gridLayoutWidget);
        gridLayout->setObjectName(QString::fromUtf8("gridLayout"));
        gridLayout->setContentsMargins(0, 0, 0, 0);
        B1 = new QPushButton(gridLayoutWidget);
        B1->setObjectName(QString::fromUtf8("B1"));
        QSizePolicy sizePolicy(QSizePolicy::Preferred, QSizePolicy::Preferred);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(B1->sizePolicy().hasHeightForWidth());
        B1->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B1, 3, 0, 1, 1);

        B8 = new QPushButton(gridLayoutWidget);
        B8->setObjectName(QString::fromUtf8("B8"));
        sizePolicy.setHeightForWidth(B8->sizePolicy().hasHeightForWidth());
        B8->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B8, 5, 1, 1, 1);

        B3 = new QPushButton(gridLayoutWidget);
        B3->setObjectName(QString::fromUtf8("B3"));
        sizePolicy.setHeightForWidth(B3->sizePolicy().hasHeightForWidth());
        B3->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B3, 3, 2, 1, 1);

        B0 = new QPushButton(gridLayoutWidget);
        B0->setObjectName(QString::fromUtf8("B0"));
        sizePolicy.setHeightForWidth(B0->sizePolicy().hasHeightForWidth());
        B0->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B0, 6, 1, 1, 1);

        BPosNeg = new QPushButton(gridLayoutWidget);
        BPosNeg->setObjectName(QString::fromUtf8("BPosNeg"));
        sizePolicy.setHeightForWidth(BPosNeg->sizePolicy().hasHeightForWidth());
        BPosNeg->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BPosNeg, 6, 0, 1, 1);

        BRightBracket = new QPushButton(gridLayoutWidget);
        BRightBracket->setObjectName(QString::fromUtf8("BRightBracket"));
        sizePolicy.setHeightForWidth(BRightBracket->sizePolicy().hasHeightForWidth());
        BRightBracket->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BRightBracket, 2, 1, 1, 1);

        InputAndResult = new QLineEdit(gridLayoutWidget);
        InputAndResult->setObjectName(QString::fromUtf8("InputAndResult"));
        sizePolicy.setHeightForWidth(InputAndResult->sizePolicy().hasHeightForWidth());
        InputAndResult->setSizePolicy(sizePolicy);
        InputAndResult->setLayoutDirection(Qt::LeftToRight);
        InputAndResult->setAutoFillBackground(false);
        InputAndResult->setStyleSheet(QString::fromUtf8("border: none; color: black;"));

        gridLayout->addWidget(InputAndResult, 1, 0, 1, 4);

        BMulti = new QPushButton(gridLayoutWidget);
        BMulti->setObjectName(QString::fromUtf8("BMulti"));
        sizePolicy.setHeightForWidth(BMulti->sizePolicy().hasHeightForWidth());
        BMulti->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BMulti, 4, 3, 1, 1);

        BResult = new QPushButton(gridLayoutWidget);
        BResult->setObjectName(QString::fromUtf8("BResult"));
        sizePolicy.setHeightForWidth(BResult->sizePolicy().hasHeightForWidth());
        BResult->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BResult, 6, 3, 1, 1);

        B4 = new QPushButton(gridLayoutWidget);
        B4->setObjectName(QString::fromUtf8("B4"));
        sizePolicy.setHeightForWidth(B4->sizePolicy().hasHeightForWidth());
        B4->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B4, 4, 0, 1, 1);

        B5 = new QPushButton(gridLayoutWidget);
        B5->setObjectName(QString::fromUtf8("B5"));
        sizePolicy.setHeightForWidth(B5->sizePolicy().hasHeightForWidth());
        B5->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B5, 4, 1, 1, 1);

        BMinus = new QPushButton(gridLayoutWidget);
        BMinus->setObjectName(QString::fromUtf8("BMinus"));
        sizePolicy.setHeightForWidth(BMinus->sizePolicy().hasHeightForWidth());
        BMinus->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BMinus, 3, 3, 1, 1);

        B2 = new QPushButton(gridLayoutWidget);
        B2->setObjectName(QString::fromUtf8("B2"));
        sizePolicy.setHeightForWidth(B2->sizePolicy().hasHeightForWidth());
        B2->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B2, 3, 1, 1, 1);

        BLeftBracket = new QPushButton(gridLayoutWidget);
        BLeftBracket->setObjectName(QString::fromUtf8("BLeftBracket"));
        sizePolicy.setHeightForWidth(BLeftBracket->sizePolicy().hasHeightForWidth());
        BLeftBracket->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BLeftBracket, 2, 0, 1, 1);

        BDelete = new QPushButton(gridLayoutWidget);
        BDelete->setObjectName(QString::fromUtf8("BDelete"));
        sizePolicy.setHeightForWidth(BDelete->sizePolicy().hasHeightForWidth());
        BDelete->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BDelete, 2, 2, 1, 1);

        BComma = new QPushButton(gridLayoutWidget);
        BComma->setObjectName(QString::fromUtf8("BComma"));
        sizePolicy.setHeightForWidth(BComma->sizePolicy().hasHeightForWidth());
        BComma->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BComma, 6, 2, 1, 1);

        B6 = new QPushButton(gridLayoutWidget);
        B6->setObjectName(QString::fromUtf8("B6"));
        sizePolicy.setHeightForWidth(B6->sizePolicy().hasHeightForWidth());
        B6->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B6, 4, 2, 1, 1);

        B9 = new QPushButton(gridLayoutWidget);
        B9->setObjectName(QString::fromUtf8("B9"));
        sizePolicy.setHeightForWidth(B9->sizePolicy().hasHeightForWidth());
        B9->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B9, 5, 2, 1, 1);

        B7 = new QPushButton(gridLayoutWidget);
        B7->setObjectName(QString::fromUtf8("B7"));
        sizePolicy.setHeightForWidth(B7->sizePolicy().hasHeightForWidth());
        B7->setSizePolicy(sizePolicy);

        gridLayout->addWidget(B7, 5, 0, 1, 1);

        BPlus = new QPushButton(gridLayoutWidget);
        BPlus->setObjectName(QString::fromUtf8("BPlus"));
        sizePolicy.setHeightForWidth(BPlus->sizePolicy().hasHeightForWidth());
        BPlus->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BPlus, 2, 3, 1, 1);

        BDivide = new QPushButton(gridLayoutWidget);
        BDivide->setObjectName(QString::fromUtf8("BDivide"));
        sizePolicy.setHeightForWidth(BDivide->sizePolicy().hasHeightForWidth());
        BDivide->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BDivide, 5, 3, 1, 1);

        History = new QLineEdit(gridLayoutWidget);
        History->setObjectName(QString::fromUtf8("History"));
        sizePolicy.setHeightForWidth(History->sizePolicy().hasHeightForWidth());
        History->setSizePolicy(sizePolicy);
        History->setStyleSheet(QString::fromUtf8("border: none;"));

        gridLayout->addWidget(History, 0, 0, 1, 3);

        BClear = new QPushButton(gridLayoutWidget);
        BClear->setObjectName(QString::fromUtf8("BClear"));
        sizePolicy.setHeightForWidth(BClear->sizePolicy().hasHeightForWidth());
        BClear->setSizePolicy(sizePolicy);

        gridLayout->addWidget(BClear, 0, 3, 1, 1);

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
        B1->setText(QCoreApplication::translate("MainWindow", "1", nullptr));
        B8->setText(QCoreApplication::translate("MainWindow", "8", nullptr));
        B3->setText(QCoreApplication::translate("MainWindow", "3", nullptr));
        B0->setText(QCoreApplication::translate("MainWindow", "0", nullptr));
        BPosNeg->setText(QCoreApplication::translate("MainWindow", "+/-", nullptr));
        BRightBracket->setText(QCoreApplication::translate("MainWindow", ")", nullptr));
        BMulti->setText(QCoreApplication::translate("MainWindow", "*", nullptr));
        BResult->setText(QCoreApplication::translate("MainWindow", "=", nullptr));
        B4->setText(QCoreApplication::translate("MainWindow", "4", nullptr));
        B5->setText(QCoreApplication::translate("MainWindow", "5", nullptr));
        BMinus->setText(QCoreApplication::translate("MainWindow", "-", nullptr));
        B2->setText(QCoreApplication::translate("MainWindow", "2", nullptr));
        BLeftBracket->setText(QCoreApplication::translate("MainWindow", "(", nullptr));
        BDelete->setText(QCoreApplication::translate("MainWindow", "Delete", nullptr));
        BComma->setText(QCoreApplication::translate("MainWindow", ".", nullptr));
        B6->setText(QCoreApplication::translate("MainWindow", "6", nullptr));
        B9->setText(QCoreApplication::translate("MainWindow", "9", nullptr));
        B7->setText(QCoreApplication::translate("MainWindow", "7", nullptr));
        BPlus->setText(QCoreApplication::translate("MainWindow", "+", nullptr));
        BDivide->setText(QCoreApplication::translate("MainWindow", "/", nullptr));
        BClear->setText(QCoreApplication::translate("MainWindow", "Clear", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
