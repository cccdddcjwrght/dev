using log4net;
using SGame;
using UnityEngine;

namespace SGame
{
	public class GameClientLoop : IGameLoop
	{
		/// <summary>
		/// 游戏主循环
		/// </summary>
		public void Update()
		{
			float deltaTime = (float)GlobalTime.deltaTime;

			m_stateMachine.Update();

			m_gameWorld.ProcessDespawns();

			// 更新网络模块
			NetworkManager.Instance.Update(deltaTime);
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

			m_stateMachine.Add(GAME_STATE.GAMEING, GameEnter, GameUpdate, GameLeave);
			
			// 默认转换到登录状态
			m_stateMachine.SwitchTo(GAME_STATE.GAMEING);

			return true;
		}

		/// <summary>
		/// 场景加载完毕, 进入游戏
		/// </summary>
		private void GameEnter()
		{
			log.Info("Game Enter!");
			m_gameClientWorld = new GameClientWorld(m_gameWorld, m_resourceManager);
		}

		/// <summary>
		/// 游戏运行中的主循环更新游戏逻辑
		/// </summary>
		private void GameUpdate()
		{
			// 更新游戏时间
			float deltaTime = (float)GlobalTime.deltaTime;
			m_gameClientWorld.Update(deltaTime);

			m_gameWorld.ProcessDespawns();
		}

		/// <summary>
		/// 游戏模块离开
		/// </summary>
		private void GameLeave()
		{
			log.Info("Game Leave!");
			m_gameClientWorld.Shutdown();
		}


		/// <summary>
		///  关闭系统
		/// </summary>
		public void Shutdown()
		{
			log.Info("Game Shutdown!");
			m_stateMachine.Shutdown();
			NetworkManager.Instance.Shutdown();

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
		///  游戏状态
		/// </summary>
		public enum GAME_STATE
		{
			LOGIN = 0,   // 登录界面, 登录逻辑
			GAMEING = 1, // 游戏中
		}

		/// <summary>
		///  ECS 对象世界, 给使用者一个统一接口创建对象. 并将GameObject 与 Entity 的生命周期绑定(战斗系统必须)
		/// </summary>
		GameWorld m_gameWorld;

		/// <summary>
		///  游戏世界, 里面包含主要的处理罗i就
		/// </summary>
		GameClientWorld m_gameClientWorld;

		/// <summary>
		/// 游戏内资源加载接口
		/// </summary>
		ResourceManager m_resourceManager;

		/// <summary>
		///  状态机, 游戏中每个大模块是互斥的, 通过状态机管理
		/// </summary>
		StateMachine<GAME_STATE> m_stateMachine;

		private static ILog log = LogManager.GetLogger("xl.Game.MainLoop");
	}
}