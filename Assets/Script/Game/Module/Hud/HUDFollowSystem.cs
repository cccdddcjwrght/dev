using Unity.Entities;
using UnityEngine;
using log4net;
using Unity.Mathematics;
using Unity.Transforms;

/// HUD 同步系统, 将3D坐标同步到UI坐标中去

namespace SGame.UI
{
    // UI跟随gameobject
    public class HUDFlow : IComponentData
    {
        // 要跟随的对象
        public Transform Value; 

        // 偏移量
        public float3 offset;
    }

    // UI跟随entity
    public struct HUDFlowE : IComponentData
    {
        public Entity Value;
        
        // 偏移量
        public float3 offset;
    }

    [UpdateAfter(typeof(SpawnUISystem))]
    [UpdateInGroup(typeof(GameUIGroup))]
    public partial class HUDFlowSystem : SystemBase
    {
        private static ILog              log = LogManager.GetLogger("game.hud");

        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithNone<DespawningEntity>().ForEach((Entity e, HUDFlow flow, ref Translation trans) =>
            {
                if (flow.Value != null)
                {
                    trans.Value = (float3)flow.Value.position + flow.offset;
                }
                else
                {
                    // 跟随的目标已经销毁了
                    commandBuffer.RemoveComponent<HUDFlow>(e);
                    commandBuffer.AddComponent<DespawningEntity>(e);
                }
            }).WithoutBurst().Run();

            var translationQuery = GetComponentDataFromEntity<Translation>();
            Entities.WithNone<DespawningEntity>().ForEach((Entity e,  ref Translation trans, in HUDFlowE flow) =>
            {
                if (flow.Value != Entity.Null && translationQuery.HasComponent(flow.Value))
                {
                    Translation other = translationQuery[flow.Value];
                    trans.Value = other.Value + flow.offset;
                }
                else
                {
                    // 跟随的目标已经销毁了
                    commandBuffer.RemoveComponent<HUDFlow>(e);
                    commandBuffer.AddComponent<DespawningEntity>(e);
                }
            }).WithoutBurst().Run();
        }
    }
}