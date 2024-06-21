using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;

namespace SGame
{
    public class CustomerBookData
    {
        private bool     m_reward = false;
        public int       ID => Config.Id;                         // 角色ID

        public string    Icon => Config.Icon;                     // 图标

        public int       FoodID(int i) => Config.LikeGoods(i);    // 喜欢食物ID

        public int       FoodLength => Config.LikeGoodsLength;    // 喜欢食物数量

        public CustomerBookData(RoleDataRowData cfg, bool isReward)
        {
            this.Config = cfg;
            this.m_reward = isReward;
        }
        
        // 解锁区域[x=关卡,y=解锁区域]
        public Vector2Int UnlockArea
        {
            get
            {
                if (Config.UnlockAreaLength == 2)
                {
                    return new Vector2Int(Config.UnlockArea(0), Config.UnlockReward(1));
                }
                
                throw new Exception("UnlockAreaLength Not 2");
            }
        } 

        /// <summary>
        /// 解锁奖励
        /// </summary>
        /// <exception cref="Exception"></exception>
        public Vector3Int UnlockReward
        {
            get
            {
                if (Config.UnlockRewardLength == 3)
                {
                    return new Vector3Int(Config.UnlockArea(0), Config.UnlockReward(1), Config.UnlockReward(2));
                }

                throw new Exception("UnlockRewardLength Not 3");
            }
        }

        /// <summary>
        /// 判断图鉴是否打开
        /// </summary>
        public bool IsUnlock
        {
            get
            {
                // 不在当前关卡判断当前关卡
                var area = UnlockArea;
                int currentLevelID = DataCenter.Instance.roomData.roomID;
                if (area.x < currentLevelID)
                {
                    return true;
                }
                else if (area.x > currentLevelID)
                {
                    return false;
                }

                // 同关卡判断解锁区域
                return DataCenter.MachineUtil.IsAreaEnable(area.y);
            }
        }
        
            
        // 静态数据
        public RoleDataRowData Config;
        
        // 奖励是否已经领取
        public bool reward { get => m_reward;
            set
            {
                if (value != m_reward && value == true)
                {
                    m_reward = value;
                    CustomerBookModule.Instance.AddReward(ID);
                }
            }
        }
    }
}