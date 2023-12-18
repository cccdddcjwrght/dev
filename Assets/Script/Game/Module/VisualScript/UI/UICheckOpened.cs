using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{/*
    [UnitTitle("UICheckOpened")] //The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Game/UI")]
    public class UICheckOpened : Unit
    {
        [DoNotSerialize]
        [PortLabel("entity")]
        public ValueInput uiEntity;
        
        [DoNotSerialize]
        [PortLabel("isReadly")]
        public ValueOutput result;

        private EntityManager entityManager;
        
        // 端口定义
        protected override void Definition()
        {
            uiEntity = ValueInput("uiEntity", Vector2Int.zero);
            result   = ValueOutput<bool>("isReady", Check);
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        /// <summary>
        /// 检测UI是否 已经加载完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        bool Check(Flow flow)
        {
            Vector2Int v2 = flow.GetValue<Vector2Int>(uiEntity);
            Entity e = new Entity() {Index = v2.x, Version = v2.y};
            if (e == Entity.Null)
                return true;
            
            return entityManager.HasComponent<UIInitalized>(e) && !entityManager.HasComponent<DespawningEntity>(e);
        }
    }
    */
}