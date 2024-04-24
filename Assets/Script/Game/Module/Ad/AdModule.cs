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

		public readonly float AD_MACHINE_RATION = GlobalDesginConfig.GetFloat("ad_machine_ratio");
		public readonly int AD_MACHINE_UP = GlobalDesginConfig.GetInt("ad_machine_up");
		public readonly int AD_MACHINE_NUM = GlobalDesginConfig.GetInt("ad_machine_num");
		public readonly int AD_BOOST_RATIO = GlobalDesginConfig.GetInt("ad_boost_ratio");
		public readonly int AD_BOOST_TIME = GlobalDesginConfig.GetInt("ad_boost_time");
		public readonly int AD_BUFF_MAX_TIME = GlobalDesginConfig.GetInt("ad_buff_max_time");

		public const int AD_BUFF_ID = 7;
		public const string AD_BUFF_TIME = "ad.bufftime";

		private EventHandleContainer m_handles = new EventHandleContainer();

		Dictionary<string, int> m_IntervalTimeDict = new Dictionary<string, int>();
		Dictionary<string, int> m_ShowTimeDict = new Dictionary<string, int>();


		public void Initalize()
		{
			m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) =>
			{
				AddBuff();
				RecordEnterTime(AdType.Invest.ToString());
			});
			ReadyAllAd();
		}

		public void AddBuff(bool isRecord = false)
		{
			var endTime = DataCenter.GetIntValue(AD_BUFF_TIME);
			var serverTime = GameServerTime.Instance.serverTime;
			if (isRecord)
			{
				if (serverTime > endTime) DataCenter.SetIntValue(AD_BUFF_TIME, serverTime + AD_BOOST_TIME);
				else
				{
					var residueTime = endTime - serverTime;
					int addTime = AD_BOOST_TIME;
					if (residueTime + AD_BOOST_TIME > AD_BUFF_MAX_TIME)
						addTime = AD_BUFF_MAX_TIME - residueTime;
					DataCenter.SetIntValue(AD_BUFF_TIME, endTime + addTime);
				}
			}

			var time = DataCenter.GetIntValue(AD_BUFF_TIME) - serverTime;
			if (time > 0)
				EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(AD_BUFF_ID, AD_BOOST_RATIO, 0, time) { from = (int)EnumFrom.Ad });
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
				m_IntervalTimeDict[id] = serverTime;
				m_ShowTimeDict[id] = 0;
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

		public void ReadyAllAd()
		{
			var cs = ConfigSystem.Instance.Finds<GameConfigs.ADConfigRowData>((c) => c.Type != 2);
			if (cs?.Count > 0)
			{
					cs.ForEach((c) => Utils.PlayAd( c.Ad , null, true));
			}
		}


	}
}
