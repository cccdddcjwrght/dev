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
        private int             m_machineID;

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
        private string          m_pathTag;
        
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

            List<Vector3>   path    = MapAgent.GetRoad(pathTag);
            Vector3         pos     = new Vector3(config.OrderPosition(0), config.OrderPosition(1), config.OrderPosition(2));
            m_orderIndex            = PathModule.FindCloseIndex(pos, path);
            if (m_orderIndex < 0)
                return false;

            m_pathPoints    = path;
            m_gap           = config.Gap;
            m_machineID     = config.MachineID;
            m_queue         = new List<Data>();
            m_orderDistance = PathModule.GetDistance(m_orderIndex, path);
            m_pathDistance  = PathModule.GetDistance(path.Count - 1, path);
            m_pathTag       = pathTag;
            return true;
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
            return true;
        }

        public int machineID => m_machineID;

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
        /// <returns></returns>
        public bool GetLinePath(Entity e, List<Vector3> path)
        {
            path.Clear();

            int order = GetOrder(e);
            // 1. 先获取距离
            if (order < 0)
            {
                // 不在队伍中
                return false;
            }
            
            // 2. 通过距离获取点
            float distance = GetLineDistance(order);
            var findIndex = PathModule.FindDistanceIndex(distance, m_pathPoints);
            if (!findIndex.isSuccess)
            {
                log.Error("path not found=" + distance + " tag=" + m_pathTag);
                return false;
            }

            for (int i = 0; i <= findIndex.Index; i++)
            {
                path.Add(m_pathPoints[i]);
            }
            if (findIndex.distance > 0)
            {
                // 有多出来的点
                path.Add(findIndex.targetPoint);
            }
            return true;
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
        /// 清空说有数据
        /// </summary>
        public void Clear()
        {
            m_datas.Clear();
        }
    }
}