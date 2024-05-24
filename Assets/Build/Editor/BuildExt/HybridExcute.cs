using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using libx;
using SGame;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using ZEditors;

[ZEditorSymbol("enable_hotfix", symbol = "ENABLE_HOTFIX")]
[ZEditor("Hotfix", name = "ÈÈ¸ü")]
[DefaultExecutionOrder(-1000)]
public class HybridBuildExcute : IZEditor
{

	static public string Content = @"distributionBase=GRADLE_USER_HOME
distributionPath=wrapper/dists
zipStoreBase=GRADLE_USER_HOME
zipStorePath=wrapper/dists
distributionUrl=https\://services.gradle.org/distributions/gradle-6.7.1-all.zip
";

	[InitializeOnLoadMethod]
	[UnityEditor.Callbacks.DidReloadScripts]
	static void Init()
	{
		Debug.Log("::Reginster BuildAB!");
		BuildCommand.DoBeforeBuild += BeforeBuildAsset;
		BuildCommand.DoBuildAsset = (v, c, p) => HotfixenuItems.OneKeyBuildHotfix(v, c);
	}


	static void BeforeBuildAsset(Func<string, string> get)
	{
#if ENABLE_HOTFIX
		HybridCLR.Editor.SettingsUtil.Enable = true;
#else
		Debug.Log("hot====>"+HybridCLR.Editor.SettingsUtil.Enable);
		HybridCLR.Editor.SettingsUtil.Enable = false;
		Debug.Log("hot====>" + HybridCLR.Editor.SettingsUtil.Enable);

#endif

#if USE_THIRD_SDK
		var path = Application.dataPath + "\\Assets\\Plugins\\Android\\mainTemplate.gradle";
		var s = File.Exists(path);
		if (s) File.Delete(path);
		if (Application.isBatchMode)
		{
			path = "Assets/Plugins/Android";
			if (!Directory.Exists(path) || Directory.GetFiles(path, "*.aar")?.Length < 10)
				GooglePlayServices.PlayServicesResolver.ResolveSync(false);
		}
#endif
		AssetDatabase.SaveAssets();
	}

}
