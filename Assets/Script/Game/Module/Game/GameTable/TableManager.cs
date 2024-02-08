using System.Collections;
using System.Collections.Generic;
using log4net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Sirenix.Utilities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 桌子管理器, 桌子只能添加不能删除
    /// </summary>
    public class TableManager : Singleton<TableManager>
    {
        private static ILog log = LogManager.GetLogger("game.table");
        
        // 座位信息
        private List<TableData>             m_datas     = new List<TableData>();
        private List<int>                   m_foodTypes = new List<int>();

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

        private List<TableData> m_sortCache = new List<TableData>();
        
        /// <summary>
        /// 获得放餐区空闲位置
        /// </summary>
        /// <returns></returns>
        public int FindPutDishTable()
        {
            m_sortCache.Clear();
            foreach (var item in m_datas)
            {
                if (item.type == TABLE_TYPE.DISH)
                    m_sortCache.Add(item);
            }
            if (m_sortCache.Count <= 0)
                return -1;
            
            m_sortCache.Sort((a, b) =>
            {
                if (a.foodsCount == b.foodsCount)
                    return 0;
                return a.foodsCount < b.foodsCount ? -1 : 1;
            });
            
            return m_sortCache[0].id;
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
            
            return ChairData.Null;
        }

        public List<ChairData> GetEmptyChairs(TABLE_TYPE tableType, CHAIR_TYPE chair)
        {
            List<ChairData> ret = new List<ChairData>();
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.CUSTOM)
                {
                    int chairIndex = t.GetEmptySit(CHAIR_TYPE.CUSTOMER);
                    if (chairIndex >= 0)
                    {
                        ret.Add(t.GetChair(chairIndex));
                    }
                }
            }

            return ret;
        }
        
        /// <summary>
        /// 获得空座位数量
        /// </summary>
        /// <param name="tableType"></param>
        /// <param name="chair"></param>
        /// <returns></returns>
        public int GetEmptyChairCount(TABLE_TYPE tableType, CHAIR_TYPE chair)
        {
            int ret = 0;
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.CUSTOM)
                {
                    int chairIndex = t.GetEmptySit(CHAIR_TYPE.CUSTOMER);
                    if (chairIndex >= 0)
                    {
                        ret++;
                    }
                }
            }

            return ret;
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
            
            return ChairData.Null;
        }

        /// <summary>
        /// 查找顾客所在位置
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <returns></returns>
        public ChairData FindCustomerChair(int customerID)
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
            
            return ChairData.Null;
        }

        /// <summary>
        /// 查找可用的工作台
        /// </summary>
        /// <param name="foodType"></param>
        /// <param name="table"></param>
        /// <param name="chair"></param>
        /// <returns></returns>
        public bool FindEmptyMatchine(int foodType, out TableData table, out ChairData chair)
        {
            table = null;
            chair = ChairData.Null;
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.MACHINE && t.foodType == foodType)
                {
                    int chairIndex = t.GetEmptySit(CHAIR_TYPE.OPERATOR);
                    if (chairIndex < 0)
                        continue;

                    table = t;
                    chair = t.GetChair(chairIndex);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 通过顾客找到点单的座位
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <returns></returns>
        public ChairData FindCustomerOrderPos(int customerID)
        {
            // 先找到顾客座位
            var chairPos = FindCustomerChair(customerID);
            if (chairPos == ChairData.Null)
                return ChairData.Null;

            var t = Get(chairPos.tableID);
            int chairIndex = t.FindFirstChair(CHAIR_TYPE.ORDER);
            if (chairIndex >= 0)
            {
                return t.GetChair(chairIndex);
            }
            
            return ChairData.Null;
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

        /// <summary>
        /// 通过食物类型找到加工台ID
        /// </summary>
        /// <param name="foodType"></param>
        /// <returns></returns>
        public int FindMachineIDFromFoodType(int foodType)
        {
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.MACHINE)
                {
                    if (t.foodType == foodType)
                        return t.machineID;
                }
            }
            
            return -1;
        }
        
        /// <summary>
        /// 通过食物类型找到可用工作台位置
        /// </summary>
        /// <param name="foodType"></param>
        /// <returns></returns>
        public ChairData FindMachineChairFromFoodType(int foodType)
        {
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.MACHINE)
                {
                    if (t.foodType == foodType)
                    {
                        var sitIndex = t.GetEmptySit(CHAIR_TYPE.OPERATOR);
                        if (sitIndex >= 0)
                        {
                            return t.GetChair(sitIndex);
                        }
                    }
                }
            }
            
            return ChairData.Null;
        }

        /// <summary>
        /// 通过工作台ID 找到对应食物类型
        /// </summary>
        /// <param name="foodType"></param>
        /// <returns>-1表示找不到</returns>
        public int FindFoodTypeFromMachineID(int machineID)
        {
            foreach (var t in m_datas)
            {
                if (t.type == TABLE_TYPE.MACHINE)
                {
                    if (t.machineID == machineID)
                        return t.machineID;
                }
            }

            return -1;
        }

        /// <summary>
        /// 更新统计信息
        /// </summary>
        /// <param name="t"></param>
        public void UpdateTableInfo(TableData t)
        {
            if (t.type == TABLE_TYPE.MACHINE)
            {
                if (!m_foodTypes.Contains(t.foodType))
                    m_foodTypes.Add(t.foodType);
            }
        }

        /// <summary>
        /// 获得已经打开的食物类型
        /// </summary>
        /// <returns></returns>
        public List<int> GetOpenFoodTypes()
        {
            return m_foodTypes;
        }
    }
}
