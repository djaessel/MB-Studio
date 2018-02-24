// - - - - - - -
// From here --> https://stackoverflow.com/questions/12105420/c-sharp-dllimport-parameters-shift#answer-12105604
// - - - - - - -

/*#ifndef _EXPORTING
#define _EXPORTING .
#endif

#ifdef _EXPORTING*/
#ifndef CLASS_DECLSPEC
#define CLASS_DECLSPEC  __declspec(dllexport)
/*#else
#define CLASS_DECLSPEC  __declspec(dllimport)*/ 
#endif
//#endif

#ifndef DLL_EXPORT
#define DLL_EXPORT EXTERN_C CLASS_DECLSPEC
#endif

#ifndef DLL_EXPORT_DEF_CALLCONV
#define DLL_EXPORT_DEF_CALLCONV __stdcall
#endif // !DLL_EXPORT_CALLINGCONVENTION

#ifdef DLL_EXPORT
#define DLL_EXPORT_VOID DLL_EXPORT void DLL_EXPORT_DEF_CALLCONV
#endif // !DLL_EXPORT

#ifndef STRING
#define STRING char*
#endif
