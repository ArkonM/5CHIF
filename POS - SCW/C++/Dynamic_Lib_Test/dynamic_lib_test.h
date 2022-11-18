#ifndef DYNAMIC_LIB_TEST_H
#define DYNAMIC_LIB_TEST_H

#include "Dynamic_Lib_Test_global.h"
#include <string>

class DYNAMIC_LIB_TEST_EXPORT Dynamic_Lib_Test
{
public:
    Dynamic_Lib_Test();
};

std::string String_Plus(std::string str1, std::string str2);

#endif // DYNAMIC_LIB_TEST_H
