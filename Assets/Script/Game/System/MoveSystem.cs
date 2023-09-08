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
        /// 移动方向
        /// </summary>
        public float3 Value;
        
        /// <summary>
        /// 持续时间
        /// </summary>
        public float  duration;
    }
    
    /// <summary>
    /// 计时系统
    /// </summary>
    /*
    public partial class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((ref Translation trans, ref MoveDirection data) =>
            {
                if (data.duration > 0)
                {
                    data.duration -= deltaTime;
                    if (data.duration <= 0)
                    {
                        
                    }
                }
                
                //if (data.Value > 0)
                //{
                //    data.Value = math.clamp(data.Value - deltaTime, 0, float.MaxValue);
                //}
            }).WithBurst().ScheduleParallel();
        }
    }
    */
}
