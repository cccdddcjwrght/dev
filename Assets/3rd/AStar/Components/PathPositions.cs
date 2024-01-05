using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PathPositions : IBufferElementData
{
    public int2 Value;
}
