using System.Collections.Generic;
using Unity.Entities;
using log4net;
using FairyGUI;
using Unity.Collections;
using UnityEngine;
using System;
using System.Linq;

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
	public class UIModule
	{
		// 创建UI的原型
		static ILog log = LogManager.GetLogger("xl.ui");

		private SpawnUISystem m_spawnSystem;
		private DespawnUISystem m_despawnSystem;
		private GameWorld m_gameWorld;
		private UIScriptFactory m_factory;

		static UIModule s_module;

		private IPreprocess m_preProcess;

		// 有效的显示
		private EntityQuery m_groupVisible;

		// UI状态
		public enum UI_STATE
		{
			EMPTY    = 0,	// 不是UI
			REQUEST  = 1,   // UI 请求中
			SHOWING  = 2,   // 正在显示
			SHOWED   = 3,   // 已经显示
			CLOSING  = 4,   // 正在关闭
			CLOSED   = 5,   // 已经关闭
			DISPOSED = 6,   // 已经销毁
		}

		private Dictionary<int, Entity> m_singletonUI;

		public UIModule()
		{
			s_module = this;
			m_singletonUI = new Dictionary<int, Entity>();
		}

		/// <summary>
		/// 初始化UI
		/// </summary>
		/// <param name="gameWorld"></param>
		/// <param name="preProcessing"></param>
		public void Initalize(GameWorld gameWorld, IPreprocess preProcessing)
		{
			m_gameWorld = gameWorld;
			m_spawnSystem = gameWorld.GetECSWorld().GetOrCreateSystemManaged<SpawnUISystem>();
			m_despawnSystem = gameWorld.GetECSWorld().GetOrCreateSystemManaged<DespawnUISystem>();

			if (m_factory != null)
				m_factory.Dispose();

			m_factory = new UIScriptFactory();
			m_preProcess = preProcessing;

			m_groupVisible = m_gameWorld.GetEntityManager().CreateEntityQuery(typeof(UIWindow),
				ComponentType.Exclude<DespawningUI>());

			// 不要FairyGUI管理
			UIPackage.unloadBundleByFGUI = false;

			// 更好的字体描边效果 
			UIConfig.enhancedTextOutlineEffect = true;

			m_spawnSystem.Initalize(m_gameWorld, m_factory, m_preProcess);
		}

		public static UIModule Instance
		{
			get
			{
				if (s_module == null)
					s_module = new UIModule();

				return s_module;
			}
		}

		/// <summary>
		/// 手动注册UI
		/// </summary>
		/// <param name="comName">元件名</param>
		/// <param name="pkgName">包名</param>
		/// <param name="creater">脚本创建器</param>
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
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		/// <param name="ui">要关闭的UI对象</param>
		/// <param name="win">Window对象</param>        
		public bool CloseUI(Entity ui, bool isDispose = false)
		{
			EntityManager mgr = m_gameWorld.GetECSWorld().EntityManager;
			if (mgr.Exists(ui))
			{
				if (mgr.HasComponent<DespawningUI>(ui))
				{
					// 该UI已经关闭 或正在销毁
					return false;
				}

				try
				{
					if (mgr.HasComponent<UIWindow>(ui))
					{
						UIWindow window = mgr.GetComponentData<UIWindow>(ui);
						log.Debug("close ui=" + window.BaseValue.configID + " name=" + window.BaseValue.uiname);
						if (window.BaseValue.isClosing)
						{
							log.Warn("repeate closing ui=" + ui);
							return true;
						}

						if (window.BaseValue != null)
						{
							window.BaseValue.Close();
							return true;
						}
					}
					else
					{
						mgr.AddComponentData(ui, new DespawningUI() { isDispose = isDispose });
						log.Error("is not ui=" + ui);
						//m_despawnSystem.RemoveEntity(ui);
						return false;
					}
				}
				catch (Exception e)
				{
					log.Error(e.Message + "-" + e.StackTrace);
				}
			}

			return false;
		}

		public void CloseAllUI(params string[] ignore)
		{
			var wins = GetVisibleUI();
			if (wins?.Count > 0)
			{
				foreach (var w in wins)
				{
					if (ignore.Length > 0 && ignore.Contains(w.name)) continue;
					CloseUI(w.entity);
				}
			}
		}

		public List<UIWindow> GetVisibleUI(bool includeNoParent = false)
		{
			List<UIWindow> ret = new List<UIWindow>();
			UIWindow[] windows = m_groupVisible.ToComponentDataArray<UIWindow>();
			foreach (var w in windows)
			{
				if (w != null && w.BaseValue != null && !w.BaseValue.isClosing && (includeNoParent || w.BaseValue.isShowing))
				{
					ret.Add(w);
				}
			}

			return ret;
		}

		/// <summary>
		/// 检测UI是否已经打开
		/// </summary>
		/// <param name="ui"></param>
		/// <returns></returns>
		public bool CheckOpened(Entity ui)
		{
			var entityManager = m_gameWorld.GetEntityManager();
			return entityManager.HasComponent<UIInitalized>(ui) && !entityManager.HasComponent<DespawningUI>(ui);
		}

		/// <summary>
		/// 获取UI对象
		/// </summary>
		/// <param name="ui">ui 名称</param>
		/// <returns></returns>
		public Entity GetUI(string ui)
		{
			UIWindow[] windows = m_groupVisible.ToComponentDataArray<UIWindow>();
			foreach (var w in windows)
			{
				if (w != null && w.name == ui)
				{
					return w.entity;
				}
			}

			return Entity.Null;
		}

		public Entity GetUI(int ui)
		{
			UIWindow[] windows = m_groupVisible.ToComponentDataArray<UIWindow>();
			foreach (var w in windows)
			{
				if (w != null && w.BaseValue.configID!=0 && w.BaseValue.configID == ui)
				{
					return w.entity;
				}
			}

			return Entity.Null;
		}

		public bool ReShow(int id , out Entity entity)
		{
			entity = GetUI(id);
			if (entity != default && UIModule.Instance.CheckOpened(entity))
			{
				var win = GetEntityManager().GetComponentData<UIWindow>(entity);
				if (win != null)
				{
					win.BaseValue.Show();
					return true;
				}
			}
			return false;
		}

		public EntityManager GetEntityManager()
		{
			return m_gameWorld.GetEntityManager();
		}

		/// <summary>
		/// 获取UI状态
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public UI_STATE GetUIState(Entity e)
		{
			var EntityManager = m_gameWorld.GetEntityManager(); //World.DefaultGameObjectInjectionWorld.EntityManager;
			if (!EntityManager.Exists(e))
				return UI_STATE.DISPOSED;
			
			bool hasRequest = EntityManager.HasComponent<UIRequest>(e);
			if (hasRequest)
				return UI_STATE.REQUEST;
			
			bool hasWindow = EntityManager.HasComponent<UIWindow>(e);
			if (!hasWindow)
				return UI_STATE.EMPTY;

			UIWindow w = EntityManager.GetComponentObject<UIWindow>(e);
			if (w.BaseValue == null || w.BaseValue.isDisposed)
				return UI_STATE.DISPOSED;
			if (w.BaseValue.isClosing)
			{
				return w.BaseValue.isClosed ? UI_STATE.CLOSED : UI_STATE.CLOSING;
			}

			if (w.BaseValue.isShowing)
			{
				return w.BaseValue.isReadyShowed ? UI_STATE.SHOWING : UI_STATE.SHOWED;
			}

			if (EntityManager.HasComponent<DespawningUI>(e))
				return UI_STATE.CLOSING;
			return UI_STATE.CLOSED;
		}

		/// <summary>
		/// 显示单列UI, 只能显示WINDOW类型
		/// </summary>
		/// <param name="configID">配置表ID</param>
		/// <returns></returns>
		public Entity ShowSingleton(int configID)
		{
			var EntityManager = m_gameWorld.GetEntityManager();
			if (m_singletonUI.TryGetValue(configID, out Entity e))
			{
				UI_STATE state = GetUIState(e);
				switch (state)
				{
					// 请求中或显示了 就直接返回UI无需重复创建
					case UI_STATE.REQUEST:
					case UI_STATE.SHOWING:
					case UI_STATE.SHOWED:
						return e;
					
					case UI_STATE.CLOSING:
					case UI_STATE.CLOSED:
						{
							// 调用过关闭, 就需要重新打开
							if (EntityManager.HasComponent<DespawningUI>(e))
								EntityManager.RemoveComponent<DespawningUI>(e);
							
							UIWindow win = EntityManager.GetComponentObject<UIWindow>(e);
							m_preProcess?.ReOpen(win.BaseValue.context);
							win.BaseValue.Reopen();
							m_preProcess?.AfterShow(win.BaseValue.context);
							return e;
						}
					default:
							break;
				}
			}

			// UI没有打开, 创建UI显示请求
			var newEntity = EntityManager.CreateEntity();
			EntityManager.AddComponentObject(newEntity, new UIRequest()
			{
				configId = configID,
				type	 = UI_TYPE.WINDOW,
				parent   = null
			});

			if (!m_singletonUI.ContainsKey(configID))
				m_singletonUI.Add(configID, newEntity);
			else
				m_singletonUI[configID] = newEntity;
			return newEntity;
		}
	}
}