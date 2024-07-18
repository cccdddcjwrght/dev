using Unity.Entities;
using Unity.Mathematics;

//[GenerateAuthoringComponent]
public struct Follow : IComponentData
{
    public int Value;
}

public struct DisableFollow : IComponentData
{
}
