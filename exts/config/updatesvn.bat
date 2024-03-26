@echo off
setlocal enabledelayedexpansion
set wd=%cd%


set dp=%wd%\.vs
set svndir=%dp%\svn
set locdir=%dp%\doc\

set spath=%dp%\stool
set svntool=
set cfg=

if not exist %dp% (md %dp%)

if exist %spath% (set /p svntool=<%spath%)
if not "%svntool%"=="" (goto step1)
set /p svntool=【svn工具路径svn tool path】:
if "%svntool%"=="" (goto error)
if not exist "%svntool%" (goto error)
echo %svntool%
echo %svntool%>%spath%

:step1
if exist %svndir% (set /p cfg=<%svndir%)
if not "%cfg%"=="" (goto do)
set /p cfg=【配置SVN路径config svn path】:

if "%cfg%"=="" (goto error)
echo %cfg%>%svndir%

:do
echo co
if exist %locdir% (goto up)
%svntool%\bin\svn co %cfg% %locdir%

:up
echo up
%svntool%\bin\svn update %locdir%
goto end

:error
echo no path
pause

:end
echo complete
pause
