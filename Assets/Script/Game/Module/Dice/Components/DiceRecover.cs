using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 骰子恢复
    /// </summary>
    [GenerateAuthoringComponent]
    public struct DiceRecover : IComponentData
    {
        // 每次恢复点数
        public int    recoverNum;
        
        // 持续时间
        public float   duration;
    }
}
