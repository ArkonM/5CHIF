// MyMath.cpp : Defines the functions for the static library.
//

#include "pch.h"
#include "framework.h"
#include "exprtk.hpp"

// TODO: This is an example of a library function
double calculate(std::string expression_string)
{
    exprtk::expression<double> expression;
    exprtk::parser<double> parser;
    parser.compile(expression_string, expression);
    return expression.value();
}
