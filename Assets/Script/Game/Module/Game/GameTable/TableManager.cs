using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 桌子管理器, 桌子只能添加不能删除
    /// </summary>
    public class TableManager : Singleton<TableManager>
    {
        // 座位信息
        private List<TableData>             m_datas     = new List<TableData>();

        // 下一个tableID
        private int m_nextTableID = 0;

        public void Clear()
        {
            m_datas.Clear();
        }

        public void Initalize()
        {
            Clear();
        }
        
        /// <summary>
        /// 获得桌子数据
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        public TableData Get(int tableID)
        {
            return m_datas[tableID - 1];
        }

        /// <summary>
        /// 添加新的桌子
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddTable(TableData data)
        {
            data.id = m_nextTableID + 1;
            m_datas.Add(data);
            m_nextTableID++;
            return true;
        }

        /// <summary>
        /// 获得放餐区空闲位置
        /// </summary>
        /// <returns></returns>
        public int FindPutDishTable()
        {
            int dishCount = 100;
            int index = -1;
            for (int i = 0; i < m_datas.Count; i++)
            {
                var t = m_datas[i];
                if (t.type == TABLE_TYPE.DISH)
                {
                    if (t.foodsID.Count == 0)
                        return i;
                    
                    if (t.foodsID.Count < dishCount)
                    {
                        index = i;
                        dishCount = t.foodsID.Count;
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// 顾客查找空闲座位
        /// </summary>
        /// <returns></returns>
        public ChairData FindEmptyCustomerChair()
        {
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.CUSTOM)
                {
                    int chairIndex = t.GetEmptySit(CHAIR_TYPE.CUSTOMER);
                    if (chairIndex >= 0)
                    {
                        return t.GetChair(chairIndex);
                    }
                }
            }
            
            return ChairData.Empty;
        }
        
        /// <summary>
        /// 顾客查找空闲座位
        /// </summary>
        /// <returns></returns>
        public ChairData FindEmptyChair(TABLE_TYPE tableType, CHAIR_TYPE chairType)
        {
            foreach (var t in m_datas)
            {
                if (t.type == tableType)
                {
                    int chairIndex = t.GetEmptySit(chairType);
                    if (chairIndex >= 0)
                    {
                        return t.GetChair(chairIndex);
                    }
                }
            }
            
            return ChairData.Empty;
        }

        /// <summary>
        /// 查找顾客所在位置
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <returns></returns>
        public ChairData FindCustomerPos(int customerID)
        {
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.CUSTOM)
                {
                    foreach (var chair in t.chairs)
                    {
                        if (chair.type == CHAIR_TYPE.CUSTOMER && chair.playerID == customerID)
                        {
                            return chair;
                        }
                    }
                }
            }
            
            return ChairData.Empty;
        }

        /// <summary>
        /// 通过顾客找到点单的座位
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <returns></returns>
        public ChairData FindCustomerOrderPos(int customerID)
        {
            // 先找到顾客座位
            var chairPos = FindCustomerPos(customerID);
            if (chairPos == ChairData.Empty)
                return ChairData.Empty;

            var t = Get(chairPos.tableID);
            int chairIndex = t.FindFirstChair(CHAIR_TYPE.ORDER);
            if (chairIndex >= 0)
            {
                return t.GetChair(chairIndex);
            }
            
            return ChairData.Empty;
        }

        /// <summary>
        /// 坐下空闲座位
        /// </summary>
        /// <param name="chair"></param>
        /// <returns></returns>
        public bool SitChair(ChairData chair, int customID)
        {
            TableData table = Get(chair.tableID);
            if (table == null)
                return false;

            return table.SitChair(customID, chair.chairIndex);
        }

        /// <summary>
        /// 离开空闲座位
        /// </summary>
        /// <param name="chir"></param>
        /// <returns></returns>
        public bool LeaveChair(ChairData chair, int customID)
        {
            TableData table = Get(chair.tableID);
            if (table == null)
                return false;

            return table.LeaveChair(customID, chair.chairIndex);
        }
    }
}
