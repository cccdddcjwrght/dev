using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using ZEditors;


[ZEditorCmd("open", cmd = "OpenCfgDir",name = "���ļ���")]
[ZEditorCmd("build", cmd = "Excute" , name = "��������")]
[ZEditor("Config", name = "����")]
public class ConfigToolEditor : IZEditor
{

	static string bat_path = "exts\\config\\importconfig.bat";
	static string dir_path = "exts\\config\\.vs\\doc";


	static public void Excute()
	{

		var p = new Process();
		p.StartInfo.FileName = bat_path;             //�O��������
		//p.StartInfo.UseShellExecute = false;          //�P�]Shell��ʹ��
		//p.StartInfo.RedirectStandardInput = true;   //�ض���˜�ݔ��
		//p.StartInfo.RedirectStandardOutput = true;  //�ض���˜�ݔ��
		//p.StartInfo.RedirectStandardError = true;   //�ض����e�`ݔ��
		//p.StartInfo.CreateNoWindow = true;           //�O�ò��@ʾ����
		p.Start();  //����
	}

	static public void OpenCfgDir()
	{
		Application.OpenURL(dir_path);
	}

}
