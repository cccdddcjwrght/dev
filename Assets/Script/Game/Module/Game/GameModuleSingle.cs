using System.Collections;
using System.Collections.Generic;
using Fibers;
using log4net;
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

        IEnumerator Logic()
        {
			//临时场景加载
			var req = SceneSystemV2.Instance.Load("Level01");
			req.logic = default;
			yield return req;

            var prefab = m_resourceManager.LoadPrefab(script);
            var go = GameObject.Instantiate(prefab);
            
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
    }
}