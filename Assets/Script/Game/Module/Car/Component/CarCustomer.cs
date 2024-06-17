using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    //[InternalBufferCapacity(4)] // 载具只有4个人, 预分配4个位置
    public class CarCustomer
    {
        public int RoleID;     // 角色ID
        public int ItemID;     // 点单食物ID
        public int ItemNum;    // 点单数量
        public Entity hud;     // 点单UI
    }
}