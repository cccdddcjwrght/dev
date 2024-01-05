using Unity.Entities;
using Unity.Mathematics;

// 血量
[GenerateAuthoringComponent]
public struct Health : IComponentData
{
    public int Value;
}
