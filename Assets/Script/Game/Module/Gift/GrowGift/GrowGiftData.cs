using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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
            UNFINISH    = 2, // 已解锁, 未达成
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
        /// 获得当前值
        /// </summary>
        /// <returns></returns>
        public int GetConditionProgress()
        {
            // 获得动态数据
            var data = GetDynamicData(); //DataCenter.Instance.m_growData.GetItem(goodsID);
            var itemGroup = data.GetOrCrateGroup();
            
            var value = itemGroup.GetNum(conditionType);
            return (int)value;
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
            var data = GetDynamicData();
            return data.isBuy;
        }

        /// <summary>
        /// 是否已经显示过红点
        /// </summary>
        /// <returns></returns>
        public bool HasRedot()
        {
            var s = GetState();
            return s == State.CANTAKE || s == State.UNLOCK2;
        }

        /// <summary>
        /// 判断按钮状态
        /// </summary>
        /// <param name="isBuyed"></param>
        /// <returns></returns>
        public State GetState()
        {
            var isBuyed = IsBuyed();
            bool isFinish = IsConditionFinish();
            if (isBuyed == false)
            {
                if (!isFinish)
                    return State.UNLOCK1;

                return State.UNLOCK2;
            }

            if (!isFinish)
                return State.UNFINISH;
            
            var data = DataCenter.Instance.m_growData.GetItem(goodsID);
            bool isTaked = data.IsTaked(configID);
            return isTaked ? State.FINISH : State.CANTAKE;
        }

        public bool IsConditionFinish()
        {
            return GetConditionProgress() >= conditionValue;
        }
        
        /// <summary>
        /// 获取动态数据
        /// </summary>
        /// <returns></returns>
        public GrowGiftItem GetDynamicData()
        {
            var data = DataCenter.Instance.m_growData.GetItem(goodsID);
            return data;
        }
    }

    // 成长礼包数据
    public class GrowGiftData
    {
        public int shopID;                 // 商品ID
        public float price;                // 价格
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

        /// <summary>
        /// 获取动态数据
        /// </summary>
        /// <returns></returns>
        public GrowGiftItem GetDynamicData()
        {
            var data = DataCenter.Instance.m_growData.GetItem(shopID);
            return data;
        }

        /// <summary>
        /// 判断是否有红点
        /// </summary>
        /// <returns></returns>
        public bool HasReddot()
        {
            foreach (var reward in m_rewards)
            {
                if (reward.HasRedot())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 获取可领取数量
        /// </summary>
        /// <returns></returns>
        public int GetReddotNum()
        {
            int num = 0;
            foreach (var reward in m_rewards)
            {
                if (reward.HasRedot())
                    return num++;
            }

            return num;
        }
        
    }
}