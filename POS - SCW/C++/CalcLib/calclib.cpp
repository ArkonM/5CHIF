#include "calclib.h"
#include "exprtk.hpp"

CalcLib::CalcLib()
{
}

double calculate(QString msg){
    exprtk::expression<double> expression;
    exprtk::parser<double> parser;
    parser.compile(msg.toStdString(), expression);
    return expression.value();
}
