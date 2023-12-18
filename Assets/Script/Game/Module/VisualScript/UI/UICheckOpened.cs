using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using SGame.UI;
using Unity.Entities;

namespace SGame.VS
{
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

        private EntityManager _entityManager;
        private EntityManager entityManager
        {
            get
            {
                if (_entityManager == null)
                    _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                return _entityManager;
            }
        }
        
        // 端口定义
        protected override void Definition()
        {
            uiEntity = ValueInput("uiEntity", Vector2Int.zero);
            result   = ValueOutput<bool>("isReady", Check);
        }

        /// <summary>
        /// 检测UI是否 已经加载完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        bool Check(Flow flow)
        {
            if (!Application.isPlaying)
                return true;
            
            Vector2Int v2 = flow.GetValue<Vector2Int>(uiEntity);
            Entity e = new Entity() {Index = v2.x, Version = v2.y};
            if (e == Entity.Null)
                return true;
            
            return entityManager.HasComponent<UIInitalized>(e) && !entityManager.HasComponent<DespawningEntity>(e);
        }
    }
}