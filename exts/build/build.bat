@echo off 

set /p target_pj=���������Ƿ��ѵ�ǰ����ΪĿ�깤�̣�1���ǣ�Ĭ�Ϸ�

if "%target_pj%"=="1" (goto set_1)
set /p input_url=��0.Զ��git·�������������Ĭ�ϡ�:
set /p input_project=��1.���̣����������Ĭ�Ϲ��̡�:
set /p input_branch=��2.��֧�����������Dev��:

:set_1
set /p input_var_file=��3.��������·�������������Ĭ��Dev,·����@���ſ�ͷ��:
set /p input_ini_file=��4.INI·�������������Ĭ��dev���ַ���ƥ�䡿:
set /p input_platform=��5.���ƽ̨��1����׿��2��IOS��Ĭ��1��
set /p before_cmd=��6.���ǰ����ű���Ĭ���ޡ�
set /p after_cmd=��7.�������ű���Ĭ���ޡ�
set /p code_var=������汾��Ĭ��0��

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


::���̲���
set ALL_VAR_KEY=@exts/buildcfgs/local_dev_dev.txt
set VERSION_CORE_VAR=%code_var%

if not "%input_var_file%" =="" (set ALL_VAR_KEY=%input_var_file%) else (
if exist env.log (set ALL_VAR_KEY=@%workdir%\env.log)
)

if not "%input_ini_file%" =="" (set INI_FILE=%input_ini_file%) else (
if exist ini.log (set INI_FILE=@%workdir%\ini.log)
)


::==================================
::��ȡgit��Ϣ

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


::�ĳ��Լ���u3d·��
set uf=./.tmp
set u3d=
if exist %uf% (set /p u3d=<%uf%)
if not "%u3d%"=="" (goto set)
set /p u3d=��8.Unity·����:

if "%u3d%"=="" (goto u3d_error)
echo %u3d%>%uf%

:set
::ƽ̨
set target=Android
if "%input_platform%" ==1 (set target=Android)
if "%input_platform%" ==2 (set target=IOS)

::�����������
set out=%workdir%\.vscode\
set cmd=BuildCommand.PerformBuild
set outname=%target%_%project%_%branch%

goto begin

:u3d_error
echo û������U3D·��
goto end

:begin
setlocal enabledelayedexpansion



if "%command%"=="" (goto project)
if exist %command% (call %command% %project%  %dst%)

:project

cd %workdir%
set input=1
if "%target_pj%"=="1" (goto set_2)
set /p input=��ciģʽ:0ȫ���̣�1�����2������,3���´����Ĭ��3��:
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
set /p input=����֧��:
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
echo git��ַ: %purl%
echo git��֧: %branch%
echo git�ύ: %cid%
echo ����·��: %dst%
echo ����������%ALL_VAR_KEY%
echo ini���ã�%INI_FILE%
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

::��output
if not exist %filepath% (goto end)
start %out%

::�����ű�����
if "%after_command%"=="" (goto end)
if exist %after_command% (call %after_command% %name% %filepath%)

echo =====================================================

::========================================================

:end
pause
