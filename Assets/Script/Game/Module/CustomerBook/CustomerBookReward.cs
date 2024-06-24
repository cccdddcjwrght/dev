using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 图鉴存储数据
    /// </summary>
    [Serializable]
    public class CustomerBookReward
    {
        // 已领取奖励ID
        public List<int> Rewarded = new List<int>();

        // 已经打开过UI
        public List<int> Opened = new List<int>();
    }
}