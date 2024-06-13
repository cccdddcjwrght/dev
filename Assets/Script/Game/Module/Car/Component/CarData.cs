using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 汽车数据
    /// </summary>
    public class CarData : IComponentData
    {
        /// <summary>
        /// 汽车ID
        /// </summary>
        public int id;
        
        /// <summary>
        /// 路径标记
        /// </summary>
        public string pathTag;
    }
}