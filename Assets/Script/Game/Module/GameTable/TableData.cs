using System.Collections;
using System.Collections.Generic;
using log4net;
using Sirenix.OdinInspector.Editor.Drawers;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    // 座椅位置类型
    public enum TABLE_TYPE : uint
    {
        CUSTOM  = 1, // 顾客桌子
        DISH    = 3, // 放餐机
        MACHINE = 4, // 机器桌子
    }

    /// <summary>
    /// 座位数据
    /// </summary>
    public class TableData
    {
        private static ILog log = LogManager.GetLogger("game.table");

        // 桌子ID
        public int          id;

        // 座位位置
        public Vector2Int   map_pos;

        // 桌子类型
        public TABLE_TYPE   type;

        /// <summary>
        /// 顾客或取餐 座位ID1
        /// </summary>
        public List<ChairData>    chairs;

        // 桌子上的食物实例对象 或金币, 该对象根据桌子类型判断
        public List<int>          foodsID;

        // 关联的食物类型
        public int                foodType;

        // 机器ID
        public int                machineID;
        
        // 该位置是否位空
        public bool IsFoodEmpty =>  foodsID == null || foodsID.Count == 0;
        
        public void ClearChairs()
        {
            chairs.Clear();
        }

        /// <summary>
        /// 添加座椅接口
        /// </summary>
        /// <param name="chairType">座椅类型</param>
        /// <param name="chairPos">座椅位置</param>
        public bool AddChair(CHAIR_TYPE chairType, Vector2Int chairPos)
        {
            if (id <= 0)
            {
                log.Error("table id not valid!" + id);
                return false;
            }
            
            if (chairs == null)
            {
                chairs = new List<ChairData>(3);
            }
            var chair = new ChairData()
            {
                map_pos = chairPos,
                playerID = 0,
                tableID = id,
                type = chairType,
                chairIndex = chairs.Count,
            };
            chairs.Add(chair);
            return true;
        }

        /// <summary>
        /// 设置座椅位置
        /// </summary>
        /// <param name="chairIndex"></param>
        /// <param name="chairType"></param>
        /// <returns></returns>
        public bool SetChairType(int chairIndex, CHAIR_TYPE chairType)
        {
            if (chairIndex < 0 || chairIndex >= chairs.Count)
            {
                log.Error("chair Index Out of Range=" + chairIndex.ToString() + " chairCount=" + chairs.Count);
                return false;
            }

            var value = chairs[chairIndex];
            value.type = chairType;
            chairs[chairIndex] = value;
            return true;
        }
        
        public ChairData GetChair(int chairIndex)
        {
            return chairs[chairIndex];
        }

        /// <summary>
        /// 获得空余座位
        /// </summary>
        /// <returns></returns>
        public int GetEmptySit(CHAIR_TYPE chairType)
        {
            for (int i = 0; i < chairs.Count; i++)
            {
                if (chairs[i].type == chairType)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 根据座位类型, 找到第一个座位
        /// </summary>
        /// <param name="chairType"></param>
        /// <returns></returns>
        public int FindFirstChair(CHAIR_TYPE chairType)
        {
            for (int i = 0; i < chairs.Count; i++)
            {
                if (chairs[i].type == chairType)
                    return i;
            }

            return -1;
        }
        
        /// <summary>
        /// 占位
        /// </summary>
        /// <param name="customID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool SitChair(int customID, int index)
        {
            if (index < 0 || index >= chairs.Count)
            {
                log.Error("out of index =" + index + " table id=" + id);
                return false;
            }
            
            var chair = chairs[index];
            if (!chair.IsEmpty)
            {
                log.Error("chair has already sit =" + index + " table id=" + id);
                return false;
            }

            chair.playerID = customID;
            return true;
        }

        /// <summary>
        /// 离开用户位置
        /// </summary>
        /// <param name="customID"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool LeaveChair(int customID, int index)
        {
            if (index < 0 || index >= chairs.Count)
            {
                log.Error("out of index =" + index + " table id=" + id);
                return false;
            }
            
            var chair = chairs[index];
            if (!chair.IsEmpty)
            {
                log.Error("chair has already sit =" + index + " table id=" + id);
                return false;
            }

            if (chair.playerID == 0 || chair.playerID != customID)
            {
                log.Error("custom ID not match=" + customID + " sit =" + chair.playerID);
                return false;
            }
            chair.playerID = 0;
            chairs[index] = chair;
            return true;
        }

        /// <summary>
        /// 放入食物
        /// </summary>
        /// <param name="foodID"></param>
        /// <returns></returns>
        public bool PutFood(int foodID)
        {
            foodsID.Add(foodID);
            return true;
        }

        /// <summary>
        /// 拿走食物
        /// </summary>
        /// <returns></returns>
        public bool TakeFood(int foodID)
        {
            return foodsID.Remove(foodID);
        }
    }
}
