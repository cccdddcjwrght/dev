@ECHO OFF
CLS

set APK_FILE=%1

jarsigner -verify -verbose -certs %APK_FILE%  | findstr "CN=Android Debug"
pause