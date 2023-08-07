using System.Collections.Generic;
using Unity.Entities;
using log4net;
using FairyGUI;
using Unity.Collections;
using UnityEngine;
using System;

//*******************************************************************
//	创建日期:	2016-8-22
//	文件名称:	UISystem.cs
//  创 建 人:   Silekey
//	版权所有:	
//	说    明:	用于 绑定assetbundle 与 fairygui 包, 通过fairygui 包直接加载bundle并返回
//              UIPackage 没有卸载功能(应该给UIPackage 加一个引用计数, 然后当引用数为0, 可以对UIPackage 进行Dispose)
//*************************************************************************
// example: 
// var ui = UISystem.Instance.ShowwUI("name");
// var req = EntityManager.GetComponentObject<UIRequest>(ui);
// yield return req;
// var uiWindow = EntityManager.GetComponentObject<UIWindow>(ui);
// var component1 = uiWindow.Value.GetChild("child1")
// UISystem.Instance.CloseUI(ui); 

namespace SGame.UI
{
	public class UISystem : SystemBase
	{
		private const string ROOT_PATH = "Assets/BuildAsset/";
		private const string DESC_BUNDLE_FMT = ROOT_PATH + "UI/{0}_fui.bytes";
		private const string RES_BUNDLE_FMT = ROOT_PATH + "UI/{0}";


		private SpawnUISystem     m_spawnSystem;
		private DespaenUISystem   m_despawnSystem;

		// UI管理 , 缓存UI对象
		private Dictionary<string, Entity> m_uiRecored;

		// UI显示的堆栈
		private Stack<string> m_uiStack;

		// 基础UI
		private Action<object> m_luaInitPanel;

		// 创建UI的原型
		static ILog log = LogManager.GetLogger("UISystem");

		// UI对象的查询条件
		private EntityQuery m_groupUIWindow;

		// UI请求对象
		private EntityQuery m_groupRequest;

		// 有效的显示
		private EntityQuery m_groupVisible;

		public static UISystem Instance
		{
			get
			{
				return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<UISystem>();
			}
		}

		/// <summary>
		///  系统创建  
		/// </summary>
		protected override void OnCreate()
		{
			base.OnCreate();

			m_spawnSystem = World.GetOrCreateSystem<SpawnUISystem>();
			m_despawnSystem = World.GetOrCreateSystem<DespaenUISystem>();

			// 不要FairyGUI管理
			UIPackage.unloadBundleByFGUI = false;

			// 更好的字体描边效果 
			UIConfig.enhancedTextOutlineEffect = true;

			//当ui对象创建时，进行本地化处理
			UIPackage.onObjectCreate += OnUIObjectCreate;

			// FairyGUI 包加载模块
			//m_packageRequest = new ResourceLoader<UIPackageRequest>();

			// UI记录
			m_uiRecored = new Dictionary<string, Entity>();
			m_uiStack = new Stack<string>();
			
			m_groupUIWindow = GetEntityQuery(typeof(UIWindow));
			m_groupVisible = GetEntityQuery(typeof(UIWindow),
																							ComponentType.Exclude<UIClosed>(),
																							ComponentType.Exclude<UICloseEvent>(),
																							ComponentType.Exclude<UIDestroy>());

			m_groupRequest = GetEntityQuery(typeof(UIRequest), ComponentType.Exclude<UIWindow>(),
				ComponentType.Exclude<UIDestroy>());

		}

		/// <summary>
		///  ECS 开始运行前事件
		/// </summary>
		protected override void OnStartRunning()
		{
			base.OnStartRunning();

			// 全局配置UI
			log.Info("UI Config Success!");
		}

		/// <summary>
		///  ECS System 销毁事件  
		/// </summary>
		protected override void OnDestroy()
		{
			// 清空UI信息
			UIWindow[] windows = m_groupUIWindow.ToComponentDataArray<UIWindow>();
			foreach (var w in windows)
			{
				w.Dispose();
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		protected override void OnUpdate()
		{
			float deltaTime = Time.DeltaTime;
			
			// 1. UI更新
			Entities.WithAll<UIInitalized>().WithNone<UIWindow, UIDestroy>().ForEach((Entity e, UIWindow win) =>
			{
				if (win.Value.isReadly == false)
					return;

				win.Value.OnFrameUpdate(deltaTime);
			});
			
			m_spawnSystem.Update();
			m_despawnSystem.Update();
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		/// <param name="ui">要关闭的UI对象</param>
		/// <param name="win">Window对象</param>        
		public void CloseUI(Entity ui)
		{
			if (EntityManager.Exists(ui))
			{
				if (EntityManager.HasComponent<UIClosed>(ui) ||
					EntityManager.HasComponent<UICloseEvent>(ui) ||
					EntityManager.HasComponent<UIDestroy>(ui))
				{
					// 该UI已经关闭 或正在销毁
					return;
				}

				EntityManager.AddComponent<UICloseEvent>(ui);
			}
		}
		
		static void Log(string msg)
		{
			log.Debug(msg);
		}
		
		/// <summary>
		/// 将res 的路径换为真实的bundle资源路径
		/// </summary>
		/// <param name="name">UI 包名</param>
		/// <return>bundle资源路径</return>
		private static string RuledAssetBundleName(string name)
		{
			return libx.Utility.RuledAssetBundleName(name);
		}

#if UNITY_EDITOR
		public static string GetPackageDescPath(string pkgName)
		{
			string descBundlePath = string.Format(RES_BUNDLE_FMT, pkgName);
			return descBundlePath;
		}
#endif

		/// <summary>
		/// 将res 的路径换为真实的 bundle资源路径
		/// </summary>
		/// <param name="pkgName">UI 包名</param>
		/// <param name="descBundlePath">UI 描述bundle路径</param>
		/// <param name="resBundlePath">UI 资源bundle路径</param>
		/// <return>是否有效</return>
		public static bool GetPackageDescAndResName(string pkgName, out string descBundlePath, out string resBundlePath)
		{
			var descFilePath = string.Format(DESC_BUNDLE_FMT, pkgName).ToLower();
			resBundlePath = string.Format(RES_BUNDLE_FMT, pkgName).ToLower();
			log.Info("desc bundlePath =" + descFilePath);
			log.Info("res bundlePath =" + resBundlePath);

			//// 由于bundle 经过了MD5 的转换, 因此获取bundle路径时要再转换一次
			//// 通过资源路径获得bundle 名称
			libx.Assets.GetAssetBundleName(descFilePath, out descBundlePath);

			// 将res 的路径换为真实的
			resBundlePath = RuledAssetBundleName(resBundlePath);

			log.Info("desc bundlePath =" + descBundlePath);
			log.Info("res bundlePath =" + resBundlePath);
			return true;
		}
	}
}