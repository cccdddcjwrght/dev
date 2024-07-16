using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

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
        private int  m_roleID = 0;

        /// <summary>
        /// 任务数据
        /// </summary>
        public struct TaskData
        {
            public OrderData orderData;      // 订单数据
            public float     workTime;       // 用于缓存订单时间, 用于后续判定订单分配

            public bool isOrder => orderData.progress != ORDER_PROGRESS.WAIT_ORDER;

            /// <summary>
            /// 获得任务完成时的位置
            /// </summary>
            /// <param name="pos"></param>
            /// <returns></returns>
            public bool GetPosition(out int2 pos)
            {
                pos = int2.zero;
                pos = orderData.GetPosition();
                return true;
            }
        }

        // 
        private List<TaskData> m_datas = new List<TaskData>();
        
        /// <summary>
        /// 是否是空闲状态
        /// </summary>
        public bool isIdle => m_datas.Count == 0;

        public bool hasWorking => m_datas.Count > 0;//(m_orderData != null || m_customerChair != ChairData.Null);
        

        public OrderData order => m_datas[0].orderData;

        /// <summary>
        /// 身上任务数量 
        /// </summary>
        public int taskNum => m_datas.Count;
        
        public int       orderID => m_datas.Count > 0 && m_datas[0].orderData != null ? m_datas[0].orderData.id : 0;

        public ChairData customerChair => m_datas[0].orderData.customer;
        
        public ChairData workerChair => m_datas[0].orderData.workerChair;

        public int takeOrderNum => m_datas.Count == 0 ? 0 : m_takeOrderNum;

        public void Initalize(int characterID, int roleID)
        {
            m_characterID   = characterID;
            m_roleID        = roleID;
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

            if (order.progress != ORDER_PROGRESS.ORDED)
            {
                log.Error("order progress error=" + order.progress);
                return false;
            }

            
            // 锁定座位
            TableManager.Instance.SitChair(workerChair, m_characterID);

            // 修改订单状态
            order.CookerTake(m_characterID, workerChair);
            AddTask(new TaskData() { orderData = order});
            return true;
        }

        /// <summary>
        /// 添加取餐订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddFoodReadlyOrder(OrderData order)
        {
            order.MoveToCustom(m_characterID);
            AddTask(new TaskData() { orderData = order });
            return true;
        }
        
        /// <summary>
        /// 添加待处理椅子订单
        /// </summary>
        /// <param name="chair"></param>
        public void AddCustomerChair(OrderData order)
        {
            AddTask(new TaskData() { orderData = order });
        }

        public bool hasOrder => m_datas.Count > 0 && m_datas[0].isOrder;//m_orderData != null;

        public bool hasCustomerChair => m_datas.Count > 0 && !m_datas[0].isOrder; //m_customerChair != ChairData.Null;

        /// <summary>
        /// 进入IDLE
        /// </summary>
        public void EnterIdle()
        {
            // 没有待处理的数据
            if (m_datas.Count > 0)
                PopTask();
        }

        void AddTask(TaskData task)
        {
            if (task.isOrder)
                m_takeOrderNum = 0;
            else
                m_takeOrderNum++;

            task.workTime = TableUtils.GetOrderWorkTime(task.orderData, m_roleID);
            //log.Debug("Order Push order id=" + task.orderData.id + " progress=" + task.orderData.progress + " characterID=" + m_characterID + " worktime=" + task.workTime);
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
            /*
            if (item.orderData.workerChair != ChairData.Null)
            {
                if (!TableManager.Instance.LeaveChair(item.orderData.workerChair, m_characterID))
                {
                    log.Error("leave chair fail order id=" + item.orderData.id + " progress=" + item.orderData.progress);
                }
            }
            */
            //log.Debug("Order Pop order id=" + m_datas[0].orderData.id + " progress=" + m_datas[0].orderData.progress + " characterID=" + m_characterID);
            m_datas.RemoveAt(0);
        }
        
        /// <summary>
        /// 解锁座位
        /// </summary>
        public void LeaveAllChairs()
        {
            var item = m_datas[0];
            if (item.orderData.workerChair != ChairData.Null)
            {
                TableManager.Instance.LeaveChair(item.orderData.workerChair, m_characterID);
                m_datas[0] = item;
            }
        }

        /// <summary>
        /// 获得工作时间
        /// </summary>
        /// <returns></returns>
        public float GetOrderTime()
        {
            float t = 0;
            foreach (var item in m_datas)
                t += item.workTime;

            return t;
        }

        /// <summary>
        /// 获得距离
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public float GetOrderDistance(Vector2Int start, Vector2Int end)
        {
            float distance = 0;
            foreach (var item in m_datas)
            {
                if (item.GetPosition(out int2 pos))
                {
                    var posv2 = new Vector2Int(pos.x, pos.y);
                    distance += Vector2Int.Distance(posv2, start);
                    start = posv2;
                }
            }
            
            distance += Vector2Int.Distance(start, end);
            return distance;
        }
    }
}