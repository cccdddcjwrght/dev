using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.ParticleSystemJobs;

// 移动到目标系统
namespace SGame
{
    // 移动方向
    public struct MoveTarget : IComponentData
    {
        public float3 Value; // 目标
    }
    
    /// <summary>
    /// 计时系统
    /// </summary>
    public class MoveTargetSystem : ComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem m_bufferSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            m_bufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            var entityCommandBufferParallel =  m_bufferSystem.CreateCommandBuffer();//.AsParallelWriter();
            Entities.ForEach((Entity e, 
                ref Translation trans, 
                ref MoveTarget target, 
                ref Speed speed) =>
            {
                var target_pos  = target.Value;
                float3 dir           = target_pos - trans.Value;
                float distanceLength = math.length(dir);
                float moveLen        = deltaTime * speed.Value;
                if (moveLen >= distanceLength)
                {
                    // 已经到达了
                    trans.Value = target.Value;
                    entityCommandBufferParallel.RemoveComponent<MoveTarget>(e);
                    return;
                }

                float3 moveDir = moveLen * math.normalize(dir);
                trans.Value += moveDir;
            });
        }
    }
}
