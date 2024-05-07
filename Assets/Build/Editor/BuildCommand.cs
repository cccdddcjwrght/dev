using UnityEditor;
using System.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEditor.SceneManagement;
using SGame;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using UnityEditor.Build;
using Firebase;

static class BuildCommand
{
	//打包前执行
	static public Action<Func<string, string>> DoBeforeBuild;
	//打包资源接口
	static public Action<int, int, int> DoBuildAsset;
	//打包后执行
	static public Action<Func<string, string>> DoAfterBuild;
	static public Action<Func<string, string>> DoAfterBuildAsset;

	#region 参数Key
	/// <summary>
	/// 全部参数值
	/// </summary>
	private const string ALL_VAR_KEY = "ALL_VAR_KEY";

	private const string KEYSTORE_PASS = "KEYSTORE_PASS";
	private const string KEY_ALIAS_PASS = "KEY_ALIAS_PASS";
	private const string KEY_ALIAS_NAME = "KEY_ALIAS_NAME";
	private const string KEYSTORE_FILE = "KEY_STORE_FILE";

	private const string KEYSTORE = "keys/user.keystore";
	private const string BUILD_OPTIONS_ENV_VAR = "BuildOptions";
	private const string BUILD_OPTIONS_SYMBOL = "DEBUG_SYMBOL";

	private const string ANDROID_BUNDLE_VERSION_CODE = "VERSION_BUILD_VAR";
	private const string ANDROID_APP_BUNDLE = "BUILD_APP_BUNDLE";
	private const string SCRIPTING_BACKEND_ENV_VAR = "SCRIPTING_BACKEND";
	private const string VERSION_NUMBER_VAR = "VERSION_NUMBER_VAR";
	private const string VERSION_iOS = "VERSION_BUILD_VAR";
	private const string PACKAGE_ID = "PACKAGE_ID";
	private const string BUILD_REPLACE_SYMBOL = "BUILD_REPLACE_SYMBOL";
	private const string BUILD_SYMBOL = "BUILD_SYMBOL";
	private const string VERSION_NUMBER_VAR_OTHER = "VERSION_NUMBER_VAR_OTHER";
	private const string APP_NAME = "APP_NAME";
	private const string APP_COM = "APP_COM";
	private const string APP_RES = "VERSION_BUILD_VAR";
	private const string CORE_RES = "VERSION_CORE_VAR";//代码版本
	private const string PROTO_RES = "VERSION_PROTO_VAR";//协议版本
	private const string SCRIPT_LEVEL = "SCRIPT_LEVEL";
	private const string CPU_TYPE = "CPU_TYPE";//安卓cpu架构
	private const string VIDEO_PATH = "VIDEO_PATH";//闪屏视频路径
	private const string ENABLE_AB = "ENABLE_AB";//打包ab资源

	private const string INI_FILE = "INI_FILE";


	private const string FIRST_SCENE = "FIRST_SCENE";
	#endregion

	#region 参数
	private const string C_KEYSTORE_PASS = "123456";
	private const string C_KEY_ALIAS_NAME = "uhi";
	private const string C_KEY_ALIAS_PASS = "123456";
	private const string C_BUILD_RESULT_FILE = ".flag";

	private static string G_VAR_FILE = null;

	private static string _old_symbol_string = null;
	private static string _output = null;
	private static Dictionary<string, string> _cfgs;

	private static bool _disableAb = false;

	#endregion

