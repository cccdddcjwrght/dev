using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using GameTools;
using log4net;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 汽车, 船等载具模块
    /// </summary>
    public class CarModule : Singleton<CarModule>
    {
        private EntityManager EntityManager;
        private EntityQuery m_AllCars;
        private EntityQuery m_CloseQuery;

        private static ILog log = LogManager.GetLogger("game.car");

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initalize()
        {
            EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            m_AllCars = EntityManager.CreateEntityQuery(typeof(CarData),
                                                                        ComponentType.Exclude<DespawningTag>());
            
            
            var query = new EntityQueryDesc()
            {
                Any = new ComponentType[]
                {
                    typeof(CarData),
                    typeof(SpawnCarSystem.SpawnRequest)
                },
                
                None = new ComponentType[] {
                    typeof(DespawningTag)
                }
            };
            m_CloseQuery = EntityManager.CreateEntityQuery(query);

            EventManager.Instance.Reg((int)GameEvent.GAME_START, OnGameStart);
        }
        
        void OnGameStart()
        {
            TestCreateCar();
        }

        void TestCreateCar()
        {
            Create(1, "path001");
        }
        
        
        /// <summary>
        /// 创建汽车
        /// </summary>
        /// <param name="carID">汽车ID</param>
        /// <param name="pathName">汽车路径点</param>
        public void Create(int carID, string pathName)
        {
            var roads = MapAgent.GetRoad(pathName);
            if (roads == null || roads.Count == 0)
            {
                log.Error("not found path");
                return;
            }

            SpawnCarSystem.SpawnRequest.Create(carID, pathName, roads[0], 0);
        }

        /// <summary>
        /// 删除汽车
        /// </summary>
        /// <param name="e"></param>
        public void Close(Entity e)
        {
            if (EntityManager.Exists(e) && !EntityManager.HasComponent<DespawningTag>(e))
            {
                EntityManager.AddComponent<DespawningTag>(e);
            }
        }

        /// <summary>
        /// 删除所有汽车相关对象
        /// </summary>
        public void ClearAll()
        {
            var entities = m_CloseQuery.ToEntityArray(Allocator.Temp);
            foreach (var e in entities)
            {
                EntityManager.AddComponent<DespawningTag>(e);
            }
            m_CloseQuery.Dispose();
        }
    }
}
