using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using UnityEngine;

namespace SGame
{

	partial class RequestExcuteSystem
	{
		static public void UnlockArea(int area)
		{
			if (area > 0)
			{
				var room = DataCenter.Instance.roomData.current;
				if (room != null && ConfigSystem.Instance.TryGet(area, out RoomAreaRowData cfg))
				{
					var state = PropertyManager.Instance.CheckCountByArgs(cfg.GetCostArray());
					if (state)
					{
						DataCenter.RoomUtil.UnlockArea(area);
						if (!string.IsNullOrEmpty(cfg.CustomerBorn))
							StaticDefine.CUSTOMER_TAG_BORN.Add(cfg.CustomerBorn);
						_eMgr.Trigger(((int)GameEvent.WORK_AREA_UNLOCK), area);
						_eMgr.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
						DelayTriggerAddRole(cfg).Start();
					}
				}

			}
		}

		static IEnumerator DelayTriggerAddRole(RoomAreaRowData cfg)
		{
			yield return new WaitEvent<int>(((int)GameEvent.WORK_AREA_UNLOCK));
			yield return new WaitForSeconds(0.1f);
			if (cfg.ChefNum > 0)
				DataCenter.RoomUtil.AddRole(((int)EnumRole.Cook), cfg.ChefNum, 0, 0);
			if (cfg.WaiterNum > 0)
				DataCenter.RoomUtil.AddRole(((int)EnumRole.Waiter), cfg.WaiterNum, 0, 0);
			if (cfg.CustomerNum > 0)
				DataCenter.RoomUtil.AddRole(((int)EnumRole.Customer), cfg.CustomerNum, 0, 0);
			_eMgr.Trigger((int)GameEvent.GAME_MAIN_REFRESH);

			yield return new WaitForSeconds(1f);
			var rewards = Utils.GetArrayList(true, cfg.GetReward1Array, cfg.GetReward2Array, cfg.GetReward3Array);
			if (rewards?.Count > 0)
				Utils.ShowRewards( rewards, null, "@ui_area_reward_title");
		}

	}

}