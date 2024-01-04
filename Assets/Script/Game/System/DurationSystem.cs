using Unity.Burst;
using Unity.Entities;

namespace SGame
{
    // 生存时间
    public struct LiveTime : IComponentData
    {
        public float Value;
    }
    
    // 根据时间自动销毁
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class DurationSystem : SystemBase
    {
        private EntityCommandBufferSystem m_commandBufferSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            
            m_commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            float t = (float)GlobalTime.deltaTime;
            Entities.WithNone<DespawningEntity>().ForEach((Entity e, int entityInQueryIndex, ref LiveTime value) =>
            {
                value.Value -= t;
                if (value.Value <= 0)
                {
                    commandBuffer.AddComponent<DespawningEntity>(entityInQueryIndex, e);
                }
            }).WithBurst().ScheduleParallel();
            m_commandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}