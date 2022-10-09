#include "calculatorlib.h"
#include "exprtk.hpp"
#include <QString>

CalculatorLib::CalculatorLib()
{
}

void CalculatorLib::calculate(QString expression_string){
    exprtk::expression<double> expression;
    exprtk::parser<double> parser;
    //parser.compile(expression_string.toStdString(), expression);
    //return expression.value();
}
