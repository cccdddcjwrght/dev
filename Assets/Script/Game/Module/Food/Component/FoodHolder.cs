
using Unity.Entities;

namespace SGame
{
    // 食物持有
    [GenerateAuthoringComponent]
    public struct FoodHolder : IComponentData
    {
        public Entity Value;
    }
}