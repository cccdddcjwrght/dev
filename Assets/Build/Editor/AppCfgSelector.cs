using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;

public class IniSelectorEditor : EditorWindow
{
	private static List<string> inis;
	private static string ini_path = "exts/inis/";

	public string savepath = @"Assets/Resources/";

	private System.Action command;

	private void OnEnable()
	{
		inis = GetAllIni();
	}

	private void OnGUI()
	{
		if (GUILayout.Button("存储路径:" + savepath))
		{
			var p = EditorUtility.OpenFolderPanel("Resources路径", savepath, default);
			if (!string.IsNullOrEmpty(p))
				savepath = p;
		}
		GUILayout.Space(20);

		GUILayout.Label("下面几个选项点击选择需要替换的配置：");
		if (inis != null && inis.Count > 0)
		{
			string select = null;
			bool s = false;
			var type = 0;
			foreach (var item in inis)
			{
				select = item;
				GUILayout.BeginHorizontal();
				if (GUILayout.Button(Path.GetFileNameWithoutExtension(item)))
				{
					s = true;
					type = 1;
				}
				if (GUILayout.Button("编辑", GUILayout.Width(100)))
				{
					type = 2;
					s = true;
				}
				GUILayout.EndHorizontal();
				if (s)
					break;
			}

			if (type != 0 && !string.IsNullOrEmpty(select))
			{
				if (type == 1)
				{
					if (EditorUtility.DisplayDialog("替换INI配置", "是否将INI配置替换成" + Path.GetFileNameWithoutExtension(select), "替换"))
					{
						ReplaceIni(select, savepath);
						AssetDatabase.Refresh();
					}
				}
				else if (type == 2)
				{
					Application.OpenURL("file://" + Application.dataPath.Replace("Assets", select));
				}
			}
		}

		GUILayout.Space(20);
		if (GUILayout.Button("刷新列表"))
		{
			inis = GetAllIni();
		}

		if (GUILayout.Button("移除INI"))
		{
			ReplaceIni(null, savepath);
			AssetDatabase.Refresh();
		}

		if (GUILayout.Button("整理配置"))
		{
			Application.OpenURL("file://" + Application.dataPath.Replace("Assets", ini_path));
		}
		GUILayout.Space(20);
		OnDrawNewIniGUI();

		command?.Invoke();
		command = null;
	}

	bool foldState = false;
	string normaweburl = null;
	string svrip = null;
	string svpport = null;
	string updateurl = null;
	string noticeurl = null;

	private void OnDrawNewIniGUI()
	{
		foldState = EditorGUILayout.Foldout(foldState, "简单配置创建");
		if (foldState)
		{
			normaweburl = EditorGUILayout.TextField("统一Web地址:", normaweburl);
			svrip = EditorGUILayout.TextField("服务器地址:", svrip);
			svpport = EditorGUILayout.TextField("服务器端口:", svpport);
			updateurl = EditorGUILayout.TextField("资源服地址:", updateurl);
			noticeurl = EditorGUILayout.TextField("公告地址:", noticeurl);
			if (GUILayout.Button("创建INI配置"))
			{
				var path = EditorUtility.SaveFilePanel("创建INI配置", ini_path, "config", "ini");
				if (!string.IsNullOrEmpty(path))
				{
					command += () =>
					{
						var sb = new StringBuilder();
						if (!string.IsNullOrEmpty(normaweburl))
							sb.AppendLine($"web={normaweburl}");
						if (!string.IsNullOrEmpty(svrip))
							sb.AppendLine($"address={svrip}");
						if (!string.IsNullOrEmpty(svpport))
							sb.AppendLine($"port={svpport}");
						if (!string.IsNullOrEmpty(updateurl))
							sb.AppendLine($"update_url={updateurl}");
						if (!string.IsNullOrEmpty(noticeurl))
							sb.AppendLine($"notice_url={noticeurl}");
						if (sb.Length > 10)
						{
							File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
							inis = GetAllIni();
							Application.OpenURL("file://" + Application.dataPath.Replace("Assets", ini_path));
						}
					};
				}
			}
		}
	}

	static public void SwitchIni(string fileOrName)
	{
		if (!string.IsNullOrEmpty(fileOrName))
		{

			if (fileOrName.Contains('='))
				ReplaceIni(fileOrName, isContent: true);
			else
			{
				if (fileOrName[0] == '@')
					ReplaceIni(fileOrName.Substring(1));
				else
				{
					inis = inis ?? GetAllIni();
					if (inis != null && inis.Count > 0)
					{
						var path = inis.Contains(fileOrName) ? fileOrName : null;
						if (path == null)
						{
							fileOrName = fileOrName.ToLower();
							path = inis.Find(f => Path.GetFileNameWithoutExtension(f).ToLower().StartsWith(fileOrName));
							ReplaceIni(path);
						}
					}
				}
			}
		}
		else
			ReplaceIni(null);
	}

	static void ReplaceIni(string select, string savepath = @"Assets/Resources/", bool isContent = false)
	{
		if (!Directory.Exists(savepath))
			Directory.CreateDirectory(savepath);

		var dst = Path.Combine(savepath, "local.bytes");

		if (string.IsNullOrEmpty(select))
		{
			if (File.Exists(dst))
			{
				File.Delete(dst);
				Debug.Log($"::清理INI!!!");
			}
			return;
		}

		if (isContent)
			File.WriteAllText(dst, select, Encoding.UTF8);
		else if (!File.Exists(select))
			return;
		else
			File.Copy(select, dst, true);

		var txt = File.ReadAllText(dst);
		Debug.Log("::===========================");
		Debug.Log(txt);
		Debug.Log($"::替换完成!!!");
		Debug.Log("::===========================");

	}

	static List<string> GetAllIni()
	{
		if (Directory.Exists(ini_path))
		{

			return Directory
				.GetFiles(ini_path, "*.ini", SearchOption.AllDirectories)
				.Where(f => !Path.GetFileNameWithoutExtension(f).StartsWith("@")).ToList(); ;
		}
		return null;
	}

	[MenuItem("[Tools]/Utils/切换InI配置")]
	static void Excute()
	{
		var win = EditorWindow.GetWindow(typeof(IniSelectorEditor), true, "选择INI配置") as IniSelectorEditor;
		win.minSize = new Vector2(300, 500);
	}

}

