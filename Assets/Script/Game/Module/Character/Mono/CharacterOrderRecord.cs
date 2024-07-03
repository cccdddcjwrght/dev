using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 角色订单相关状态存储
    /// </summary>
    public class CharacterOrderRecord
    {
        private static ILog log = LogManager.GetLogger("game.character");
        private bool m_isIdle       = false;
        private int  m_characterID  = 0;
        private int  m_takeOrderNum     = 0;  // 
        
        /// <summary>
        /// 是否是空闲状态
        /// </summary>
        public bool isIdle => m_isIdle;

        public bool hasWorking => !m_isIdle && (m_orderData != null || m_customerChair != ChairData.Null);

        // 身上待处理的订单
        private OrderData m_orderData = null; 

        // 待处理的角色座椅, 用于去到客户位置
        private ChairData m_customerChair = ChairData.Null;
        
        // 工作台位置, 用于制作食物 
        private ChairData m_workerChair = ChairData.Null;

        public OrderData order => m_orderData;
        
        public int       orderID => m_orderData != null ? m_orderData.id : 0;

        public ChairData customerChair => m_customerChair;
        
        public ChairData workerChair => m_workerChair;

        public int takeOrderNum => m_takeOrderNum;

        public void Initalize(int characterID)
        {
            m_characterID = characterID;
            m_isIdle = false;
        }
        
        /// <summary>
        /// 添加工作订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="workerChair">工位</param>
        public bool AddOrder(OrderData order, ChairData workerChair)
        {
            m_takeOrderNum = 0;
            m_orderData = order;
            if (workerChair == ChairData.Null)
            {
                log.Error("worker chair is null");
                return false;
            }
            
            // 锁定座位
            TableManager.Instance.SitChair(workerChair, m_characterID);
            m_workerChair = workerChair;

            // 修改订单状态
            m_orderData.CookerTake(m_characterID);
            m_isIdle = false;
            return true;
        }

        /// <summary>
        /// 添加取餐订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddFoodReadlyOrder(OrderData order)
        {
            m_takeOrderNum = 0;
            m_orderData = order;
            order.MoveToCustom(m_characterID);
            m_isIdle = false;
            return true;
        }

        /// <summary>
        /// 添加待处理椅子
        /// </summary>
        /// <param name="chair"></param>
        public void AddCustomerChair(ChairData chair)
        {
            m_takeOrderNum++;
            m_customerChair = chair;
            m_isIdle = false;
        }

        public void WaiterTakeFoodToCustomer(ChairData chair)
        {
            
        }

        public bool hasOrder => m_orderData != null;

        public bool hasCustomerChair => m_customerChair != ChairData.Null;

        /// <summary>
        /// 进入IDLE
        /// </summary>
        public void EnterIdle()
        {
            // 没有待处理的数据
            m_orderData = null;
            LeaveAllChairs();
            m_isIdle = true;
        }

        /// <summary>
        /// 解锁座位
        /// </summary>
        public void LeaveAllChairs()
        {
            if (m_workerChair != ChairData.Null)
            {
                TableManager.Instance.LeaveChair(m_workerChair, m_characterID);
                m_workerChair = ChairData.Null;
            }
            
            m_customerChair = ChairData.Null;
        }
    }
}