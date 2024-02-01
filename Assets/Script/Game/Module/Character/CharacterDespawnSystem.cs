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
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        private List<GameObject>                        m_destoryGameObject;
        
        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            m_destoryGameObject = new List<GameObject>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<DespawningEntity>().ForEach((Entity entity, Character character) =>
            {
                m_destoryGameObject.Add(character.gameObject);
                
                // 删除FOOD
                if (EntityManager.HasComponent<FoodHolder>(entity))
                {
                    var foodHolder = EntityManager.GetComponentData<FoodHolder>(entity);
                    if (EntityManager.Exists(foodHolder.Value))
                    {
                        commandBuffer.AddComponent<DespawningEntity>(foodHolder.Value);
                    }
                }
            }).WithoutBurst().Run();
            
            foreach (var go in m_destoryGameObject)
                GameObject.Destroy(go);
        }
    }
}