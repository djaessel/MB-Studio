;
; MB Studio Universal Installer Script
; by Johandros
;

!include x64.nsh
!include "header\mb_studio_universal.nsh"
!include "header\mb_studio_x86.nsh"
!include "header\mb_studio_x64.nsh"

;-------------------------------- 
;General

${UniversalProperties}

OutFile "universal\MB Studio - Installer (universal).exe"

;-------------------------------- 
;Installer Sections     
Section "Install" installInfo

  ${If} ${RunningX64}
    ${InstallDefault64BitFiles}
  ${Else}
    ${InstallDefault32BitFiles}
  ${EndIf}
  
  ${CreateApplicationLinks}
  
  SetRegView 32
  ${If} ${RunningX64}
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "EstimatedSize" "${SIZE_OF_FILE_64}"
  ${Else}
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "EstimatedSize" "${SIZE_OF_FILE_32}"
  ${EndIf}
  
  ${If} ${RunningX64}
  	${InstallCpp2017_64Bit}
    ${InstallPython64Bit}
  ${Else}
  	${InstallCpp2017_32Bit}
    ${InstallPython32Bit}
  ${EndIf}
  
  ${WriteMBUninstaller}
  
SectionEnd

Section "Uninstall" uninstall
  ${UninstallAll}
SectionEnd

;--------------------------------
;Functions

Function .onInit
  ${If} ${RunningX64}
    SectionSetSize ${installInfo} ${SIZE_OF_FILE_64_INT}
  ${Else}
    SectionSetSize ${installInfo} ${SIZE_OF_FILE_32_INT}
  ${EndIf}
FunctionEnd

;--------------------------------
;eof