	#region 接口
	static string GetArgument(string name)
	{
		string[] args = Environment.GetCommandLineArgs();
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i].Contains(name))
			{
				return args[i + 1];
			}
		}
		return GetArgumentFromCfg(name);
	}

	static string GetArgumentFromCfg(string name)
	{
		if (!string.IsNullOrEmpty(name) && _cfgs != null)
		{
			_cfgs.TryGetValue(name, out var v);
			return v;
		}
		return default;
	}

	static string[] GetEnabledScenes()
	{
		return (
			from scene in EditorBuildSettings.scenes
			where scene.enabled
			where !string.IsNullOrEmpty(scene.path)
			where !scene.path.Contains("BuildAsset/Scenes")
			where File.Exists(scene.path)
			select scene.path
		).ToArray();
	}

	static BuildTarget GetBuildTarget()
	{
		string buildTargetName = GetArgument("customBuildTarget");
		Console.WriteLine(":: Received customBuildTarget " + buildTargetName);

		if (!string.IsNullOrEmpty(buildTargetName))
		{
			if (buildTargetName?.ToLower() == "android")
			{
#if !UNITY_5_6_OR_NEWER
			// https://issuetracker.unity3d.com/issues/buildoptions-dot-acceptexternalmodificationstoplayer-causes-unityexception-unknown-project-type-0
			// Fixed in Unity 5.6.0
			// side effect to fix android build system:
			EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
#endif
			}

			if (buildTargetName.TryConvertToEnum(out BuildTarget target))
				return target;
		}
		Console.WriteLine($":: {nameof(buildTargetName)} \"{buildTargetName}\" not defined on enum {nameof(BuildTarget)}, using {nameof(EditorUserBuildSettings.activeBuildTarget)} enum to build");
		return EditorUserBuildSettings.activeBuildTarget;

	}

	static string GetBuildPath()
	{
		string buildPath = GetArgument("customBuildPath");
		Console.WriteLine(":: Received customBuildPath " + buildPath);
		if (string.IsNullOrEmpty(buildPath))
		{
			buildPath = ".output/";
			Console.WriteLine($"customBuildPath argument is missing , now use dir: {buildPath}");

		}
		return buildPath;
	}

	static string GetBuildName()
	{
		string buildName = GetArgument("customBuildName");
		Console.WriteLine(":: Received customBuildName " + buildName);
		if (string.IsNullOrEmpty(buildName))
		{
			buildName = Application.productName + "_" + System.DateTime.Now.ToString("g")
				.Replace("/", "_")
				.Replace(":", "_")
				.Replace(" ", "_");
			Console.WriteLine($"customBuildName argument is missing,now use name {buildName}");
		}
		return buildName;
	}

	static string GetFixedBuildPath(BuildTarget buildTarget, string buildPath, string buildName)
	{
		if (buildTarget.ToString().ToLower().Contains("windows"))
		{
			buildName += ".exe";
		}
		else if (buildTarget == BuildTarget.Android)
		{
#if UNITY_2018_3_OR_NEWER
			buildName += EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk";
#else
            buildName += ".apk";
#endif
		}
		return buildPath + buildName;
	}

	static BuildOptions GetBuildOptions()
	{
		if (TryGetEnv(BUILD_OPTIONS_ENV_VAR, out string envVar))
		{
			string[] allOptionVars = envVar.Split(',');
			BuildOptions allOptions = BuildOptions.None;
			BuildOptions option;
			string optionVar;
			int length = allOptionVars.Length;

			Console.WriteLine($":: Detecting {BUILD_OPTIONS_ENV_VAR} env var with {length} elements ({envVar})");

			for (int i = 0; i < length; i++)
			{
				optionVar = allOptionVars[i];

				if (optionVar.TryConvertToEnum(out option))
				{
					allOptions |= option;
				}
				else
				{
					Console.WriteLine($":: Cannot convert {optionVar} to {nameof(BuildOptions)} enum, skipping it.");
				}
			}

			return allOptions;
		}

		return BuildOptions.None;
	}

	// https://stackoverflow.com/questions/1082532/how-to-tryparse-for-enum-value
	static bool TryConvertToEnum<TEnum>(this string strEnumValue, out TEnum value)
	{
		if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
		{
			value = default;
			return false;
		}

		value = (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
		return true;
	}

	static bool TryGetEnv(string key, out string value)
	{
		value = Environment.GetEnvironmentVariable(key);
		if (string.IsNullOrEmpty(value))
			value = GetArgumentFromCfg(key);
		return !string.IsNullOrEmpty(value);
	}

	static void SetScriptingBackendFromEnv(BuildTarget platform)
	{
		var targetGroup = BuildPipeline.GetBuildTargetGroup(platform);
		if (TryGetEnv(SCRIPTING_BACKEND_ENV_VAR, out string scriptingBackend))
		{
			if (scriptingBackend.TryConvertToEnum(out ScriptingImplementation backend))
			{
				Console.WriteLine($":: Setting ScriptingBackend to {backend}");
				PlayerSettings.SetScriptingBackend(targetGroup, backend);
			}
			else
			{
				string possibleValues = string.Join(", ", Enum.GetValues(typeof(ScriptingImplementation)).Cast<ScriptingImplementation>());
				throw new Exception($"Could not find '{scriptingBackend}' in ScriptingImplementation enum. Possible values are: {possibleValues}");
			}
		}
		else
		{
			var defaultBackend = PlayerSettings.GetDefaultScriptingBackend(targetGroup);
			Console.WriteLine($":: Using project's configured ScriptingBackend (should be {defaultBackend} for targetGroup {targetGroup}");
		}
	}
	#endregion

	#region Excute
	static void PerformBuild()
	{
		Console.WriteLine(":: Performing build");
		if (File.Exists(C_BUILD_RESULT_FILE))
			File.Delete(C_BUILD_RESULT_FILE);
		HandlAllVar();
		var buildTarget = GetBuildTarget();
		if (TryGetEnv(VERSION_NUMBER_VAR_OTHER, out var bundleVersionNumber) || TryGetEnv(VERSION_NUMBER_VAR, out bundleVersionNumber))
		{
			Console.WriteLine($":: Setting bundleVersionNumber to '{bundleVersionNumber}' (Length: {bundleVersionNumber.Length})");
			PlayerSettings.bundleVersion = bundleVersionNumber;
			if (buildTarget == BuildTarget.iOS)
				PlayerSettings.iOS.buildNumber = GetIosVersion();
		}

		if (buildTarget == BuildTarget.Android)
		{
			HandleAndroidAppBundle();
			HandleAndroidBundleVersionCode();
			HandleAndroidKeystore();
			HandlerTargetCPUType();
		}

		HandlePackageID();
		HandleAppNameAndCom();
		HandleSymbol(buildTarget);
		HandleINIFile();
		HandleScriptLevel(buildTarget);
		var ver = HandleResVer();
		var core = HandleCoreVer();
		var proto = HandleProtoVer();

		var buildPath = GetBuildPath();
		var buildName = GetBuildName();
		var buildOptions = GetBuildOptions();
		var fixedBuildPath = GetFixedBuildPath(buildTarget, buildPath, buildName);
		SetScriptingBackendFromEnv(buildTarget);

		DoBeforeBuild?.Invoke(GetArgument);
		//生成垃圾代码
		if (GetArgument("ENABLE_CODEGEN") == "1")
			CodeGenEditor.Excute();


#if !_DisableAB
		HandlerEnableAB();
		if (!_disableAb)
		{
			if (buildTarget != EditorUserBuildSettings.activeBuildTarget)
				EditorUserBuildSettings.SwitchActiveBuildTarget(BuildPipeline.GetBuildTargetGroup(buildTarget), buildTarget);
			DoBuildAsset?.Invoke(ver, core, proto);
		}
#endif

		DoAfterBuildAsset?.Invoke(GetArgument);
		HandleFirstScene(out _);
		HandlSplashVideoToStream();
		var buildReport = BuildPipeline.BuildPlayer(GetEnabledScenes(), fixedBuildPath, buildTarget, buildOptions);
#if !UNITY_EDITOR_LINUX
		ResetSymbol(buildTarget);
#endif

		if (buildReport != null && buildReport.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
			throw new Exception($"Build ended with {buildReport.summary.result} status");

		_output = buildPath;

		File.WriteAllText(C_BUILD_RESULT_FILE, contents: "");
		DoAfterBuild?.Invoke(GetArgument);

		Console.WriteLine(":: Done with build");
		if (!Application.isBatchMode)
			AssetDatabase.Refresh();
	}

	private static void HandlerEnableAB()
	{
		_disableAb = false;
		var abval = GetArgument(ENABLE_AB);
		if (abval == null)
		{
			if (!Application.isBatchMode)
			{
				if (!EditorUtility.DisplayDialog("AB打包", "是否打包AB文件?", "打包", "不打包"))
					_disableAb = true;
			}
		}
		else
			_disableAb = !(abval == "1" || abval.ToLower() == "true");
	}

	private static void HandlAllVar()
	{
		string value = null;
		if (G_VAR_FILE != null || TryGetEnv(ALL_VAR_KEY, out value))
		{
			value = value ?? G_VAR_FILE;
			Console.WriteLine("::" + value);
			if (value.StartsWith("@"))
			{
				value = value.Substring(1);
				if (File.Exists(value))
					value = File.ReadAllText(value, System.Text.Encoding.UTF8);
			}
			else if (value.StartsWith("http"))
			{
				var req = WebRequest.Create(value);
				req.Method = "GET";
				var stream = req.GetResponse().GetResponseStream();
				var buff = new byte[1024 * 10];
				var len = stream.Read(buff, 0, buff.Length);
				if (len > 0)
					value = Encoding.UTF8.GetString(buff, 0, len);
			}
			Console.WriteLine($":: ALL_VAR_KEY =======================");
			Console.WriteLine(value);
			Console.WriteLine($":: ALL_VAR_KEY =======================");

			if (string.IsNullOrEmpty(value)) return;
			var lines = value.Split(new string[] { "\r\n", "\n", System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if (lines != null && lines.Length > 0)
			{
				_cfgs = lines.Where(Line => !string.IsNullOrEmpty(Line) && !Line.StartsWith("#") && Line.IndexOf('=') > 0)
							.Select(Line => Line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
							.Where(Lines => lines.Length > 1)
							.ToDictionary(Parts => Parts[0].Trim(), Parts => Parts.Length > 1 ? Parts[1].Trim() : null);
			}
		}
	}

	private static void HandleAndroidAppBundle()
	{
		EditorUserBuildSettings.buildAppBundle = false;
		EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
		if (TryGetEnv(ANDROID_APP_BUNDLE, out string value))
		{
#if UNITY_2018_3_OR_NEWER
			if (bool.TryParse(value, out bool buildAppBundle))
			{
				EditorUserBuildSettings.buildAppBundle = buildAppBundle;
				Console.WriteLine($":: {ANDROID_APP_BUNDLE} env var detected, set buildAppBundle to {value}.");
				return;
			}
			else if (value == "pj")
			{
				EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
				Console.WriteLine($":: {ANDROID_APP_BUNDLE} env var detected;set the value \"{value}\" == exportAsGoogleAndroidProject.");
			}
			else
			{
				Console.WriteLine($":: {ANDROID_APP_BUNDLE} env var detected but the value \"{value}\" is not a boolean.");
			}
#else
            Console.WriteLine($":: {ANDROID_APP_BUNDLE} env var detected but does not work with lower Unity version than 2018.3");
#endif
		}
	}

	private static void HandleAndroidBundleVersionCode()
	{
		if (TryGetEnv(ANDROID_BUNDLE_VERSION_CODE, out string value))
		{
			if (int.TryParse(value, out int version))
			{
				PlayerSettings.Android.bundleVersionCode = version;
				Console.WriteLine($":: {ANDROID_BUNDLE_VERSION_CODE} env var detected, set the bundle version code to {value}.");
			}
			else
				Console.WriteLine($":: {ANDROID_BUNDLE_VERSION_CODE} env var detected but the version value \"{value}\" is not an integer.");
		}
	}

	private static void HandlePackageID()
	{
		if (TryGetEnv(PACKAGE_ID, out string value))
			PlayerSettings.applicationIdentifier = value;
		Console.WriteLine($":: App applicationIdentifier : {PlayerSettings.applicationIdentifier}.");
	}

	private static int HandleResVer()
	{
		if (TryGetEnv(APP_RES, out string value))
		{
			if (int.TryParse(value, out int ver))
			{
				Console.WriteLine($":: App Res Version : {ver}.");
				return ver;
			}
		}
		return 0;
	}

	private static int HandleCoreVer()
	{
		if (TryGetEnv(CORE_RES, out string value))
		{
			if (int.TryParse(value, out int ver))
			{
				Console.WriteLine($":: Script Version : {ver}.");
				return ver;
			}
		}
		return 0;
	}

	private static int HandleProtoVer()
	{
		if (TryGetEnv(PROTO_RES, out string value))
		{
			if (int.TryParse(value, out int ver))
			{
				Console.WriteLine($":: Protocol Version : {ver}.");
				return ver;
			}
		}
		return 0;
	}


	private static bool HandleFirstScene(out string scene)
	{
		scene = null;
		if (TryGetEnv(FIRST_SCENE, out string val))
		{
			Debug.Log("scene===>" + val);
			var scenes = EditorBuildSettings.scenes.ToList();
			var index = scenes.FindIndex(o => o.path.Contains(val));
			if (index >= 0)
			{
				var s = scenes[index];
				scenes.RemoveAt(index);
				scenes.Insert(0, s);
				EditorBuildSettings.scenes = scenes.ToArray();
				EditorSceneManager.OpenScene(s.path);
				Console.WriteLine($":: Start Scene : {val}.");
				scene = val;
				return true;
			}
		}
		return false;
	}

	private static void HandleAppNameAndCom()
	{
		if (TryGetEnv(APP_NAME, out string name))
		{
			if (name.StartsWith("file://"))
			{
				name = name.Replace("file://", "");
				if (File.Exists(name))
					name = File.ReadAllLines(name)?.FirstOrDefault();
			}

			if (!string.IsNullOrEmpty(name))
			{
				PlayerSettings.productName = name;
				Console.WriteLine($":: App Name : {PlayerSettings.productName}.");
			}
		}

		if (TryGetEnv(APP_COM, out string com))
		{
			PlayerSettings.companyName = com;
			Console.WriteLine($":: App Com : {PlayerSettings.companyName}.");
		}
	}

	private static void HandleSymbol(BuildTarget target, string addsymbol = null)
	{
		var group = BuildPipeline.GetBuildTargetGroup(target);
		var symbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);

		_old_symbol_string = symbol;
		if (TryGetEnv(BUILD_REPLACE_SYMBOL, out string value) && !string.IsNullOrEmpty(value))
		{
			symbol = value;
			Console.WriteLine($":: Replace Symbol : {symbol}");
		}

		if (TryGetEnv(BUILD_SYMBOL, out value) && !string.IsNullOrEmpty(value))
		{
			if (!string.IsNullOrEmpty(addsymbol))
				addsymbol += ";" + value;
			else
				addsymbol = value;
			Console.WriteLine($":: Add Symbol : {addsymbol}");
		}

		if (!string.IsNullOrEmpty(addsymbol))
		{
			if (string.IsNullOrEmpty(symbol))
				symbol = addsymbol;
			else
			{
				var arr = symbol.Split(';').ToList();
				arr.AddRange(addsymbol.Split(';'));
				symbol = string.Join(';', arr.Distinct());
			}
			PlayerSettings.SetScriptingDefineSymbolsForGroup(group, symbol);
			AssetDatabase.Refresh();
		}

		var str = $":: App Build Symbol : {symbol}";
		Debug.Log(str);
		Console.WriteLine(str);
	}

	private static void SetSymbol(BuildTarget target, string symbol)
	{
		var group = BuildPipeline.GetBuildTargetGroup(target);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(group, symbol);
	}

	private static void HandleINIFile()
	{
		if (TryGetEnv(INI_FILE, out string value))
		{
			IniSelectorEditor.SwitchIni(value);
		}
	}

	public static void AddSymbol()
	{
		HandleSymbol(BuildTarget.Android);
	}

	private static string GetIosVersion()
	{
		if (TryGetEnv(VERSION_iOS, out string value))
		{
			if (int.TryParse(value, out int version))
			{
				Console.WriteLine($":: {VERSION_iOS} env var detected, set the version to {value}.");
				return version.ToString();
			}
			else
				Console.WriteLine($":: {VERSION_iOS} env var detected but the version value \"{value}\" is not an integer.");
		}

		throw new ArgumentNullException(nameof(value), $":: Error finding {VERSION_iOS} env var");
	}

	private static void HandleAndroidKeystore()
	{
#if UNITY_2019_1_OR_NEWER
		PlayerSettings.Android.useCustomKeystore = false;
#endif

		var file = KEYSTORE;
		if (!TryGetEnv(KEYSTORE_FILE, out file) || !File.Exists(file)) file = KEYSTORE;

		if (!File.Exists(file))
		{
			Console.WriteLine($":: {file} not found, skipping setup, using Unity's default keystore");
			return;
		}

		PlayerSettings.Android.keystoreName = file;

		string keyaliasName;
		string keystorePass;
		string keystoreAliasPass;

		if (TryGetEnv(KEY_ALIAS_NAME, out keyaliasName) || !string.IsNullOrEmpty(C_KEY_ALIAS_NAME))
		{
			PlayerSettings.Android.keyaliasName = keyaliasName ?? C_KEY_ALIAS_NAME;
			Console.WriteLine($":: using ${KEY_ALIAS_NAME} env var on PlayerSettings");
		}
		else
		{
			Console.WriteLine($":: ${KEY_ALIAS_NAME} env var not set, using Project's PlayerSettings");
		}

		if (!TryGetEnv(KEYSTORE_PASS, out keystorePass) && string.IsNullOrEmpty(C_KEYSTORE_PASS))
		{
			Console.WriteLine($":: ${KEYSTORE_PASS} env var not set, skipping setup, using Unity's default keystore");
			return;
		}
		keystorePass = string.IsNullOrEmpty(keystorePass) ? C_KEYSTORE_PASS : keystorePass;

		if (!TryGetEnv(KEY_ALIAS_PASS, out keystoreAliasPass) && string.IsNullOrEmpty(C_KEY_ALIAS_PASS))
		{
			Console.WriteLine($":: ${KEY_ALIAS_PASS} env var not set, skipping setup, using Unity's default keystore");
			return;
		}
		keystoreAliasPass = string.IsNullOrEmpty(keystoreAliasPass) ? C_KEY_ALIAS_PASS : keystoreAliasPass;

#if UNITY_2019_1_OR_NEWER
		PlayerSettings.Android.useCustomKeystore = true;
#endif
		PlayerSettings.Android.keystorePass = keystorePass;
		PlayerSettings.Android.keyaliasPass = keystoreAliasPass;

		Console.WriteLine($":: KeyStore: {PlayerSettings.Android.keystoreName} ");
		Console.WriteLine($":: KeyStorePass: {PlayerSettings.Android.keystorePass} ");
		Console.WriteLine($":: KeyaliasName: {PlayerSettings.Android.keyaliasName} ");
		Console.WriteLine($":: KeyaliasPass: {PlayerSettings.Android.keyaliasPass} ");

	}

	private static void ResetSymbol(BuildTarget target)
	{
		if (!string.IsNullOrEmpty(_old_symbol_string))
		{
			//还原原始宏
			SetSymbol(target, _old_symbol_string);
			_old_symbol_string = null;
		}
	}

	private static void HandleScriptLevel(BuildTarget target)
	{
		var tg = BuildPipeline.GetBuildTargetGroup(target);
		var level = ManagedStrippingLevel.Disabled;
		if (TryGetEnv(SCRIPT_LEVEL, out var lv) && !string.IsNullOrEmpty(lv))
			level = (ManagedStrippingLevel)Enum.Parse(typeof(ManagedStrippingLevel), lv, true);
		Console.WriteLine($"::Set Script Level : {level}");
		if (level == ManagedStrippingLevel.Disabled)
		{
			PlayerSettings.stripEngineCode = false;
			PlayerSettings.SetManagedStrippingLevel(tg, ManagedStrippingLevel.Disabled);
		}
		else
		{
			PlayerSettings.stripEngineCode = true;
			PlayerSettings.SetManagedStrippingLevel(tg, level);
		}
		if (target == BuildTarget.Android)
		{
			PlayerSettings.Android.minifyWithR8 = true;
			PlayerSettings.Android.minifyRelease = true;
		}
	}

	private static void HandlSplashVideoToStream()
	{
		var path = "exts/apploge/splash.mp4";
		if (TryGetEnv(VIDEO_PATH, out var v) && !string.IsNullOrEmpty(v) && File.Exists(v))
			path = v;

		if (File.Exists(path))
		{
			if (!Directory.Exists(Application.streamingAssetsPath))
				Directory.CreateDirectory(Application.streamingAssetsPath);
			File.Copy(path, Path.Combine(Application.streamingAssetsPath, "splash.mp4"), true);
			Console.WriteLine($"::Set Splash Video : {path}");
		}

	}

	#region 安卓

	private static void HandlerTargetCPUType()
	{
		var target = PlayerSettings.Android.targetArchitectures;
		if (TryGetEnv(CPU_TYPE, out var type) && !string.IsNullOrEmpty(type) && Enum.TryParse<AndroidArchitecture>(type, true, out var v))
			target = v;
		else
		{
			Console.WriteLine($":: {CPU_TYPE} not found, skipping setup, using default {target}");
			return;
		}
		Console.WriteLine($"::Set Cpu type : {target}");
		PlayerSettings.Android.targetArchitectures = target;

		if (TryGetEnv(BUILD_OPTIONS_SYMBOL, out string s) && !string.IsNullOrEmpty(s))
			EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Debugging;
		else
			EditorUserBuildSettings.androidCreateSymbols = AndroidCreateSymbols.Disabled;
	}

	#endregion

	#endregion

	[MenuItem("[Tools]/Build/DevBuild")]
	private static void BuildProject()
	{
		G_VAR_FILE = "@exts/buildcfgs/local_dev_dev.txt";
		PerformBuild();
		G_VAR_FILE = null;
	}

	[MenuItem("[Tools]/Build/SelectBuild")]
	private static void BuildProjectBySelect()
	{
		var file = EditorUtility.OpenFilePanelWithFilters("选择打包配置", "exts/buildcfgs", new string[] { "TextAsset", "txt" });
		if (!string.IsNullOrEmpty(file))
		{

			G_VAR_FILE = "@" + file;
			PerformBuild();
			G_VAR_FILE = null;
		}
	}
}
