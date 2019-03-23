; -------------------------------
;     mb_studio_universal.nsh
; -------------------------------

!include LogicLib.nsh

!ifndef ___MB_STUDIO_UNIVERSAL__NSH___
!define ___MB_STUDIO_UNIVERSAL__NSH___

;-------------------------------
;Definitions

!define PRODUCT_NAME "MB Studio"
!define PRODUCT_VERSION "3.2.1"
!define MUI_BRANDINGTEXT "MB Studio"
!define PUBLISHER_NAME "J.SYS"

!define MUI_ICON "${PRODUCT_NAME}.ico"
!define MUI_UNICON "${PRODUCT_NAME}.ico"
!define MUI_SPECIALBITMAP "${PRODUCT_NAME}.ico"

;--------------------------------
;Functions

; Trim
;   Removes leading & trailing whitespace from a string
; Usage:
;   Push 
;   Call Trim
;   Pop 
Function Trim
	Exch $R1 ; Original string
	Push $R2
 
Loop:
	StrCpy $R2 "$R1" 1
	StrCmp "$R2" " " TrimLeft
	StrCmp "$R2" "$\r" TrimLeft
	StrCmp "$R2" "$\n" TrimLeft
	StrCmp "$R2" "$\t" TrimLeft
	GoTo Loop2
TrimLeft:	
	StrCpy $R1 "$R1" "" 1
	Goto Loop
 
Loop2:
	StrCpy $R2 "$R1" 1 -1
	StrCmp "$R2" " " TrimRight
	StrCmp "$R2" "$\r" TrimRight
	StrCmp "$R2" "$\n" TrimRight
	StrCmp "$R2" "$\t" TrimRight
	GoTo Done
TrimRight:	
	StrCpy $R1 "$R1" -1
	Goto Loop2
 
Done:
	Pop $R2
	Exch $R1
FunctionEnd

;!define UninstallPython "Call un.UninstallPython"
;Function un.UninstallPython
;
;Remove Python Completly
;  ClearErrors
;  SetRegView 32
;  ReadRegStr $R0 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "PythonUninstall"
;  
;;  ${IF} ${Errors}
;;  ${ORIF} $R0 == ""
;  ${IF} $R0 == ""
;  ${ORIF} $R0 == "NOT_FOUND"
;    DetailPrint "ERROR: GUID_NOT_FOUND_IN_REGISTRY - Python"
;  ${ELSE}
;    ExecWait '"$SYSDIR\msiexec" /x "$R0" /passive /norestart'
;  ${ENDIF}
;  
;  DetailPrint "$R0"
;  
;  ClearErrors
;
;FunctionEnd

!define UninstallDefault "Call un.UninstallDefault"
Function un.UninstallDefault

;Delete Files 
  RMDir /r "$INSTDIR\*.*"    
 
;Remove the installation directory
  RMDir "$INSTDIR"
 
;Delete Start Menu Shortcuts
  Delete "$DESKTOP\${PRODUCT_NAME}.lnk"
  Delete "$SMPROGRAMS\${PRODUCT_NAME}\*.*"
  RmDir  "$SMPROGRAMS\${PRODUCT_NAME}"
 
;Delete Uninstaller And Unistall Registry Entries
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\${PRODUCT_NAME}"
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"

FunctionEnd

;--------------------------------
;Macros

!define GetUninstallStringByAppID "!insertmacro GetUninstallStringByAppID"
!macro GetUninstallStringByAppID ROOT MAIN_KEY VALUE_NAME VALUE BIT_VERSION

!define Index 'Line${__LINE__}'

DetailPrint "Suche ${VALUE} in Registry..."
StrCpy $0 0

"${Index}-Loop:"
  SetRegView ${BIT_VERSION}
  EnumRegKey $1 ${ROOT} "${MAIN_KEY}" $0
  StrCmp $1 "" "${Index}-FailEnd"
  IntOp $0 $0 + 1
  
  ClearErrors
  SetRegView ${BIT_VERSION}
  ReadRegStr $2 ${ROOT} "${MAIN_KEY}\$1" "${VALUE_NAME}"
  ${IF} ${Errors}
    DetailPrint "$1 -> ${VALUE_NAME}: NULL"
  ${ELSE}
    Push $2
    Call Trim
    Pop $3
    DetailPrint "$1 -> ${VALUE_NAME}: $3"
    ${IF} $3 == ${VALUE}
        DetailPrint "Found ${VALUE} in $1 for ${VALUE_NAME} -> Value: $3!"
		goto "${Index}-SuccessEnd"
	${ENDIF}
  ${ENDIF}
  goto "${Index}-Loop"

