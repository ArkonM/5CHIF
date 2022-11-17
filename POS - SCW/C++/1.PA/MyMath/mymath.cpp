#include "mymath.h"
#include "exprtk.hpp"

double calculate(std::string expression_string) {

    exprtk::expression<double> expression;
    exprtk::parser<double> parser;
    parser.compile(expression_string, expression);
    return expression.value();

}
