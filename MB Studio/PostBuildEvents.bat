IF "$(ConfigurationName)" == "Debug" goto :Debug

:Release
IF "$(PlatformName)" == "x64" goto :Release_x64

:Release_x86
IF NOT EXIST "$(TargetDir)\Qt5OpenGL.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5OpenGL.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgets.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Widgets.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Gui.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Gui.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xml.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Xml.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Core.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Core.dll" "$(TargetDir)"
goto :AllAndExit

:Release_x64
IF NOT EXIST "$(TargetDir)\Qt5OpenGL.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5OpenGL.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgets.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Widgets.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Gui.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Gui.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xml.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Xml.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Core.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Core.dll" "$(TargetDir)"
goto :AllAndExit

:Debug
IF "$(PlatformName)" == "x64" goto :Debug_x64

:Debug_x86
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5OpenGLd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgetsd.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Widgetsd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Guid.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Guid.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Xmld.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Cored.dll" COPY "F:\Qt\5.10.1\msvc2015\bin\Qt5Cored.dll" "$(TargetDir)"
goto  :AllAndExit

:Debug_x64
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5OpenGLd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgetsd.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Widgetsd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Guid.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Guid.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xmld.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Xmld.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Cored.dll" COPY "F:\Qt\5.10.1\msvc2015_64\bin\Qt5Cored.dll" "$(TargetDir)"

:AllAndExit
COPY "$(SolutionDir)\openBrf\$(PlatformName)\$(ConfigurationName)\openBrf.dll" "$(TargetDir)"
:Exit