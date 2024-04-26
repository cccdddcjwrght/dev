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
        //public readonly int     AD_BOOST_RATIO      = GlobalDesginConfig.GetInt("ad_boost_ratio");
        //public readonly int     AD_BOOST_TIME       = GlobalDesginConfig.GetInt("ad_boost_time");
        public readonly int     AD_BUFF_MAX_TIME    = GlobalDesginConfig.GetInt("ad_buff_max_time");
        public readonly int     AD_INVEST_MAX_LEVEL = GlobalDesginConfig.GetInt("ad_invest_max_level");

        public const int AD_BUFF_ID = 7;
        public const int AD_TECH_ID = 6;
        public const string AD_BUFF_TIME = "ad.bufftime";

        private EventHandleContainer m_handles = new EventHandleContainer();

        Dictionary<string, int> m_IntervalTimeDict = new Dictionary<string, int>();
        Dictionary<string, int> m_ShowTimeDict = new Dictionary<string, int>();


        public void Initalize() 
        {
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM,(s)=> { 
                AddBuff();
                RecordEnterTime(AdType.Invest.ToString());
            });
            m_handles += EventManager.Instance.Reg<int, int>((int)GameEvent.TECH_LEVEL, (id, level) =>
            {
                if (id == AD_TECH_ID) AddBuff();
            });
			ReadyAllAd();
        }

        //后续优化
        public bool CheckInvestShow() 
        {
            var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable && w.level > 0);
            int maxLv = 0;
            ws.ForEach((w) => { if (w.level > maxLv) maxLv = w.level; });
            return maxLv >= AD_INVEST_MAX_LEVEL;
        }

        public void AddBuff(bool isRecord = false) 
        {
            var endTime = DataCenter.GetIntValue(AD_BUFF_TIME);
            var serverTime = GameServerTime.Instance.serverTime;
            if (isRecord)
            {
                if (serverTime > endTime) DataCenter.SetIntValue(AD_BUFF_TIME, serverTime + Math.Min(GetAdDuration(), AD_BUFF_MAX_TIME));
                else 
                {
                    var residueTime = endTime - serverTime;
                    Debug.Log((int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.AdTime));
                    int addTime = GetAdDuration();
                    if (residueTime + addTime > AD_BUFF_MAX_TIME)
                        addTime = AD_BUFF_MAX_TIME - residueTime;
                    DataCenter.SetIntValue(AD_BUFF_TIME, endTime + addTime); 
                }
            }

            var time = DataCenter.GetIntValue(AD_BUFF_TIME) - serverTime;
            if(time > 0) 
                EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(AD_BUFF_ID, GetAdRatio() - 100, 0, time){ from = (int)EnumFrom.Ad });
        }

        public int GetBuffTime() 
        {
            return DataCenter.GetIntValue(AD_BUFF_TIME) - GameServerTime.Instance.serverTime;
        }

        public void RecordEnterTime(string id) 
        {
            m_IntervalTimeDict[id] = GameServerTime.Instance.serverTime;
            m_ShowTimeDict[id] = 0;
        }


        public void GetAdShowTime(string id, out bool state, out int time) 
        {
            time = 0; state = false;

            if (!DataCenter.AdUtil.IsAdCanPlay(id)) return;
            if (id == AdType.Invest.ToString() && !CheckInvestShow()) return;

            int serverTime = GameServerTime.Instance.serverTime;
            int interval = DataCenter.AdUtil.GetAdIntervalTime(id);
            int sustain = DataCenter.AdUtil.GetAdSustainTime(id);

            state = serverTime > m_IntervalTimeDict[id] + interval;
            if (!state) return;

            if (m_ShowTimeDict[id] == 0)
                m_ShowTimeDict[id] = serverTime + sustain;
            
            if (m_ShowTimeDict[id] > 0 && m_ShowTimeDict[id] > serverTime) 
                time = m_ShowTimeDict[id] - serverTime;
            if (serverTime > m_ShowTimeDict[id]) 
            {
                RecordEnterTime(id);
                //m_IntervalTimeDict[id] = serverTime;
                //m_ShowTimeDict[id] = 0;
            }
        }


 
        double  m_AdAddCoin;
        int     m_LastTime;
        public double GetAdAddCoinNum() 
        {
            if (m_LastTime < m_ShowTimeDict[AdType.Invest.ToString()]) 
            {
                var min = GlobalDesginConfig.GetInt("min_offline_Value");
                var rate = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.Gold);
                //var adRate = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.AdAddition);
                var gold = (double)min;
                var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable && w.level > 0);
                if (ws?.Count > 0) ws.ForEach(w => gold += w.GetPrice() / w.GetWorkTime());
                m_AdAddCoin = (ConstDefine.C_PER_SCALE * gold * rate).ToInt();
                m_LastTime = m_ShowTimeDict[AdType.Invest.ToString()];
            }
            return m_AdAddCoin;
        }

        public int GetAdDuration() 
        {
            return (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.AdTime);
        }

        public int GetAdRatio() 
        {
            return (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.AdAddition);
        }

        public void PlayAd(string id, Action complete , Action<bool> other = null) 
        {
            if (!DataCenter.AdUtil.IsAdCanPlay(id))
			{
				complete?.Invoke();
				other?.Invoke(false);
				return;
			}

            if (!DataCenter.AdUtil.IsAdNeedPlay(id)) 
            {
                DataCenter.AdUtil.RecordPlayAD(id);
                complete?.Invoke();
                other?.Invoke(true);
                return;
            }

            if (DataCenter.IsIgnoreAd())
            {
                DataCenter.AdUtil.RecordPlayAD(id);
                complete?.Invoke();
				other?.Invoke(true);
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
					other?.Invoke(state);
				});
            }
        }

		public void ReadyAllAd()
		{
			var cs = ConfigSystem.Instance.Finds<GameConfigs.ADConfigRowData>((c) => c.Type != 2);
			if (cs?.Count > 0)
			{
				cs.ForEach((c) => Utils.PlayAd(c.Ad, null, true));
			}
		}

		public static void PlayAd(string id , Action<bool> call = null)
		{
			Instance.PlayAd(id, null, call);
		}
	}
}
