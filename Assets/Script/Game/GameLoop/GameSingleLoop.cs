using log4net;
using SGame;
using UnityEngine;
using System.Collections.Generic;
using SGame.UI;
using Unity.Entities;
//[UpdateAfter()]

namespace SGame
{
	/// <summary>
	/// 单机逻辑
	/// </summary>
	public class GameSingleLoop :  IGameLoop
	{
		/// <summary>
		///  游戏状态
		/// </summary>
		public enum GAME_STATE
		{
			LOGIN	= 0, // 登录界面, 登录逻辑
			GAMEING = 1, // 游戏中
			LOBBY   = 2, // 大厅
		}
		
		/// <summary>
		/// 模块初始化
		/// </summary>
		void InitModule()
		{
			//GameLogicGroup.UpdateSystem();
			m_commonSystem		 = new SystemCollection();
			m_commonModule		 = new List<IModule>();
			var world			 = m_gameWorld;
			var ecsWorld		 = world.GetECSWorld();
			var randomSystem	 = RandomSystem.Instance;
			var propertyManager  = PropertyManager.Instance;

			randomSystem.Initalize((uint)Time.frameCount);
			propertyManager.Initalize();

			// 初始化UI
			InitalizeUI();
			
			// 数据中心
			var dataCenter = new DataCenter(m_gameWorld);
			dataCenter.Load();
			m_commonModule.Add(dataCenter);
			
			m_loginModule = new LoginModuleSingle(world);
			m_gameModule = new GameModuleSingle(world, m_resourceManager,  randomSystem);
			
			// 初始化语言
			DataCenter.Instance.setData.InitItemDataDic();

			AudioSystem.Instance.SetSoundVolume("BackgroundVolume",DataCenter.Instance.setData.musicVal);
			AudioSystem.Instance.SetSoundVolume("UIVolume",DataCenter.Instance.setData.soundVal);
	
			Firend.FriendModule.Instance.Initalize();

			ExclusiveModule.Instance.Initalize();
			ReputationModule.Instance.Initalize();
			
			NewbieGiftModule.Instance.Initalize();
		}

		/// <summary>
		///  初始化
		/// </summary>
		/// <param name="args">参数数组, 方便以后通过命令行启动</param>
		/// <return>初始化是否成功</return>
		public bool Init(string[] args)
		{
			m_stateMachine =		 new StateMachine<GAME_STATE>();
			
			// 客户端网络模块初始化
			m_gameWorld			= new GameWorld();
			
			// 资源管理系统
			m_resourceManager	= new ResourceManager(m_gameWorld);
			
			// 初始化状体机
			m_stateMachine.Add(GAME_STATE.GAMEING, ()=>m_gameModule.Enter(), () => m_gameModule.Update(), () => m_gameModule.Shutdown());
			m_stateMachine.Add(GAME_STATE.LOGIN, ()=>m_loginModule.Enter(), ()=>
			{
				m_loginModule.Update();
				if (m_loginModule.IsFinished)
					m_stateMachine.SwitchTo(GAME_STATE.GAMEING);
			}, ()=> m_loginModule.Shutdown());

			// 初始化模块
			InitModule();

			// 默认转换到登录状态
			m_stateMachine.SwitchTo(GAME_STATE.LOGIN);
			return true;
		}
		
		/// <summary>
		/// 游戏主循环
		/// </summary>
		public void Update()
		{
			float deltaTime = (float)GlobalTime.deltaTime;
			
			// 更新游戏时间
			m_gameTime.AddDuration(deltaTime);

			// 显示时间更新
			m_renderTime.AddDuration(deltaTime);
			
			// 公共模块更新
			foreach (var module in m_commonModule)
				module.Update();
			
			// 公共系统更新
			m_commonSystem.Update();

			// 状态机更新
			m_stateMachine.Update();

			m_gameWorld.ProcessDespawns();

			// 更新网络模块
			//NetworkManager.Instance.Update(deltaTime);
		}

		/// <summary>
		/// 保留手东注册示例代码
		/// </summary>
		public void InitalizeUI()
		{
			/// 后续该代码要做成自动化
			// 手动绑定登录的
			var	uiModule		= UIModule.Instance;
			uiModule.Initalize(m_gameWorld, new UIPreprocess());
			var	reg				= new UIReg();
			reg.RegAllUI(new UIContext() {uiModule = uiModule});
			
			uiModule.Reg("Login", "Login", UILogin.Create);
			uiModule.Reg("Progress", "Hud", HUDProgress.Create);
			uiModule.Reg("FloatText", "Hud", HUDFloatText.Create);
			uiModule.Reg("OrderTip", "Hud", HUDOrderTip.Create);
			uiModule.Reg("Effect", "Hud", HUDEffect.Create);
			uiModule.Reg("ReputationTip", "Hud", HUDReputationTip.Create);

			//uiModule.Reg("Hotfix", "Hotfix", UIHotfix.Create);
			SGame.UI.Login.LoginBinder.BindAll();
			
		}

		/// <summary>
		///  关闭系统
		/// </summary>
		public void Shutdown()
		{
			log.Info("Game Shutdown!");
			m_stateMachine.Shutdown();
			//NetworkManager.Instance.Shutdown();
			
			foreach (var m in m_commonModule)
				m.Shutdown();
			m_commonSystem.Shutdown(m_gameWorld.GetECSWorld());
			
			m_gameWorld.Shutdown();
		}

		/// <summary>
		/// 二次Update
		/// </summary>
		public void LateUpdate()
		{
			
		}

		///////////////////////////////////////////////////////// DATA ////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///  ECS 对象世界, 给使用者一个统一接口创建对象. 并将GameObject 与 Entity 的生命周期绑定(战斗系统必须)
		/// </summary>
		GameWorld                m_gameWorld;

		/// <summary>
		/// 公共模块
		/// </summary>
		private List<IModule>    m_commonModule;

		/// <summary>
		/// SYSTEM 组成的公共模块
		/// </summary>
		private SystemCollection m_commonSystem;

		/// <summary>
		/// 登录模块
		/// </summary>
		private LoginModuleSingle m_loginModule;

		private GameModuleSingle  m_gameModule;

		/// <summary>
		/// 游戏内资源加载接口
		/// </summary>
		ResourceManager          m_resourceManager;

		/// <summary>
		///  游戏时间, 游戏逻辑以该时间为准
		/// </summary>
		private GameTime         m_gameTime;
		
		/// <summary>
		/// 游戏对象的显示时间
		/// </summary>
		private GameTime          m_renderTime;

		/// <summary>
		///  状态机, 游戏中每个大模块是互斥的, 通过状态机管理
		/// </summary>
		StateMachine<GAME_STATE> m_stateMachine;

		private static ILog log = LogManager.GetLogger("xl.Game.MainLoop");
	}
}