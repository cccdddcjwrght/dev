using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class RequestExcuteSystem
	{
		static bool focusflag = true;
		static int minOfflineTime = 0;

		const string offline_ui = "offlineui";

		[InitCall]
		static void InitOffline()
		{
			minOfflineTime = GlobalDesginConfig.GetInt("min_offline_time");

			new WaitEvent<int>(((int)GameEvent.AFTER_ENTER_ROOM)).Wait((e) =>
			{
				StaticDefine.G_Offline_Time = DataCenter.Instance.GetOfflineTime();
				log.Warn("[begin offline]svr " + GameServerTime.Instance.serverTime);
				log.Warn("[begin offline]last " + DataCenter.Instance.offlinetime);
				log.Warn("[begin offline]all " + StaticDefine.G_Offline_Time);
				DataCenter.Instance.offlinetime = -1;
				if (StaticDefine.G_Offline_Time >= minOfflineTime)
				{
					if ("offline".IsOpend(false))
						_delayer.DelayOpen(11, "mainui");
				}
			});



			static void OnContinue()
			{
				if ("offline".IsOpend(false))
				{
					var flag = UIUtils.CheckUIIsOpen(offline_ui);
					if (!flag)
					{
						log.Info("[offlinetime end]->" + UnityEngine.Time.realtimeSinceStartup.ToString());

						StaticDefine.G_Offline_Time = (int)UnityEngine.Time.realtimeSinceStartup - DataCenter.Instance.offlinetime;
						if (StaticDefine.G_Offline_Time > minOfflineTime || flag)
							11.Goto();
						else
							GetOfflineReward(DataCenter.CaluOfflineReward(StaticDefine.G_Offline_Time));
						log.Info("[offlinetime]->" + StaticDefine.G_Offline_Time.ToString());
					}
				}
			}

			static void OnPause()
			{
				if (DataCenter.Instance.offlinetime < 0)
				{
					log.Info("[offlinetime start]->" + UnityEngine.Time.realtimeSinceStartup.ToString());
					DataCenter.Instance.offlinetime = (int)UnityEngine.Time.realtimeSinceStartup;
				}
			}


			((Action)OnPause).CallWhenPause();
			((Action)OnContinue).CallWhenContinue();

		}

		static public void GetOfflineReward(double gold, bool cache = false)
		{
			log.Info("[offline] reward:" + gold);
			DataCenter.Instance.offlinetime = -1;
			if (gold > 0)
			{
				if (cache)
					PropertyManager.Instance.Insert2Cache(new List<double[]>() { new double[] { ((int)PropertyGroup.ITEM), ((int)ItemID.GOLD), gold } });
				else
					PropertyManager.Instance.Update(PropertyGroup.ITEM, 1, gold);
			}
			if (!cache)
				PropertyManager.Instance.CombineCache2Items();
		}
	}
}
