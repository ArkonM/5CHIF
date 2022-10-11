#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "include/calclib.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->InputAndResult->setDisabled(true);
    ui->History->setDisabled(true);

}

MainWindow::~MainWindow()
{
    delete ui;
}

//Fügt die eingegebene Nummer zur Anzeige hinzu
void MainWindow::addSymbol(QString input) {

    if(ui->History->text().size() != 0 && ui->History->text()[ui->History->text().size()-1] == '='){
        ui->History->setText("");
        ui->InputAndResult->setText("");
    }

    QString text = ui->InputAndResult->text();

    if((text.size() == 0 || text[text.size()-1] == '(' || (text[text.size()-2] == '+' || text[text.size()-2] == '-'
        || text[text.size()-2] == '*' || text[text.size()-2] == '/')) && input == '(' ){

        text.append(input);
        ui->InputAndResult->setText(text);
    } else if (text.size() != 0 && input == ')'){
        for(int i = text.size()-1; i >= 0; i--){
            if (text[text.size()-1] == '0' || text[text.size()-1] == '1' || text[text.size()-1] == '2' || text[text.size()-1] == '3'
                       || text[text.size()-1] == '4' || text[text.size()-1] == '5' || text[text.size()-1] == '6' || text[text.size()-1] == '7'
                       || text[text.size()-1] == '8' || text[text.size()-1] == '9' || text[text.size()-1] == ')'){
                text.append(input);
                ui->InputAndResult->setText(text);
                break;
            } else {
                break;
            }
        }
    } else if (input != '('){
        text.append(input);
        ui->InputAndResult->setText(text);
    }

}


void MainWindow::addOperator(QString input){

    QString input_text = ui->InputAndResult->text();
    QString history_text = ui->History->text();

    if(checkInput()){

        input_text.append(" " + input + " ");

        if(checkBrackets() == 0){
            history_text.append(input_text);
            ui->History->setText(history_text);
            ui->InputAndResult->setText("");
        } else {
            ui->InputAndResult->setText(input_text);
        }
    }
}

//Überprüft die Vollständigkeit von Klammern
int MainWindow::checkBrackets() {

    QString text = ui->InputAndResult->text();
    int brackets = 0;

    for(int i = 0; i < text.size(); i++){
        if(text[i] == '('){
            brackets++;
        } else if (text[i] == ')'){
            brackets--;
        }
    }

    return brackets;
}



bool MainWindow::checkInput(){

    QString text = ui->InputAndResult->text();

    if(text.size() != 0){
        QString lastCharacter = text[text.size()-1];

        if(lastCharacter != "0" && lastCharacter != "1" && lastCharacter != "2" && lastCharacter != "3"
                && lastCharacter != "4" && lastCharacter != "5" && lastCharacter != "6" && lastCharacter != "7"
                && lastCharacter != "8" && lastCharacter != "9" && lastCharacter != ")"){

            return false;
        }
    } else {
        return false;
    }

    return true;
}


void MainWindow::setComma(){

    QString text = ui->InputAndResult->text();

    if(text.size() == 0 || text[text.size()-1] == ' ' || text[text.size()-1] == '('){

            text.append("0.");

    } else if(!(text[text.size()-1] == '.')) {
        bool alreadyComma = false;
        for(int i = 0; i < text.size(); i++){
            if(text[i] == '.'){
                alreadyComma = true;
            } else if(text[i] == ' '){
                alreadyComma = false;
            }
        }
        if(!alreadyComma){
            text.append(".");
        }
    }

    ui->InputAndResult->setText(text);
}


void MainWindow::on_B0_clicked()
{
    addSymbol("0");
}


void MainWindow::on_B1_clicked()
{
    addSymbol("1");
}


void MainWindow::on_B2_clicked()
{
    addSymbol("2");
}


void MainWindow::on_B3_clicked()
{
    addSymbol("3");
}


void MainWindow::on_B4_clicked()
{
    addSymbol("4");
}


void MainWindow::on_B5_clicked()
{
    addSymbol("5");
}


void MainWindow::on_B6_clicked()
{
    addSymbol("6");
}


void MainWindow::on_B7_clicked()
{
    addSymbol("7");
}


void MainWindow::on_B8_clicked()
{
    addSymbol("8");
}


void MainWindow::on_B9_clicked()
{
    addSymbol("9");
}


void MainWindow::on_BClear_clicked()
{
    ui->InputAndResult->setText("");
    ui->History->setText("");
}


void MainWindow::on_BDelete_clicked()
{
    QString text = ui->InputAndResult->text();
    text.resize(text.length()-1);
    ui->InputAndResult->setText(text);
}


void MainWindow::on_BPlus_clicked()
{
    addOperator("+");
}


void MainWindow::on_BMinus_clicked()
{
    addOperator("-");
}


void MainWindow::on_BMulti_clicked()
{
    addOperator("*");
}


void MainWindow::on_BDivide_clicked()
{
    addOperator("/");
}


void MainWindow::on_BPosNeg_clicked()
{
    QString inputText = ui->InputAndResult->text();
    for(int i = inputText.size()-1; i >= 0; i--){
        if((inputText[i] == ' ' && (inputText[i-1] == '+' || inputText[i-1] == '-' || inputText[i-1] == '*' || inputText[i-1] == '/')) || inputText[i] == '('){
            inputText.insert(i+1, '-');
            ui->InputAndResult->setText(inputText);
            break;
        } else if (inputText[i] == '-'){
            inputText.remove(i,1);
            ui->InputAndResult->setText(inputText);
            break;
        } else if (inputText[i] == '+'){
            inputText[i] = '-';
            ui->InputAndResult->setText(inputText);
            break;
        }
    } if (inputText.size() == 0){
        inputText.append('-');
        ui->InputAndResult->setText(inputText);
    }
}


void MainWindow::on_BComma_clicked()
{
    setComma();
}


void MainWindow::on_BResult_clicked()
{   

    if(checkBrackets() == 0){
        QString inputText = ui->InputAndResult->text();
        QString historyText = ui->History->text();
        historyText.append(inputText);

        double result = calculate(historyText);
        historyText.append(" =");

        ui->History->setText(historyText);
        ui->InputAndResult->setText("");
        ui->InputAndResult->setText(QString::number(result));
    }
}


void MainWindow::on_BLeftBracket_clicked()
{
    addSymbol("(");
}


void MainWindow::on_BRightBracket_clicked()
{
    if(checkBrackets() > 0){
        addSymbol(")");
    }
}

