using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SGame
{
    /*
    public class FoodModule : Singleton<FoodModule>
    {
        private int lastFoodID = 0;
        private Dictionary<int, Entity> m_foods;
        
        public int CreateFood(int foodType, Entity parent, float3 pos, quaternion rot)
        {
            lastFoodID++;

            SpawnFoodRequest request = new SpawnFoodRequest()
            {
                foodID = lastFoodID,
                parent = parent,
                pos = pos,
                rot = rot,
            };

            var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var e = EntityManager.CreateEntity(typeof(SpawnFoodRequest));
            EntityManager.SetComponentData(e, request);
            return lastFoodID;
        }

        public void Reg(int foodID, Entity food)
        {
            m_foods.Add(foodID, food);
        }
    }
            */

}