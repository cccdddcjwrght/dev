﻿using System;
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

		static int c_drop_equip_exp;
		static int c_drop_equip_coin;


		[InitCall]
		static void InitExplore()
		{
			c_drop_equip_exp =  GlobalDesginConfig.GetInt("battle_explore_drop_exp", 100);
			c_drop_equip_coin = GlobalDesginConfig.GetInt("battle_explore_drop_coin", 100);

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
					if (data.uplvtime > 0 && data.ToolUpRemaining() <= 0)
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
					conditon = data.exploreToolLevel.ExploreLevel;
					if (conditon > 0 && data.level >= conditon)
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
							"item_not_enough".Local(null, Utils.GetItemName(1, cost[1]).Local()).Tips();
					}
				}
			}

			return false;


		}

		static public bool ExploreToolUpLv(bool imm = false, double cost = 0)
		{

			var data = DataCenter.Instance.exploreData;
			if (data.uplvtime >= 0)
			{
				if (imm || data.ToolUpRemaining() <= 0)
				{
					data.ToolUpLv();
					if (cost > 0)
					{
						if (!Utils.CheckItemCount(2, cost)) return false;
						PropertyManager.Instance.Update(1, 2, cost, true);
					}
					_eMgr.Trigger(((int)GameEvent.EXPLORE_TOOL_UP_LV));
					return true;
				}
			}
			return false;
		}

		static public bool ExploreSuccess()
		{
			var data = DataCenter.Instance.exploreData;

			var toolcfg = data.exploreToolLevel;
			var eq = DataCenter.ExploreUtil.RandomFightEquip(toolcfg);
			if (eq != null)
			{
				data.cacheEquip = eq;
				eq.isnew = 1;
				return true;
			}

			return false;
		}

		static public void ExplorePutOnEquip(FightEquip equip, FightEquip drop)
		{
			if (equip != null || drop != null)
			{
				var data = DataCenter.Instance.exploreData;

				if (equip != null)
				{
					if (data.explorer.Puton(equip))
					{
						data.explorer.GetPower(true);
						_eMgr.Trigger(((int)GameEvent.EXPLORE_CHNAGE_EQUIP), equip);
					}
				}

				if (drop != null)
				{
					if (drop == data.cacheEquip) drop.Clear();
					var gold = BuffShopModule.Instance.GetBuffShopCoin(c_drop_equip_coin);
					PropertyManager.Instance.Update(1, 1, gold);
					data.AddExp(c_drop_equip_exp);
					_eMgr.Trigger(((int)GameEvent.EXPLORE_UP_LEVEL));

					if (data.showgoldfly)
						TransitionModule.Instance.PlayFlight(FairyGUI.GRoot.inst, 1);

				}

				data.cacheEquip = default;
			}
		}

	}
}
