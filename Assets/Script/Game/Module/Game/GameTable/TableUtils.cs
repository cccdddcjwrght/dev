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
    }
}