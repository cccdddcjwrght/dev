using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    // 移動控件
    [GenerateAuthoringComponent]
    public struct SnakeMovement : IComponentData
    {
        // 移動方向  
        public float2 m_dir;

        // 移動距離
        public float movement;
    }
}
