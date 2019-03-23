;
; MB Studio x86 Installer Script
; by Johandros
;

!include "header\mb_studio_universal.nsh"
!include "header\mb_studio_x86.nsh"

;-------------------------------- 
;General

${UniversalProperties}

OutFile "x86\MB Studio - Installer (x86).exe"

;-------------------------------- 
;Installer Sections     
Section "Install" installInfo
  
  ${InstallDefault32BitFiles}
  
  ${CreateApplicationLinks}
  
;write uninstall information to the registry
  SetRegView 32
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "EstimatedSize" "${SIZE_OF_FILE_32}‬‬‬‬"
  
  ${InstallCpp2017_32Bit}
  
;  ${InstallPython32Bit}
  
  ${WriteMBUninstaller}
  
SectionEnd

Section "Uninstall" uninstall
  ${UninstallAll}
SectionEnd

;--------------------------------    
;Functions

Function .onInit
  SectionSetSize ${installInfo} ${SIZE_OF_FILE_32_INT}
FunctionEnd

;--------------------------------
;eof