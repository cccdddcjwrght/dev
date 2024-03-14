using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;
namespace SGame
{
    public struct SpawnFoodRequestTag : IComponentData
    {
    }

    public struct FoodInitalizedTag : IComponentData
    {
    }

    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial class SpawnFoodSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("game.food");
        private BeginSimulationEntityCommandBufferSystem m_commandBuffer;

        private string[] RES_PATHS = new string[]
        {
            "Assets/BuildAsset/Prefabs/ECS/food/goods_1.prefab",
            "Assets/BuildAsset/Prefabs/ECS/food/goods_2.prefab",
            "Assets/BuildAsset/Prefabs/ECS/food/goods_3.prefab",
            "Assets/BuildAsset/Prefabs/ECS/food/goods_4.prefab",
            "Assets/BuildAsset/Prefabs/ECS/food/goods_5.prefab",
        };

        private EntityQuery m_foodsQuery;
        private Dictionary<int, Entity> m_foodsPrefab;
        
        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();


            m_foodsQuery = EntityManager.CreateEntityQuery(typeof(FoodType), typeof(Prefab));
            m_foodsPrefab = new Dictionary<int, Entity>(RES_PATHS.Length);
            RequireForUpdate(m_foodsQuery);
            
            EventManager.Instance.Reg((int)GameEvent.ENTER_GAME, OnGameInitAfter);
        }
    

        void OnGameInitAfter()
        {
            log.Info("OnGameInitAfter SpawnFoodSystem");
            foreach (var r in RES_PATHS)
            {
                ECSPrefabManager.Instance.AddPrefab(r);
            }
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            var prefabs = m_foodsQuery.ToEntityArray(Allocator.Temp);
            var types = m_foodsQuery.ToComponentDataArray<FoodType>(Allocator.Temp);
            for (int i = 0; i < prefabs.Length; i++)
            {
                var v = types[i];
                var e = prefabs[i];
                EntityManager.AddComponent<Parent>(e);
                EntityManager.AddComponent<LocalToParent>(e);
                m_foodsPrefab.Add(v.Value, e);
            }
            prefabs.Dispose();
            types.Dispose();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithNone<DespawningEntity>().ForEach((Entity e, in SpawnFoodRequestTag req, in FoodType foodType, in LocalToWorld ltw) =>
            {
                commandBuffer.RemoveComponent<SpawnFoodRequestTag>(e);
                if (!m_foodsPrefab.TryGetValue(foodType.Value, out Entity prefab))
                {
                    log.Error("foodType not found = " + foodType.Value);
                    return;
                }

                // 添加子节点
                var newFood = commandBuffer.Instantiate(prefab);
                commandBuffer.SetComponent(newFood, new Parent(){Value = e});
                commandBuffer.AppendToBuffer(e, new Child(){Value = newFood});
            }).WithoutBurst().Run();
        }
    }
}