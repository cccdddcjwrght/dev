using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 角色订单相关状态存储
    /// </summary>
    public class CharacterOrderRecord
    {
        private static ILog log = LogManager.GetLogger("game.character");
        //private bool m_isIdle       = false;
        private int  m_characterID  = 0;
        private int  m_takeOrderNum     = 0;  // 

        /// <summary>
        /// 任务数据
        /// </summary>
        public struct TaskData
        {
            public OrderData orderData;      // 订单数据
            public ChairData customerChair;  // 点单的顾客位置
            public ChairData workerChair;    // 分配的工作位置

            public bool isOrder => orderData != null;
        }

        // 
        private List<TaskData> m_datas = new List<TaskData>();
        
        /// <summary>
        /// 是否是空闲状态
        /// </summary>
        public bool isIdle => m_datas.Count == 0;

        public bool hasWorking => m_datas.Count > 0;//(m_orderData != null || m_customerChair != ChairData.Null);

        // 身上待处理的订单
        //private OrderData m_orderData = null; 

        // 待处理的角色座椅, 用于去到客户位置
        //private ChairData m_customerChair = ChairData.Null;
        
        // 工作台位置, 用于制作食物 
        //private ChairData m_workerChair = ChairData.Null;

        public OrderData order => m_datas[0].orderData;
        
        public int       orderID => m_datas.Count > 0 && m_datas[0].orderData != null ? m_datas[0].orderData.id : 0;

        public ChairData customerChair => m_datas[0].customerChair;
        
        public ChairData workerChair => m_datas[0].workerChair;

        public int takeOrderNum => m_takeOrderNum;

        public void Initalize(int characterID)
        {
            m_characterID = characterID;
            m_datas.Clear();
        }
        
        /// <summary>
        /// 添加工作订单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="workerChair">工位</param>
        public bool AddOrder(OrderData order, ChairData workerChair)
        {
            if (workerChair == ChairData.Null)
            {
                log.Error("worker chair is null");
                return false;
            }

            AddTask(new TaskData() { orderData = order, workerChair = workerChair});
            
            // 锁定座位
            TableManager.Instance.SitChair(workerChair, m_characterID);

            // 修改订单状态
            order.CookerTake(m_characterID);
            return true;
        }

        /// <summary>
        /// 添加取餐订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddFoodReadlyOrder(OrderData order)
        {
            AddTask(new TaskData() { orderData = order });
            order.MoveToCustom(m_characterID);
            return true;
        }

        /// <summary>
        /// 添加待处理椅子
        /// </summary>
        /// <param name="chair"></param>
        public void AddCustomerChair(ChairData chair)
        {
            AddTask(new TaskData() { orderData = null, customerChair = chair});
        }


        public bool hasOrder => m_datas.Count > 0 && m_datas[0].isOrder;//m_orderData != null;

        public bool hasCustomerChair => m_datas.Count > 0 && !m_datas[0].isOrder; //m_customerChair != ChairData.Null;

        /// <summary>
        /// 进入IDLE
        /// </summary>
        public void EnterIdle()
        {
            // 没有待处理的数据
            PopTask();
            //LeaveAllChairs();
        }

        void AddTask(TaskData task)
        {
            if (task.isOrder)
                m_takeOrderNum = 0;
            else
                m_takeOrderNum++;
            m_datas.Add(task);
        }

        void PopTask()
        {
            if (m_datas.Count == 0)
            {
                log.Error("data is null");
                return;
            }

            var item = m_datas[0];
            if (item.isOrder && item.workerChair != ChairData.Null)
            {
                TableManager.Instance.LeaveChair(item.workerChair, m_characterID);
            }
            m_datas.RemoveAt(0);
        }
        
        /// <summary>
        /// 解锁座位
        /// </summary>
        public void LeaveAllChairs()
        {
            var item = m_datas[0];
            if (item.isOrder && item.workerChair != ChairData.Null)
            {
                TableManager.Instance.LeaveChair(item.workerChair, m_characterID);
                item.workerChair = ChairData.Null;
                item.customerChair = ChairData.Null;
                m_datas[0] = item;
            }
        }
    }
}