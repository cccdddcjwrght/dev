using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using ZEditors;

[ZEditor("AppLogo")]
[ZEditorCmd("Xiaochi", "ReplaceApplogo", args = new object[] { "xiaochi" }, refresh = true)]
[ZEditorCmd("Xianhua", "ReplaceApplogo", args = new object[] { "xianhua" }, refresh = true)]
[ZEditorCmd("Wakuang", "ReplaceApplogo", args = new object[] { "wakuang" }, refresh = true)]
public class AppLogoExcute : IZEditor
{
	static private string source_path = "exts/apploge/";
	static private string target_path = "Assets/BuildAsset/UI";

	static Dictionary<string, string[]> nameReplaceDic = new Dictionary<string, string[]>() {

		{ "ui_bg" , new string[]{ "GameLoge_atlas_vm0m4", "Hotfix_atlas_cuja5", "Assets/BuildAsset/App/splash.png" } },
		{ "ui_logo" , new string[]{ "GameLoge_atlas_vm0m1" , "Hotfix_atlas_cuja6" } },
		{ "ui_loading_1" , new string[]{ "GameLoge_atlas_vm0m2" , "Hotfix_atlas_cuja3" } },
		{ "ui_loading_2" , new string[]{ "GameLoge_atlas_vm0m3" , "Hotfix_atlas_cuja4" } },
		{ "icon1024x1024" , new string[]{ "Assets/BuildAsset/App/icon1024x1024.png" } },
		
		{ "banner/Banner_atlas_b32e0" , new string[]{ "Assets/Resources/Banner/Banner_atlas_b32e0.png" } },
		{ "banner/Banner_atlas_b32e1" , new string[]{ "Assets/Resources/Banner/Banner_atlas_b32e1.png" } },
		{ "banner/Banner_atlas_b32e2" , new string[]{ "Assets/Resources/Banner/Banner_atlas_b32e2.png" } },
		{ "banner/Banner_atlas_b32e3" , new string[]{ "Assets/Resources/Banner/Banner_atlas_b32e3.png" } },
		{ "banner/Banner_atlas_b32e4" , new string[]{ "Assets/Resources/Banner/Banner_atlas_b32e4.png" } },


	};

	[InitializeOnLoadMethod]
	static void Init()
	{
		BuildCommand.DoBeforeBuild += BeforeBuild;
	}

	static void BeforeBuild(Func<string, string> get)
	{
		ReplaceApplogo(get?.Invoke("game_type") ?? "xiaochi");
	}

	static public void ReplaceApplogo(string type)
	{
		var path = Path.Combine(source_path, type);
		if (Directory.Exists(path) && nameReplaceDic?.Count > 0)
		{
			foreach (var item in nameReplaceDic)
			{
				if (item.Value?.Length > 0)
				{
					var src = Path.Combine(path, item.Key + ".png");
					if (File.Exists(src))
					{
						for (int i = 0; i < item.Value.Length; i++)
						{
							var file = item.Value[i].EndsWith(".png") ? item.Value[i] : Path.Combine(target_path, item.Value[i] + ".png");
							if (File.Exists(file))
								File.Copy(src, file, true);
						}
					}
				}
			}
		}
	}

}
