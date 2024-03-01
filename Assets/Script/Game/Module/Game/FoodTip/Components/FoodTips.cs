using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 金币信息
    /// </summary>
    public struct FoodTips : IComponentData
    {
        // 金币数量
        public double gold;

        // 金币HUD
        public Entity ui;
    }
}