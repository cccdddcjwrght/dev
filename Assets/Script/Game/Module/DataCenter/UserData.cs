using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 
    /// </summary>
    public struct UserData : IComponentData
    {
        // 金币数据
        public int    gold;

        // 角色ID
        public Entity player;
    }
}