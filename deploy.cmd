@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

IF %ERRORLEVEL% NEQ 0 goto error


IF NOT EXIST "Tools" (md "Tools")
IF %ERRORLEVEL% NEQ 0 goto error
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
IF %ERRORLEVEL% NEQ 0 goto error
nuget install Cake -ExcludeVersion -OutputDirectory "Tools"
.\Tools\Cake\Cake.exe deploy.cake -verbosity=Verbose %*
IF %ERRORLEVEL% NEQ 0 goto error

REM  IF EXIST "Tools" (rmdir /S /Q "Tools" ) 
REM IF %ERRORLEVEL% NEQ 0 goto error


goto end

:error
endlocal
echo An error has occurred during web site deployment. 
REM IF EXIST "Tools" (rmdir /S /Q "Tools" ) 
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Finished successfully.