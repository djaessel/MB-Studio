/* OpenBRF -- by marco tarini. Provided under GNU General Public License */

#include <QApplication>
#include "mainwindow.h"

using namespace std;

static void showUsage(){
  system(
    "echo off&"
    "echo off&"
    "echo.usages: &"
    "echo.&"
    "echo.  OpenBRF &"
    "echo.     ...starts GUI&"
    "echo.&"
    "echo.  OpenBRF ^<file.brf^> &"
    "echo.     ...starts GUI, opens file.brf&"
    "echo.&"
    "echo.  OpenBRF --dump ^<module_path^> ^<file.txt^>&"
    "echo.     ...shell only, dumps objects names into file.txt&"
    "echo.&"
    "echo.&"
    "pause"
  );
}

extern const char* applVersion;
static MainWindow* curWindow;
static QApplication* curApp;
static int windowShownMode = -1;//startValue

static void MainWindowNotFound()
{
	MessageBoxA(NULL, "CURWINDOW_NOT_FOUND", "ERROR", MB_ICONERROR);
}

static bool CurWindowIsShown(/*bool showError = true*/)
{
	bool b = (curWindow);
	if (!b/* && showError*/)
		MainWindowNotFound();
	return b;
}

static bool CurAppIsNotNull()
{
	return (curApp);
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV IsCurHWndShown()
{
	bool bb = false;
	if (windowShownMode > 0) bb = !bb;
	return bb;
}

DLL_EXPORT_VOID CloseApp()
{
	curApp->quit();
	windowShownMode = 0x00;
}

DLL_EXPORT INT_PTR DLL_EXPORT_DEF_CALLCONV GetCurWindowPtr()
{
	return (INT_PTR)curWindow->winId();
}

DLL_EXPORT_VOID SetModPath(char* modPath)
{
	if (CurWindowIsShown(/*false*/))
		curWindow->setModPathExternal(modPath);
}

DLL_EXPORT_VOID SelectIndexOfKind(int kind, int i)
{
	if (CurWindowIsShown())
		curWindow->selectTypeAndIndex(kind, i);
}

DLL_EXPORT_VOID SelectCurKindMany(int startIndex, int endIndex)
{
	if (CurWindowIsShown())
		curWindow->selectCurManyIndices(startIndex, endIndex);
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV SelectItemByNameAndKind(char* name, int kind = 0)
{
	if (CurWindowIsShown())
		return curWindow->searchIniExplicit(QString(name), kind);
	return false;
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV SelectItemByNameAndKindFromCurFile(char* name, int kind = 0)
{
	bool found = false;
	if (CurWindowIsShown())
	{
		typedef std::vector<char*> StringArray;
		StringArray names = curWindow->getMeshNames();
		string sName = string(name);
		for (size_t i = 0; i < names.size(); i++)
		{
			if (string(names[i]) == sName)
			{
				curWindow->selectTypeAndIndex(kind, (int)i);
				found = !found;
				i = names.size();
			}
		}
	}
	return found;
}

DLL_EXPORT/*_VOID*//**/ bool DLL_EXPORT_DEF_CALLCONV/**/ AddMeshToXViewModel(char* meshName, int bone = 0, int skeleton = 0, int carryPosition = -1/*, bool isAtOrigin = true*/)
{
	bool retur = SelectItemByNameAndKind(meshName);
	if (retur) {//includes CurWindowIsShown()
		curWindow->addLastSelectedToXViewMesh(bone, skeleton, carryPosition/*, isAtOrigin*/);
	}
	return retur;
	//if (CurWindowIsShown()) {
	//	curWindow->addMeshByNameToXViewMesh(meshName, bone, skeleton, carryPosition/*, isAtOrigin*/);
	//	return true;
	//}
	//return false;
}

DLL_EXPORT_VOID ShowTroop3DPreview()
{
	if (CurWindowIsShown())
		curWindow->showTroop3DPreview();
}

DLL_EXPORT_VOID RemoveMeshFromXViewModel(char* meshName)
{
	//add skin name if needed here
	//if (SelectItemByNameAndKind(meshName)) {//includes CurWindowIsShown()
	//	curWindow->removeLastSelectedFromXViewMesh();
	//	return true;//maybe return remove method later (in case of error)
	//}
	//return false;
	if (CurWindowIsShown())
		curWindow->removeMeshByNameFromXViewMesh(meshName);
}

DLL_EXPORT_VOID ClearTroop3DPreview()
{
	if (CurWindowIsShown())
		curWindow->clearTroop3DPreview();
}

/**
* Main Method - For External Usage
*/
DLL_EXPORT int DLL_EXPORT_DEF_CALLCONV StartExternal(int argc, char* argv[])
{
#ifdef DEBUG_MODE
	bool debugMode = false;
	if (argc == 1)
		if (argv[0] == "--debug")
			debugMode = !debugMode; // true
#endif // DEBUG_MODE

	windowShownMode = 0x00;

	QString nextTranslator;
	QApplication app(argc, argv);

	curApp = &app;

	QStringList arguments = QCoreApplication::arguments();

	app.setApplicationVersion(applVersion);
	app.setApplicationName("OpenBrf");
	app.setOrganizationName("Marco Tarini");
	app.setOrganizationDomain("Marco Tarini");

	bool changeModule = false;
	bool useAlphaC = false;
	if (arguments.size() > 1)
	{
		if ((arguments[1].startsWith("-")))
		{
			if ((arguments[1] == "--dump") && (arguments.size() == 4))
			{
				switch (MainWindow().loadModAndDump(arguments[2], arguments[3]))
				{
					case -1: system("echo OpenBRF: invalid module folder & pause"); break;
					case -2: system("echo OpenBRF: error scanning brf data or ini file & pause"); break;
					case -3: system("echo OpenBRF: error writing output file & pause"); break;
					default: return 0;
				}
				return -1;
			}
			else if ((arguments[1] == "--useAlphaCommands") && (arguments.size() == 2)) {
				useAlphaC = true;
				arguments.clear();
			}
			else
			{
				showUsage();
				return -1;
			}
		}
		else if (arguments.size() >= 4)
			if (arguments[2] == "-mod")
				changeModule = true;
	}

	while (1)
	{
		QTranslator translator;
		QTranslator qtTranslator;

		if (nextTranslator.isEmpty()) {
			QString loc;
			switch (MainWindow::getLanguageOption()) {
				default: loc = QLocale::system().name(); break;
				case 1: loc = QString("en"); break;
				case 2: loc = QString("zh_CN"); break;
				case 3: loc = QString("es"); break;
				case 4: loc = QString("de"); break;
			}
			translator.load(QString(":/translations/openbrf_%1.qm").arg(loc));
			qtTranslator.load(QString(":/translations/qt_%1.qm").arg(loc));
		}
		else
			translator.load(nextTranslator);

		app.installTranslator(&translator);
		app.installTranslator(&qtTranslator);

		MainWindow w;
		curWindow = &w;
		
		//windowShownMode = 0x01;

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow initialized!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		w.setUseAlphaCommands(useAlphaC);

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow settings changed!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		w.show();

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow shown!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		if (changeModule)
			w.setModPathExternal((char*)arguments[3].toStdString().c_str());

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - Modpath set!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		if (arguments.size() > 1) w.loadFile(arguments[1]); arguments.clear();

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - FINAL TEST!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		windowShownMode = 0x64;//100

		if (app.exec() == 101) {
			nextTranslator = w.getNextTranslatorFilename();
			continue; // just changed language! another run
		}

		windowShownMode = 0x00;

#ifdef DEBUG_MODE
		if (debugMode)
			MessageBoxA(NULL, "MAIN() - APPLICATION CLOSED!", "INFO", 0); //NOT WORKING
#endif // DEBUG_MODE

		break;
	}
	return 0;
}

/**
* Main Method
*/
int main(int argc, char* argv[])
{
	Q_INIT_RESOURCE(resource);
	return StartExternal(argc, argv);
}

