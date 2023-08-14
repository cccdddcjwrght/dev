using SGame;
using SGame.UI;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// 游戏逻辑, 新加入的游戏模块都在这里加入!
	/// </summary>
	public class GameClientWorld
	{
		public GameClientWorld(GameWorld world, ResourceManager resourceManager)
		{
			m_gameWorld			= world;
			m_resourceManager	= resourceManager;
			m_randomSystem      = RandomSystem.Instance;

			m_uiModule          = new UIModule(world, new UIPreprocess());

			m_character			= new CharacterModule(world, resourceManager);
			m_dice              = new DiceModule(world, resourceManager);
			
			m_snake			    = new SnakeModule(world, resourceManager);
			m_syncSystem        = world.GetECSWorld().CreateSystem<EntitySyncGameObjectSystem>();
			m_userInputSystem   = world.GetECSWorld().CreateSystem<UserInputsystem>();

			m_dataCenter        = new DataCenter(world);

			m_randomSystem.Initalize((uint)Time.frameCount);
			m_gameModule        = new GameModule(world, 
				resourceManager, 
				m_randomSystem, 
				m_userInputSystem, 
				m_character, 
				m_dice);
			m_tileEventModule = new TileEventModule(world);

			InitalizeUI();

			SGame.UI.UIReg reg = new UIReg();
			reg.RegAllUI(new UIContext() {uiModule = m_uiModule});

		}

		public void InitalizeUI()
		{
			/// 后续该代码要做成自动化
			//m_uiModule.Reg("MainUI", "Main", UIMain.Create);
			m_uiModule.Reg("Login", "Login", UILogin.Create);
			m_uiModule.Reg("Hotfix", "Hotfix", UIHotfix.Create);
			m_uiModule.Reg("LoadingUI", "Loading", UILoading.Create);
		}

		/// <summary>
		/// 游戏主循环
		/// </summary>
		public void Update(float deltaTime)
		{
			// 更新游戏时间
			m_gameTime.AddDuration(deltaTime);

			// 显示时间更新
			m_renderTime.AddDuration(deltaTime);
			
			// 更新UI系统
			m_uiModule.Update();

			// 输入系统
			m_userInputSystem.Update();
			
			// 角色模块更新
			m_character.Update();
			
			// 骰子更新
			m_dice.Update();
			
			// 游戏逻辑模块
			m_gameModule.Update();
			
			// 游戏内场景事件
			m_tileEventModule.Update();

			// 同步模块
			m_syncSystem.Update();
			
			// 数据中心更新
			m_dataCenter.Update();
		}

		/// <summary>
		///  关闭系统
		/// </summary>
		public void Shutdown()
		{
			m_character.Shutdown();
			m_dice.Shutdown();
			m_gameModule.Shutdown();
			m_gameWorld.GetECSWorld().DestroySystem(m_syncSystem);
			m_gameWorld.GetECSWorld().DestroySystem(m_userInputSystem);
			m_tileEventModule.Shutdown();
			m_uiModule.Shutdown();
			m_dataCenter.Shutdown();
		}

		public void LateUpdate()
		{
		}


		/// <summary>
		///  ECS 对象世界, 给使用者一个统一接口创建对象. 并将GameObject 与 Entity 的生命周期绑定(战斗系统必须)
		/// </summary>
		GameWorld m_gameWorld;

		/// <summary>
		///  游戏时间, 游戏逻辑以该时间为准
		/// </summary>
		GameTime m_gameTime;
		
		

		/// <summary>
		/// 游戏对象的显示时间
		/// </summary>
		private GameTime m_renderTime;

		/// <summary>
		/// 游戏内的资源管理
		/// </summary>
		private ResourceManager m_resourceManager;

		/// <summary>
		/// 角色模块
		/// </summary>
		private CharacterModule              m_character;

		/// <summary>
		/// 骰子模块 
		/// </summary>
		private DiceModule                    m_dice;

		/// <summary>
		/// 游戏内容模块, 处理游戏中的流程
		/// </summary>
		private GameModule                    m_gameModule;
		
		/// <summary>
		/// 同步系统
		/// </summary>
		private EntitySyncGameObjectSystem    m_syncSystem;

		/// <summary>
		/// 随机系统
		/// </summary>
		private RandomSystem                  m_randomSystem;

		/// <summary>
		/// 用户输入系统
		/// </summary>
		/// <returns></returns>
		private UserInputsystem               m_userInputSystem;

		/// <summary>
		/// UI系统
		/// </summary>
		private UIModule                       m_uiModule;

		/// <summary>
		/// 地块事件系统
		/// </summary>
		private TileEventModule                m_tileEventModule;

		/// <summary>
		/// 数据中心
		/// </summary>
		private DataCenter                     m_dataCenter;
		

		private SnakeModule                    m_snake;
	}
}