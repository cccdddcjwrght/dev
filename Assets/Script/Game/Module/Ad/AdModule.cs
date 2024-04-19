using GameConfigs;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum AdType 
    {
        Table,
        Buff,
        Invest,
    }

    public class AdModule : Singleton<AdModule>
    {
        private static ILog log = LogManager.GetLogger("game.ad");

        public readonly float   AD_MACHINE_RATION   = GlobalDesginConfig.GetFloat("ad_machine_ratio");
        public readonly int     AD_MACHINE_UP       = GlobalDesginConfig.GetInt("ad_machine_up");
        public readonly int     AD_MACHINE_NUM      = GlobalDesginConfig.GetInt("ad_machine_num");
        public readonly int     AD_BOOST_RATIO      = GlobalDesginConfig.GetInt("ad_boost_ratio");
        public readonly int     AD_BOOST_TIME       = GlobalDesginConfig.GetInt("ad_boost_time");

        public const int AD_BUFF_ID = 7;
        public const string AD_BUFF_TIME = "ad.bufftime";

        private EventHandleContainer m_handles = new EventHandleContainer();

        int m_EnterTime;
        int m_vaildEndTime;

        public void Initalize() 
        {
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM,(s)=> { 
                AddBuff();
                RecordEnterTime();
            });
        }

        public void AddBuff(bool isRecord = false) 
        {
            var endTime = DataCenter.GetIntValue(AD_BUFF_TIME);
            var serverTime = GameServerTime.Instance.serverTime;
            if (isRecord)
            {
                if (serverTime > endTime) DataCenter.SetIntValue(AD_BUFF_TIME, serverTime + AD_BOOST_TIME);
                else DataCenter.SetIntValue(AD_BUFF_TIME, endTime + AD_BOOST_TIME);
            }

            var time = DataCenter.GetIntValue(AD_BUFF_TIME) - serverTime;
            if(time > 0) 
                EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(AD_BUFF_ID, AD_BOOST_RATIO, 0, time){ from = (int)EnumFrom.Ad });
        }

        public int GetBuffTime() 
        {
            return DataCenter.GetIntValue(AD_BUFF_TIME) - GameServerTime.Instance.serverTime;
        }

        public void RecordEnterTime() 
        {
            m_EnterTime = GameServerTime.Instance.serverTime;
        }

        public void GetAdShowTime(string id, out bool state, out int time) 
        {
            time = 0; state = false;
            if (!DataCenter.AdUtil.IsAdCanPlay(id)) return;

            int serverTime = GameServerTime.Instance.serverTime;
            int interval = DataCenter.AdUtil.GetAdIntervalTime(id);
            int sustain = DataCenter.AdUtil.GetAdSustainTime(id);

            state = serverTime > m_EnterTime + interval;
            if (!state) return;

            if (m_vaildEndTime == 0) 
                m_vaildEndTime = serverTime + sustain;
            
            if (m_vaildEndTime > 0 && m_vaildEndTime > serverTime) 
                time = m_vaildEndTime - serverTime;
            if (serverTime > m_vaildEndTime) 
            {
                m_EnterTime = serverTime;
                m_vaildEndTime = 0;
            }
        }

        public double GetAdAddCoinNum() 
        {
            var min = GlobalDesginConfig.GetInt("min_offline_Value");
            var rate = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.Gold);
            var gold = (double)min;
            var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable && w.level > 0);
            if (ws?.Count > 0) ws.ForEach(w => gold += w.GetPrice() / w.GetWorkTime());
            return (ConstDefine.C_PER_SCALE * gold * rate).ToInt();
        }

        public void PlayAd(string id, Action complete) 
        {
            if (!DataCenter.AdUtil.IsAdCanPlay(id)) return;
            if (DataCenter.IsIgnoreAd())
            {
                DataCenter.AdUtil.RecordPlayAD(id);
                complete?.Invoke();
            }
            else 
            {
                Utils.PlayAd(id, (state, t) =>
                {
                    if (state)
                    {
                        DataCenter.AdUtil.RecordPlayAD(id);
                        complete?.Invoke();
                    }
                });
            }
        }
    }
}
