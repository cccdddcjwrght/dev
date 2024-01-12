using System;
using System.Collections;
using System.Collections.Generic;
using libx;
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
		BuildCommand.DoBuildAsset = (a,b,c) => BuildScript.BuildBundleAndCopyToStream(a);
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
