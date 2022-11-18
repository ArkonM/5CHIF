#ifndef FORMEN_GLOBAL_H
#define FORMEN_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(FORMEN_LIBRARY)
#  define FORMEN_LIBRARY_SDK Q_DECL_EXPORT
#else
#  define FORMEN_LIBRARY_SDK Q_DECL_IMPORT
#endif

#endif // FORMEN_GLOBAL_H
