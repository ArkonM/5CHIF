QT -= gui

TEMPLATE = lib
DEFINES += DYNAMIC_LIB_TEST_LIBRARY

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    dynamic_lib_test.cpp

HEADERS += \
    Dynamic_Lib_Test_global.h \
    dynamic_lib_test.h

# Default rules for deployment.
unix {
    target.path = /usr/lib
}
!isEmpty(target.path): INSTALLS += target
