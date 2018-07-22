@echo off
Call F:\NSIS\makensis.exe mb_studio_installer_universal.nsi
Call "Compile x64.bat"
Call "Compile x86.bat"
REM pause