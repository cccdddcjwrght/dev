using SGame;

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
			m_character			= new CharacterModule(world, resourceManager);
			// m_snake             = new SnakeModule(world, resourceManager);
		}

		/// <summary>
		/// 游戏主循环
		/// </summary>
		public void Update(float deltaTime)
		{
			// 更新游戏时间
			m_gameTime.AddDuration(deltaTime);

			m_renderTime.AddDuration(deltaTime);
			
			// 角色模块更新
			m_character.Update();

			// 貪吃蛇模塊更新
			//m_snake.Update();
		}


		/// <summary>
		///  关闭系统
		/// </summary>
		public void Shutdown()
		{
			m_character.Shutdown();
			//m_snake.Shutdown();
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
		private CharacterModule m_character;

		/// <summary>
		/// 貪吃蛇模塊
		/// </summary>
		//private SnakeModule m_snake;
	}
}