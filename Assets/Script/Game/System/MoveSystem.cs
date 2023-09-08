using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;

namespace SGame
{
    // 移动方向
    public struct MoveDirection : IComponentData
    {
        /// <summary>
        /// 移动方向+速度
        /// </summary>
        public float3 Value;   // 每秒运行多少距离
        
        /// <summary>
        /// 持续时间
        /// </summary>
        public float  duration;
    }
    
    /// <summary>
    /// 计时系统
    /// </summary>
    public partial class MoveSystem : SystemBase
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
            var entityCommandBufferParallel =  m_bufferSystem.CreateCommandBuffer().AsParallelWriter();
            Entities.ForEach((int entityInQueryIndex, Entity e,ref Translation trans, ref MoveDirection data) =>
            {
                if (data.duration > 0)
                {
                    data.duration -= deltaTime;
                    if (data.duration <= 0)
                    {
                        entityCommandBufferParallel.RemoveComponent<MoveDirection>(entityInQueryIndex, e);
                    }
                }

                trans.Value += data.Value * deltaTime;
            }).WithBurst().ScheduleParallel();
            
            m_bufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
