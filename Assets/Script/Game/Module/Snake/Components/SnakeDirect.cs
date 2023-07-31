using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇移動方向
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SnakeDirect : IComponentData
    {
        public float2 Value;
    }
}