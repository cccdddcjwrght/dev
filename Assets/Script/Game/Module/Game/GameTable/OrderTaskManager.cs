using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fbg;
using FlatBuffers;
using log4net;
using SGame;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    public class OrderTask
    {
        private List<OrderData> m_tasks = new List<OrderData>();

        public int Count => m_tasks.Count;

        public OrderData Pop()
        {
            OrderData r = m_tasks[0];
            m_tasks.RemoveAt(0);
            return r;
        }

        public OrderData Peek()
        {
            return m_tasks[0];
        }
        
        public void Add(OrderData order)
        {
            m_tasks.Add(order);
        }
    }
    
    /// <summary>
    /// 待处理的订单任务
    /// </summary>
    public class OrderTaskManager : Singleton<OrderTaskManager>
    {
        // 待处理的订单
        private Dictionary<ORDER_PROGRESS, OrderTask> m_tasks;

        public OrderTaskManager()
        {
            m_tasks = new Dictionary<ORDER_PROGRESS, OrderTask>();
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER, OnOrderEvent);             // 开始订单
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER_REQUEST, OnOrderEvent);     // 请求订单
            EventManager.Instance.Reg<int>((int)GameEvent.ORDER_FOOD_READLY, OnOrderEvent); // 订单完成, 要去
            EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, Clear);
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
    }
}