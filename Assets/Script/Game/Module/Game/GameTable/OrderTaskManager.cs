using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using log4net;
using SGame;
using Sirenix.Utilities;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace SGame
{
    public class OrderTaskItem
    {
        public OrderData    m_order;       // 订单
        public float        m_workTime;    // 预估订单处理时间
        public int          m_characterID; // 角色ID, 分配的角色ID
        public bool         m_isRemoved;   // 是否已经删除(已分配给玩家)
        //public bool         m_isReadly;     // 任务是否准备号
        private static ILog log = LogManager.GetLogger("game.order");
        
        public bool isReadly {
            get
            {
                if (m_order.progress == ORDER_PROGRESS.ORDED)
                {
                    // 需要准备号作为
                    return false;
                    // m_order.CookerTake()
                }

                return true;
            }
        }

        public OrderTaskItem(OrderData order)
        {
            m_workTime = 0;
            m_characterID = 0;
            m_order = order;
            m_isRemoved = false;
            //m_isReadly = false;
        }

        public Vector2Int GetPosition()
        {
            var pos = m_order.GetPosition();
            return new Vector2Int(pos.x, pos.y);
        }

        public Vector2Int GetCustomerPos()
        {
            var pos = m_order.customer.map_pos;
            return new Vector2Int(pos.x, pos.y);
        }

        public Vector2Int GetWorkerPos()
        {
            var pos = m_order.workerChair.map_pos;
            return new Vector2Int(pos.x, pos.y);
        }

        /// <summary>
        /// 派发任务
        /// </summary>
        public void Dispatch()
        {
            if (m_isRemoved)
            {
                log.Error("order task already dispatch=" + m_order.id + " characterID=" + m_characterID);
                return;
            }

            Character c = CharacterModule.Instance.FindCharacter(m_characterID);
            if (c == null)
            {
                log.Error("dispatch character is null=" + m_characterID);
                return;
            }

            /*
            if (c.taskNum >= 4)
            {
                log.Warn("too manay orders = " + c.taskNum + " id=" + c.CharacterID);
                return;
            }
            */

            c.TakeOrder(m_order);
            m_characterID = 0;
            m_isRemoved = true;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="characterID"></param>
        /// <param name="score"></param>
        public void UpdateScore(int characterID, float workTime)
        {
            if (m_characterID == 0 || workTime < m_workTime)
            {
                m_characterID = characterID;
                m_workTime = workTime;
            }
        }
        
        /// <summary>
        /// 比较函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Compare(OrderTaskItem a, OrderTaskItem b)
        {
            if (a.m_characterID == b.m_characterID)
            {
                if (a.m_workTime < b.m_workTime)
                    return -1;
                else if (a.m_workTime > b.m_workTime)
                    return 1;

                return 0;
            }

            return a.m_characterID < b.m_characterID ? -1 : 1;
        }
    }
    
    public class OrderTask
    {
        private static ILog     log = LogManager.GetLogger("game.order");
        private List<OrderTaskItem>      m_tasks = new List<OrderTaskItem>();
        

        public int Count => m_tasks.Count;

        public List<OrderTaskItem> Datas => m_tasks;
        
        public OrderTaskItem Get(int index) => m_tasks[index];
        
        /// <summary>
        /// 添加新的订单
        /// </summary>
        /// <param name="order"></param>
        public void Add(OrderData order)
        {
            m_tasks.Add(new OrderTaskItem(order));
        }

        public void UpdateRemoves()
        {
            for (int i = m_tasks.Count - 1; i >= 0; i--)
            {
                if (m_tasks[i].m_isRemoved)
                {
                    m_tasks.RemoveAt(i);
                }
            }
        }
        
        
        /*
        public OrderData Pop()
        {
            OrderData r = m_tasks[0];
            m_tasks.RemoveAt(0);
            //Comparer<int>
            return r;
        }

        public OrderData Peek()
        {
            return m_tasks[0];
        }
        */
    }
    
    /// <summary>
    /// 待处理的订单任务
    /// </summary>
    public class OrderTaskManager : Singleton<OrderTaskManager>
    {
        // 待处理的订单
        private Dictionary<ORDER_PROGRESS, OrderTask> m_tasks;
        private List<OrderTaskItem>                  m_caches;

        public OrderTaskManager()
        {
            m_tasks = new Dictionary<ORDER_PROGRESS, OrderTask>();
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER, OnOrderEvent);             // 开始订单
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER_REQUEST, OnOrderEvent);     // 请求订单
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER_FOOD_READLY, OnOrderEvent); // 订单完成, 要去
            EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, Clear);

            m_caches = new List<OrderTaskItem>();
        }

        void Clear()
        {
            m_tasks.Clear();
        }

        void OnOrderEvent(int orderID)
        {
            var order = OrderManager.Instance.Get(orderID);
            OrderTask task = GetOrCreateTask(order.progress);
            task.Add(order);
        }

        public OrderTask GetOrCreateTask(ORDER_PROGRESS progress)
        {
            if (m_tasks.TryGetValue(progress, out OrderTask task))
            {
                return task;
            }

            task = new OrderTask();
            m_tasks.Add(progress, task);
            return task;
        }

        
        public int TaskCount
        {
            get
            {
                int count = 0;
                foreach (var item in m_tasks)
                {
                    count += item.Value.Count;
                }

                return count;
            }
        }
        
        /// <summary>
        /// 派发角色任务
        /// </summary>
        public void DispatchEvent()
        {
            m_caches.Clear();
            
            // 排序并过滤任务, 只处理可用处理的任务
            foreach (var item in m_tasks)
            {
                foreach (var task in item.Value.Datas)
                {
                    if (task.isReadly && task.m_characterID > 0)
                    {
                        m_caches.Add(task);
                    }
                }
            }
            if (m_caches.Count == 0)
                return;
            
            // 对得分排序
            m_caches.Sort(OrderTaskItem.Compare);
            
            // 只分配一个对象
            int lastCharacter = 0;
            foreach (var item in m_caches)
            {
                if (item.m_characterID != 0 && lastCharacter != item.m_characterID)
                {
                    lastCharacter = item.m_characterID;
                    item.Dispatch();
                }
            }
            
            // 删除多余订单数据
            foreach (var item in m_tasks)
            {
                item.Value.UpdateRemoves();
            }

            // 清空角色信息
            foreach (var item in m_caches)
                item.m_characterID = 0;
        }
    }
}