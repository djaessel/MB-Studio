; -------------------------------
;     mb_studio_x64.nsh
; -------------------------------

;!include LogicLib.nsh

;--------------------------------

!ifndef ___MB_STUDIO_x64__NSH___
!define ___MB_STUDIO_x64__NSH___

;--------------------------------
;Definitions

!define SIZE_OF_FILE_64 "0x00018A2C"
!define SIZE_OF_FILE_64_INT 100908 ;KB

;--------------------------------
;Macros

!define InstallDefault64BitFiles "!insertmacro InstallDefault64BitFiles"
!macro InstallDefault64BitFiles

;-------------------------------- 
;Installer Sections     

;Add files
  SetOutPath "$INSTDIR"
  
  File "x64\common\${PRODUCT_NAME}.exe"
  File "universal\common\J.SYS.ico"
  
  File "x64\common\MB_Decompiler.exe"
  File "universal\common\MB_Decompiler -CONSOLE.lnk"
;  File "x64\common\MB_Decompiler_GUI.exe"
  File "..\updater\x64\MB Studio Updater.exe"
  File "x64\common\MB_Decompiler_Library.dll"
  File "x64\common\skillhunter.dll"
  File "x64\common\Warband - Translator.exe"
  
  File "x64\common\importantLib.dll"
  
  File "x64\common\brfManager.dll"
  File "x64\common\openBrf.dll"
  
  File "universal\common\qt.conf"
  File "universal\common\reference.brf"
  File "universal\common\customPreviewShaders.xml"
  File "universal\common\carry_positions.txt"
  
  File /r "x64\imageformats"
  File /r "x64\platforms"
  
  File "x64\common\Qt5OpenGL.dll"
  File "x64\common\Qt5Widgets.dll"
  File "x64\common\Qt5Gui.dll"
  File "x64\common\Qt5Xml.dll"
  File "x64\common\Qt5Core.dll"
  
  File /r "universal\files"
  
  CreateDirectory "$INSTDIR\Python"
  
  StrCpy $2 ""
!macroend

!define InstallPython64Bit "!insertmacro InstallPython64Bit"
!macro InstallPython64Bit
  CreateDirectory "$PLUGINSDIR\python"
  inetc::get "https://www.python.org/ftp/python/2.7.13/python-2.7.13.amd64.msi" "$PLUGINSDIR\python\python-2.7.13.amd64.msi"
  Pop $0 ;Return value from download - OK is good!
; Executes MSI Installer for Python
  ExecWait '"$SYSDIR\msiexec" /i "$PLUGINSDIR\python\python-2.7.13.amd64.msi" /passive /norestart ADDLOCAL=ALL TARGETDIR="$INSTDIR\Python"'
  StrCpy $4 "$PLUGINSDIR\python\"
  ${GetUninstallStringByAppID} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall" "InstallSource" $4 64
;returns $1
!macroend

!define InstallCpp2017_64Bit "!insertmacro InstallCpp2017_64Bit"
!macro InstallCpp2017_64Bit
  CreateDirectory "$PLUGINSDIR\vc_redist"
  inetc::get "https://download.visualstudio.microsoft.com/download/pr/11687625/2cd2dba5748dc95950a5c42c2d2d78e4/VC_redist.x64.exe" "$PLUGINSDIR\vc_redist\vc_redist.x64.exe"
  Pop $0 ;Return value from download - OK is good!
; Executes Installer for C++ Package
  ExecWait '"$PLUGINSDIR\vc_redist\vc_redist.x64.exe" /q /norestart'
!macroend

;--------------------------------

!endif # !___MB_STUDIO_x64__NSH___