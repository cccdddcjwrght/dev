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
        
        private Entity                          m_characterPrefab;
        private const string CHARACTER_PREFAB = "Assets/BuildAsset/Prefabs/Character.prefab";

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

            m_characterPrefab = resourceManager.GetEntityPrefab(CHARACTER_PREFAB);
        }

        // 角色创建
        public Entity CreateCharacter(int id)
        {
            // 加载对象
            Entity e = m_world.GetEntityManager().Instantiate(m_characterPrefab);
            
            // 设置名字, 加载资源等初始化信息
            Character character = m_entityManager.GetComponentObject<Character>(e);
            character.characterName = "yuehaiyouxi";
            return e;
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