using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class CharacterSpawnSystem : SystemBase
    {
        public string ENTITY_PREFAB_PATH = "Assets/BuildAsset/Prefabs/ECS/Character.prefab";
        
        public struct CharacterSpawn : IComponentData
        {
            // 地图2D 位置
            public Vector3 pos; 
        
            // ID
            public int id;

            // 是否使用属性系统
            public bool hasAttribute;

            // 是否是雇佣好友
            //public bool isEmployee;
            
            // 雇佣好友ID
            public int playerID;

            public static CharacterSpawnResult Create(int id, Vector3 pos, bool hasAttriburte = true, int playerID = 0)
            {
                var mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                var entity = mgr.CreateEntity(typeof(CharacterSpawn));
                mgr.SetComponentData(entity, new CharacterSpawn()
                {
                    id          = id,
                    pos         = pos,
                    hasAttribute = hasAttriburte,
                    playerID  = playerID
                });

                var request = new CharacterSpawnResult()
                {
                    characterID = 0,
                    entity = Entity.Null
                };

                mgr.AddComponentData(entity, request);

                return request;
            }
        }
        
        /// <summary>
        /// 角色初始化完成标记
        /// </summary>
        public struct CharacterInitalized : IComponentData {
        }
        
        
        public class CharacterLoading : IComponentData
        {
            //public AssetRequest       baseChacterPrefab;
            //public CharacterGenerator gen;
            public CharacterPool        pool;
            public AssetRequest       aiPrefab;
            public int modelId;
            public bool isDone => pool.isDone && aiPrefab.isDone;
        }

        public struct CharacterEvent
        {
            public Character character;
            public Entity    entity;
            public CharacterSpawnResult result;
        }
        
        
        private static ILog log = LogManager.GetLogger("game.character");
        private GameObject                             m_characterbase;
        private List<CharacterEvent>                           m_triggerInit;
        private int lasterCharacterID;

        private Dictionary<int, Entity>             m_characters;

        /// <summary>
        /// 角色ECS的预制
        /// </summary>
        private Entity                              m_characterPerfab;

        private EntityQuery                         m_prefabReadly;
        
        protected override void OnCreate()
        {
            m_triggerInit = new List<CharacterEvent>();
            m_characters = new Dictionary<int, Entity>();

            m_prefabReadly = EntityManager.CreateEntityQuery( typeof(GameInitFinish));
            
            lasterCharacterID = 0;
            
            RequireForUpdate(m_prefabReadly);
        }

        protected override void OnStartRunning()
        {
            m_characterPerfab = EntityManager.CreateEntity(
                typeof(Speed),
                typeof(RotationSpeed),
                typeof(CharacterAttribue),
                typeof(Follow),
                typeof(LocalToWorld),
                typeof(Translation),
                typeof(Rotation),
                typeof(Prefab),
                typeof(GameObjectSyncTag));
            EntityManager.SetComponentData(m_characterPerfab, new Speed(){Value = 10.0f});
            EntityManager.SetComponentData(m_characterPerfab, new RotationSpeed(){Value = 10.0f});
            EntityManager.SetComponentData(m_characterPerfab, new LocalToWorld(){Value = float4x4.identity});
        }

        /// <summary>
        /// 通过角色ID获得角色
        /// </summary>
        /// <param name="characterid"></param>
        /// <returns></returns>
        public Entity GetCharacter(int characterid)
        {
            if (m_characters.TryGetValue(characterid, out Entity e))
                return e;
            
            return Entity.Null;
        }

        public void RemoveCharacrID(int characterid)
        {
            m_characters.Remove(characterid);
        }
        
        /// <summary>
        /// 加载AI脚本
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        AssetRequest LoadAI(string name)
        {
            string path = "Assets/BuildAsset/VisualScript/Prefabs/AI/" + name + ".prefab";
            return Assets.LoadAssetAsync(path, typeof(GameObject));
        }

        public void Clear()
        {
            
        }

        protected override void OnUpdate()
        {
            if (CharacterGenerator.ReadyToUse == false)
                return;
            
            // 触发事件
            foreach (var item in m_triggerInit)
            {
                if (EntityManager.Exists(item.entity) && !EntityManager.HasComponent<DespawningEntity>(item.entity))
                {
                    if (item.character != null && item.character.model != null)
                    {
                        m_characters.Add(item.character.CharacterID, item.entity);
                        item.character.model.SetActive(true);
                        item.character.OnInitCharacter(item.entity, EntityManager);
                        item.result.entity = item.entity;
                        item.result.characterID = item.character.CharacterID;
                    }
                    else
                    {
                        log.Warn("character already destory!");
                    }
                }
            }
            m_triggerInit.Clear();
            
            // 获取数据
            //var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithNone<CharacterLoading, DespawningEntity>().ForEach((Entity e, in CharacterSpawn req) =>
            {
                if (!ConfigSystem.Instance.TryGet(req.id, out GameConfigs.RoleDataRowData roleData))
                {
                    log.Error("role data config not found=" + req.id);
                    EntityManager.DestroyEntity(e);
                    return;
                }
                
                if (!ConfigSystem.Instance.TryGet(roleData.Model, out GameConfigs.roleRowData config))
                {
                    log.Error("role model config not found=" + roleData.Model + " role id=" + req.id);
                    EntityManager.DestroyEntity(e);
                    return;
                }

                if (string.IsNullOrEmpty(config.Part))
                {
                    log.Error("role part is null" + config.Part + " role id=" + req.id + " model id=" + roleData.Model);
                    EntityManager.DestroyEntity(e);
                    return;
                }

                string configAI = config.Ai;
                if (req.playerID != 0 && !string.IsNullOrEmpty(config.FriendAI))
                {
                    // 好友AI
                    configAI = config.FriendAI;
                }

                CharacterLoading loading = new CharacterLoading()
                {
                    pool      = CharacterFactory.Instance.GetOrCreate(config.Part),//CharacterGenerator.CreateWithConfig(config.Part),
                    modelId   = config.ID,
                    aiPrefab  = LoadAI(configAI)
                };
                EntityManager.AddComponentData(e, loading);
            }).WithoutBurst().WithStructuralChanges().Run();
            
            // 等待资源加载并生成对象
            Entities.WithNone<DespawningEntity>().ForEach((Entity e, CharacterSpawnResult result, in CharacterSpawn req,  in CharacterLoading loading) =>
            {
                if (!loading.isDone)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(loading.aiPrefab.error))
                {
                    log.Error("prefab load fail=" + loading.aiPrefab.error);
                    EntityManager.DestroyEntity(e);
                    return;
                }
                var aiPrefab = loading.aiPrefab.asset as GameObject;
                if (aiPrefab == null)
                {
                    log.Error("ai prefab load null=" + loading.aiPrefab.error);
                    EntityManager.DestroyEntity(e);
                    return;
                }
                
                ConfigSystem.Instance.TryGet(req.id, out GameConfigs.RoleDataRowData roleData);
                ConfigSystem.Instance.TryGet(roleData.Model, out GameConfigs.roleRowData config);

                lasterCharacterID++;

                // 获得地图位置
                var character         = new GameObject();
                character.transform.position    = req.pos;
                character.transform.rotation    = Quaternion.identity;
                Character c = character.AddComponent<Character>();//()
                character.name = config.Name + "_id_" + lasterCharacterID;

                Entity characterEntity = EntityManager.Instantiate(m_characterPerfab);
                EntityManager.AddComponentObject(characterEntity, c);
                EntityManager.AddComponentObject(characterEntity, c.transform);

                // 禁用属性系统
                if (!req.hasAttribute)
                    EntityManager.AddComponent<DisableAttributeTag>(characterEntity);

                // 创建AI
                GameObject ai               = GameObject.Instantiate(loading.aiPrefab.asset as GameObject);
                ai.transform.parent         = character.transform;
                ai.transform.localRotation  = Quaternion.identity;
                ai.transform.localPosition  = Vector3.zero;
                ai.transform.localScale     = Vector3.one;
                ai.name                     = "AI";
                
                // 创建对象
                //var id = loading.pool.Spawn();
                GameObject ani = CharacterFactory.Instance.Spawn(loading.pool);  //loading.pool.GetObject(id);
                ani.transform.SetParent(character.transform, false);
                ani.transform.localRotation = Quaternion.identity;
                ani.transform.localPosition = Vector3.zero;
                ani.transform.localScale = Vector3.one;
                //ani.name = "Model";
                ani.SetActive(false);
                if (config.RoleScaleLength == 3)
                {
                    var scaleVector = new Vector3(config.RoleScale(0), config.RoleScale(1), config.RoleScale(2));
                    ani.transform.localScale = scaleVector;
                }
                
                c.script = ai;
                c.model = ani;
                c.entity = characterEntity;
                c.CharacterID = lasterCharacterID;
                c.roleType = roleData.Type;
                c.roleID = roleData.Id;
                c.playerID = req.playerID;
                c.SetLooking(loading.pool.config);

				//TODO:先不使用CommandBuff处理

				// 设置属性
				EntityManager.SetComponentData(characterEntity, new Translation() { Value = req.pos });
				EntityManager.SetComponentData(characterEntity, new CharacterAttribue() { roleID = roleData.Id, roleType = roleData.Type, characterID = lasterCharacterID });
				EntityManager.SetComponentData(characterEntity, new Speed() { Value = roleData.MoveSpeed });
				EntityManager.AddComponentObject(characterEntity, result);
				EntityManager.DestroyEntity(e);
			}).WithStructuralChanges().WithoutBurst().Run();
            
            // 等待角色创建完成
            Entities.WithNone<DespawningEntity, CharacterInitalized>().ForEach((Entity entity, CharacterSpawnResult result, Character character) =>
            {
                m_triggerInit.Add(new CharacterEvent() {entity = entity, character = character, result = result});
                EntityManager.AddComponent<CharacterInitalized>(entity);
                EntityManager.RemoveComponent<CharacterSpawnResult>(entity);
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}