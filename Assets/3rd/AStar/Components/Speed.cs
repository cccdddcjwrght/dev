using Unity.Entities;
using Unity.Mathematics;

// 速度
[GenerateAuthoringComponent]
public struct Speed : IComponentData
{
    public float Value;
}
