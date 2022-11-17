#ifndef TINYXMLDYNAMICLIB_GLOBAL_H
#define TINYXMLDYNAMICLIB_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(TINYXMLDYNAMICLIB_LIBRARY)
#  define TINYXMLDYNAMICLIB_EXPORT Q_DECL_EXPORT
#else
#  define TINYXMLDYNAMICLIB_EXPORT Q_DECL_IMPORT
#endif

#endif // TINYXMLDYNAMICLIB_GLOBAL_H