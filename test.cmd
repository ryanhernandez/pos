@echo off
REM Convenience batch wrapper to run tests from repository root
echo Running solution tests: dotnet test %~dp0pos.sln %*
dotnet test "%~dp0pos.sln" %*
exit /b %errorlevel%
