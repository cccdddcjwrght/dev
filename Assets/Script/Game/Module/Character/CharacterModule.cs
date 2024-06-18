using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;

namespace SGame
{
    public class CharacterModule : Singleton<CharacterModule>
    {
        private EntityQuery m_characterQuery;
        private EntityQuery m_characterFindQuery;

        
        public void Initlaize()
        {
            EntityQueryDesc desc = new EntityQueryDesc()
            {
                Any = new ComponentType[] { typeof(CharacterSpawnSystem.CharacterSpawn), typeof(CharacterSpawnSystem.CharacterInitalized) },
                None = new ComponentType[] { typeof(DespawningEntity) }
            };
            EntityQueryDesc desc2 = new EntityQueryDesc()
            {
                All = new ComponentType[] { typeof(CharacterSpawnSystem.CharacterInitalized) },
                None = new ComponentType[] { typeof(DespawningEntity) }
            };
            
            m_characterQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(desc);
            m_characterFindQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(desc2);
            CharacterIdleModule.Instance.Initlaize();
        }

        /// <summary>
        /// 根据配置表ID创建角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public CharacterSpawnResult Create(int id, Vector3 pos, bool hasAttribute = true, long playerID = 0)
        {
            return CharacterSpawnSystem.CharacterSpawn.Create(id, pos, hasAttribute, playerID);
        }

        /// <summary>
        /// 创建汽车顾客 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public CharacterSpawnResult CreateCarCustomer(int id, int roleAI, Vector3 pos)
        {
            return CharacterSpawnSystem.CharacterSpawn.Create(id, pos, true, 0, roleAI);
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
        public void Clear(bool isImmediately)
        {
            var entities = m_characterQuery.ToEntityArray(Allocator.Temp);
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            var despawnSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>();
            foreach (var e in entities)
            {
                if (entityManager.HasComponent<Character>(e))
                {
                    var character = entityManager.GetComponentObject<Character>(e);
                    if (character != null)
                        character.script.SetActive(false);
                }

                if (isImmediately)
                    entityManager.AddComponent<DespawningEntity>(e); // 立马设置 
                else
                    despawnSystem.DespawnEntity(e);
            }
            entities.Dispose();
            
            CharacterFactory.Instance.ClearEmpty();
        }

        /// <summary>
        /// 查找角色
        /// </summary>
        /// <returns></returns>
        public bool FindCharacters(List<Character> rets, Func<Character, bool> check = null)
        {
            rets.Clear();
            var entities = m_characterFindQuery.ToEntityArray(Allocator.Temp);
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            foreach (var e in entities)
            {
                if (entityManager.HasComponent<Character>(e))
                {
                    Character character = entityManager.GetComponentObject<Character>(e);
                    if (check == null || check(character))
                    {
                        if (rets == null)
                            rets = new List<Character>();
                        rets.Add(character);
                    }
                }
            }
            entities.Dispose();
            return rets.Count > 0;
        }

        /*
        /// <summary>
        /// 获得空闲角色
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public List<Character> GetIdleCharacters(int roleType1, int roleType2)
        {
            return FindCharacters((character) => character.isIdle && character.roleType == roleType);
        }
        */
        

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