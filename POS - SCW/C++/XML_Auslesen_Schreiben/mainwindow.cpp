#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "tinyxml2.h"
#include <iostream>
#include <sstream>

using namespace std;

QString FILENAME = "C:/GitHub/5CHIF/POS - SCW/C++/XML_Auslesen_Schreiben/XML_Test.xml";

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


void MainWindow::on_pushButton_clicked()
{
    tinyxml2::XMLDocument doc;
    doc.LoadFile(FILENAME.toStdString().c_str());
    unsigned int first;
    unsigned int second;
    const char* op;
    stringstream temp;
    tinyxml2::XMLElement *e = doc.RootElement();
    //Iterate over Element
    temp << e->FirstChildElement("first")->GetText();
    temp >> first;
    temp << e->FirstChildElement("second")->GetText();
    temp >> second;
    op = e->FirstChildElement("operator")->GetText();
    cout << op << endl;
    //qDebug() << first << second;
    //cout << op << endl;
    stringstream ss;
    int res{0};
    if (strcmp(op, "+")) {
        res = first + second;
    } else if (strcmp(op, "-")) {
        res = first - second;
    } else if (strcmp(op, "/")) {
        res = first / second;
    } else if (strcmp(op, "*")) {
        res = first * second;
    } else {
        ui->lineEdit->setText("Not allowed operator!");
        return;
    }
    ui->showOperator->setText(op);

    ss << res;
    QString s = QString::fromStdString(ss.str());
    ui->lineEdit->setText(s);
}

