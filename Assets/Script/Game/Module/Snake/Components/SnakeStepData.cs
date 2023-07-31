using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 蛇每步移動多少
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SnakeStepData : IComponentData
    {
        public float Value;
    }
}