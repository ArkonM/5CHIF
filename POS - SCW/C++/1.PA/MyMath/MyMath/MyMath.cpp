// MyMath.cpp : Hiermit werden die Funktionen für die statische Bibliothek definiert.
//

#include "pch.h"
#include "framework.h"
#include "exprtk.hpp"

// TODO: Dies ist ein Beispiel für eine Bibliotheksfunktion.
double calculate(std::string expression_string)
{
    exprtk::expression<double> expression;
    exprtk::parser<double> parser;
    parser.compile(expression_string, expression);
    return expression.value();
}
