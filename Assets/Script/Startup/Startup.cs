using UnityEngine;
using System.Reflection;
using System.Linq;
using HybridCLR;
using System;
using System.Collections.Generic;
using System.Collections;
using log4net;
using System.IO;
using libx;
using SGame.Hotfix;
using Sirenix.OdinInspector;
using Unity.Entities;

namespace SGame
{
	// 游戏启动类, 包括调用与加载热更新代码
	/// <summary>
	/// 游戏启动类, 启动核心逻辑与游戏等逻辑
	/// </summary>
	[DefaultExecutionOrder(-1000)]
	public class Startup : MonoBehaviour
	{
		[Serializable]
		public struct Module
		{
			public string m_dllName;
			public string m_className;
			public string m_funcName;
			public bool m_initECS;      // 是否初始化ECS
		}

		[ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "m_dllName")]
		public Module[] m_modules;

		// 热更新资源路径
		public const string HOTFIX_PATH = "Assets/BuildAsset/Code/";

		const string LogInitPath = "log4net.xml";

		// 初始日志
		private static ILog log = LogManager.GetLogger("startup");

		// 逻辑, 有三中模式
		// 1. 编辑器下直接使用改代码
		// 2. 编辑器下使用
		// Start is called before the first frame update
		void Start()
		{
			Debug.Log("App Start begin:" + Time.realtimeSinceStartup);
#if ENABLE_HOTFIX
			FiberCtrl.Pool.Run(Run()); 
#else
			StartCoroutine(NoHotFixRun());
#endif
		}

		// 基础模块初始化
		IEnumerator BaseModuleInitalize()
		{
			SetupAssetPath();

			Debug.Log("call 1!!!:" + Time.realtimeSinceStartup);

			// 1. 初始化日志
			TextAsset logAsset = Resources.Load<TextAsset>(LogInitPath);
			if (logAsset != null && logAsset.bytes != null)
			{
				using (MemoryStream ms = new MemoryStream(logAsset.bytes))
				{
					Debug.Log("logConfig bytes = " + logAsset.bytes.Length);
					log4net.Config.XmlConfigurator.Configure(ms);
				}
			}
			else
			{
				Debug.LogError("Log Init Fail!!!!!");
			}

			// 2. 初始化加载器
			// 资源加载初始化
			libx.ManifestRequest assetRequest = libx.Assets.Initialize();
			yield return assetRequest;
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				log.Error("Asset Bundle Load Fail=" + assetRequest.error);
				yield break;
			}
			Debug.Log("Assets InitSuccess:" + Time.realtimeSinceStartup);


