using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SGame
{
	partial class RequestExcuteSystem
	{

		[InitCall]
		static void InitExplore()
		{

			DataCenter.ExploreUtil.Init();
			StartUpToolTimer();
		}

		static private void StartUpToolTimer()
		{
			var data = DataCenter.Instance.exploreData;
			var w = new WaitForSeconds(0.1f);
			IEnumerator Run()
			{
				while (true)
				{
					if (data.uplvtime > 0 && data.ToolUpRemaining()<=0)
						ExploreToolUpLv(true);
					yield return w;
				}
			}
			Run().Start();
		}

		static public bool ExploreBeginToolUpLv()
		{

			var data = DataCenter.Instance.exploreData;

			if (!data.IsExploreToolMaxLv())
			{
				var room = DataCenter.Instance.roomData.roomID;
				var conditon = data.exploreToolLevel.MapId;
				if (room >= conditon)
				{
					var cost = data.exploreToolLevel.GetCostArray();
					if (PropertyManager.Instance.CheckCountByArgs(cost))
					{
						PropertyManager.Instance.UpdateByArgs(true, cost);
						data.uplvtime = GameServerTime.Instance.serverTime + data.exploreToolLevel.Time;
						_eMgr.Trigger(((int)GameEvent.EXPLORE_TOOL_UP_LV_START));
						return true;
					}
					else
						"item_not_enough".Local(null, Utils.GetItemName(1, cost[2]).Local()).Tips();
				}
			}

			return false;


		}

		static public bool ExploreToolUpLv(bool imm = false , double cost = 0) {

			var data = DataCenter.Instance.exploreData;
			if (data.uplvtime >= 0)
			{
				if (imm || data.ToolUpRemaining() <= 0)
				{
					data.ToolUpLv();
					if (cost > 0) PropertyManager.Instance.Update(1, 2, cost, true);
					_eMgr.Trigger(((int)GameEvent.EXPLORE_TOOL_UP_LV));
					return true;
				}
			}
			return false;
		}

	}
}
