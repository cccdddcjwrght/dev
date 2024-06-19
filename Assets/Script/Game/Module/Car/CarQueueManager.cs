using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using GameTools;
using log4net;
using SGame;
using Sirenix.Utilities;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    /// <summary>
    /// 排队模块
    /// </summary>
    public class CarQueue
    {
        private static ILog log = LogManager.GetLogger("game.car");

        struct Data
        {
            public Entity entity;
            public float carLength; // 汽车长度
        }
        
        /// <summary>
        /// 路径队列
        /// </summary>
        private List<Vector3>   m_pathPoints;

        /// <summary>
        /// 公交站点
        /// </summary>
        private List<Vector2Int> m_busStops;

        /// <summary>
        /// 汽车队伍
        /// </summary>
        private List<Data>      m_queue;

        /// <summary>
        /// 汽车间隔
        /// </summary>
        private float           m_gap;

        /// <summary>
        /// 排队点
        /// </summary>
        private int             m_orderIndex;

        /// <summary>
        /// 点单的工作台
        /// </summary>
        private int             m_tableID;

        /// <summary>
        /// 点单位置距离路径
        /// </summary>
        private float           m_orderDistance;

        /// <summary>
        /// 整个路径点的距离
        /// </summary>
        private float           m_pathDistance;

        /// <summary>
        /// 路径数据
        /// </summary>
        private string          m_pathTag => m_config.PathTag;

        /// <summary>
        /// 最大数量
        /// </summary>
        private int             m_max; 

        /// <summary>
        /// 最大汽车数量
        /// </summary>
        public int max => m_max;

        /// <summary>
        /// 当前汽车数量 
        /// </summary>
        public int m_carNum;

        // 配置表
        private LevelPathRowData m_config;

        /// <summary>
        /// 配置表
        /// </summary>
        public LevelPathRowData cfg => m_config;

        public List<Vector2Int> busStopses => m_busStops;

        /// <summary>
        /// 用于获取汽车随机
        /// </summary>
        public List<int> m_carIDs;
        public List<int> m_carWidgets;

        
        /// <summary>
        /// 通过路径名称 初始化排队
        /// </summary>
        /// <param name="pathTag"></param>
        /// <returns></returns>
        public bool Initalize(string pathTag)//int orderIndex, List<Vector3> path, float gap, int machine_id)
        {
            if (!PathModule.GetLevelPathInfo(pathTag, out LevelPathRowData config))
            {
                log.Error("path config not found =" + pathTag);
                return false;
            }

            m_config = config;
            List<Vector3>   path    = MapAgent.GetRoad(pathTag);
            Vector3         pos     = new Vector3(config.OrderPosition(0), config.OrderPosition(1), config.OrderPosition(2));
            m_orderIndex            = PathModule.FindCloseIndex(pos, path);
            if (m_orderIndex < 0)
                return false;

            m_pathPoints    = path;
            m_gap           = config.Gap;
            m_tableID       = 0;//config.MachineID;
            m_queue         = new List<Data>();
            m_orderDistance = PathModule.GetDistance(m_orderIndex, path);
            m_pathDistance  = PathModule.GetDistance(path.Count - 1, path);
            m_max           = config.CarNum; // 初始最大数量

            m_carIDs = new List<int>();
            m_carWidgets = new List<int>();
            for (int i = 0; i < config.CarIdLength; i++)
                m_carIDs.Add(config.CarId(i));
            for (int i = 0; i < config.CarWeightLength; i++)
                m_carWidgets.Add(config.CarWeight(i));

            SetupBusStop();
            return true;
        }
    
        void SetupBusStop()
        {
             m_busStops = new List<Vector2Int>();
             if (m_config.BusStop1Length != 2)
                 return;
             m_busStops.Add(new Vector2Int(m_config.BusStop1(0), m_config.BusStop1(1)));
             
             if (m_config.BusStop2Length != 2)
                 return;
             m_busStops.Add(new Vector2Int(m_config.BusStop2(0), m_config.BusStop2(1)));
             
             if (m_config.BusStop3Length != 2)
                 return;
             m_busStops.Add(new Vector2Int(m_config.BusStop3(0), m_config.BusStop3(1)));
        }

        public int GetRandomCar()
        {
            if (m_carIDs.Count == 0 || m_carIDs.Count != m_carWidgets.Count)
            {
                log.Error("car ids fail path id=" + m_config.Id);
                return 0;
            }
            return RandomSystem.Instance.GetRandomID(m_carIDs, m_carWidgets);
        }

        /// <summary>
        /// 添加排队车辆
        /// </summary>
        /// <param name="e"></param>
        /// <param name="length"></param>
        public bool Add(Entity e, float length = 10.0f)
        {
            if (GetOrder(e) >= 0)
            {
                // 已经有了
                log.Error("alreay add queue=" + e);
                return false;
            }
            
            m_queue.Add(new Data() {entity = e, carLength = length});
            return true;
        }

        /// <summary>
        /// 退出队列
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Remove(Entity e)
        {
            int order = GetOrder(e);
            if (order < 0)
                return false;
            
            m_queue.RemoveAt(order);
            m_carNum--;
            return true;
        }

        public int tableID { get => m_tableID; set => m_tableID = value; }

        public bool IsValid => m_tableID > 0;

        /// <summary>
        /// 获取排名
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetOrder(Entity e)
        {
            for (int i = 0; i < m_queue.Count; i++)
            {
                if (m_queue[i].entity == e)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 获取排队距离
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public float GetLineDistance(int order)
        {
            float offset = 0; // 相对开始路径的排队距离
            for (int i = 1; i <= order; i++)
            {
                offset += (m_queue[i].carLength + m_queue[i-1].carLength)/ 2 + m_gap;
            }

            float distance = m_orderDistance - offset;
            if (distance < 0)
            {
                log.Error("line distance out of queue=" + distance);
                return 0;
            }
            return distance;
        }

        /// <summary>
        /// 获取排队路径点
        /// </summary>
        /// <param name="e"></param>
        /// <param name="path"></param>
        /// <returns>返回路径点的位置</returns>
        public int GetLinePath(Entity e, int starPos, List<Vector3> path)
        {
            path.Clear();

            int order = GetOrder(e);
            // 1. 先获取距离
            if (order < 0)
            {
                // 不在队伍中
                return -1;
            }
            
            // 2. 通过距离获取点
            float distance = GetLineDistance(order);
            var findIndex = PathModule.FindDistanceIndex(distance, m_pathPoints);
            if (!findIndex.isSuccess)
            {
                log.Error("path not found=" + distance + " tag=" + m_pathTag);
                return -1;
            }

            for (int i = starPos; i <= findIndex.Index; i++)
            {
                path.Add(m_pathPoints[i]);
            }
            
            if (findIndex.distance > 0)
            {
                // 有多出来的点
                path.Add(findIndex.targetPoint);
            }
            return findIndex.Index;
        }

        /// <summary>
        /// 获取离开的路径
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool GetOrderToEndPath(List<Vector3> path)
        {
            return GetOverPathFromIndex(m_orderIndex + 1, path);
        }
        
        /// <summary>
        /// 获取离开的路径
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool GetOverPathFromIndex(int pathIndex, List<Vector3> path)
        {
            path.Clear();
            for (int i = pathIndex; i < m_pathPoints.Count; i++)
                path.Add(m_pathPoints[i]);
            return true;
        }
        
        /// <summary>
        /// 创建新的车辆
        /// </summary>
        public void NewCar()
        {
            if (!IsValid)
                return;
            
            if (m_carNum >= max)
            {
                return;
            }

            m_carNum++;
            CarModule.Create(GetRandomCarID());
        }

        /// <summary>
        /// 获得随机汽车ID
        /// </summary>
        /// <returns></returns>
        int GetRandomCarID()
        {
            if (m_carIDs.Count != m_carWidgets.Count || m_carIDs.Count == 0)
            {
                log.Error("car ids and widget value error path=" + m_config.Id);
                return 0;
            }
            
            int id = RandomSystem.Instance.GetRandomID(m_carIDs, m_carWidgets);
            return id;
        }

        /// <summary>
        /// 获得队伍第一个
        /// </summary>
        /// <returns></returns>
        public Entity GetFirst()
        {
            if (m_queue.Count == 0)
                return Entity.Null;

            return m_queue[0].entity;
        }
    }
    
    /// <summary>
    /// 汽车排队模块管理
    /// </summary>
    public class CarQueueManager : Singleton<CarQueueManager>
    {
        private static ILog log = LogManager.GetLogger("game.car");
        
        /// <summary>
        /// 关卡内包含多个队列
        /// </summary>
        private Dictionary<string, CarQueue> m_datas = new Dictionary<string, CarQueue>();

        /// <summary>
        /// 创建队伍
        /// </summary>
        /// <param name="pathTag"></param>
        /// <returns></returns>
        public CarQueue GetOrCreate(string pathTag)
        {
            if (m_datas.TryGetValue(pathTag, out CarQueue carQueue))
            {
                return carQueue;
            }

            carQueue = new CarQueue();
            if (!carQueue.Initalize(pathTag))
            {
                return null;
            }
            m_datas.Add(pathTag, carQueue);
            return carQueue;
        }
        
        /// <summary>
        /// 通过点单位置查找
        /// </summary>
        /// <param name="tablePos"></param>
        /// <returns></returns>
        public CarQueue GetOrCreateFromOrderPos(Vector2Int tablePos)
        {
            if (PathModule.GetLevelPathInfo(tablePos, out GameConfigs.LevelPathRowData config))
            {
                return GetOrCreate(config.PathTag);
            }

            return null;
        }

        /// <summary>
        /// 获得汽车队伍
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarQueue> GetCars()
        {
            return m_datas.Values;
        }

        /// <summary>
        /// 清空说有数据
        /// </summary>
        public void Clear()
        {
            m_datas.Clear();
        }

        /// <summary>
        /// 触发创建汽车 
        /// </summary>
        public void CreateNewCar()
        {
            foreach (var queue in m_datas.Values)
            {
                if (queue.IsValid)
                {
                    queue.NewCar();
                }
            }
        }
        
        /// <summary>
        /// 判断某个路径点是否已经被开启了
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool HasPath(string path)
        {
            CarQueue queue = GetOrCreate(path);
            if (queue == null)
                return false;

            return queue.IsValid;
        }
    }
}