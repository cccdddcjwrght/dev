using GameConfigs;
using log4net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame 
{
    public partial class DataCenter 
    {
        public PiggyBankData piggybankData = new PiggyBankData();

        public static class PiggyBankUtils 
        {
            public static int PIGGYBANK_ADD = GlobalDesginConfig.GetInt("piggybank_add");
            public static int PIGGYBANK_MID = GlobalDesginConfig.GetInt("piggybank_mid");
            public static int PIGGYBANK_MAX = GlobalDesginConfig.GetInt("piggybank_max");

            public static List<int> PIGGYBANK_CHANCE_FIRST = GlobalDesginConfig.GetIntArray("piggybank_chance_first").ToList();
            public static List<int> PIGGYBANK_CHANCE = GlobalDesginConfig.GetIntArray("piggybank_chance").ToList();

            public const int PIGGYITEM_ID = 102;

            public static PiggyBankData m_data = Instance.piggybankData;

            private static GameConfigs.ShopRowData m_ShopRowData;
            public static GameConfigs.ShopRowData shopRowData 
            {
                get 
                {
                    if (!m_ShopRowData.IsValid() && ConfigSystem.Instance.TryGet<ShopRowData>(PIGGYITEM_ID, out var data))
                        m_ShopRowData = data;
                    return m_ShopRowData;
                }
            }

            public static void AddPiggyBankValue() 
            {
                if (!PiggyBankModule.Instance.CanTake()) return;

                m_data.progress += PIGGYBANK_ADD;
                m_data.progress = Mathf.Min(m_data.progress, PIGGYBANK_MAX);
                EventManager.Instance.Trigger((int)GameEvent.PIGGYBANK_UPDATE);
            }

            public static bool CheckBuyPiggyBank() 
            {
                return m_data.progress >= PIGGYBANK_MID;
            }

            //购买
            public static void BuyPiggyBank() 
            {
                //有免费次数
                if (GetNextFreeRefreshTime() <= 0)
                {
                    m_data.nextRefreshTime = GameServerTime.Instance.serverTime + 86400;
                    BuySuccessResult();
                    EventManager.Instance.Trigger((int)GameEvent.PIGGYBANK_UPDATE);
                    return;
                }

                RequestExcuteSystem.BuyGoods(PIGGYITEM_ID);
            }

            public static void BuySuccessResult() 
            {
                var list = m_data.isBroken ? PIGGYBANK_CHANCE : PIGGYBANK_CHANCE_FIRST;
                var weights = list.GetRange(m_data.stage, list.Count - m_data.stage);
                var index = SGame.Randoms.Random._R.NextWeight(weights) + 1;
                m_data.stage += index;
                if (m_data.stage >= list.Count)
                    GainPiggyBankResult();
            }

            public static void GainPiggyBankResult()
            {
                m_data.isBroken = true;
                PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.DIAMOND, m_data.progress);
                Reset();

                EventManager.Instance.Trigger((int)GameEvent.PIGGYBANK_UPDATE);
            }

            public static int GetNextFreeRefreshTime() 
            {
                return Mathf.Max(m_data.nextRefreshTime - GameServerTime.Instance.serverTime, 0);
            }

            
            public static void Reset() 
            {
                m_data.progress = 0;
                m_data.stage    = 0;
            }
        }
    }

    public class PiggyBankModule : Singleton<PiggyBankModule> 
    {
        private static ILog log = LogManager.GetLogger("game.piggybank");
        public const int PIGGYBANK_OEPNID = 21;
        private EventHanle m_enterGameEvent;
        public void Initalize()
        {
            m_enterGameEvent = EventManager.Instance.Reg<int>((int)GameEvent.AFTER_ENTER_ROOM, OnFirstEnterRoom);
        }

        void OnFirstEnterRoom(int scene) 
        {
            m_enterGameEvent.Close();
            m_enterGameEvent = null;

            if (CanTake()) 
            {
                log.Info("Open piggybank");
                DelayExcuter.Instance.DelayOpen("piggybank", "mainui");
            }
        }

        public bool CanTake()
        {
            return PIGGYBANK_OEPNID.IsOpend(false);
        }
    }

    [System.Serializable]
    public class PiggyBankData
    {
        //存钱罐进度
        public int progress;
        //破损阶段
        public int stage;
        //下一次免费刷新时间
        public int nextRefreshTime;
        //是否打破过存储罐
        public bool isBroken;
    }
}


