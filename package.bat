
mkdir "bin\Input Devices\AtsEx\1.0\Resources"
mkdir "bin\Input Devices\AtsEx\1.0\Extensions"
set "dst=bin\Input Devices\AtsEx"

xcopy "AtsEx\bin\Release\*.dll" "%dst%\1.0" /s /y
xcopy "AtsEx\bin\Release\*.xml" "%dst%\1.0" /s /y
xcopy "AtsEx\Resources\*.resx" "%dst%\1.0\Resources" /s /y
xcopy "AtsEx.PluginHost\bin\Release\*.dll" "%dst%\1.0" /s /y
xcopy "AtsEx.PluginHost\bin\Release\*.xml" "%dst%\1.0" /s /y
xcopy "AtsEx.PluginHost\Resources\*.resx" "%dst%\1.0\Resources" /s /y
xcopy "AtsEx.Scripting\bin\Release\*.dll" "%dst%\1.0" /s /y
xcopy "AtsEx.Scripting\bin\Release\*.xml" "%dst%\1.0" /s /y
xcopy "AtsEx.Scripting\Resources\*.resx" "%dst%\1.0\Resources" /s /y
xcopy "AtsEx.Launcher\bin\Release\*.dll" "%dst%\" /s /y
xcopy "AtsEx.Launcher\bin\Release\*.dll.config" "%dst%\" /s /y
xcopy "AtsEx.Launcher\bin\Release\*.xml" "%dst%\" /s /y
xcopy "AtsEx.Caller.InputDevice\bin\Release\*.dll" "bin\Input Devices\" /s /y
xcopy "AtsEx.Caller.InputDevice\bin\Release\*.xml" "bin\Input Devices\" /s /y

xcopy "*.svg" "%dst%\" /y
xcopy "*.md" "%dst%\" /y
xcopy "LICENSE" "%dst%\" /y

xcopy "*.svg" "bin\" /y
xcopy "*.md" "bin\" /y
xcopy "LICENSE" "bin\" /y

for /d %%i in ("Extensions\*") do (
    xcopy "%%i\bin\Release\*.dll" "%dst%\1.0\Extensions" /s /y
)
