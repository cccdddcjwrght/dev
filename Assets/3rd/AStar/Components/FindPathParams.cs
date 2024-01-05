using Unity.Entities;
using Unity.Mathematics;

// 寻路参数
[GenerateAuthoringComponent]
public struct FindPathParams : IComponentData
{
    public int2 start_pos;  // 开始节点 
    public int2 end_pos;    // 结束节点
}
