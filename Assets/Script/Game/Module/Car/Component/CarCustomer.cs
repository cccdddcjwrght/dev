using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    //[InternalBufferCapacity(4)] // 载具只有4个人, 预分配4个位置
    public class CarCustomer
    {
        public enum SeatState
        {
            NOLEAVE = 0, // 顾客初始未离开
            LEAVE   = 1, // 顾客已经离开了
            RETURN  = 2, // 顾客回来了
        }
        
        public int RoleID;      // 角色ID
        public int ItemID;      // 点单食物ID
        public int ItemNum;     // 点单数量
        public Entity customer; // 顾客实体
        public Entity hud;      // 点单UI
        public SeatState state; // 座位状态
        

        public bool IsEmptySeat => state == SeatState.LEAVE;

        public bool Return(Entity customer)
        {
            if (state != SeatState.LEAVE)
            {
                return false;
            }
            
            this.customer = customer;
            return true;
        }
    }
}