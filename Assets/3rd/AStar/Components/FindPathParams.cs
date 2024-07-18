using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// 寻路参数
//[GenerateAuthoringComponent]
public struct FindPathParams : IComponentData
{
    public int2 start_pos;  // 开始节点 
    public int2 end_pos;    // 结束节点
}
/*
public class FindPathParamsAuthoring : MonoBehaviour
{
    public int2 start_pos; // 开始节点 
    public int2 end_pos;   // 结束节点
}

public class FindPathParamsAuthoringBaker : Baker<FindPathParamsAuthoring>
{
    public override void Bake(FindPathParamsAuthoring authoring)
    {
        AddComponent(new FindPathParams
        {
            start_pos = authoring.start_pos,
            end_pos = authoring.end_pos
        }); 
    }
}*/