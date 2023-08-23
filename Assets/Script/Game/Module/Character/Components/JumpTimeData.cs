using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 移动速度组件, 涉及性能的组件尽量使用 struct 
    /// </summary>
    [GenerateAuthoringComponent]
    public struct JumpTimeData : IComponentData
    {
        // 移动时间 
        public float Value;
    }
}
