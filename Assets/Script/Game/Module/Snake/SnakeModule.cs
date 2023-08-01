using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇游戲模塊
    /// </summary>
    public class SnakeModule
    {
        // 游戲世界對象
        private GameWorld        m_world;
        
        // 資源加載器
        private ResourceManager  m_resourceManager;

        private EntityManager    m_entityManager;

        private SnakeSpawnSystem m_spawnSystem;

        private SnakeMoveSystem  m_moveSystem;

        private SnakeBodyMoveSystem  m_bodyMoveSystem;

        private SnakeControlSystem m_controlSystem;

        private const string SNAKE_PREFAGB = "Assets/BuildAsset/Prefabs/Snake/Snake.prefab";

        private Entity m_snakePrefab;

        public SnakeModule(GameWorld world, ResourceManager resourceManager)
        {
            m_world            = world;
            m_entityManager    = world.GetECSWorld().EntityManager;
            m_resourceManager  = resourceManager;

            m_controlSystem = m_world.GetECSWorld().CreateSystem<SnakeControlSystem>();
            m_spawnSystem      = m_world.GetECSWorld().CreateSystem<SnakeSpawnSystem>();
            m_moveSystem       = m_world.GetECSWorld().CreateSystem<SnakeMoveSystem>();
            m_bodyMoveSystem   = m_world.GetECSWorld().CreateSystem<SnakeBodyMoveSystem>();
            
            
            m_spawnSystem.Initalize(resourceManager);
            
            m_snakePrefab      = m_resourceManager.GetEntityPrefab(SNAKE_PREFAGB);
            
            TestRun();
        }

        void TestRun()
        {
            // 創建一個貪吃蛇
            Entity e1 = m_entityManager.Instantiate(m_snakePrefab);
            //Entity e2 = m_entityManager.Instantiate(m_snakePrefab);
            m_entityManager.GetComponentObject<SnakeData>(e1).m_name = "abc1";
            //m_entityManager.GetComponentObject<SnakeData>(e2).m_name = "abc2";
        }

        /// <summary>
        /// 創建一個貪吃蛇對象
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Entity CreatSnake(float2 startPosition, int num)
        {
            var e = m_entityManager.CreateEntity();
            var snake = SnakeData.Create(startPosition, num);
            m_entityManager.AddComponentObject(e, snake);
            return e;
        }

        /// <summary>
        /// 模塊更新
        /// </summary>
        public void Update()
        {
            m_controlSystem.Update();
            m_spawnSystem.Update();
            m_moveSystem.Update();
            m_bodyMoveSystem.Update();;
        }

        /// <summary>
        /// 模塊初始話
        /// </summary>
        public void Shutdown()
        {
            m_world.GetECSWorld().DestroySystem(m_controlSystem);
            m_world.GetECSWorld().DestroySystem(m_spawnSystem);
            m_world.GetECSWorld().DestroySystem(m_moveSystem);
            m_world.GetECSWorld().DestroySystem(m_bodyMoveSystem);
        }
    }
}