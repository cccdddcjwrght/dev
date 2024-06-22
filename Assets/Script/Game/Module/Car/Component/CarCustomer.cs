using System.Collections;
using System.Collections.Generic;
using Fibers;
using log4net;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    //[InternalBufferCapacity(4)] // 载具只有4个人, 预分配4个位置
    public class CarCustomer
    {
        public enum SeatState
        {
            NOLEAVE     = 0, // 顾客初始未离开
            LEAVE       = 1, // 顾客已经离开了
            RETURNING   = 2, // 正在回归
            RETURN      = 3, // 顾客回来了
        }

        private static ILog log = LogManager.GetLogger("game.car");
        
        public int RoleID;      // 角色ID
        public int ItemID;      // 点单食物ID
        public int ItemNum;     // 点单数量
        public Entity customer; // 顾客实体
        public Entity hud;      // 点单UI
        public SeatState state; // 座位状态
        public int Index = 0;
        

        public bool IsEmptySeat => state == SeatState.LEAVE;

        public bool Leave()
        {
            if (state != SeatState.NOLEAVE)
            {
                log.Error("leave state not match");
                return false;
            }

            state = SeatState.LEAVE;
            return true;
        }

        public bool ReturnEnd(Entity customer)
        {
            if (state != SeatState.RETURNING)
            {
                return false;
            }

            if (this.customer != customer)
            {
                log.Error("customer not match");
                return false;
            }
            
            state = SeatState.RETURN;
            return true;
        }

        public bool ReturnBegin(Entity customer)
        {
            if (state != SeatState.LEAVE)
            {
                return false;
            }
            
            this.customer = customer;
            state = SeatState.RETURNING;
            return true;
        }
    }
}