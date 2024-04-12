using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 成长礼包配置数据重新组织
namespace SGame
{
    // 解锁条件
    public struct GiftReward
    {
        public int            configID;          // 配置表ID
        public int            conditionType;     // 条件类型
        public int            conditionValue;    // 条件值
        public string         desc;              // 描述信息
        public bool           isFree;            // 是否免费
        public ItemData.Value reward;            // 奖励数据
    }

    // 成长礼包数据
    public class GrowGfitData
    {
        public int shopID;                 // 商品ID
        public int activeID;               // 活动ID
        public List<GiftReward> m_rewards; // 奖励数据
    }
}