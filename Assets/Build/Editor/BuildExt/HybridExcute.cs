using System;
using System.Collections;
using System.Collections.Generic;
using libx;
using SGame;
using UnityEditor;
using UnityEngine;
using ZEditors;

[ZEditorSymbol("enable_hotfix",symbol = "ENABLE_HOTFIX")]
[ZEditor("Hotfix" , name = "ÈÈ¸ü")]
public class HybridBuildExcute:IZEditor
{

	[InitializeOnLoadMethod]
	static void Init()
	{
		BuildCommand.DoBeforeBuild += BeforeBuildAsset;
		BuildCommand.DoBuildAsset = (v, c, p) => HotfixenuItems.OneKeyBuildHotfix(v, c);
	}

	static void BeforeBuildAsset(Func<string,string> get)
	{
#if ENABLE_HOTFIX
		HybridCLR.Editor.SettingsUtil.Enable = true;
#else
		HybridCLR.Editor.SettingsUtil.Enable = false;
#endif
	}

}
