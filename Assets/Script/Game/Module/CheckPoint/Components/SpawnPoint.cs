using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 骰子初始点
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SpawnPoint : IComponentData
    {
        /// <summary>
        /// 类型
        /// </summary>
        public enum PointType
        {
            NORMAL_DICE,
            TRAVEL_DICE,
        }

        public PointType Value;
    }
}
