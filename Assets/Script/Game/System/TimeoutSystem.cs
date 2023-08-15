using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    // 超时系统
    public struct TimeoutData : IComponentData
    {
        public float Value;
    }
    
    /// <summary>
    /// 计时系统
    /// </summary>
    public partial class TimeoutSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((ref TimeoutData data) =>
            {
                if (data.Value >= 0)
                {
                    data.Value -= deltaTime;
                }
            }).WithoutBurst().ScheduleParallel();
        }
    }
}
