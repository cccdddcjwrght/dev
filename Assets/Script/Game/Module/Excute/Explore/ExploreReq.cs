using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using GameConfigs;
using SGame.UI.Common;
using SGame.UI.Explore;
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
			c_drop_equip_exp = GlobalDesginConfig.GetInt("battle_explore_drop_exp", 100);
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
					if (cost > 0)
					{
						if (!Utils.CheckItemCount(2, cost)) return false;
						PropertyManager.Instance.Update(1, 2, cost, true);
					}
					data.ToolUpLv();
					_eMgr.Trigger(((int)GameEvent.EXPLORE_TOOL_UP_LV));
					return true;
				}
			}
			return false;
		}

		static public bool ExploreSuccess(int count = 1)
		{
			var data = DataCenter.Instance.exploreData;

			var toolcfg = data.exploreToolLevel;
			var eqs = DataCenter.ExploreUtil.RandomFightEquip(toolcfg, count);
			if (eqs?.Count > 0)
			{
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EXPLORE, 1);
				data.cacheEqs = eqs;
				return true;
			}

			return false;
		}

		static public void ExplorePutOnEquip(FightEquip equip, params FightEquip[] drops)
		{
			if (equip != null || drops?.Length > 0)
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

				if (drops?.Length > 0)
				{
					var old = data.level;
					var count = 0;
					foreach (var drop in drops)
					{
						if (drop != null)
						{
							count++;
							if (drop.isnew == 1) { drop.Clear(); data.cacheEqs?.Remove(drop); }
							if (data.AddExp(c_drop_equip_exp))
								data.waitFlag = true;
						}
						else break;
					}
					if (count > 0)
					{
						var gold = BuffShopModule.Instance.GetBuffShopCoin(c_drop_equip_coin * count);
						PropertyManager.Instance.Update(1, 1, gold);
						if (data.showgoldfly) RunFly(data.addExp > 0).Start();
						if (data.waitFlag)
						{
							Utils.ShowRewards(() => data.waitFlag = false, title: "@ui_explore_uplv_title",
								contentCall: (view) => OnShowExploreLvUp(view, data.level, old)
							);
						}
						_eMgr.Trigger(((int)GameEvent.EXPLORE_UP_LEVEL));
					}
				}
			}
		}

		static void OnShowExploreLvUp(UI_CommonRewardBody view, int lv, int old)
		{
			view.m_loader.url = "ui://Explore/ExploreLvBody";
			var panel = view.m_loader.component as UI_ExploreLvBody;
			if (panel != null)
			{
				55.ToAudioID().PlayAudio();
				panel.m_lv.text = lv.ToString();
				panel.m_old.text = old.ToString();
				view.parent.TweenFade(0, 0.5f).SetDelay(3).OnComplete(() =>
				{
					UIUtils.CloseUIByName("rewardlist");
				});
			}
		}

		static IEnumerator RunFly(bool showexp = true)
		{
			TransitionModule.Instance.PlayFlight(FairyGUI.GRoot.inst, 1);

			if (showexp)
			{
				yield return new WaitForSeconds(0.5f);
				var p = UIUtils.GetUIPosition("explore", "progress.n2", true);
				TransitionModule.Instance.PlayFlight(GRoot.inst, ((int)ItemID.EXP), endpos: p);
			}
		}
	}
}
