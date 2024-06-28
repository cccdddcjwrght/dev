using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;

namespace SGame
{
    public class CustomerBookData
    {
        public int       ID => Config.Id;                         // 角色ID

        public string    Icon => "ui://Cookbook/" + Config.Icon;                     // 图标

        public int       FoodID(int i) => Config.LikeGoods(i);    // 喜欢食物ID

        public int       FoodLength => Config.LikeGoodsLength;    // 喜欢食物数量

        public string   Name => LanagueSystem.Instance.GetValue(Config.Name);

        private CustomerBookReward.Record m_dynamicData = null; // 动态数据
        

        public CustomerBookData(RoleDataRowData cfg, CustomerBookReward.Record record)
        {
            this.Config = cfg;
            m_dynamicData = record;
        }

        /// <summary>
        /// 判断图鉴是否打开
        /// </summary>
        public bool IsUnlock => m_dynamicData.isUnlock;
        
        // 静态数据
        public RoleDataRowData Config;
        
        // 奖励是否已经领取
        public bool isRewarded => m_dynamicData.isRewared;

        public bool isOpened => m_dynamicData.isOpened;

        public void SetRewared()
        {
            m_dynamicData.isRewared = true;
        }

        public void SetOpened()
        {
            m_dynamicData.isOpened = true;
        }
    }
}