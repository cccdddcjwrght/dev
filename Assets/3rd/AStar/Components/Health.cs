using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// 血量
//[GenerateAuthoringComponent]
public struct Health : IComponentData
{
    public int Value;
}

/*
public class HealthAuthoring : MonoBehaviour
{
    public int Value;
}

public class HealthAuthoringBaker : Baker<HealthAuthoring>
{
    public override void Bake(HealthAuthoring authoring)
    {
        AddComponent(new Health
        {
            Value = authoring.Value
        }); 
    }
}
*/