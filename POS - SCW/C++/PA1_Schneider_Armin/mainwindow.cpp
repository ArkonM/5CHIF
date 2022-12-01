#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "spdlog/common.h"
#include <QFileDialog>

bool selection = false;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    ui->DatumsFormat->addItems({"MM/dd/yyyy hh:mm ap",
                               "dd.MM.yyyy hh:mm",
                               "yyyy-MM-dd-hh-mm"});

    ui->ShowWork->setHeaderLabel("Aufgaben");
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_AddToCalnd_clicked()
{
    QTreeWidgetItem *ri = new QTreeWidgetItem();
    ri->setText(0 , ui->AufgabenName->text());
    ri->setData(0, Qt::UserRole, ui->SetFinishDate->dateTime());

    if(selection){
        ui->ShowWork->currentItem()->addChild(ri);
        ui->ShowWork->clearSelection();
        selection = false;
    } else {
        ui->ShowWork->addTopLevelItem(ri);
    }

}


void MainWindow::on_ShowWork_itemSelectionChanged()
{
    selection = true;

    QString stringDate = ui->ShowWork->currentItem()->data(0, Qt::UserRole).toString();
    ui->Show_FinishDate->setText(stringDate);
    //QDateTime dateTime = QDateTime::fromString(stringDate);

    //ui->Show_FinishDate->setText(dateTime.date().toString());


}


void MainWindow::on_DatumsFormat_currentTextChanged(const QString &arg1)
{
    ui->SetFinishDate->setDisplayFormat(arg1);
}


void MainWindow::on_choosFile_clicked()
{
    QString title;
    QString directory;
    QString selfilter = tr("Text Document (*.txt)");
    QString fileName = QFileDialog::getOpenFileName(
            this,
            title,
            directory,
            tr("Text Document (*.txt)" ),
            &selfilter
    );

    ui->show_FilePath->setText(fileName);
}

