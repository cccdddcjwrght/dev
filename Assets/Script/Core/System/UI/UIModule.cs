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
	[DisableAutoCreation]
	public class UIModule 
	{
		// 创建UI的原型
		static ILog log = LogManager.GetLogger("xl.ui");

		private SpawnUISystem     m_spawnSystem;
		private DespaenUISystem   m_despawnSystem;
		private UISystem          m_updateSystem;
		private GameWorld         m_gameWorld;
		private UIScriptFactory   m_factory;

		static  UIModule          s_module;

		private IPreprocess       m_preProcess;

		public UIModule(GameWorld gameWorld, IPreprocess preProcessing)
		{
			m_gameWorld       = gameWorld;
		     m_spawnSystem    = gameWorld.GetECSWorld().CreateSystem<SpawnUISystem>();
		     m_updateSystem   = gameWorld.GetECSWorld().CreateSystem<UISystem>();;
		     m_despawnSystem  = gameWorld.GetECSWorld().CreateSystem<DespaenUISystem>();
		     m_factory        = new UIScriptFactory();
		     m_preProcess    = preProcessing;
		     
		     // 不要FairyGUI管理
		     UIPackage.unloadBundleByFGUI = false;

		     // 更好的字体描边效果 
		     UIConfig.enhancedTextOutlineEffect = true;

		     m_spawnSystem.Initalize(m_gameWorld, m_factory, m_preProcess);
		     s_module = this;
		}
		
		public static UIModule Instance { get { return s_module; } }

		public void Reg(string comName, string pkgName, UIScriptFactory.CREATER creater)
		{
			UIInfo info = new UIInfo() { comName = comName, pkgName = pkgName };
			m_factory.Register(info, creater);
		}
		
		/// <summary>
		/// 结束
		/// </summary>
		public void Shutdown()
		{
			m_gameWorld.GetECSWorld().DestroySystem(m_spawnSystem);
			m_gameWorld.GetECSWorld().DestroySystem(m_updateSystem);
			m_gameWorld.GetECSWorld().DestroySystem(m_despawnSystem);
		}

		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			m_spawnSystem.Update();
			m_updateSystem.Update();
			m_despawnSystem.Update();
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		/// <param name="ui">要关闭的UI对象</param>
		/// <param name="win">Window对象</param>        
		public void CloseUI(Entity ui)
		{
			EntityManager mgr = m_gameWorld.GetECSWorld().EntityManager;
			if (mgr.Exists(ui))
			{
				if (mgr.HasComponent<DespawningEntity>(ui) == false)
				{
					// 该UI已经关闭 或正在销毁
					return;
				}

				mgr.AddComponent<DespawningEntity>(ui);
			}
		}
	}
}