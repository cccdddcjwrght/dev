using System.Collections.Generic;
using Unity.Entities;
using log4net;

namespace SGame
{
    public class QueueData
    {
        private static ILog log = LogManager.GetLogger("game.car");

        public struct Data
        {
            public Entity entity;
            public float carLength; // 汽车长度
        }
        
        /// <summary>
        /// 汽车队伍
        /// </summary>
        private List<Data>      m_queue = new List<Data>();

        /// <summary>
        /// 最大数量
        /// </summary>
        private int             m_max = 0; 

        /// <summary>
        /// 最大汽车数量
        /// </summary>
        public int max      => m_max;

        /// <summary>
        /// 当前汽车数量 
        /// </summary>
        public int m_carNum = 0;

        private int m_id = 0;

        public bool isFull => m_carNum >= m_max;

        public int Count => m_queue.Count;

        public int id => m_id;
        
        public QueueData(int id, int maxNum)
        {
            m_id = id;
            m_max = maxNum;
            m_carNum = 0;
        }

        public Data Get(int index) => m_queue[index];

        public void AddCar() => m_carNum++;
        
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
            {
                log.Error("leave queue fail=" + e);
                return false;
            }

            m_queue.RemoveAt(order);
            m_carNum--;
            return true;
        }

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
  
    // 共享排队队列
    public class ShareQueueManager : Singleton<ShareQueueManager>
    {
        // 共享队列数据
        private Dictionary<int, QueueData> m_datas = new Dictionary<int, QueueData>();
        
        public QueueData GetOrCreate(int queueID, int maxNum)
        {
            if (queueID == 0)
            {
                return new QueueData(0, maxNum);
            }

            // 创建队列
            if (m_datas.TryGetValue(queueID, out QueueData data))
            {
                return data;
            }
            data = new QueueData(queueID, maxNum);
            m_datas.Add(queueID, data);
            return data;
        }

        public void Clear()
        {
            m_datas.Clear();
        }
    }
}
