#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

    void addSymbol(QString text);
    void addOperator(QString text);
    int checkBrackets();
    bool checkInput();
    void setComma();

private slots:
    void on_B1_clicked();

    void on_B2_clicked();

    void on_B3_clicked();

    void on_B4_clicked();

    void on_B5_clicked();

    void on_B6_clicked();

    void on_B7_clicked();

    void on_B8_clicked();

    void on_B9_clicked();

    void on_B0_clicked();

    void on_BClear_clicked();

    void on_BDelete_clicked();

    void on_BPlus_clicked();

    void on_BMinus_clicked();

    void on_BMulti_clicked();

    void on_BDivide_clicked();

    void on_BPosNeg_clicked();

    void on_BComma_clicked();

    void on_BResult_clicked();

    void on_BLeftBracket_clicked();

    void on_BRightBracket_clicked();

private:
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
