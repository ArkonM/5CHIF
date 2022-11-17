QT -= gui

TEMPLATE = lib
DEFINES += \
    TINYXMLDYNAMICLIB_LIBRARY \
    TINYXML2_EXPORT

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    tinyxml2.cpp \
    tinyxmldynamiclib.cpp

HEADERS += \
    TinyXMLDynamicLib_global.h \
    tinyxml2.h \
    tinyxmldynamiclib.h

# Default rules for deployment.
unix {
    target.path = /usr/lib
}
!isEmpty(target.path): INSTALLS += target
