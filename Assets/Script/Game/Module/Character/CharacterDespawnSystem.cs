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
    public class CharacterDespawnSystem : ComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        struct EventData
        {
            //public GameObject gameObject;
            public Character character;
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
                m_destoryGameObject.Add(new EventData() {character = character, entity = entity, characterID = character.CharacterID});
            });

            foreach (var item in m_destoryGameObject)
            {
                EventManager.Instance.Trigger<int>((int)GameEvent.CHARACTER_REMOVE, item.characterID);
                m_spawnSystem.RemoveCharacrID(item.characterID);

                if (item.character != null)
                {
                    item.character.Clear();
                    GameObject.Destroy(item.character.gameObject);
                }
            }

			m_destoryGameObject.Clear();

		}
    }
}