using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using ZEditors;


[ZEditorCmd("open", cmd = "OpenCfgDir",name = "打开文件夹")]
[ZEditorCmd("build", cmd = "Excute" , name = "导出配置")]
[ZEditor("Config", name = "配置")]
public class ConfigToolEditor : IZEditor
{

	static string bat_path = "exts\\config\\importconfig.bat";
	static string dir_path = "exts\\config\\.vs\\doc";


	static public void Excute()
	{

		var p = new Process();
		p.StartInfo.FileName = bat_path;             //設定程序名
		//p.StartInfo.UseShellExecute = false;          //關閉Shell的使用
		//p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
		//p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
		//p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
		//p.StartInfo.CreateNoWindow = true;           //設置不顯示窗口
		p.Start();  //啟動
	}

	static public void OpenCfgDir()
	{
		Application.OpenURL(dir_path);
	}

}
