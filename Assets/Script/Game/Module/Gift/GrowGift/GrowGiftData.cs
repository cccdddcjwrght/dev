using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 成长礼包配置数据重新组织
namespace SGame
{
    // 解锁条件
    public struct GiftReward
    {
        /// <summary>
        /// 奖励状态
        /// </summary>
        public enum State : int
        {
            UNLOCK1     = 0, // 未解锁, 未达成 
            UNLOCK2     = 1, // 未解锁, 达成
            UNFINISHED  = 2, // 已解锁, 未达成
            CANTAKE     = 3, // 已解锁, 已达成(但未领取)
            FINISH      = 4, // 已领取
        }
        
        public int            goodsID;           // 商品ID
        public int            configID;          // 配置表ID
        public int            conditionType;     // 条件类型
        public int            conditionValue;    // 条件值
        public string         desc;              // 描述信息
        public bool           isFree;            // 是否免费
        public ItemData.Value reward;            // 奖励数据

        /// <summary>
        /// 获取条件达成进度[0-100]
        /// </summary>
        /// <returns></returns>
        public int GetConditionProgress()
        {
            // 获得动态数据
            var data = DataCenter.Instance.m_growData.GetItem(goodsID);
            var itemGroup = data.GetOrCrateGroup();
            
            var value = itemGroup.GetNum(conditionType);
            var baseValue = (double)conditionValue;
            if (baseValue <= value)
                return 100;

            int ret = (int)(value / baseValue);
            return ret;
        }

        /// <summary>
        /// 判断是否已经购买
        /// </summary>
        /// <returns></returns>
        public bool IsBuyed()
        {
            if (isFree)
                return true;
            
            // 判断
            var data = DataCenter.Instance.m_growData.GetItem(goodsID);
            return data.isBuy;
        }

        /// <summary>
        /// 判断按钮状态
        /// </summary>
        /// <param name="isBuyed"></param>
        /// <returns></returns>
        public State GetState(bool isBuyed)
        {
            int progress = GetConditionProgress();
            if (isBuyed == false)
            {
                if (progress < 100)
                    return State.UNLOCK1;

                return State.UNLOCK2;
            }

            if (progress < 100)
                return State.UNFINISHED;
            
            var data = DataCenter.Instance.m_growData.GetItem(goodsID);
            bool isTaked = data.IsTaked(configID);
            return isTaked ? State.FINISH : State.CANTAKE;
        }
        
    }

    // 成长礼包数据
    public class GrowGiftData
    {
        public int shopID;                 // 商品ID
        public int activeID;               // 活动ID
        public List<GiftReward> m_rewards; // 奖励数据

        public bool IsAllBuyed()
        {
            foreach (var reward in m_rewards)
            {
                if (!reward.IsBuyed())
                    return false;
            }

            return true;
        }
    }
}