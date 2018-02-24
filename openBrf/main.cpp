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
static byte windowShownMode = 69;

static void MainWindowNotFound()
{
	MessageBoxA(NULL, "CURWINDOW_NOT_FOUND", "ERROR", MB_ICONERROR);
}

static bool CurWindowIsShown()
{
	return (curWindow);
}

static bool CurAppIsNotNull()
{
	return (curApp);
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV IsCurHWndShown()
{
	bool b = false;
	if (windowShownMode == 0x64) // 100
		b = true;
	return b;
}

DLL_EXPORT_VOID CloseApp()
{
	curApp->quit();
	windowShownMode = (byte)0;
}

DLL_EXPORT INT_PTR DLL_EXPORT_DEF_CALLCONV GetCurWindowPtr()
{
	return (INT_PTR)curWindow->winId();
}

DLL_EXPORT_VOID SetModPath(char* modPath)
{
	if (CurWindowIsShown())
		curWindow->setModPathExternal(modPath);
	else
		MainWindowNotFound();
}

DLL_EXPORT_VOID SelectIndexOfKind(int kind, int i)
{
	if (CurWindowIsShown())
		curWindow->selectTypeAndIndex(kind, i);
	else
		MainWindowNotFound();
}

DLL_EXPORT_VOID SelectCurKindMany(int startIndex, int endIndex)
{
	if (CurWindowIsShown())
		curWindow->selectCurManyIndices(startIndex, endIndex);
	else
		MainWindowNotFound();
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV SelectItemByNameAndKind(char* name, int kind = 0)
{
	bool found = false;
	if (CurWindowIsShown())
		found = curWindow->searchIniExplicit(QString(name), kind);
	else
		MainWindowNotFound();
	return found;
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
	else
		MainWindowNotFound();
	return found;
}

DLL_EXPORT_VOID AddMeshToXViewModel(char* meshName)
{
	bool b = SelectItemByNameAndKind(meshName);
	if (b) {
		curWindow->addLastSelectedToXViewMesh();
		//SelectItemByNameAndKind(meshName);
	}
	else
		MainWindowNotFound();
}

DLL_EXPORT bool DLL_EXPORT_DEF_CALLCONV RemoveMeshFromXViewModel(char* meshName)
{
	//add skin name if needed here
	bool b = SelectItemByNameAndKind(meshName);
	if (b) {
		curWindow->removeLastSelectedFromXViewMesh();
		return true;
	}
	return false;
}

DLL_EXPORT int DLL_EXPORT_DEF_CALLCONV StartExternal(int argc, char* argv[])
{
	bool debugMode = false;
	if (argc == 1)
		if (argv[0] == "--debug")
			debugMode = !debugMode; // true

	windowShownMode = 0;

	QString nextTranslator;
	QApplication app(argc, argv);

	curApp = &app;

	QStringList arguments = QCoreApplication::arguments();

	app.setApplicationVersion(applVersion);
	app.setApplicationName("OpenBrf");
	app.setOrganizationName("Marco Tarini");
	app.setOrganizationDomain("Marco Tarini");

	bool changeModule = false;

	//if (debugMode)
	//	MessageBoxA(NULL, "START!", "INFO", 0); //WORKING

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
		{
			if (arguments[2] == "-mod")
				changeModule = true;
		}
	}

	//if (debugMode)
	//	MessageBoxA(NULL, "MAIN() - BEFORE THE LOOP", "INFO", 0); //LAST WORKING ONE SO FAR

	while (1)
	{
		QTranslator translator;
		QTranslator qtTranslator;

		//if (debugMode)
		//	MessageBoxA(NULL, "MAIN() - TEST 1", "INFO", 0); //NOT CLEAR

		if (nextTranslator.isEmpty()) {
			QString loc;
			//if (debugMode)
			//	MessageBoxA(NULL, "MAIN() - TEST 1.1", "INFO", 0); //NOT CLEAR
			switch (MainWindow::getLanguageOption()) {
				default: loc = QLocale::system().name(); break;
				case 1: loc = QString("en"); break;
				case 2: loc = QString("zh_CN"); break;
				case 3: loc = QString("es"); break;
				case 4: loc = QString("de"); break;
			}
			//if (debugMode)
			//	MessageBoxA(NULL, "MAIN() - TEST 1.2", "INFO", 0);
			translator.load(QString(":/translations/openbrf_%1.qm").arg(loc));
			qtTranslator.load(QString(":/translations/qt_%1.qm").arg(loc));
			//if (debugMode)
			//	MessageBoxA(NULL, "MAIN() - TEST 1.3", "INFO", 0); //NOT CLEAR
		}
		else
			translator.load(nextTranslator);

		//if (debugMode)
		//	MessageBoxA(NULL, "MAIN() - TEST 2", "INFO", 0); //NOT CLEAR

		app.installTranslator(&translator);
		app.installTranslator(&qtTranslator);

		//if (debugMode)
		//	MessageBoxA(NULL, "MAIN() - TEST 3", "INFO", 0); //NOT CLEAR

		MainWindow w;

		curWindow = &w;
		
		windowShownMode = 1;

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow initialized!", "INFO", 0); //NOT WORKING

		w.setUseAlphaCommands(useAlphaC);

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow settings changed!", "INFO", 0); //NOT WORKING

		w.show();

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - MainWindow shown!", "INFO", 0); //NOT WORKING

		if (changeModule)
			w.setModPathExternal((char*)arguments[3].toStdString().c_str());

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - Modpath set!", "INFO", 0); //NOT WORKING

		if (arguments.size() > 1) w.loadFile(arguments[1]); arguments.clear();

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - FINAL TEST!", "INFO", 0); //NOT WORKING

		windowShownMode = 0x64; // 100

		if (app.exec() == 101) {
			nextTranslator = w.getNextTranslatorFilename();
			continue; // just changed language! another run
		}

		windowShownMode = 0;

		if (debugMode)
			MessageBoxA(NULL, "MAIN() - APPLICATION CLOSED!", "INFO", 0); //NOT WORKING

		break;
	}
	return 0;
}

int main(int argc, char* argv[])
{
	Q_INIT_RESOURCE(resource);
	return StartExternal(argc, argv);
}

