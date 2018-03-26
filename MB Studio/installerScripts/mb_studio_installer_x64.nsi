;
; MB Studio x64 Installer Script
; by Johandros
;

!include "header\mb_studio_universal.nsh"
!include "header\mb_studio_x64.nsh"

;-------------------------------- 
;General

${UniversalProperties}

OutFile "x64\MB Studio - Installer (x64).exe"
 
;-------------------------------- 
;Installer Sections     
Section "Install" installInfo
  
  ${InstallDefault64BitFiles}
  
  ${CreateApplicationLinks}
  
;write uninstall information to the registry
  SetRegView 32
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "EstimatedSize" "${SIZE_OF_FILE_64}"
  
  ${InstallCpp2017_64Bit}
  
  ${InstallPython64Bit}
  
  ${WriteMBUninstaller}
  
SectionEnd

Section "Uninstall" uninstall
  ${UninstallAll}
SectionEnd

;--------------------------------    
;Functions

Function .onInit
  SectionSetSize ${installInfo} ${SIZE_OF_FILE_64_INT}
FunctionEnd

;--------------------------------
;eof