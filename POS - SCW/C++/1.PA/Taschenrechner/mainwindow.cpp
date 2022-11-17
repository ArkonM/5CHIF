#include "mainwindow.h"
#include "ui_mainwindow.h"
//#include "exprtk.hpp"
#include "include/MyMath/mymath.h"

#include <iostream>
#include <string>

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

//typedef exprtk::expression<double> expression_t;
//typedef exprtk::parser<double> parser_t;

void MainWindow::on_pushButton_open_clicked()
{
    setInput("(");
}


void MainWindow::on_pushButton_0_clicked()
{
    setInput("0");
}


void MainWindow::on_pushButton_close_clicked()
{
    setInput(")");
}


void MainWindow::on_pushButton_1_clicked()
{
    setInput("1");
}


void MainWindow::on_pushButton_2_clicked()
{
    setInput("2");
}


void MainWindow::on_pushButton_3_clicked()
{
    setInput("3");
}


void MainWindow::on_pushButton_4_clicked()
{
    setInput("4");
}


void MainWindow::on_pushButton_5_clicked()
{
    setInput("5");
}


void MainWindow::on_pushButton_6_clicked()
{
    setInput("6");
}


void MainWindow::on_pushButton_7_clicked()
{
    setInput("7");
}


void MainWindow::on_pushButton_8_clicked()
{
    setInput("8");
}


void MainWindow::on_pushButton_9_clicked()
{
    setInput("9");
}


void MainWindow::on_pushButton_equals_clicked()
{

  /* expression_t expression;
   parser_t parser;

   std::string expression_string = ui->label_input->text().toLocal8Bit().constData(); //QString to string

   if (parser.compile(expression_string, expression)) {

       double result = expression.value();
       setInput(" = ");
       ui->lineEdit_solution->setText(QString::fromStdString(std::to_string(result)));  //Qstring to string

   } else {

       ui->lineEdit_solution->setText("Fehler im Ausdruck");

   }*/

    ui->lineEdit_solution->setText(QString::number(calculate(ui->label_input->text().toStdString())));
   // ui->lineEdit_solution->setText("");
    MainWindow::input = "";
}


void MainWindow::on_pushButton_minus_clicked()
{
    setInput(" - ");
}


void MainWindow::on_pushButton_plus_clicked()
{
    setInput(" + ");
}


void MainWindow::on_pushButton_multiply_clicked()
{
    setInput(" * ");
}


void MainWindow::on_pushButton_divide_clicked()
{
    setInput(" / ");
}

void MainWindow::setInput(QString text) {

    MainWindow::input += text;
    ui->label_input->setText(MainWindow::input);

}
