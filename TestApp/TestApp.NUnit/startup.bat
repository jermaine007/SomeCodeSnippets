@echo off

set WORKSPACE=%~dp0
set TEST_PROJECT="%WORKSPACE%TestApp.NUnit.csproj"
set ARGUMENTS=-l "console;verbosity=detailed"

dotnet test %TEST_PROJECT% %ARGUMENTS%

pause