#include "mainwindow.h"
#include "ui_mainwindow.h"

#include "include/tinyxml2/tinyxml2.h"

#include <QFileDialog>
#include <string>
#include <iostream>
#include <QMessageBox>

using namespace std;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->treeWidget->setHeaderLabel("XML-Elemente");
}

MainWindow::~MainWindow()
{
    delete ui;
}

tinyxml2::XMLDocument doc;
QString FILENAME;

void MainWindow::on_pushButton_openFile_clicked()
{
   // QString filter = "XML Files (*.xml)";
  //  QString fName = QFileDialog::getOpenFileName(this, "Select a file to open...", QDir::homePath(), filter);

    QFileDialog dialog(this);
   // QStringList selectedFiles;

   // dialog.setNameFilter(tr("XML-Files (*.xml)"));

    if (dialog.exec()) {

        FILENAME = dialog.selectedFiles().at(0);

    }

    //tinyxml2::XMLDocument doc;

    //Teststr: "C:\\Users\\elvin\\QtCreatorProjects\\XMLExplorer\\mainwindow.ui"
    if (!doc.LoadFile(FILENAME.toStdString().c_str())) {

        //cout << filename.toStdString() << endl;

       // tree = ui->treeWidget;
        ui->treeWidget->clear();
        ui->comboBoxAttributes->clear();
        ui->textEditAttributes->clear();

        tinyxml2::XMLElement *e = doc.RootElement();

        QTreeWidgetItem *ri = new QTreeWidgetItem();
        ri->setText(0, e->Name());
        ri->setData(0, Qt::UserRole, QVariant::fromValue(e));

        addChildren(e, ri);
        ui->treeWidget->addTopLevelItem(ri);

    }
}

void MainWindow::addChildren(tinyxml2::XMLElement *element, QTreeWidgetItem *item) {

    tinyxml2::XMLElement *e = element->FirstChildElement();

    while (e) {

        QTreeWidgetItem *i = new QTreeWidgetItem(item);
        i->setText(0, e->Name());
        i->setData(0, Qt::UserRole, QVariant::fromValue(e));
        addChildren(e, i);
        item->addChild(i);
       // tree->addTopLevelItem(item);
        e = e->NextSiblingElement();

    }

}



void MainWindow::on_treeWidget_itemSelectionChanged()
{
   // cout << ui->treeWidget->currentItem()->text(ui->treeWidget->currentColumn())
     //       .toStdString() << endl;

    /*QModelIndexList selection = ui->treeWidget->selectionModel()->selectedRows();

    // Multiple rows can be selected
    for(int i=0; i< selection.count(); i++)
    {
        QModelIndex index = selection.at(i);
        qDebug() << index.row();
        index.data()
    }*/

   // QTreeWidgetItem* selectedItem = ui->treeWidget->currentItem();

    //tinyxml2::XMLElement* element = doc.RootElement()->FirstChildElement()->NextSiblingElement(); //zum Testen

    if (ui->treeWidget->selectedItems().count() > 0) {

        QString text;
        tinyxml2::XMLElement* element = ui->treeWidget->selectedItems()[0]->data(0, Qt::UserRole).value<tinyxml2::XMLElement*>();
        ui->comboBoxAttributes->clear();

        for (const tinyxml2::XMLAttribute* attr = element->FirstAttribute(); attr!=0; attr = attr->Next()) {

           // cout << attr->Name();
            const char* attrName = element->Attribute(attr->Name());

            if (attrName) {

                //cout << attrName;
                text += attr->Name() + QString::fromStdString(": ") + attrName + "\n";

            }

            ui->comboBoxAttributes->addItem(attr->Name());

        }

        const char* xmlText = element->GetText();

        if (xmlText) {

            text += QString::fromStdString("Elementtext: ") + xmlText;

        }

        ui->textEdit->setText(text);


    }

}




void MainWindow::on_pushButtonAttributes_clicked()
{
    if (ui->textEditAttributes->toPlainText().isEmpty()) {

        QMessageBox msgBox;
        msgBox.setText("Please enter a new attribute!");
        msgBox.exec();
        return;

    }

    tinyxml2::XMLElement* element = ui->treeWidget->selectedItems()[0]->data(0, Qt::UserRole).value<tinyxml2::XMLElement*>();

    element->SetAttribute(ui->comboBoxAttributes->currentText().toLocal8Bit().data(), ui->textEditAttributes->toPlainText().toLocal8Bit().data());
    //cout << element->Attribute(ui->comboBoxAttributes->currentText().toLocal8Bit().data());
}


void MainWindow::on_pushButtonSaveFile_clicked()
{
    doc.SaveFile(FILENAME.toLocal8Bit().constData());
}

