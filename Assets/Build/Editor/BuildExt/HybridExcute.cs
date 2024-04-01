using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using libx;
using SGame;
using UnityEditor;
using UnityEngine;
using ZEditors;

[ZEditorSymbol("enable_hotfix", symbol = "ENABLE_HOTFIX")]
[ZEditor("Hotfix", name = "ÈÈ¸ü")]
public class HybridBuildExcute : IZEditor
{

	static public string Content = @"distributionBase=GRADLE_USER_HOME
distributionPath=wrapper/dists
zipStoreBase=GRADLE_USER_HOME
zipStorePath=wrapper/dists
distributionUrl=https\://services.gradle.org/distributions/gradle-6.7.1-all.zip
";

	[InitializeOnLoadMethod]
	static void Init()
	{
		BuildCommand.DoBeforeBuild += BeforeBuildAsset;
		BuildCommand.DoBuildAsset = (v, c, p) => HotfixenuItems.OneKeyBuildHotfix(v, c);
	}

	static void BeforeBuildAsset(Func<string, string> get)
	{
#if ENABLE_HOTFIX
		HybridCLR.Editor.SettingsUtil.Enable = true;
#else
		HybridCLR.Editor.SettingsUtil.Enable = false;
#endif

#if USE_THIRD_SDK
		var path = Application.dataPath + "\\Assets\\Plugins\\Android\\mainTemplate.gradle";
		var s = File.Exists(path);
		if (s) File.Delete(path);
		if (Application.isBatchMode)
			GooglePlayServices.PlayServicesResolver.ResolveSync(true); 
#endif
	}

}
