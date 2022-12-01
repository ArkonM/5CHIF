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
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTextEdit>
#include <QtWidgets/QTreeWidget>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QPushButton *pushButton_openFile;
    QTreeWidget *treeWidget;
    QTextEdit *textEdit;
    QComboBox *comboBoxAttributes;
    QTextEdit *textEditAttributes;
    QPushButton *pushButtonAttributes;
    QPushButton *pushButtonSaveFile;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QString::fromUtf8("MainWindow"));
        MainWindow->resize(655, 272);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName(QString::fromUtf8("centralwidget"));
        pushButton_openFile = new QPushButton(centralwidget);
        pushButton_openFile->setObjectName(QString::fromUtf8("pushButton_openFile"));
        pushButton_openFile->setGeometry(QRect(20, 30, 80, 21));
        treeWidget = new QTreeWidget(centralwidget);
        treeWidget->headerItem()->setText(0, QString());
        treeWidget->setObjectName(QString::fromUtf8("treeWidget"));
        treeWidget->setGeometry(QRect(130, 30, 256, 171));
        textEdit = new QTextEdit(centralwidget);
        textEdit->setObjectName(QString::fromUtf8("textEdit"));
        textEdit->setGeometry(QRect(400, 30, 241, 171));
        comboBoxAttributes = new QComboBox(centralwidget);
        comboBoxAttributes->setObjectName(QString::fromUtf8("comboBoxAttributes"));
        comboBoxAttributes->setGeometry(QRect(20, 70, 81, 22));
        textEditAttributes = new QTextEdit(centralwidget);
        textEditAttributes->setObjectName(QString::fromUtf8("textEditAttributes"));
        textEditAttributes->setGeometry(QRect(20, 100, 81, 31));
        pushButtonAttributes = new QPushButton(centralwidget);
        pushButtonAttributes->setObjectName(QString::fromUtf8("pushButtonAttributes"));
        pushButtonAttributes->setGeometry(QRect(10, 140, 101, 21));
        pushButtonSaveFile = new QPushButton(centralwidget);
        pushButtonSaveFile->setObjectName(QString::fromUtf8("pushButtonSaveFile"));
        pushButtonSaveFile->setGeometry(QRect(10, 170, 101, 21));
        MainWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName(QString::fromUtf8("menubar"));
        menubar->setGeometry(QRect(0, 0, 655, 17));
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
        pushButton_openFile->setText(QCoreApplication::translate("MainWindow", "Open File", nullptr));
        pushButtonAttributes->setText(QCoreApplication::translate("MainWindow", "Change Attribute", nullptr));
        pushButtonSaveFile->setText(QCoreApplication::translate("MainWindow", "Save File", nullptr));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
