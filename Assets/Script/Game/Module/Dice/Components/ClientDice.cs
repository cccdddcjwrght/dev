using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 客户端骰子对象
/// </summary>
namespace SGame
{
    // 用于控制与标记 GameObject 骰子
    public class ClientDice : MonoBehaviour
    {
        // 对象池编号(目前是直接销毁, 后续会优化)
        public int PoolIndex;
    }
}
