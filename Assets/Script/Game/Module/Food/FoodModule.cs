using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public class FoodModule : Singleton<FoodModule>
    {
        private EntityManager EntityManager;
        private EntityArchetype m_RequestType;
        private DespawnEntitySystem m_destorySystem;
        public FoodModule()
        {
            EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            m_RequestType = EntityManager.CreateArchetype(
                typeof(SpawnFoodRequestTag),
                typeof(LocalToWorld),
                typeof(LocalToParent),
                typeof(Parent),
                typeof(Rotation),
                typeof(Translation),
                typeof(FoodType),
                typeof(Child)
            );
            
            m_destorySystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<DespawnEntitySystem>();
        }
        
        /// <summary>
        /// 创建shiwu
        /// </summary>
        /// <param name="foodType"></param>
        /// <param name="parent"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        public Entity CreateFood(int foodType, float3 pos, quaternion rot)
        {
            var e = EntityManager.CreateEntity(m_RequestType);
            EntityManager.SetComponentData(e, new Translation(){Value = pos});
            EntityManager.SetComponentData(e, new Rotation(){Value = rot});
            EntityManager.SetComponentData(e, new FoodType(){Value = foodType});
            return e;
        }

        /// <summary>
        /// 销毁食物
        /// </summary>
        /// <param name="food"></param>
        public void CloseFood(Entity food)
        {
            DynamicBuffer<Child> childs = EntityManager.GetBuffer<Child>(food);
            List<Entity> closing = new List<Entity>();
            for (int i = 0; i < childs.Length; i++)
            {
                //m_destorySystem.DespawnEntity(childs[i].Value);
                //EntityManager.DestroyEntity(childs[i].Value);
                closing.Add(childs[i].Value);
            }
            foreach (var e in closing)
                EntityManager.DestroyEntity(e);
            EntityManager.DestroyEntity(food);
            //m_destorySystem.DespawnEntity(food);
        }
    }
}