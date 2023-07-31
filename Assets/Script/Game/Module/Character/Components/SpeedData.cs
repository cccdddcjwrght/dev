using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 移动速度组件, 涉及性能的组件尽量使用 struct 
    /// </summary>
    [GenerateAuthoringComponent]
    public struct SpeedData : IComponentData
    {
        // 移动速度
        public float Value;
    }
}