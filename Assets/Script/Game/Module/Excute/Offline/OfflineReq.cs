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

					log.Info("[offlinetime end]->" + UnityEngine.Time.realtimeSinceStartup.ToString());

					StaticDefine.G_Offline_Time = (int)UnityEngine.Time.realtimeSinceStartup - DataCenter.Instance.offlinetime;
					if (StaticDefine.G_Offline_Time > minOfflineTime || flag)
						11.Goto();
					else
						GetOfflineReward(DataCenter.CaluOfflineReward(StaticDefine.G_Offline_Time));
					log.Info("[offlinetime]->" + StaticDefine.G_Offline_Time.ToString());
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

		static public void GetOfflineReward(double gold)
		{
			if (gold > 0)
			{
				DataCenter.Instance.offlinetime = -1;
				PropertyManager.Instance.Update(PropertyGroup.ITEM, 1, gold);
			}
		}
	}
}
