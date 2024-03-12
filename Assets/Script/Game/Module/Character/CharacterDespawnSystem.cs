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

        struct EventData
        {
            public GameObject gameObject;
            public Entity     entity;
            public int        characterID;
        }
        private List<EventData>                        m_destoryGameObject;
        private CharacterSpawnSystem                    m_spawnSystem;
        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            m_spawnSystem = World.GetOrCreateSystem<CharacterSpawnSystem>();
            m_destoryGameObject = new List<EventData>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<DespawningEntity>().ForEach((Entity entity, Character character) =>
            {
                m_destoryGameObject.Add(new EventData() {gameObject = character != null ? character.gameObject : null, entity = entity, characterID = character.CharacterID});
            }).WithoutBurst().Run();

            foreach (var item in m_destoryGameObject)
            {
                EventManager.Instance.Trigger<int>((int)GameEvent.CHARACTER_REMOVE, item.characterID);
                m_spawnSystem.RemoveCharacrID(item.characterID);

                if (item.gameObject != null)
                    GameObject.Destroy(item.gameObject);
            }
        }
    }
}