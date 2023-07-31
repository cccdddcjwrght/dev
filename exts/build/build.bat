@echo off 

set /p target_pj=【！！！是否已当前工程为目标工程，1：是，默认否】

if "%target_pj%"=="1" (goto set_1)
set /p input_url=【0.远端git路径，不输入就是默认】:
set /p input_project=【1.工程，不输入就是默认工程】:
set /p input_branch=【2.分支，不输入就是Dev】:

:set_1
set /p input_var_file=【3.环境变量路径，不输入就是默认Dev,路径用@符号开头】:
set /p input_ini_file=【4.INI路径，不输入就是默认dev，字符串匹配】:
set /p input_platform=【5.打包平台，1：安卓，2：IOS，默认1】
set /p before_cmd=【6.打包前处理脚本，默认无】
set /p after_cmd=【7.打包后处理脚本，默认无】
set /p code_var=【代码版本，默认0】

set workdir=%cd%
set project=poker_client
set branch=
set url=
set dst=.vs
set command=
set after_command=

if not "%input_url%" =="" (set url=%input_url%)
if not "%input_project%" =="" (set project=%input_project%)
if not "%input_branch%" =="" (set branch=%input_branch%)
if not "%before_cmd%" =="" (set command=%before_cmd%)
if not "%after_cmd%" =="" (set after_command=%after_cmd%)


::工程参数
set ALL_VAR_KEY=@exts/buildcfgs/local_dev_dev.txt
set VERSION_CORE_VAR=%code_var%

if not "%input_var_file%" =="" (set ALL_VAR_KEY=%input_var_file%) else (
if exist env.log (set ALL_VAR_KEY=@%workdir%\env.log)
)

if not "%input_ini_file%" =="" (set INI_FILE=%input_ini_file%) else (
if exist ini.log (set INI_FILE=@%workdir%\ini.log)
)


::==================================
::获取git信息

set tstr=%date:~0,4%-%date:~5,2%-%date:~8,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%
set purl=%url%%project%.git
set cid=


set gf=.gf
del /f  /Q %gf%
if not "%url%"=="" (goto git_info)
git remote -v>>%gf%
for /f  "tokens=2"   %%i in (%gf%) do (set purl=%%i)&(goto git_info)

:git_info
if "%branch%"=="" (for /f "delims=" %%i in ('git rev-parse --abbrev-ref HEAD') do (set branch=%%i))

:git_info_end
del /f /Q %gf%

if "%target_pj%"=="1" (for /f "delims=" %%i in ('git rev-parse --show-toplevel') do (set dst=%%i))
(for /f "delims=" %%i in ('git rev-parse --short HEAD') do (set cid=%%i))

::==================================


::改成自己的u3d路径
set uf=./.tmp
set u3d=
if exist %uf% (set /p u3d=<%uf%)
if not "%u3d%"=="" (goto set)
set /p u3d=【8.Unity路径】:

if "%u3d%"=="" (goto u3d_error)
echo %u3d%>%uf%

:set
::平台
set target=Android
if "%input_platform%" ==1 (set target=Android)
if "%input_platform%" ==2 (set target=IOS)

::其他打包参数
set out=%workdir%\.vscode\
set cmd=BuildCommand.PerformBuild
set outname=%target%_%project%_%branch%

goto begin

:u3d_error
echo 没有设置U3D路径
goto end

:begin
setlocal enabledelayedexpansion



if "%command%"=="" (goto project)
if exist %command% (call %command% %project%  %dst%)

:project

cd %workdir%
set input=1
if "%target_pj%"=="1" (goto set_2)
set /p input=【ci模式:0全流程，1打包，2拉工程,3更新打包，默认3】:
if "%input%"=="" (set input=3)

:set_2
echo %input%
set type=%input%;
if %type%==3 (if exist %dst% goto update)
if exist %dst% (if %type%==1 goto build)
if exist %dst% (if %type%==2 goto update)
::==================git checkout==========================
:clone
rd /s /Q %dst%
git clone %purl% %dst%

:update
cd %dst%
git reset --hard HEAD
git clean -df
git pull
git branch -a
if not "%branch%" =="" goto checkout
set /p input=【分支】:
echo %input%
set branch=%input%

:checkout
set rbranch=remotes/origin/%branch%
%rbrabch%
::git checkout %rbranch%
git switch %branch%
git pull
for /f "delims=" %%i in ('git rev-parse --short HEAD') do (set cid=%%i)
echo %cid%
cd %workdir%


if %type%==2 goto end
goto build

echo =====================================================
::=====================build==============================
:build
if not "%target_pj%"=="1" (goto start)

TASKLIST /V /S localhost /U %username%>tmp_process_list.txt
TYPE tmp_process_list.txt |FIND "Unity.exe"
if errorlevel 0 (goto kill_unity)
else (goto start)

:kill_unity
taskkill /f /im Unity.exe
ping 127.0.0.1 -n 1 >nul

:start
rd /s /Q "%dst%\DLC"
del /f /Q log.txt
del /f /Q tmp_process_list.txt

echo =============================
echo git地址: %purl%
echo git分支: %branch%
echo git提交: %cid%
echo 工程路径: %dst%
echo 环境变量：%ALL_VAR_KEY%
echo ini配置：%INI_FILE%
echo =============================

set name=%outname%_%cid%_%tstr%
set filepath=%out%%name%.apk
rd /s /Q %filepath%
echo %filepath%
echo =====================================================
echo start-%time%
echo %project%  building
set pro=%u3d% -projectPath %dst% -quit -batchmode -nographics -buildTarget %target% -customBuildTarget %target% -customBuildName %name% -customBuildPath %out% -executeMethod %cmd% -logFile build.log
echo %pro%
echo wait-----%filepath%
%pro%
echo build completed!!!!!
echo end-%time%

::打开output
if not exist %filepath% (goto end)
start %out%

::结束脚本调用
if "%after_command%"=="" (goto end)
if exist %after_command% (call %after_command% %name% %filepath%)

echo =====================================================

::========================================================

:end
pause
