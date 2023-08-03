using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 角色模块, 控制角色所有行为, 包括角色创建, 角色移动, 
    /// </summary>
    public class CharacterModule
    {
        // 所有的和移动优化的系统
        private SystemCollection                m_moveCollection;

        // 角色创建
        private HandleCharacterSpawnRequests    m_spawerRequest;

        // 角色初始化系统
        private CharacterSpawnSystem            m_spawnSystem;
        
        // 游戏世界对象
        private GameWorld                       m_world;

        // 资源加载管理
        private ResourceManager                 m_resourceManager;

        private EntityManager                   m_entityManager;
        private static CharacterModule          s_instance;
        
        private Entity                          m_character;

        public static CharacterModule Insatance
        {
            get { return s_instance; }
        }

        public CharacterModule(GameWorld world, ResourceManager resourceManager)
        {
            s_instance = this;
            m_world                         = world;
            m_resourceManager               = resourceManager;
            m_moveCollection                = new SystemCollection();
            m_entityManager                 = world.GetECSWorld().EntityManager;
            m_spawerRequest                 = m_world.GetECSWorld().CreateSystem<HandleCharacterSpawnRequests>();
            m_spawnSystem                   = m_world.GetECSWorld().CreateSystem<CharacterSpawnSystem>();
            CharacterMoveSystem moveSystem  = m_world.GetECSWorld().CreateSystem<CharacterMoveSystem>();
            m_moveCollection.Add(moveSystem);
            
            // 不能直接在构造函数里初始化, 只能创建后调用初始化函数
            m_spawerRequest.Initalize(m_world, resourceManager);
            m_spawnSystem.Initalize(m_world, resourceManager);
            moveSystem.Initalize(m_world, resourceManager);

            Test();
        }

        /// <summary>
        /// 测试代码
        /// </summary>
        void Test()
        {
            // 角色创建创建方式2 并执行移动操作测试
            Entity e = CreateCharacter(102);
            CharacterMover mover = m_entityManager.GetComponentObject<CharacterMover>(e);
            mover.MoveTo(new List<float3>()
            {
                Vector3.zero,
                new Vector3(0, 0, 3),
                new Vector3(3, 0, 3),
                new Vector3(2, 0, 2),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 0),
            });
            m_character = e;
        }

        // 角色创建
        public Entity CreateCharacter(int id)
        {
            // 加载对象
            Entity e = m_resourceManager.LoadCharacter(id);
            
            // 设置名字, 加载资源等初始化信息
            Character character = m_entityManager.GetComponentObject<Character>(e);
            character.m_characterName = "yuehaiyouxi";
            return e;
        }

        public void MoveTo(Entity player, List<float3> paths)
        {
            var entityManger = m_world.GetEntityManager();
            CharacterMover mover = entityManger.GetComponentObject<CharacterMover>(player);
            mover.MoveTo(paths);
        }

        public Entity GetCharacter()
        {
            return m_character;
        }

        /// <summary>
        /// 系统更新
        /// </summary>
        public void Update()
        {
            // 角色创建相关
            m_spawerRequest.Update();
            m_spawnSystem.Update();
            
            // 移动系统相关
            m_moveCollection.Update();
        }

        /// <summary>
        /// 功能结束释放资源
        /// </summary>
        public void Shutdown()
        {
            m_moveCollection.Shutdown(m_world.GetECSWorld());
            m_world.GetECSWorld().DestroySystem(m_spawerRequest);
            m_world.GetECSWorld().DestroySystem(m_spawnSystem);
        }
    }
}