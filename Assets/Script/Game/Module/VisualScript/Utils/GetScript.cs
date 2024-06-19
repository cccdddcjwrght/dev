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
    [UnitTitle("GetScript")] // 
    [UnitCategory("Game/Utils")]
    public class GetScript : Unit
    {
        private static ILog log = LogManager.GetLogger("game.vs");
        
        [DoNotSerialize]
        public ValueOutput m_script;
        
        [DoNotSerialize]
        public ValueInput m_entity;
        
        // 端口定义
        protected override void Definition()
        {
            m_entity = ValueInput<Entity>("entity");
            m_script = ValueOutput<GameObject>("script", (flow) =>
            {
                var entity = flow.GetValue<Entity>(m_entity);
                var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (!EntityManager.Exists(entity))
                {
                    log.Error("entity is no exits=" + entity);
                    return null;
                }

                if (EntityManager.HasComponent<Character>(entity))
                {
                    Character c = EntityManager.GetComponentObject<Character>(entity);
                    return c.script;
                }
                
                if (EntityManager.HasComponent<CarMono>(entity))
                {
                    CarMono car = EntityManager.GetComponentObject<CarMono>(entity);
                    return car.script;
                }

                return null;
            });
        }
    }
}
