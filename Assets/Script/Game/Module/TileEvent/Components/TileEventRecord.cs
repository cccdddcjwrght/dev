using System.Runtime.CompilerServices;
using Unity.Entities;

namespace SGame
{
    // 已经事件当前进度
    [GenerateAuthoringComponent]
    public struct TileEventRecord : IComponentData
    {
        public int titleId;         // 已处理的TileEvent
        public int previousTitleId; // 上一个ID
    }
}