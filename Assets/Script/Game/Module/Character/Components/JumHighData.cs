using Unity.Entities;
using UnityEngine;

/// <summary>
/// 跳跃高度
/// </summary>
namespace SGame
{
    [GenerateAuthoringComponent]
    public struct JumHighData : IComponentData
    {
        public float Value;
    }
}