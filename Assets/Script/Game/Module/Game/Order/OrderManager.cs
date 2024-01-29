using System.Collections;
using System.Collections.Generic;
using log4net;
using SGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    public class OrderManager : Singleton<OrderManager>
    {
        private int lastOrderID = 0;

        private static ILog log = LogManager.GetLogger("game.order");
        
        /// <summary>
        /// 订单数据
        /// </summary>
        private Dictionary<int, OrderData>  m_datas     = new Dictionary<int, OrderData>();
        private List<int>                   m_caches    = new List<int>();

        public void Initalize()
        {
            m_datas.Clear();
            m_caches.Clear();
        }
        
        /// <summary>
        /// 清空订单数据
        /// </summary>
        public void Clear()
        {
            m_datas.Clear();
            m_caches.Clear();
            lastOrderID = 0;
        }

        /// <summary>
        /// 根据进度查找订单
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public List<int> FindOrders(ORDER_PROGRESS progress)
        {
            m_caches.Clear();
            foreach (var item in m_datas.Values)
            {
                if (item.progress == progress)
                {
                    m_caches.Add(item.id);
                }
            }

            return m_caches;
        }

        /// <summary>
        /// 获得第一个订单
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public int FindFirstOrder(ORDER_PROGRESS progress)
        {
            foreach (var item in m_datas.Values)
            {
                if (item.progress == progress)
                {
                    return item.id;
                }
            }

            return 0;
        }

        /// <summary>
        /// 创建新的订单
        /// </summary>
        /// <param name="customID"></param>
        /// <param name="makerID"></param>
        /// <param name="foodID"></param>
        /// <returns></returns>
        public OrderData Create(int customID)
        {
            lastOrderID++;
            OrderData order = OrderData.Create(lastOrderID, customID);
            order.startTime = Time.realtimeSinceStartup;

            if (!m_datas.TryAdd(order.id, order))
            {
                log.Error("order id add fail=" + order.id);
                return null;
            }
            
            EventManager.Instance.Trigger((int)GameEvent.ORDER_NEW, order.id);
            return order;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CloseOrder(int orderID)
        {
            var order = Get(orderID);
            if (order == null)
            {
                log.Error("order not found =" + orderID);
                return false;
            }
            if (order.progress != ORDER_PROGRESS.FINISH)
            {
                log.Warn("order is not finish id=" + orderID);
            }
            
            m_datas.Remove(orderID);
            return true;
        }

        /// <summary>
        /// 查询订单数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderData Get(int orderID)
        {
            if (m_datas.TryGetValue(orderID, out OrderData data))
            {
                return data;
            }

            return null;
        }
    }
}