			// 3. 初始化UI
			yield return null;
			/*
			// 4. 去报UDPATE 将Asset 销毁
			yield return null;
			*/
		}

		IEnumerator LoadModules()
		{
			// 先处理热更新
			foreach (var module in m_modules)
			{
				//Debug.Log("module =" + module.m_className);

				log.Info("Moudle Start Load =" + module.m_dllName);
				Type t = LoadDll(module.m_dllName, module.m_className, out Assembly assembly);
				if (t == null)
				{
					log.Error("Module Load Fail=" + module.m_dllName);
					break;
				}

				if (module.m_initECS == true)
				{
					InitializeECS(assembly);
				}

				MethodInfo method = t.GetMethod(module.m_funcName);
				if (method == null)
				{
					log.Error("Method Not Found=" + module.m_dllName);
					break;
				}

				IEnumerator iter = method.Invoke(null, null) as IEnumerator;
				if (iter == null)
				{
					log.Error("Method Return Value Is Not IEnumerator Module Name=" + module.m_dllName);
					break;
				}
				yield return iter;

				log.Info("Moudle Call Finish =" + module.m_dllName);
			}
		}

		IEnumerator LoadModule(Module module)
		{
			log.Info("Moudle Start Load =" + module.m_dllName);
			Type t = LoadDll(module.m_dllName, module.m_className, out Assembly assembly);
			if (t == null)
			{
				log.Error("Module Load Fail=" + module.m_dllName);
				yield break;
			}

			if (module.m_initECS == true)
			{
				InitializeECS(assembly);
			}

			MethodInfo method = t.GetMethod(module.m_funcName);
			if (method == null)
			{
				log.Error("Method Not Found=" + module.m_dllName);
				yield break;
			}

			IEnumerator iter = method.Invoke(null, null) as IEnumerator;
			if (iter == null)
			{
				log.Error("Method Return Value Is Not IEnumerator Module Name=" + module.m_dllName);
				yield break;
			}
			yield return iter;
			log.Info("Moudle Call Finish =" + module.m_dllName);
		}

		// 加载startup
		IEnumerator Run()
		{
			// 初始化基础公共对象
			yield return BaseModuleInitalize();

			Debug.Log("BaseModuleInitalize Finish:" + Time.realtimeSinceStartup);

			// 运行热更新
#if ENABLE_HOTFIX
				yield return HotfixModule.Instance.RunHotfix();
#endif

			Debug.Log("call 2!!!:" + Time.realtimeSinceStartup);

			LoadAOTMeta();

			yield return LoadModules();

			log.Info("LoadAOTMeta Finish");

			//Debug.Log("LoadAOTMeta game scene!!!");
			Debug.Log("App Start 2:" + Time.realtimeSinceStartup);

			// 销毁startup
			Destroy(gameObject);
		}

		IEnumerator NoHotFixRun()
		{
			SetupAssetPath();
			// 资源加载初始化
			libx.ManifestRequest assetRequest = libx.Assets.Initialize();
			yield return assetRequest;
			if (!string.IsNullOrEmpty(assetRequest.error))
			{
				log.Error("Asset Bundle Load Fail=" + assetRequest.error);
				yield break;
			}
			if (m_modules.Length > 0)
				yield return LoadModule(m_modules[0]);
			Debug.Log("App Start end:" + Time.realtimeSinceStartup);
			Destroy(gameObject);
		}

		Type LoadDll(string module, string className, out Assembly assembly)
		{
			Assembly hotUpdateAss = null;
			assembly = null;
#if !UNITY_EDITOR && ENABLE_HOTFIX
            // 实际加载
            AssetRequest req = libx.Assets.LoadAsset(HOTFIX_PATH + module + ".dll.bytes", typeof(TextAsset));
            if (req == null || req.asset == null)
                return null;
            var asset = req.asset as TextAsset;
            if (asset == null || asset.bytes == null)
                return null;
            hotUpdateAss = Assembly.Load(asset.bytes);
#else
			// Editor下无需加载，直接查找获得HotUpdate程序集
			hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == module);
#endif

			assembly = hotUpdateAss;
			Type t = hotUpdateAss.GetType(className);
			return t;
		}


		/// <summary>
		/// 加载AOT 原数据
		/// </summary>
		void LoadAOTMeta()
		{
			//Assembly.GetAssembly()
			//Assembly.GetEntryAssembly()

#if !UNITY_EDITOR && ENABLE_HOTFIX
            const string AOTBasePath = "Assets/BuildAsset/AOTMeta/";
			var files = IniUtils.GetAotFiles();
            foreach (var aotDllName in files)
            {
                string dllPath = AOTBasePath + aotDllName + ".bytes";
                var req = Assets.LoadAsset(dllPath, typeof(TextAsset));
                var asset = req.asset as TextAsset;
                byte[] dllBytes = asset.bytes;
                LoadImageErrorCode err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
                if (err != LoadImageErrorCode.OK)
                {
                    log.Error("Load Dll Meta Fail File=" + aotDllName + " err=" + err);
                }
                req.Release();
            }
#endif
		}

		// 设置加载路径
		static public void SetupAssetPath()
		{
#if !UNITY_EDITOR
			Assets.updatePath = GetUpdatePath();
#endif
		}

		// 更新目录
		public static string GetUpdatePath()
		{
			return string.Format("{0}{1}DLC{1}", Application.persistentDataPath, Path.DirectorySeparatorChar);
		}

		static bool ECS_Initalize = false;

		// 解决场景初始话可能导致失败的问题
		//[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeECS(Assembly asselbly)
		{
			if (ECS_Initalize == true)
			{
				Debug.LogError("ECS Reinitalize!!!!");
				return;
			}
			ECS_Initalize = true;

#if UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP
#if !UNITY_EDITOR && ENABLE_HOTFIX
				Debug.Log("BEGIN ECS TYPES...");
				var dotsAssemblies = new Assembly[] {asselbly};
				var componentTypes = new HashSet<System.Type>();
				TypeManager.CollectComponentTypes(dotsAssemblies, componentTypes);
				var components = componentTypes.ToArray();
				foreach (var c in components)
					Debug.Log("reg ecs component=" + c.FullName);
				TypeManager.AddNewComponentTypes(components);
				TypeManager.EarlyInitAssemblies(dotsAssemblies);
#endif

			log.Info("UNITY_DISABLE_AUTOMATIC_SYSTEM_BOOTSTRAP Init!");
			Unity.Entities.DefaultWorldInitialization.Initialize("Default World", false);
			Debug.Log("ECS Inited");
#endif
		}
	}

}