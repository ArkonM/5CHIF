#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "tinyxml2.h"
#include <QFileDialog>

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
    tinyxml2::XMLDocument Docs{};
    QFileDialog dialog(this, "Datei wählen", "D:/Github/5CHIF/POS - SCW/C++/XML_Explorer");
    dialog.setNameFilter(tr("XML (*.xml)"));
    QStringList fileNames{};
    if (dialog.exec()){
        fileNames = dialog.selectedFiles();

        ui->XMLTree->setColumnCount(1);
        ui->XMLTree->addTopLevelItem();

    }
}

