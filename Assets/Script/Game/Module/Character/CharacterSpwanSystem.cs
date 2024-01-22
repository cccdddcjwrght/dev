using System;
using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace SGame
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
    
    public partial class CharacterSpawnSystem : SystemBase
    {
        public class CharacterLoading : IComponentData
        {
            public AssetRequest       baseChacterPrefab;
            public CharacterGenerator gen;
            public AssetRequest       aiPrefab;
            public bool isDone => baseChacterPrefab.isDone && gen.ConfigReady && aiPrefab.isDone;
        }
        
        private static ILog log = LogManager.GetLogger("game.character");
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;
        private GameObject                             m_characterbase;
        
        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
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
                
                // 创建AI
                GameObject ai       = GameObject.Instantiate(loading.aiPrefab.asset as GameObject);
                ai.transform.parent = character.transform;
                
                // 创建对象
                GameObject ani = loading.gen.Generate();
                ani.transform.SetParent(character.transform, false);
                ani.transform.localRotation = Quaternion.identity;
                ani.transform.localPosition = Vector3.zero;
                ani.transform.localScale = Vector3.one;
                commandBuffer.DestroyEntity(e);
            }).WithoutBurst().Run();
        }
    }
}