using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 汽车数据
    /// </summary>
    public struct CarData : IComponentData
    {
        /// <summary>
        /// 汽车ID
        /// </summary>
        public int id;
    }
}