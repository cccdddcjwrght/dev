using System.Collections;
using System.Collections.Generic;
using Fibers;
using log4net;
using SGame.UI;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
	// 主要用于运行单机游戏逻辑
	public class GameModuleSingle : IModule
	{
		private const string script = "Assets/BuildAsset/VisualScript/Prefabs/Game.prefab";
		private const string guidescript = "Assets/BuildAsset/VisualScript/Prefabs/Guide.prefab";

		GameObject guideGo;
		public GameModuleSingle(
			GameWorld gameWorld,
			ResourceManager resourceManager,
			RandomSystem randomSystem
			)
		{
			m_resourceManager = resourceManager;
			m_gameWorld = gameWorld;
		}

		public EntityManager EntityManager { get { return m_gameWorld.GetEntityManager(); } }

		private const float MOVE_INTERVAL_TIME = 0.0f;

		public void Update()
		{
			m_fiber.Step();
			GuideManager.Instance.Update();
		}

		/// <summary>
		/// 初始化游戏系统
		/// </summary>
		/// <returns></returns>
		void InitModule()
		{
			// 角色模块初始化
			CharacterModule.Instance.Initlaize();
			
			//处理模块
			RequestExcuteSystem.Instance.Init(this.m_gameWorld , m_resourceManager);

			//场景加载
			SGame.SceneSystemV2.Instance.SetUISys(UIUtils.WaitUIWithAnimation, UIUtils.CloseUI);

			//餐厅管理
			SGame.Dining.DiningRoomSystem.Instance.Init(this.m_gameWorld);

			//初始化属性系统
			AttributeSystem.Instance.Initalize();

			// 初始化订单系统
			OrderManager.Instance.Initalize();

			// 初始化桌子系统
			TableManager.Instance.Initalize();

			//科技数据初始化
			DataCenter.Instance.abilityData.InitAbilityList();

			GuideManager.Instance.Initalize();
			
			// 汽车模块初始化
			CarModule.Instance.Initalize();
		}

		IEnumerator TestData()
		{
			// 等1秒再创建角色
			/*
            yield return FiberHelper.Wait(1.0f);
            HudModule.Instance.ShowGameTips("ABC", new float3(0,0,0), TipType.BLUE);
            yield return  FiberHelper.Wait(1.0f);
            HudModule.Instance.ShowGameTips("DEF", new float3(2,0,0), TipType.YELLOW);

            HudModule.Instance.SystemTips("system tip aaa bbb");
            yield return FiberHelper.Wait(1.0f);
            HudModule.Instance.SystemTips("system cctv aaa bbb");
            yield return FiberHelper.Wait(1.0f);
            HudModule.Instance.SystemTips("system cctv2 aaa bbb");
            */
			yield break;
		}

		void OnStageChange(int stage)
		{
			if (stage == 6) EventManager.Instance.Trigger((int)GameEvent.ENTER_GAME);
		}

		IEnumerator Logic()
		{
			InitModule();
			// 初始化MASK
			UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("mask"));

			EventManager.Instance.Reg((int)GameEvent.GUIDE_CREATE, CreateGuide);
			EventManager.Instance.Reg<int>(((int)GameEvent.ENTER_ROOM), OnEnterRoom);

			//临时场景加载
			var ud = DataCenter.Instance.GetUserData();
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(ud.scene, OnStageChange);

			// 初始化角色系统
			m_hudModule = new HudModule(m_gameWorld);

			// 等待HUD 模块加载完成
			while (m_hudModule.IsReadly == false)
			{
				m_hudModule.Update();
				yield return null;
			}

			yield return TestData();


			// 游戏逻辑
			while (true)
			{
				// 防止进入死循环

				yield return null;
			}

		}

		void OnEnterRoom(int room)
		{
			log.Info("[game]Start Game Init");
			
			var prefab = m_resourceManager.LoadPrefab(script);
			log.Info("[game]LooadPrefab");
			var go = GameObject.Instantiate(prefab);
			log.Info("[game]End Game Init");
			
			CreateGuide();
			log.Info("[game]End Guild");
		}

		public void CreateGuide() 
		{
#if GAME_GUIDE
			if (Game.Instance.enableGuide)
			{
				//如果开场动画没播放玩重启游戏，这里跳过第一步
				if (DataCenter.Instance.guideData.guideId == 1 && !DataCenter.Instance.roomData.room.isnew)
					DataCenter.Instance.guideData.guideId++;
				GuideManager.Instance.StartGuide(DataCenter.Instance.guideData.guideId);
				//	if (guideGo != null) GameObject.Destroy(guideGo);

				//	var guidePrefab = m_resourceManager.LoadPrefab(guidescript);
				//	guideGo = GameObject.Instantiate(guidePrefab);
			}
#endif
		}

		public void Enter()
		{
			m_fiber = new Fiber(Logic());
		}

		public void Shutdown()
		{
		}

		private GameWorld m_gameWorld;
		private ResourceManager m_resourceManager;
		private Fiber m_fiber;


		// 游戏主逻辑
		private static ILog log = LogManager.GetLogger("xl.Game.Main");

		private ItemGroup m_userData;

		private UserSetting m_userSetting;

		private HudModule m_hudModule;
	}
}