using System;
using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class CharacterSpawnSystem : SystemBase
    {
        public struct CharacterSpawn : IComponentData
        {
            // 地图2D 位置
            public Vector3 pos; 
        
            // ID
            public int id;

            public static Entity Create(int id, Vector3 pos)
            {
                var mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
                var entity = mgr.CreateEntity(typeof(CharacterSpawn));
                mgr.SetComponentData(entity, new CharacterSpawn()
                {
                    id = id,
                    pos = pos
                });

                return entity;
            }
        }
        
        /// <summary>
        /// 角色初始化完成标记
        /// </summary>
        public struct CharacterInitalized : IComponentData {
        }
        
        
        public class CharacterLoading : IComponentData
        {
            public AssetRequest       baseChacterPrefab;
            public CharacterGenerator gen;
            public AssetRequest       aiPrefab;
            public bool isDone => baseChacterPrefab.isDone && gen.ConfigReady && aiPrefab.isDone;
        }

        public struct CharacterEvent
        {
            public Character character;
            public Entity    entity;
        }
        
        
        private static ILog log = LogManager.GetLogger("game.character");
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;
        private GameObject                             m_characterbase;
        private List<CharacterEvent>                           m_triggerInit;
        private int lasterCharacterID;

        private Dictionary<int, Entity>             m_characters;

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            m_triggerInit = new List<CharacterEvent>();
            m_characters = new Dictionary<int, Entity>();
            lasterCharacterID = 0;
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

        /// <summary>
        /// 加载基础角色预制
        /// </summary>
        /// <returns></returns>
        AssetRequest LoadBaseCharacter()
        {
            return Assets.LoadAssetAsync("Assets/BuildAsset/CustomCharacters/CharacterBase.prefab", typeof(GameObject));
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

        protected override void OnUpdate()
        {
            // 获取数据
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithNone<CharacterLoading>().ForEach((Entity e, CharacterSpawn req) =>
            {
                if (!ConfigSystem.Instance.TryGet(req.id, out GameConfigs.roleRowData config))
                {
                    log.Error("role config not found=" + req.id);
                    commandBuffer.DestroyEntity(e);
                    return;
                }

                if (string.IsNullOrEmpty(config.Part))
                {
                    log.Error("role part is null" + config.Part);
                    commandBuffer.DestroyEntity(e);
                    return;
                }

                CharacterLoading loading = new CharacterLoading()
                {
                    gen       = CharacterGenerator.CreateWithConfig(config.Part),
                    baseChacterPrefab = LoadBaseCharacter(),
                    aiPrefab  = LoadAI(config.Ai)
                };
                commandBuffer.AddComponent(e, loading);
            }).WithoutBurst().Run();
            
            // 等待资源加载并生成对象
            Entities.ForEach((Entity e, CharacterSpawn req, CharacterLoading loading) =>
            {
                if (!loading.isDone)
                {
                    return;
                }

                // 获得地图位置
                var characterPrefab             = loading.baseChacterPrefab.asset as GameObject;
                var character         = GameObject.Instantiate(characterPrefab);
                character.transform.position    = req.pos;
                character.transform.rotation    = Quaternion.identity;
                Character c = character.AddComponent<Character>();//()
                
                // 创建AI
                GameObject ai       = GameObject.Instantiate(loading.aiPrefab.asset as GameObject);
                ai.transform.parent = character.transform;
                ai.name = "AI";
                
                // 创建对象
                GameObject ani = loading.gen.Generate();
                ani.transform.SetParent(character.transform, false);
                ani.transform.localRotation = Quaternion.identity;
                ani.transform.localPosition = Vector3.zero;
                ani.transform.localScale = Vector3.one;
                ani.name = "Model";
                commandBuffer.DestroyEntity(e);

                lasterCharacterID++;
                c.script = ai;
                c.model = ani;
                c.CharacterID = lasterCharacterID;
            }).WithoutBurst().Run();


            // 等待角色创建完成
            Entities.WithNone<CharacterInitalized>().ForEach((Entity entity, Character character) =>
            {
                m_triggerInit.Add(new CharacterEvent() {entity = entity, character = character});
                commandBuffer.AddComponent<CharacterInitalized>(entity);
            }).WithoutBurst().Run();

            // 触发事件
            foreach (var item in m_triggerInit)
            {
                item.character.OnInitCharacter(item.entity, EntityManager);
                m_characters.Add(item.character.CharacterID, item.entity);
            }
            m_triggerInit.Clear();
        }
    }
}