using System.Collections.Generic;
using System.Linq;
using log4net;


namespace SGame
{
    /// <summary>
    /// 订单统计模块, 用户统计某个类型的食材订单有多少个
    /// </summary>
    public class OrderCounter
    {
        private ILog                        log           = LogManager.GetLogger("game.order");
        private List<int>                   m_removeKeys  = new List<int>() ;
        private Dictionary<int, List<int>>  m_datas                         ;

        public OrderCounter()
        {
            m_datas = new Dictionary<int, List<int>>();
        }

        /// <summary>
        /// 创建一个订单统计
        /// </summary>
        /// <returns></returns>
        public static OrderCounter Create()
        {
            return new OrderCounter();
        }
        
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool Add(int orderID)
        {
            var order = OrderManager.Instance.Get(orderID);
            if (order == null || order.IsFinished)
            {
                log.Error("order not found or finish=" + orderID);
                return false;
            }

            if (order.foodType <= 0)
            {
                log.Error("order foodType not found!" + orderID);
                return false;
            }

            if (!m_datas.TryGetValue(order.foodType, out List<int> orders))
            {
                orders = new List<int>();
                m_datas.Add(order.foodType, orders);
            }
            if (orders.Contains(orderID))
            {
                log.Error("order id repeate=" + orderID);
                return false;
            }
            
            
            return true;
        }

        /// <summary>
        /// 获得食物类型
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public List<int> GetFoodTypes(List<int> orders)
        {
            orders.Clear();
            orders.AddRange(m_datas.Keys);
            return orders;
        }

        /// <summary>
        /// 获得某个食物订单数量
        /// </summary>
        /// <param name="foodType"></param>
        /// <returns></returns>
        public int GetFoodNum(int foodType)
        {
            if (foodType == 0 && m_datas.Count > 0)
            {
                return m_datas.First().Value.Count;
            }
            
            if (!m_datas.TryGetValue(foodType, out List<int> orders))
            {
                return 0;
            }

            return orders.Count;
        }

        /// <summary>
        /// 判断是否空了
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return m_datas.Count == 0;
        }
        
        /// <summary>
        /// 更新订单状态
        /// </summary>
        public void Update()
        {
            OrderManager manager = OrderManager.Instance;
            m_removeKeys.Clear();
            foreach (var item in m_datas)
            {
                var values = item.Value;
                
                // 删除完成的订单
                for (int i = values.Count - 1; i >= 0; i--)
                {
                    OrderData order = manager.Get(values[i]);
                    if (order == null || order.IsFinished)
                    {
                        values.RemoveAt(i);
                    }
                }
                if (values.Count == 0)
                {
                    m_removeKeys.Add(item.Key);
                }
            }

            foreach (var key in m_removeKeys)
                m_datas.Remove(key);
            m_removeKeys.Clear();
        }
    }
}