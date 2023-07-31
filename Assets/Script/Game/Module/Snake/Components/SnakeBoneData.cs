using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇身體部位
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SnakeBoneData : IComponentData
    {
        // 關聯第幾個骨骼
        public int Value;

        // 蛇的本體
        public Entity snake;
    }
}