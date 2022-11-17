#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "tinyxml2.h"
#include <QDebug>
#include <QFileDialog>
#include <iostream>
#include <sstream>

using namespace std;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

QString FILENAME;
void MainWindow::on_openFile_clicked()
{
    //click on openFile Button:
    //open File Explorer
    QFileDialog dialog(this);
    if (dialog.exec()) {
        FILENAME = dialog.selectedFiles().at(0);
    }
    //Read XML File
    tinyxml2::XMLDocument doc;
    doc.LoadFile(FILENAME.toStdString().c_str());


    tinyxml2::XMLElement *e = doc.RootElement()->FirstChildElement();
    while (e) {
        if (!e->FirstChildElement()) return;
        const char* op;
        stringstream temp;
        unsigned int first;
        unsigned int second;
        //cout << e->Name() << endl;
        temp << e->FirstChildElement("first")->GetText();
        temp >> first;
        //cout << "temp" << temp.str() << endl;
        temp << e->FirstChildElement("second")->GetText();
        temp >> second;
        op = e->FirstChildElement("operator")->GetText();
        //cout << op << endl;
        //qDebug() << first << second;
        //cout << op << endl;
        stringstream ss;
        int res{0};
        qDebug() << op;
        if (strcmp(op, "+") == 0) {
            res = first + second;
            qDebug() << "plus";
        } else if (strcmp(op, "-") == 0) {
            res = first - second;
            qDebug() << "minus";
        } else if (strcmp(op, "/") == 0) {
            res = first / second;
        } else if (strcmp(op, "*") == 0) {
            res = first * second;
        } else {
            e->NextSiblingElement();
            return;
        }
        //cout << first << second << op << res << endl;
        if (e->FirstChildElement("result")) {
            e->FirstChildElement("result")->SetText(res);
        } else {
            tinyxml2::XMLElement* rElement = doc.NewElement("result");
            ss << res;
            rElement->SetText(res);
            e->InsertEndChild(rElement);

        }
        e = e->NextSiblingElement();

    }
    doc.SaveFile(FILENAME.toStdString().c_str());

}

