using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 成长礼包模块
    /// </summary>
    public class GrowGiftModule : Singleton<GrowGiftModule>
    {
        // 重新组织配置数据
        //private Dictionary<int, GrowGfitData> m_configDatas = new Dictionary<int, GrowGfitData>();
        SortedDictionary<int, GrowGfitData> m_configDatas = new SortedDictionary<int, GrowGfitData>();

        private static ILog log = LogManager.GetLogger("game.growgift");
        
        public void Initialize()
        {
            // 初始配置表
            InitConfigs();
            
            // 统计升星数据 
            EventManager.Instance.Reg<int, int>((int)GameEvent.WORK_TABLE_UP_STAR, (a, b) => RecordTargetNum(GrowTargetID.STAR_UP));
        }

        void InitConfigs()
        {
            var configs = ConfigSystem.Instance.LoadConfig<ProgressPack>();
            GrowGfitData value = null;//new GrowGfitData() {}
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var item = configs.Datalist(i);
                if (value == null || value.shopID != item.Value.ShopId)
                {
                    value = new GrowGfitData()
                    {
                        shopID = item.Value.ShopId,
                        activeID =  item.Value.ActiveID,
                        m_rewards = new List<GiftReward>(),
                    };
                    if (!m_configDatas.TryAdd(item.Value.ShopId, value))
                    {
                        log.Error("add shopID fail =" + item.Value.ShopId);
                        return;
                    }
                }
                
                value.m_rewards.Add(new GiftReward()
                {
                    configID = item.Value.Id,
                    conditionType = item.Value.Type,
                    conditionValue = item.Value.Value,
                    desc = item.Value.Des,
                    isFree = item.Value.Pay == 0,
                    reward = new ItemData.Value(){id = item.Value.Reward(1), num = item.Value.Reward(2), type = (PropertyGroup)item.Value.Reward(0)}
                });
            }
        }
        
        
        /// <summary>
        /// 统计目标
        /// </summary>
        /// <param name="id"></param>
        static void RecordTargetNum(GrowTargetID id)
        {
            PropertyManager.Instance.GetGroup(PropertyGroup.GROW_GIFT).AddNum((int)id, 1);
        }

        /// <summary>
        /// 根据礼包ID, 获取整个礼包的数据
        /// </summary>
        /// <param name="goodID">商品ID</param>
        /// <returns></returns>
        public GrowGfitData GetGiftData(int goodID)
        {
            return null;
        }
        
        /// <summary>
        /// 获取第N个在活动中的奖励
        /// </summary>
        /// <returns></returns>
        public int GetActiveGoodID(int index)
        {
            int activeIndex = 0;
            int currentTime = GameServerTime.Instance.serverTime;
            foreach (var item in m_configDatas)
            {
                if (ActiveTimeSystem.Instance.IsActive(item.Value.activeID, currentTime))
                {
                    if (index == activeIndex)
                        return item.Key;
                    else
                    {
                        activeIndex++;
                    }
                }
                
            }
            return 0;
        }
    }
}