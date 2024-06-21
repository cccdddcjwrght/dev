using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 图鉴存储数据
    /// </summary>
    [SerializeField]
    public class CustomerBookReward
    {
        // 已领取奖励ID
        public List<int> Values = new List<int>();
    }
}