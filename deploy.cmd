@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

IF %ERRORLEVEL% NEQ 0 goto error

IF DEFINED WEBSITE_SITE_NAME (
 echo Running on Azure. (%WEBSITE_SITE_NAME%)
)

IF NOT EXIST "Tools" (md "Tools")
IF %ERRORLEVEL% NEQ 0 goto error
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
IF %ERRORLEVEL% NEQ 0 goto error









nuget install Cake -ExcludeVersion -OutputDirectory "Tools" -Version 0.19.5
.\Tools\Cake\Cake.exe deploy.cake -verbosity=Verbose %*
IF %ERRORLEVEL% NEQ 0 goto error



goto end

:error
endlocal
echo An error has occurred during web site deployment. 

IF DEFINED WEBSITE_SITE_NAME (
 echo Try to remove Tools. 
 IF EXIST "Tools" (rmdir /S /Q "Tools" ) 
 nuget locals all -clear
)


call :exitSetErrorLevel
call :exitFromFunction 2>nul
:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal

IF DEFINED WEBSITE_SITE_NAME (
 echo Try to remove Tools. 
 IF EXIST "Tools" (rmdir /S /Q "Tools" ) 
 nuget locals all -clear
)

echo Finished successfully.