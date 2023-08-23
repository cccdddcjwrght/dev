using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 建筑基础数据
    /// </summary>
    public struct BuildingData : IComponentData
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int      id;

        /// <summary>
        /// 建筑等级
        /// </summary>
        public int      level;

        /// <summary>
        /// 建筑资源
        /// </summary>
        public int      res;

        /// <summary>
        /// 建筑类型
        /// </summary>
        public int      buildType;
    }
}
