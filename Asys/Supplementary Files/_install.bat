@echo off
REM
REM
REM	If you are reading this, please delete me.
REM	I am an artifact of the Asys Updater, and am no longer of use to you.
REM
REM

set arg1=%1
shift
taskkill /f /im asys.exe
msiexec /i %arg1% /quiet
echo %arg1%
pause
REM Attempt to self-destruct
(goto) 2>nul & del "%~f0"