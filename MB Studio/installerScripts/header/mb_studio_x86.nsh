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
  File "universal\common\J.SYS.ico"
  
  File "x86\common\MB_Decompiler.exe"
  File "universal\common\MB_Decompiler -CONSOLE.lnk"
;  File "x86\common\MB_Decompiler_GUI.exe"
  File "x86\common\MB_Decompiler_Library.dll"
  File "x86\common\skillhunter.dll"
  File "x86\common\Warband - Translator.exe"
  
  File "x86\common\importantLib.dll"
  
  File "x86\common\brfManager.dll"
  File "x86\common\openBrf.dll"
  
  File "universal\common\qt.conf"
  File "universal\common\reference.brf"
  File "universal\common\customPreviewShaders.xml"
  File "universal\common\carry_positions.txt"
  
  File /r "x86\imageformats"
  File /r "x86\platforms"
  
  File "x86\common\Qt5OpenGL.dll"
  File "x86\common\Qt5Widgets.dll"
  File "x86\common\Qt5Gui.dll"
  File "x86\common\Qt5Xml.dll"
  File "x86\common\Qt5Core.dll"
  
  File /r "universal\files"
  
  CreateDirectory "$INSTDIR\Python"
  
  StrCpy $2 ""
  
!macroend

!define InstallPython32Bit "!insertmacro InstallPython32Bit"
!macro InstallPython32Bit
;  File "x86\python\python-2.7.13.msi"

  CreateDirectory "$PLUGINSDIR\python"
  inetc::get "https://www.dropbox.com/s/x6fznmxh99b1mgn/test.txt?dl=1" "$PLUGINSDIR\python\python-2.7.13.msi"
  Pop $0 ;Return value from download - OK is good!
  
; Executes MSI Installer for Python
  ExecWait '"$SYSDIR\msiexec" /i "python-2.7.13.msi" /passive /norestart ADDLOCAL=ALL TARGETDIR="$INSTDIR\Python"'
  
;  StrCpy $4 "Python 2.7.13" ; check if 32 Bit name is correct
  StrCpy $4 "$INSTDIR\"
 
;  Delete "python-2.7.13.msi"
!macroend

;--------------------------------

!endif # !___MB_STUDIO_x86__NSH___