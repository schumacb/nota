@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off


IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -ExcludeVersion -OutputDirectory "Tools"
.\Tools\Cake\Cake.exe deploy.cake -verbosity=Verbose %*
IF !ERRORLEVEL! NEQ 0 goto error
IF EXIST "Tools" (rmdir /S /Q "Tools" )
IF !ERRORLEVEL! NEQ 0 goto error

goto end

:error
endlocal
echo An error has occurred during web site deployment.
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Finished successfully.