using System.Collections.Generic;
using GameConfigs;
using log4net;

namespace SGame
{
    /// <summary>
    /// 成长礼包模块
    /// </summary>
    public class GrowGiftModule : Singleton<GrowGiftModule>
    {
        private static int _OPEND_ID = -1;
        public const string DAY_TIME_KEY = "growgift.daytime"; // 记录同一天数据

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
        
        SortedDictionary<int, GrowGiftData> m_configDatas = new SortedDictionary<int, GrowGiftData>();
        private Dictionary<int, GiftReward> m_configRewards = new Dictionary<int, GiftReward>();

        private static ILog log = LogManager.GetLogger("game.growgift");
        
        public void Initialize()
        {
            // 初始配置表
            InitConfigs();

            // 统计升星数据 
            EventManager.Instance.Reg<int, int>((int)GameEvent.WORK_TABLE_UP_STAR, (a, b) => RecordTargetNum(GrowTargetID.STAR_UP));
            EventManager.Instance.Reg<int>((int)GameEvent.ACTIVITY_OPEN, OnActivityOpend);
            EventManager.Instance.Reg<int>((int)GameEvent.ACTIVITY_CLOSE, OnActivityClosed);
            EventManager.Instance.Reg((int)GameEvent.DATA_INIT_COMPLETE, OnDataInitalizeFinish);
            EventManager.Instance.Reg<int>((int)GameEvent.AFTER_ENTER_ROOM, OnFirstEnterRoom);
        }

        void OnFirstEnterRoom(int levelID)
        {
            if (!IsOpend(0))
                return;

            if (Utils.IsFirstLoginInDay(DAY_TIME_KEY))
            {
                // 自动打开成长礼包
                DelayExcuter.Instance.DelayOpen("growgift", "mainui", false, null, 0);
            }
        }


        void OnDataInitalizeFinish()
        {
            // 初始化数据
            RefreshRecordData();
        }
        
        /// <summary>
        /// 获得动态数据
        /// </summary>
        /// <returns></returns>
        public GrowGiftRecord GetData()
        {
            return DataCenter.Instance.m_growData;
        }

        /// <summary>
        /// 活动开启
        /// </summary>
        /// <param name="activityID"></param>
        void OnActivityOpend(int activityID)
        {
            // 不是我们的模块
            var config = FindGiftConfig(activityID);
            if (config == null)
                return;

            if (RefreshRecordData())
                EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        /// <summary>
        /// 活动结束
        /// </summary>
        /// <param name="activityID"></param>
        void OnActivityClosed(int activityID)
        {
            // 不是我们的模块
            var config = FindGiftConfig(activityID);
            if (config == null)
                return;

            if (RefreshRecordData())
                EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_MAIN_REFRESH);
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
            GrowGiftData value = null;
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var item = configs.Datalist(i);
                if (value == null || value.shopID != item.Value.ShopId)
                {
                    if (!ConfigSystem.Instance.TryGet(item.Value.ShopId, out GameConfigs.ShopRowData shopConfig))
                    {
                        log.Error("shop id not found=" + item.Value.ShopId);
                        return;
                    }
                    
                    value = new GrowGiftData()
                    {
                        shopID      = item.Value.ShopId,
                        activeID    = item.Value.ActiveID,
                        price       = shopConfig.Price,
                        m_rewards   = new List<GiftReward>(),
                    };
                    if (!m_configDatas.TryAdd(item.Value.ShopId, value))
                    {
                        log.Error("add shopID fail =" + item.Value.ShopId);
                        return;
                    }
                }

                var giftReward = new GiftReward()
                {
                    goodsID = item.Value.ShopId,
                    configID = item.Value.Id,
                    conditionType = item.Value.Type,
                    conditionValue = item.Value.Value,
                    desc = item.Value.Des,
                    isFree = item.Value.Pay == 0,
                    reward = new ItemData.Value() { id = item.Value.Reward(1), num = item.Value.Reward(2), type = (PropertyGroup)item.Value.Reward(0) }
                };
                
                value.m_rewards.Add(giftReward);
                m_configRewards.Add(giftReward.configID, giftReward);
            }
        }

        /// <summary>
        /// 关闭活动
        /// </summary>
        /// <param name="data"></param>
        void OnCloseGrowGift(GrowGiftItem data)
        {
            log.Info("Close GrowGift=" + data.goodsID);
        }

        /// <summary>
        /// 打开活动
        /// </summary>
        /// <param name="data"></param>
        void OnOpenGrowGift(GrowGiftItem data)
        {
            log.Info("Open GrowGift=" + data.goodsID);
        }


