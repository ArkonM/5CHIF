#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <iostream>
#include "factory.h"
#include <QDir>
#include <QLibrary>

using namespace std;

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    formen = new QList<Form*>();
    factory = new Factory();

    load_plugins();
    //ui->form_select->insertItems(0, Factory::instance()->getFormList());
}

void MainWindow::load_plugins()
{
    cout << "Loading Plugins ..." << endl;
    cout << "Current Directory " << QDir::currentPath().toStdString() << endl;
    QDir directory("../plugins");
    QStringList plugins = directory.entryList(QStringList() << "*.dll" << "*.DLL",QDir::Files);
    foreach(QString filename, plugins) {
        cout << filename.toStdString() << endl;
        QLibrary library("../plugins/" + filename);
            if (!library.load())
            {
                qDebug() << library.errorString();
            }
            else
            {
                qDebug() << "library loaded";
            }

            typedef bool (*RegisterFormFunction)(Factory *);
            RegisterFormFunction rff = (RegisterFormFunction)library.resolve("registerForm");
            if (!rff || !rff(factory)) {
                qDebug() << "Could not show widget from the loaded library";
            }
    }

    ui->form_select->insertItems(0, factory->getFormList());
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    Form *f = factory->createForm(ui->form_select->currentText(), ui->data_input->text());
    if(f)
    {
        formen->append(f);
    }
    update();
}

void MainWindow::on_form_select_currentTextChanged(const QString &arg1)
{
    ui->help_text->setText(factory->getHelpText(arg1));
}

void MainWindow::paintEvent(QPaintEvent *event)
{
    QMainWindow::paintEvent(event);
    QPainter painter(this);
    QListIterator<Form*> iter(*formen);
    while( iter.hasNext() )
    {
        iter.next()->draw(painter);
    }
}
