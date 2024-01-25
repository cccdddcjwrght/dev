using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using SGame.UI;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace SGame
{
    [UpdateAfter(typeof(CharacterSpawnSystem))]
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class CharacterDespawnSystem : SystemBase
    {
        /// <summary>
        /// 角色销毁标记
        /// </summary>
        public struct DespawnCharacterTag : IComponentData {
        }

        private EndSimulationEntityCommandBufferSystem m_commandBuffer;


        private List<GameObject>                    m_destoryGameObject;
        

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            m_destoryGameObject = new List<GameObject>();
        }

        public void DespawnEntity(Entity e)
        {
            if (!EntityManager.HasComponent<DespawnCharacterTag>(e))
                EntityManager.AddComponent<DespawnCharacterTag>(e);
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            
            Entities.WithAll<DespawnCharacterTag>().ForEach((Entity entity, Character character) =>
            {
                commandBuffer.DestroyEntity(entity);
                m_destoryGameObject.Add(character.gameObject);
            }).WithoutBurst().Run();
            
            foreach (var go in m_destoryGameObject)
                GameObject.Destroy(go);
        }
    }
}