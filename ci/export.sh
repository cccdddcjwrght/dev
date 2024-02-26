#!/bin/bash

:<<!Author!
" Author        : Daniel
" Create date   : 2023-08-07 10:18
" File name     : export.sh
" Last modified : 2023-08-07 10:18
" Description   : 
!Author!

_SVN_URL=$SVN_URL
_SVN_USERNAME=$SVN_USERNAME
_SVN_PASSWORD=$SVN_PASSWORD
_EXPORT_TOOLS_URL=$EXPORT_TOOLS_URL

function DefParam() {
    param1="$1"
    promp="$2"
    if [ -z "$param1" ]; then
        echo 1
    else
        echo 0
    fi
}

function DoExport() {
    set CD=`pwd`
    chmod 755 ci/svn
    git clone $_EXPORT_TOOLS_URL .excel_tools
    ci/svn co --username=$_SVN_USERNAME --password=$_SVN_PASSWORD $_SVN_URL .excel_tools/Tools/excel2Flat/excel_build

    
    chmod 755 ci/get-pip.py
    python3 ci/get-pip.py

    cd .excel_tools/Tools/excel2Flat/
    ls -la
    chmod 755 flatc/flatc
    pip install -r ./requirements.txt
    python3 ci.py
    cd $CD
}

function ExportExcel() {
    value=$(DefParam $_SVN_URL "svn url")
    echo $value
    if [[ $value -eq 1 ]]; then
        echo "skip export excel not found SVN_URL"
        return
    fi

    value=$(DefParam $_SVN_USERNAME "svn username")
    if [[ $value -eq 1 ]]; then
        echo "skip export excel not found SVN_USERNAME"
        return
    fi

    value=$(DefParam $_SVN_PASSWORD "svn password")
    if [[ $value -eq 1 ]]; then
        echo "skip export excel not found SVN_PASSWORD"
        return
    fi
    value=$(DefParam $_EXPORT_TOOLS_URL "export tools url")
    if [[ $value -eq 1 ]]; then
        echo "skip export excel not found SVN_PASSWORD"
        return
    fi

    DoExport
}

ExportExcel
