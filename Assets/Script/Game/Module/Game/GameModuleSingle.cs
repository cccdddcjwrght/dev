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

        
        public GameModuleSingle(
            GameWorld       gameWorld,
            ResourceManager resourceManager,
            RandomSystem    randomSystem
            )
        {
            m_resourceManager = resourceManager;
            m_gameWorld       = gameWorld;
        }
        
        public EntityManager EntityManager { get { return m_gameWorld.GetEntityManager(); } }

        private const float MOVE_INTERVAL_TIME = 0.0f;
        
        public void Update()
        {
            m_fiber.Step();
        }

        /// <summary>
        /// 初始化游戏系统
        /// </summary>
        /// <returns></returns>
        void InitModule()
        {
			//处理模块
			RequestExcuteSystem.Instance.Init(this.m_gameWorld);

			//场景加载
			SGame.SceneSystemV2.Instance.SetUISys(UIUtils.WaitUI, UIUtils.CloseUI);
            
			//餐厅管理
			SGame.Dining.DiningRoomSystem.Instance.Init();

			//初始化属性系统
			AttributeSystem.Instance.Initalize();
            
			// 初始化订单系统
            OrderManager.Instance.Initalize();

            // 初始化桌子系统
            TableManager.Instance.Initalize();
        }

        IEnumerator TestData()
        {
            // 等1秒再创建角色
            yield return FiberHelper.Wait(1.0f);
            // CharacterSpawn.Create(1, Vector3.zero);
            //CharacterModule.Instance.Create(4, Vector3.zero);
        }

        IEnumerator Logic()
        {
            InitModule();

			//临时场景加载
			var ud = DataCenter.Instance.GetUserData();
			yield return Dining.DiningRoomSystem.Instance.LoadRoom(ud.scene);
            
            // 初始化MASK
            UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("mask"));
            
            // 初始化角色系统
            m_hudModule = new HudModule(m_gameWorld);
            
            // 等待HUD 模块加载完成
            while (m_hudModule.IsReadly == false)
            {
                m_hudModule.Update();
                yield return null;
            }
            
            // 播放背景
            AudioSystem.Instance.Play((int)AudioDefine.BGM_LEVEL);

            var prefab = m_resourceManager.LoadPrefab(script);
            var go = GameObject.Instantiate(prefab);

            yield return TestData();
            
            // 游戏逻辑
            while (true)
            {
                // 防止进入死循环
                
                yield return null;
            }
            
            GameObject.Destroy(go);
        }


        public void Enter()
        {
            m_fiber           = new Fiber(Logic());
        }

        public void Shutdown()
        {
        }
        
        private GameWorld           m_gameWorld      ;
        private ResourceManager     m_resourceManager;
        private Fiber               m_fiber          ;
        

        // 游戏主逻辑
        private static ILog log = LogManager.GetLogger("xl.Game.Main");
        
        private ItemGroup           m_userData;

        private UserSetting         m_userSetting;

        private HudModule m_hudModule;
    }
}