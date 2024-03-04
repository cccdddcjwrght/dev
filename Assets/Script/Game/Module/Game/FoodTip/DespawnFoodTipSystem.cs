using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 食物小费管理
    /// </summary>
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class DespawnFoodTipSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<DespawningEntity>().ForEach((Entity e, FoodTips tip) =>
            {
                if (EntityManager.Exists(tip.ui))
                {
                    commandBuffer.AddComponent<DespawningEntity>(tip.ui);
                }
            }).WithoutBurst().Run();
        }
    }
}