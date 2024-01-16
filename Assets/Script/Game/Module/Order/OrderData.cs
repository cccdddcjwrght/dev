using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

namespace SGame
{
    /// <summary>
    /// 菜单进度
    /// </summary>
    public enum ORDER_PROGRESS : uint
    {
         ORDING         = 0, // 下单中
         FOOD_MAKING    = 1, // 制作中
         FOOD_MAKED     = 2, // 制作完成
    }
    
    // 订单数据 
    public class OrderData
    {
        public int      id;                 // 订单ID
        public int      startTime;          // 下单时间
        public int      cookTime;           // 制作时间
        public int      finishTime;         // 订单完成时间
        public int      customerID;         // 顾客
        public int      orderMakerID;       // 下单服务员
        public int      servicerID;         // 消单服务员
        public int      cookerID;           // 厨师ID
        
        public bool     perfect;            // 是否完美菜品
        public int      dishPointID;        // 放餐点

        public int      foodID;  // 菜品ID
        
        public BigInteger  price;    // 菜品价格
        
        public ORDER_PROGRESS progress; // 订单进度
    }
}