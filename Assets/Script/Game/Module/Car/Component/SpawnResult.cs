using Unity.Entities;

namespace SGame
{
    public class SpawnResult : IComponentData
    {
        public Entity entity;

        public string error;
        
        /// <summary>
        /// 等待角色创建完毕
        /// </summary>
        /// <returns></returns>
        public bool IsReadly => entity != Entity.Null;

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool IsDone => !string.IsNullOrEmpty(error) || IsReadly;
    }
}