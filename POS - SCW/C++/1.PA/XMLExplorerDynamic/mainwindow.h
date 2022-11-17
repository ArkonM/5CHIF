#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTreeWidgetItem>

#include "tinyxml2.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:

    void on_pushButton_openFile_clicked();

    void on_treeWidget_itemSelectionChanged();

    void on_pushButtonAttributes_clicked();

    void on_pushButtonSaveFile_clicked();

private:
    Ui::MainWindow *ui;
    void addChildren(tinyxml2::XMLElement *element, QTreeWidgetItem *item);
};
#endif // MAINWINDOW_H
