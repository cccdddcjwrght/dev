using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [InternalBufferCapacity(4)] // 载具只有4个人, 预分配4个位置
    public struct CarCustomer : IBufferElementData
    {
        public int RoleID; // 角色ID
    }
}