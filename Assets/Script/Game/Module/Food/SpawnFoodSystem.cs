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
    public struct SpawnFoodRequest : IComponentData
    {
        // 食物类型
        public int   foodType;

        // 父节点
        public Entity parent;

        // 偏移量
        public float3 pos;

        // 旋转
        public quaternion rot;

        // 是否显示
        public bool isShow;
    }
    
    public partial class SpawnFoodSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("game.food");
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;
        public struct FoodInitalizeTag : IComponentData
        {
        }

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
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();


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
                m_foodsPrefab.Add(v.Value, prefabs[i]);
            }
            prefabs.Dispose();
            types.Dispose();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.ForEach((Entity e, in SpawnFoodRequest req) =>
            {
                if (!m_foodsPrefab.TryGetValue(req.foodType, out Entity prefab))
                {
                    log.Error("foodType not found = " + req.foodType);
                    return;
                }

                var parent = req.parent;
                var newFood = commandBuffer.Instantiate(prefab);
                if (parent != Entity.Null && EntityManager.Exists(parent))
                {
                    commandBuffer.SetComponent(parent, new FoodHolder() { Value = newFood });
                    commandBuffer.AddComponent<Parent>(newFood);
                    commandBuffer.AddComponent<LocalToParent>(newFood);
                    commandBuffer.SetComponent(newFood, new Parent(){Value = parent});

                    if (!EntityManager.HasComponent<Child>(parent))
                    {
                        DynamicBuffer<Child> buff = commandBuffer.AddBuffer<Child>(parent);
                        buff.Add(new Child() { Value = newFood });
                    }
                    else
                    {
                        DynamicBuffer<Child> buff = EntityManager.GetBuffer<Child>(parent);
                        commandBuffer.AppendToBuffer<Child>(parent, new Child(){Value = newFood});
                    }
                }
                
                
                //commandBuffer.AddBuffer<>()
                
                commandBuffer.SetComponent(newFood, new Translation() {Value = req.pos});
                commandBuffer.SetComponent(newFood, new Rotation() {Value = req.rot});
                commandBuffer.DestroyEntity(e);

                if (req.isShow == false)
                {
                    commandBuffer.AddComponent<Disabled>(newFood);
                }
            }).WithoutBurst().Run();

            /*
            Entities.WithAll<FoodType>().WithNone<FoodInitalizeTag>().ForEach((Entity e, FoodID id) =>
            {
                commandBuffer.AddComponent<FoodInitalizeTag>(e);
                FoodModule.Instance.Reg(id.Value, e);
            }).WithoutBurst().Run();
            */
        }
    }
}