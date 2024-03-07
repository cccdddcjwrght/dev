using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class RequestExcuteSystem
	{
		[InitCall]
		static void InitOffline()
		{

			new WaitEvent<int>(((int)GameEvent.AFTER_ENTER_ROOM)).Wait((e) =>
			{
					StaticDefine.G_Offline_Time = DataCenter.Instance.GetOfflineTime();
					var min = GlobalDesginConfig.GetInt("min_offline_time");
					if (StaticDefine.G_Offline_Time >= min)
						_delayer.DelayOpen(11, "mainui");
			});

		}

		static public void GetOfflineReward(double gold)
		{
			if (gold > 0)
				PropertyManager.Instance.Update(PropertyGroup.ITEM, 1, gold);
		}
	}
}
