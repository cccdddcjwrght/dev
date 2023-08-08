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
	public partial class UISystem : SystemBase
	{

		private SpawnUISystem     m_spawnSystem;
		private DespaenUISystem   m_despawnSystem;
		

		// 创建UI的原型
		static ILog log = LogManager.GetLogger("xl.ui");

		// UI对象的查询条件
		private EntityQuery    m_groupUIWindow;

		// 有效的显示
		private EntityQuery    m_groupVisible;

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

			m_groupUIWindow = GetEntityQuery(typeof(UIWindow));
			m_groupVisible = GetEntityQuery(typeof(UIWindow),
																							ComponentType.Exclude<DespawningEntity>());
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
			Entities.WithAll<UIInitalized>().ForEach((Entity e, UIWindow win) =>
			{
				if (win.Value.isReadly == false)
					return;

				win.Value.OnFrameUpdate(deltaTime);
			}).WithoutBurst().Run();
			
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
	}
}