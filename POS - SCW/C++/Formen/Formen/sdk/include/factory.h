#ifndef FACTORY_H
#define FACTORY_H

#include <QObject>
#include <QMap>
#include <QStringList>
#include "form.h"

class FORMEN_LIBRARY_SDK Factory : public QObject
{
    Q_OBJECT

private:
    QMap<QString, Form*> *forms;
    QStringList *formNameList;
public:
    Form* createForm(const QString &form, const QString &data);
    const QString &getHelpText(const QString &form);
    QStringList &getFormList();
    Factory(QObject* parent = 0);
    ~Factory();
    void registerForm(Form *form);
};

#endif // FACTORY_H
