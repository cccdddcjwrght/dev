using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [GenerateAuthoringComponent]
    public struct RotationSpeed : IComponentData
    {
        public float Value;
    }
}