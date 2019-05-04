:All
IF NOT EXIST "$(TargetDir)\platforms" MKDIR "$(TargetDir)\platforms"
IF NOT EXIST "$(TargetDir)\imageformats" MKDIR "$(TargetDir)\imageformats"
COPY "$(SolutionDir)\openBrf\$(PlatformName)\$(ConfigurationName)\openBrf.dll" "$(TargetDir)"

IF "$(ConfigurationName)" == "Debug" GOTO :Debug

:Release
IF "$(PlatformName)" == "x64" GOTO :Release_x64

:Release_x86
IF NOT EXIST "$(TargetDir)\Qt5OpenGL.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5OpenGL.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgets.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Widgets.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Gui.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Gui.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xml.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Xml.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Core.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Core.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\platforms\qwindows.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\platforms\qwindows.dll" "$(TargetDir)\platforms"
IF NOT EXIST "$(TargetDir)\imageformats\qicns.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qicns.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qico.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qico.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qsvg.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qsvg.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qwbmp.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qwbmp.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qdds.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qdds.dll" "$(TargetDir)\imageformats"
GOTO :eof

:Release_x64
IF NOT EXIST "$(TargetDir)\Qt5OpenGL.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5OpenGL.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgets.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Widgets.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Gui.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Gui.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xml.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Xml.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Core.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Core.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\platforms\qwindows.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\platforms\qwindows.dll" "$(TargetDir)\platforms"
IF NOT EXIST "$(TargetDir)\imageformats\qicns.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qicns.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qico.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qico.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qsvg.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qsvg.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qwbmp.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qwbmp.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qdds.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qdds.dll" "$(TargetDir)\imageformats"
GOTO :eof

:Debug
IF "$(PlatformName)" == "x64" GOTO :Debug_x64

:Debug_x86
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5OpenGLd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgetsd.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Widgetsd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Guid.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Guid.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Xmld.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Cored.dll" COPY "F:\Qt\5.12.3\msvc2017\bin\Qt5Cored.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\platforms\qwindowsd.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\platforms\qwindowsd.dll" "$(TargetDir)\platforms"
IF NOT EXIST "$(TargetDir)\imageformats\qicnsd.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qicnsd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qicod.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qicod.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qsvgd.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qsvgd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qwbmpd.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qwbmpd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qddsd.dll" COPY "F:\Qt\5.12.3\msvc2017\plugins\imageformats\qddsd.dll" "$(TargetDir)\imageformats"
GOTO :eof

:Debug_x64
IF NOT EXIST "$(TargetDir)\Qt5OpenGLd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5OpenGLd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Widgetsd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Widgetsd.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Guid.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Guid.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Xmld.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Xmld.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\Qt5Cored.dll" COPY "F:\Qt\5.12.3\msvc2017_64\bin\Qt5Cored.dll" "$(TargetDir)"
IF NOT EXIST "$(TargetDir)\platforms\qwindowsd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\platforms\qwindowsd.dll" "$(TargetDir)\platforms"
IF NOT EXIST "$(TargetDir)\imageformats\qicnsd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qicnsd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qicod.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qicod.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qsvgd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qsvgd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qwbmpd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qwbmpd.dll" "$(TargetDir)\imageformats"
IF NOT EXIST "$(TargetDir)\imageformats\qddsd.dll" COPY "F:\Qt\5.12.3\msvc2017_64\plugins\imageformats\qddsd.dll" "$(TargetDir)\imageformats"
