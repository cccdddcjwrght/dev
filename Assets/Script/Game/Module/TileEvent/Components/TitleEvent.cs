using Unity.Entities;

namespace SGame
{
    // 已经事件当前进度
    public struct TitleEvent : IComponentData
    {
        public int titleId; // 已处理的TileEvent
    }
}