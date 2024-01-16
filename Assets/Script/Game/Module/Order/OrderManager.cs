using System.Collections;
using System.Collections.Generic;
using SGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    public class OrderManager : Singleton<OrderManager>
    {
        private int lastOrderID = 0;
        
        /// <summary>
        /// 订单数据
        /// </summary>
        private Dictionary<int, OrderData> m_datas = new Dictionary<int, OrderData>();

        /// <summary>
        /// 新的订单
        /// </summary>
        private List<int>                  m_newOrders;

        /// <summary>
        /// 在餐牌里的
        /// </summary>
        private List<int>                  m_inDishOrders;
        
        public void Clear()
        {
            m_datas.Clear();
            lastOrderID = 0;
        }

        /// <summary>
        /// 创建新的订单
        /// </summary>
        /// <param name="customID"></param>
        /// <param name="makerID"></param>
        /// <param name="foodID"></param>
        /// <returns></returns>
        public OrderData Create(int customID, int makerID, int foodID)
        {
            ///EventBus.Trigger(EventHooks.Custom, );
            //EventManager.Instance.Trigger((int)GameEvent.ORDER_NEW);
            return null;
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

        /// <summary>
        /// 开始制作食物
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="cookerID"></param>
        public bool StartCook(int orderID, int cookerID)
        {
            return true;
        }

        /// <summary>
        /// 完成食物的制作
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="cookerID"></param>
        /// <returns></returns>
        public bool FinishCook(int orderID, int cookerID)
        {
            return false;
        }
    }
}