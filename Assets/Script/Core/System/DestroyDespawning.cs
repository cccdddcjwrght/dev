using Unity.Entities;
using Unity.Transforms;

namespace SGame
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial class DestroyDespawning : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandbuffer = m_commandBuffer.CreateCommandBuffer().AsParallelWriter();

            // 清除不带有child的
            var job1 = Entities.WithAll<DespawningEntity>().WithNone<Child>().ForEach((int entityInQueryIndex, Entity e) =>
            {
                commandbuffer.DestroyEntity(entityInQueryIndex, e);
            }).WithBurst().ScheduleParallel(Dependency);

            // 清除带有child的
            var job2 = Entities.WithAll<DespawningEntity>().ForEach((int entityInQueryIndex, Entity e, in DynamicBuffer<Child> childs) =>
            {
                for (int i = 0; i < childs.Length; i++)
                {
                    var childEntity = childs[i].Value;
                    if (!HasComponent<DespawningEntity>(childEntity))
                    {
                        commandbuffer.AddComponent<DespawningEntity>(entityInQueryIndex, childEntity);
                    }
                }
                commandbuffer.DestroyEntity(entityInQueryIndex, e);
            }).WithBurst().ScheduleParallel(job1);

            Dependency = job2;
            m_commandBuffer.AddJobHandleForProducer(Dependency);
        }
    }
}