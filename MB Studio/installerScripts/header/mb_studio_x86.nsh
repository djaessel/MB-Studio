; -------------------------------
;     mb_studio_x86.nsh
; -------------------------------

;!include LogicLib.nsh

;--------------------------------

!ifndef ___MB_STUDIO_x86__NSH___
!define ___MB_STUDIO_x86__NSH___

;--------------------------------
;Definitions

!define SIZE_OF_FILE_32 "0x000‭16D7D‬"
!define SIZE_OF_FILE_32_INT 93565 ;KB

;--------------------------------
;Macros

!define InstallDefault32BitFiles "!insertmacro InstallDefault32BitFiles"
!macro InstallDefault32BitFiles
  
;-------------------------------- 
;Installer Sections     

;Add files
  SetOutPath "$INSTDIR"
  
  File "x86\common\${PRODUCT_NAME}.exe"
  File "..\universal\common\J.SYS.ico"
  
  File "x86\common\MB_Decompiler.exe"
  File "..\universal\common\MB_Decompiler -CONSOLE.lnk"
;  File "x86\common\MB_Decompiler_GUI.exe"
  
  File "..\updater\stable\x86\MB Studio Updater.exe"
  
  File "x86\common\MB_Decompiler_Library.dll"
  File "x86\common\Warband - Translator.exe"
  
  File "x86\common\importantLib.dll"
  
  File "x86\common\brfManager.dll"
  File "x86\common\openBrf.dll"
  
  File "..\universal\common\qt.conf"
  File "..\universal\common\reference.brf"
  File "..\universal\common\customPreviewShaders.xml"
  File "..\universal\common\carry_positions.txt"
  
  File /r "x86\imageformats"
  File /r "x86\platforms"
  
  File "x86\common\Qt5OpenGL.dll"
  File "x86\common\Qt5Widgets.dll"
  File "x86\common\Qt5Gui.dll"
  File "x86\common\Qt5Xml.dll"
  File "x86\common\Qt5Core.dll"
  
  File /r "universal\files"
  File /r "..\universal\files"
  
  CreateDirectory "$INSTDIR\Python"
  
  StrCpy $2 ""
!macroend

!define InstallPython32Bit "!insertmacro InstallPython32Bit"
!macro InstallPython32Bit
  CreateDirectory "$PLUGINSDIR\python"
  inetc::get "https://www.python.org/ftp/python/2.7.13/python-2.7.13.msi" "$PLUGINSDIR\python\python-2.7.13.msi"
  Pop $0 ;Return value from download - OK is good!
; Executes MSI Installer for Python
  ExecWait '"$SYSDIR\msiexec" /i "$PLUGINSDIR\python\python-2.7.13.msi" /passive /norestart ADDLOCAL=ALL TARGETDIR="$INSTDIR\Python"'
  StrCpy $4 "$PLUGINSDIR\python\"
!macroend

!define InstallCpp2017_32Bit "!insertmacro InstallCpp2017_32Bit"
!macro InstallCpp2017_32Bit
  CreateDirectory "$PLUGINSDIR\vc_redist"
  inetc::get "https://download.visualstudio.microsoft.com/download/pr/11687613/88b50ce70017bf10f2d56d60fcba6ab1/VC_redist.x86.exe" "$PLUGINSDIR\vc_redist\vc_redist.x86.exe"
  Pop $0 ;Return value from download - OK is good!
; Executes Installer for C++ Package
  ExecWait '"$PLUGINSDIR\vc_redist\vc_redist.x86.exe" /q /norestart'
!macroend

;--------------------------------

!endif # !___MB_STUDIO_x86__NSH___