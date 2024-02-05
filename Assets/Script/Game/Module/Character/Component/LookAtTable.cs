using Unity.Entities;
using Unity.Mathematics;
namespace SGame
{
    // 看向左面位置
    public struct LookAtTable : IComponentData
    {
        public float3 Value;
    }
}