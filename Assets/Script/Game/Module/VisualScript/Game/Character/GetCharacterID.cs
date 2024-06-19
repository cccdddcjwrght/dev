using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;
using Unity.Mathematics;

/*
 * 通过Entity 获得 script
 */

namespace SGame.VS
{
    [UnitTitle("GetCharacterID")] // 
    [UnitCategory("Game/Character")]
    public class GetCharacterID : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueOutput m_characterID;
        
        [DoNotSerialize]
        public ValueInput m_entity;
        
        // 端口定义
        protected override void Definition()
        {
            m_entity = ValueInput<Entity>("entity");
            m_characterID = ValueOutput<int>("characterID", (flow) =>
            {
                var entity = flow.GetValue<Entity>(m_entity);
                var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (!EntityManager.Exists(entity))
                {
                    log.Error("entity is no exits=" + entity);
                    return 0;
                }

                if (!EntityManager.HasComponent<Character>(entity))
                    return 0;
                
                Character c = EntityManager.GetComponentObject<Character>(entity);
                return c.CharacterID;
            });
        }
    }
}
