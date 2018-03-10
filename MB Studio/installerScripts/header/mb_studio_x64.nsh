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
;  File "x64\python\python-2.7.13.amd64.msi"

  CreateDirectory "$PLUGINSDIR\python"
  inetc::get "https://www.dropbox.com/s/x6fznmxh99b1mgn/test.txt?dl=1" "$PLUGINSDIR\python\python-2.7.13.amd64.msi"
  Pop $0 ;Return value from download - OK is good!

; Executes MSI Installer for Python
  ExecWait '"$SYSDIR\msiexec" /i "python-2.7.13.amd64.msi" /passive /norestart ADDLOCAL=ALL TARGETDIR="$INSTDIR\Python"'
  
;  StrCpy $4 "Python 2.7.13 (64-bit)"
;  ${GetUninstallStringByAppID} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall" "DisplayName" $4 64
  StrCpy $4 "$INSTDIR\"
  ${GetUninstallStringByAppID} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall" "InstallSource" $4 64
  
;  Delete "python-2.7.13.amd64.msi"
!macroend

;--------------------------------

!endif # !___MB_STUDIO_x64__NSH___