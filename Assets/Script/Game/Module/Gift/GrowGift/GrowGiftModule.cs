using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 成长礼包模块
    /// </summary>
    public class GrowGiftModule : Singleton<GrowGiftModule>
    {
        private static int _OPEND_ID = -1;
        public const int ACTIVE_TYPE = 1; // 活动类型

        public GrowGiftRecord GetData()
        {
            return DataCenter.Instance.m_growData;
        }
        
        // 重新组织配置数据
        public static int OPEND_ID
        {
            get
            {
                if (_OPEND_ID < 0)
                {
                    _OPEND_ID = GlobalDesginConfig.GetInt("growgift_func", 0);
                    if (_OPEND_ID == 0)
                    {
                        log.Error("growgift_func open id not found");
                    }
                }

                return _OPEND_ID;
            }
        }
        
        SortedDictionary<int, GrowGfitData> m_configDatas = new SortedDictionary<int, GrowGfitData>();

        private static ILog log = LogManager.GetLogger("game.growgift");
        
        public void Initialize()
        {
            // 初始配置表
            InitConfigs();

            // 初始化存储数据
            InitRecordData();
            
            // 统计升星数据 
            EventManager.Instance.Reg<int, int>((int)GameEvent.WORK_TABLE_UP_STAR, (a, b) => RecordTargetNum(GrowTargetID.STAR_UP));
            
            //EventManager.Instance.Reg<int, int>
        }

        public bool IsOpend()
        {
            return OPEND_ID.IsOpend(false);
        }

        /// <summary>
        /// 判断第N个成长礼包是否开启
        /// </summary>
        /// <param name="opendIndex"></param>
        /// <returns></returns>
        public bool IsOpend(int opendIndex)
        {
            if (!IsOpend())
                return false;

            return GetActiveGoodID(opendIndex) > 0;
        }

        /// <summary>
        /// 通过配置表初始化数据
        /// </summary>
        void InitConfigs()
        {
            // 初始化配置表
            var configs = ConfigSystem.Instance.LoadConfig<ProgressPack>();
            GrowGfitData value = null;
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
                    goodsID         = item.Value.ShopId,
                    configID        = item.Value.Id,
                    conditionType   = item.Value.Type,
                    conditionValue  = item.Value.Value,
                    desc            = item.Value.Des,
                    isFree          = item.Value.Pay == 0,
                    reward          = new ItemData.Value(){id = item.Value.Reward(1), num = item.Value.Reward(2), type = (PropertyGroup)item.Value.Reward(0)}
                });
            }
            
            // updatefree
            foreach (var item in m_configDatas)
            {
                item.Value.isAllFree = IsAllFree(item.Value.m_rewards);
            }
        }

        void InitRecordData()
        {
            // updatefree
            foreach (var item in m_configDatas)
            {
                item.Value.isAllFree = IsAllFree(item.Value.m_rewards);
            }

            // 删除不在活动中的对象
            int currentTime = GameServerTime.Instance.serverTime;
            var datas = GetData();
            for (int i = datas.Values.Count - 1; i >= 0; i--)
            {
                var data = datas.Values[i];
                if (!ActiveTimeSystem.Instance.IsActive(data.activlityID, currentTime))
                {
                    datas.Values.RemoveAt(i);
                }
            }
            
            // 找到未开启的活动并打开
            foreach (var item in m_configDatas)
            {
                if (ActiveTimeSystem.Instance.IsActive(item.Value.activeID, currentTime))
                {
                    if (GetGiftRecord(item.Value.shopID) == null)
                    {
                        var newItem = GrowGiftItem.Create(item.Value.shopID, item.Value.activeID);
                        datas.Values.Add(newItem);
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否是全免费的
        /// </summary>
        /// <param name="rewards"></param>
        /// <returns></returns>
        bool IsAllFree(List<GiftReward> rewards)
        {
            foreach (var item in rewards)
                if (!item.isFree)
                    return false;

            return true;
        }
        
        /// <summary>
        /// 统计目标
        /// </summary>
        /// <param name="id"></param>
        void RecordTargetNum(GrowTargetID id)
        {
            int type = (int)id;
            var datas = GetData();
            foreach (var item in datas.Values)
            {
                var group = item.GetOrCrateGroup();
                group.AddNum((int)id, 1);
            }
        }

        /// <summary>
        /// 找到东岱数据
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public GrowGiftItem GetGiftRecord(int goodsID)
        {
            return GetData().GetItem(goodsID);
        }

        /// <summary>
        /// 根据礼包ID, 获取整个礼包的数据
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <returns></returns>
        public GrowGfitData GetGiftData(int goodsID)
        {
            if (m_configDatas.TryGetValue(goodsID, out GrowGfitData config))
            {
                return config;
            }
            
            log.Info("goodsID not found=" + goodsID);
            return null;
        }
        
        /// <summary>
        /// 获取第N个在活动中的奖励
        /// </summary>
        /// <returns></returns>
        public int GetActiveGoodID(int index)
        {
            var data = GetData();
            if (index >= data.Values.Count)
            {
                return 0;
            }

            return data.Values[index].goodsID;
        }

        /// <summary>
        /// 判断是否已经获得奖励
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public bool IsGetReward(bool configID)
        {
            return false;
        }

        /// <summary>
        /// 判断商品是否购买
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public bool IsBuy(int goodsID)
        {
            if (m_configDatas.TryGetValue(goodsID, out GrowGfitData config))
            {
                log.Error("config id not found=" + goodsID);
                return false;
            }

            if (config.isAllFree)
                return true;
            
            return DataCenter.ShopUtil.IsHasBuy(goodsID);
        }

        public bool OnOpend()
        {
            return false;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData()
        {
            
        }
        // public bool CollectReward(int goodsID, int 
    }
}