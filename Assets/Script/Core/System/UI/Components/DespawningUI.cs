using Unity.Entities;

namespace SGame.UI
{
    /// <summary>
    /// 销毁UI对象
    /// </summary>
    public struct DespawningUI : IComponentData
    {
        public bool isDispose; // 是否强制销毁, 否则会根据配置表将UI缓存起来
    }
}

