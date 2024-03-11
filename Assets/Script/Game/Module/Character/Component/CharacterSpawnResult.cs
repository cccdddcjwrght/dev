using Unity.Entities;
using UnityEngine;
namespace SGame
{
    public class CharacterSpawnResult : IComponentData
    {
        public Entity entity;
        public int    characterID; // 角色ID

        /// <summary>
        /// 等待角色创建完毕
        /// </summary>
        /// <returns></returns>
        public bool IsReadly()
        {
            return entity != Entity.Null;
        }
    }
}