"${Index}-SuccessEnd:"
  DetailPrint "Suche nach ${VALUE} erfolgreich beendet!"
  StrCpy $2 "${MAIN_KEY}\$1"
  goto "${Index}-End"

"${Index}-FailEnd:"
  DetailPrint "Suche nach ${VALUE} fehlgeschlagen!"
  StrCpy $2 "NOT_FOUND"
  goto "${Index}-End"

"${Index}-End:"
  !undef Index
  pop $1
  pop $2

!macroend

!define UniversalProperties "!insertmacro UniversalProperties"
!macro UniversalProperties

; MB Studio Universal Installer Script
; by Johandros
;

; -------------------------------
; Start

  Name "MB Studio"
  
  CRCCheck On
  
  !include "${NSISDIR}\Contrib\Modern UI\System.nsh"
 
;---------------------------------
;General
  
  ShowInstDetails "show"
  ShowUninstDetails "show"
  SetCompressor "bzip2";maybe change later!
  
;--------------------------------
;Folder selection page

;  InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
  InstallDir "C:\${PRODUCT_NAME}"
  
  !insertmacro MUI_PAGE_WELCOME
  
;  !insertmacro MUI_PAGE_LICENSE "${NSISDIR}\Docs\Modern UI\License.txt"
;  !insertmacro MUI_PAGE_COMPONENTS

  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES

  !insertmacro MUI_PAGE_FINISH ; enable after testing is finished - disable for testing

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH
 
;--------------------------------
;Language
 
  !insertmacro MUI_LANGUAGE "English"
  !insertmacro MUI_LANGUAGE "German"
  
!macroend

!define CreateApplicationLinks "!insertmacro CreateApplicationLinks"
!macro CreateApplicationLinks

;create desktop shortcut
  CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\${PRODUCT_NAME}.exe" ""
 
;create start-menu items
  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\Uninstall.lnk" "$INSTDIR\Uninstall.exe" "" "$INSTDIR\Uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\${PRODUCT_NAME}.lnk" "$INSTDIR\${PRODUCT_NAME}.exe" "" "$INSTDIR\${PRODUCT_NAME}.exe" 0

;write uninstall information to the registry
  SetRegView 32
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayName" "${PRODUCT_NAME}"
  SetRegView 32
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayIcon" "$INSTDIR\Uninstall.exe"
  SetRegView 32
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayVersion" "${PRODUCT_VERSION}"
  SetRegView 32
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "UninstallString" "$INSTDIR\Uninstall.exe"
  SetRegView 32
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "Publisher" "${PUBLISHER_NAME}"

!macroend

!define WriteMBUninstaller "!insertmacro WriteMBUninstaller"
!macro WriteMBUninstaller

  WriteUninstaller "$INSTDIR\Uninstall.exe"

;write Python uninstall path
;  ClearErrors
;  ${IF} $2 == "" ; if 32Bit System or not found in 64Bit registry part
;    ${GetUninstallStringByAppID} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall" "InstallSource" $4 32
;  ${ENDIF}

;;  ${IF} ${Errors}
;;  ${ORIF} $1 == ""
;  ${IF} $1 == 1
;    StrCpy $1 "NOT_FOUND"
;  ${ENDIF}

;  SetRegView 32
;  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "PythonUninstall" "$1"
;
;  ClearErrors
;  SetRegView 32
;  ReadRegStr $R0 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "PythonUninstall"
 
!macroend

!define UninstallAll "!insertmacro UninstallAll"
!macro UninstallAll

;${UninstallPython}
${UninstallDefault}

;vc_redist.x86.exe /uninstall /q
;vc_redist.x64.exe /uninstall /q

!macroend

;--------------------------------

!endif # !___MB_STUDIO_UNIVERSAL__NSH___