using Unity.Entities;
using UnityEngine;
namespace SGame
{
    public class CharacterSpawnResult : IComponentData
    {
        public Entity entity;
        public int    characterID; // 角色ID
        public bool   isClose = false;

        /// <summary>
        /// 等待角色创建完毕
        /// </summary>
        /// <returns></returns>
        public bool IsReadly()
        {
            return entity != Entity.Null;
        }
        
        public bool Close()
        {
            isClose = true;
            if (IsReadly())
            {
                World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<DespawningEntity>(entity);
                entity = Entity.Null;
            }
            return true;
        }
    }
}