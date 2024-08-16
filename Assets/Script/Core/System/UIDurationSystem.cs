using SGame.UI;
using Unity.Burst;
using Unity.Entities;

namespace SGame
{
    // 生存时间
    public struct UILiveTime : IComponentData
    {
        public float Value;
    }
    
    // 根据时间自动销毁
    public partial class UIDurationSystem : SystemBase
    {
        private EntityCommandBufferSystem m_commandBufferSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            
            m_commandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            float t = (float)GlobalTime.deltaTime;
            Entities.WithNone<DespawningUI>().ForEach((int entityInQueryIndex, Entity e, ref UILiveTime value) =>
            {
                value.Value -= t;
                if (value.Value <= 0)
                {
                    commandBuffer.AddComponent<DespawningUI>(entityInQueryIndex, e);
                    //commandBuffer.SetComponent(entityInQueryIndex, e, new DespawningUI(){isDispose = false});
                }
            }).WithBurst().ScheduleParallel();
            m_commandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}