        /// <summary>
        /// 获取活动剩余时间
        /// </summary>
        /// <param name="index">第几个活动 </param>
        /// <returns></returns>
        public int GetActiveTime(int index)
        {
            var data = GetData();
            if (index < 0 || index >= data.Values.Count)
            {
                return 0;
            }

            var giftData = data.Values[index];
            int currentTime = GameServerTime.Instance.serverTime;
            return ActiveTimeSystem.Instance.GetLeftTime(giftData.activlityID, currentTime);
        }

        /// <summary>
        /// 刷新活动数据
        /// </summary>
        bool RefreshRecordData()
        {
            // 未开启时不处理
            /*
            if (!OPEND_ID.IsOpend(false))
            {
                return false;
            }
            */
            
            // 删除不在活动中的对象
            int currentTime = GameServerTime.Instance.serverTime;
            var datas = GetData();
            List<GrowGiftItem> removeGifts = new List<GrowGiftItem>();
            List<GrowGiftItem> addGifts     = new List<GrowGiftItem>();

            
            for (int i = datas.Values.Count - 1; i >= 0; i--)
            {
                var data = datas.Values[i];
                if (!ActiveTimeSystem.Instance.IsActive(data.activlityID, currentTime))
                {
                    removeGifts.Add(data);
                    datas.Values.RemoveAt(i);
                }
            }
            
            // 找到未开启的活动并打开
            foreach (var item in m_configDatas)
            {
                if (ActiveTimeSystem.Instance.IsActive(item.Value.activeID, currentTime))
                {
                    if (item.Value.GetDynamicData() == null)
                    {
                        var newItem = datas.AddNewItem(item.Value.shopID, item.Value.activeID);
                        addGifts.Add(newItem);
                    }
                }
            }

            foreach (var item in removeGifts)
                OnCloseGrowGift(item);
            
            foreach (var item in addGifts)
                OnOpenGrowGift(item);

            return removeGifts.Count > 0 || addGifts.Count > 0;
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
        /// 根据礼包ID, 获取整个礼包的数据
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <returns></returns>
        public GrowGiftData GetGiftData(int goodsID)
        {
            if (m_configDatas.TryGetValue(goodsID, out GrowGiftData config))
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
        /// 通过活动ID找到对应的配置
        /// </summary>
        /// <param name="activeID"></param>
        /// <returns></returns>
        public GrowGiftData FindGiftConfig(int activeID)
        {
            foreach (var value in m_configDatas.Values)
            {
                if (value.activeID == activeID)
                    return value;
            }

            return null;
        }
        
        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public bool BuyGoods(int goodsID)
        {
            //GROW_GIFT_UPDATE
            var config = GetGiftData(goodsID);
            if (config == null)
            {
                log.Error("goodsID not found=" + goodsID);
                return false;
            }

            ///var data = config.GetDynamicData();
            if (config.IsAllBuyed())
            {
                log.Error("goodsID already buy=" + goodsID);
                return false;
            }
            
            RequestExcuteSystem.BuyGoods(config.shopID, (success) =>
            {
                if (success)
                {
                    // 购买成功 
                    config.GetDynamicData().isBuy = true;                                // 标记已购买
                    EventManager.Instance.AsyncTrigger((int)GameEvent.GROW_GIFT_REFRESH); // 刷新UI
                }
            });
            return true;
        }

        /// <summary>
        /// 获取奖励
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public bool TakedReward(int configID)
        {
            if (!m_configRewards.TryGetValue(configID, out GiftReward reward))
            {
                log.Error("config ID not found=" + configID);
                return false;
            }

            var state = reward.GetState();
            if (state != GiftReward.State.CANTAKE)
            {
                log.Warn("state not match=" + state.ToString());
                return false;
            }
            
            // 领取奖励
            var itemGroup = PropertyManager.Instance.GetGroup(reward.reward.type);
            //if (!itemGroup.AddNum(reward.reward.id, reward.reward.num))
            itemGroup.AddNum(reward.reward.id, reward.reward.num);
            //{
                //log.Error("Add Item Fail=" + reward.reward.id + " num=" + reward.reward.num);
                //return false;
            //}
            
            // 成功领取奖励
            var data = reward.GetDynamicData();
            data.takedID.Add(configID);
            //EventManager.Instance.AsyncTrigger((int)GameEvent.GROW_GIFT_REFRESH); // 刷新UI
            return true;
        }

        /// <summary>
        /// 判断是否存在红点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RedDot(int index)
        {
            var data = GetData();
            if (index >= data.Values.Count)
            {
                return false;
            }

            var rewardItem = data.Values[index];
            var giftData = GetGiftData(rewardItem.goodsID);
            return giftData.HasReddot();
        }
    }
}