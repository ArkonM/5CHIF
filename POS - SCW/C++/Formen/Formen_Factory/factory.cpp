#include "factory.h"
#include <QObject>

Factory::Factory(QObject* parent):
 QObject(parent)
{
    forms = new QMap<QString, Form*>();
    formNameList = new QStringList();
}

Factory::~Factory()
{
}

void Factory::registerForm(Form *form)
{
    forms->insert(form->getName(), form);
    formNameList = new QStringList(forms->keys());
}

QStringList &Factory::getFormList()
{
    return *formNameList;
}

Form* Factory::createForm(const QString &form, const QString &data)
{
    if(forms->contains(form))
        return forms->find(form).value()->parseForm(data);
    return nullptr;
}

const QString &Factory::getHelpText(const QString &form)
{
    return forms->find(form).value()->getHelp();
}
