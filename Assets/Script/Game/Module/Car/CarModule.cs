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
        private EntityManager   EntityManager;
        private EntityQuery     m_AllCars;
        private EntityQuery     m_CarCreating;
        private EntityQuery     m_CloseQuery;

        private static ILog log = LogManager.GetLogger("game.car");

        private int             m_customerNum = 0;

        public int customerNum => m_customerNum;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initalize()
        {
            EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            m_AllCars = EntityManager.CreateEntityQuery(typeof(CarData),
                                                                        ComponentType.Exclude<DespawningTag>());
            m_CarCreating = EntityManager.CreateEntityQuery(typeof(SpawnCarSystem.SpawnRequest),
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
            m_customerNum = 0;

            EventManager.Instance.Reg((int)GameEvent.GAME_START, OnGameStart);
            EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLeaveRoom);
        }

        public void AddCustomerRef(int num)
        {
            
        }

        public void ReleaseCustomerRef(int num)
        {
            
        }
        
        void OnGameStart()
        {
            TestCreateCar();
        }

        /// <summary>
        /// 离开房间
        /// </summary>
        void OnLeaveRoom()
        {
            CarQueueManager.Instance.Clear();
            ClearAll();
        }

        void TestCreateCar()
        {
            //Create(1);
        }
        
        /// <summary>
        /// 创建汽车
        /// </summary>
        /// <param name="carID">汽车ID</param>
        /// <param name="pathName">汽车路径点</param>
        public static void Create(int carID)
        {
            if (!ConfigSystem.Instance.TryGet(carID, out GameConfigs.CarDataRowData config))
            {
                log.Error("CarData Config Not Found=" + carID);
                return;
            }
            var roads = MapAgent.GetRoad(config.PathTag);
            if (roads == null || roads.Count == 0)
            {
                log.Error("not found path");
                return;
            }

            SpawnCarSystem.SpawnRequest.Create(carID, roads[0], 0);
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
        /// 返回所有汽车数量
        /// </summary>
        /// <returns></returns>
        public int CarNum => m_AllCars.CalculateEntityCount() + m_CarCreating.CalculateEntityCount();

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
            entities.Dispose();
        }

        /// <summary>
        /// 获得CarMono对象
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public CarMono Get(Entity e)
        {
            if (EntityManager.Exists(e) && EntityManager.HasComponent<CarMono>(e))
            {
                return EntityManager.GetComponentObject<CarMono>(e);
            }

            return null;
        }




    }
}
