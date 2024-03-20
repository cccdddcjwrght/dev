using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    public class CharacterModule : Singleton<CharacterModule>
    {
        private EntityQuery m_characterQuery;
        public void Initlaize()
        {
            EntityQueryDesc desc = new EntityQueryDesc()
            {
                Any = new ComponentType[] { typeof(CharacterSpawnSystem.CharacterSpawn), typeof(CharacterSpawnSystem.CharacterInitalized) },
                None = new ComponentType[] { typeof(DespawningEntity) }
            };
            m_characterQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(desc);
        }

        /// <summary>
        /// 根据配置表ID创建角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public CharacterSpawnResult Create(int id, Vector3 pos, bool hasAttribute = true)
        {
            return CharacterSpawnSystem.CharacterSpawn.Create(id, pos, hasAttribute);
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsReadly(Entity e)
        {
            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (!EntityManager.Exists(e))
                return false;
            
            return EntityManager.HasComponent<CharacterSpawnSystem.CharacterInitalized>(e);
        }
        

        /// <summary>
        /// 通过Entity获得对象
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Character FindCharacter(Entity e)
        {
            if (!IsReadly(e))
                return null;
            
            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            return EntityManager.GetComponentObject<Character>(e);
        }

        /// <summary>
        /// 查找角色
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        public Character FindCharacter(int characterID)
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var spawnSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<CharacterSpawnSystem>();
            Entity e = spawnSystem.GetCharacter(characterID);
            if (e == Entity.Null)
                return null;

            if (!entityManager.Exists(e))
                return null;

            Character ret = entityManager.GetComponentObject<Character>(e);
            return ret;
        }

        /// <summary>
        /// 清空所有角色
        /// </summary>
        public void Clear()
        {
            var entities = m_characterQuery.ToEntityArray(Allocator.Temp);
            var despawnSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>();
            foreach (var e in entities)
            {
                despawnSystem.DespawnEntity(e);
            }
            entities.Dispose();
        }
        
        

        public bool DespawnCharacter(int charcterID)
        {
            var despawnSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>();
            
            var character = FindCharacter(charcterID);
            if (character == null)
                return false;

            despawnSystem.DespawnEntity(character.entity);
            return true;
        }

        public bool DespawnCharacterEntity(Entity e)
        {
            var despawnSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>();
            despawnSystem.DespawnEntity(e);
            return true;
        }
    }
}