using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    public class TableUtils
    {
        /// <summary>
        /// 对放餐桌计算评分, 值小的优先
        /// </summary>
        /// <param name="data">桌子数据</param>
        /// <param name="character_pos"></param>
        /// <returns></returns>
        public static int GetPutDishTableScore(TableData data, int2
            character_pos)
        {
            const int MAX_LEN = 1000;
            int2 diff = data.map_pos - character_pos;
            int len = math.abs(diff.x) + math.abs(diff.y);
            len = math.min(MAX_LEN - 1, len);
            
            int score = data.foodsCount * MAX_LEN + len;
            return score;
        }

        /// <summary>
        /// 获得菜品工作时间
        /// </summary>
        /// <returns></returns>
        public static float GetFoodMakingTime(int roleID, int foodType)
        {
            int machineID = TableManager.Instance.FindMachineIDFromFoodType(foodType);
            
            // 食物制作时间
            double workTime = DataCenter.MachineUtil.GetWorkTime(machineID);
            
            // 玩家制作效率
            double workSpeed = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.WorkSpeed);
            
            // 最终时间
            double finalTime = workTime / workSpeed;
            return (float)finalTime;
        }
        
        private static ConfigValueFloat ORDER_BASE_TIME     = new ConfigValueFloat("order_time", 1);

        /// <summary>
        /// 获得点单时间
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static float GetOrderTime(int roleID)
        {
            double orderSpeed = AttributeSystem.Instance.GetValueByRoleID(roleID, EnumAttribute.OrderSpeed);
            return ORDER_BASE_TIME.Value / (float)orderSpeed;
        }

        public static float GetOrderWorkTime(OrderData order, int roleID)
        {
            if (order.progress == ORDER_PROGRESS.FOOD_START)
            {
                // 工作时间
                return TableUtils.GetFoodMakingTime(roleID, order.foodType);
            }
            else if (order.progress == ORDER_PROGRESS.WAIT_ORDER)
            {
                // 等待点单
                return TableUtils.GetOrderTime(roleID);
            }

            return 0;
        }
    }
}