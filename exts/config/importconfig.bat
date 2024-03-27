@echo off 

cd /d "%~dp0"

call updatesvn.bat

set temp=.vs
set cdir=%cd%
set adir=.vs\doc\

set dir=%cdir%\.vs\
set doc=%cdir%\%adir%
set tpath=%dir%/tool


set bytepath=BuildAsset\configs\
set scriptpath=Script\Game\Config\

set tool=
set cfg=%cdir%\config.yml

if not exist %dir% (md %dir%)
goto set

:error_tool
echo toolpath is null or not exist
goto end


:set
if exist %tpath% (set /p tool=<%tpath%)
if not "%tool%"=="" (goto update)
set /p tool=【导表工具路径(richman_desgin\Tools\excel2Flat)Tool path】:


if "%tool%"=="" (goto error_tool)
if not exist "%tool%" (goto error_tool)
echo %tool%>%tpath%

:update
cd %tool%
if exist %cfg% (goto do)

:do
set bp=%tool%\%temp%\%bytepath%
set sp=%tool%\%temp%\%scriptpath%

if exist %adir% (del /s /q %temp%)
xcopy %doc% %adir% /s/y/f
python run.py -p %cfg%

cd %cdir%/../../Assets/
echo %cd%
xcopy %bp% %cd%\%bytepath% /s/y/f
xcopy %sp% %cd%\%scriptpath% /s/y/f


echo ===============================================
echo config export completed!!!
echo ===============================================

pause

:end
echo completed
pause