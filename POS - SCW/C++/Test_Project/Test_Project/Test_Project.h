#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_Test_Project.h"

class Test_Project : public QMainWindow
{
    Q_OBJECT

public:
    Test_Project(QWidget *parent = nullptr);
    ~Test_Project();

private:
    Ui::Test_ProjectClass ui;
};
