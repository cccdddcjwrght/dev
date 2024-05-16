
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using Unity.Mathematics;
	using System.Collections.Generic;
	using GameConfigs;
	using Unity.VisualScripting;
	using System.Linq;

	public partial class UIWorktablePanel
	{
		private SGame.Worktable data;
		private WorktableInfo info;
		private int[] stars;

		partial void BeforeInit(UIContext context)
		{
			if (context.GetParam()?.Value is WorktableInfo info)
			{
				this.info = info;
			}
		}

		partial void InitLogic(UIContext context)
		{
			if (info.id > 0)
			{
				this.Delay(() =>
				{
					EventManager.Instance.Trigger(((int)GameEvent.WORK_HUD_SHOW));
				}, 200);

				this.data = DataCenter.MachineUtil.GetWorktable(info.id);
				m_view.m_pos.selectedIndex = GetOffset(info.target);
				switch (info.type)
				{
					case 1:
						SetUnlockInfo();
						break;
					case 2:
						SetUplevelInfo();
						break;
				}

			}
		}

		partial void UnInitLogic(UIContext context)
		{

		}

		private void SetUnlockInfo()
		{
			m_view.m_type.selectedIndex = 1;
			if (!data.isTable)
			{
				var state = PropertyManager.Instance.CheckCountByArgs(data.cfg.GetUnlockPriceArray());
				UIListener.SetText(m_view.m_click, SGame.Utils.ConvertNumberStr(data.cfg.UnlockPrice(2)));
				UIListener.SetControllerSelect(m_view.m_click, "limit", 0);
				UIListener.SetControllerSelect(m_view.m_click, "gray", state ? 0 : 1);
			}
			else
			{
				UIListener.SetControllerSelect(m_view.m_click, "hasIcon", 1);
				UIListener.SetTextByKey(m_view.m_click, "ui_unlock_tips");
			}
		}

		private void SetUplevelInfo()
		{
			m_view.m_type.selectedIndex = 0;
			UIListener.SetText(m_view.m_time, data.GetWorkTime().ToString() + "s");
			m_view.m_list.itemRenderer = SetStarInfo;
			LevelRefresh();
			AdRefresh();
		}

		private void LevelRefresh()
		{
			if (m_view == null) return;

			stars = DataCenter.MachineUtil.GetWorktableStarInfo(data.id);
			var cost = data.GetUpCost(out var cty, out var cid);
			var lvmax = data.maxlv <= data.level;
			var maxStar = DataCenter.MachineUtil.GetWorkertableMaxStar(data.maxlv);
			if (lvmax)
			{
				UIListener.SetControllerSelect(m_view.m_click, "limit", 1);
				UIListener.SetTextByKey(m_view.m_click, "ui_main_btn_upgrademax");
				m_view.m_rewardlist?.RemoveChildrenToPool();
			}
			else
			{
				RefreshClick();
				UIListener.SetText(m_view.m_click, SGame.Utils.ConvertNumberStr(cost));
				SetUpStarRewards();

			}
			UIListener.SetTextByKey(m_view, data.cfg.MachineName);
			UIListener.SetText(m_view.m_price, SGame.Utils.ConvertNumberStr(data.GetPrice()));
			SetLevelText(UIListener.LocalFormat("ui_main_btn_upgradelevel", data.level));
			SetProgressValue(data.lvcfg.MachineStar == maxStar ? 100 : DataCenter.MachineUtil.GetStarProgress(data.id));

			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.numItems = stars.Length;

			if (m_view.m_isAd.selectedIndex == 1) AdRefresh();
		}

		private void SetUpStarRewards()
		{
			var ls = data.starRewards;
			m_view.m_rewardlist?.RemoveChildrenToPool();

			void Set(int index, object data, GObject gObject)
			{
				gObject.SetCommonItem(null, data as int[]);
			}

			if (ls?.Count > 0)
			{
				for (int i = 1; i < ls.Count; i++)
				{
					var item = ls[i];
					if (ConfigSystem.Instance.TryGet<ItemRowData>(item[0], out var cfg))
					{
						if (ActiveTimeSystem.Instance.IsActiveBySubID(cfg.TypeId, GameServerTime.Instance.serverTime, out var data))
						{
							if (("act" + data.actSubID).IsOpend(false) || ("act" + data.configID).IsOpend(false))
								SGame.UIUtils.AddListItem(m_view.m_rewardlist, Set, item);
						}
					}
				}
				SGame.UIUtils.AddListItem(m_view.m_rewardlist, Set, ls[0]).name = "0";
				m_view.m_rewardlist.touchable = m_view.m_rewardlist.numItems > 5;
				m_view.m_rewardlist.ScrollToView(m_view.m_rewardlist.numItems - 1);
			}
		}

		//广告刷新（超过工作台一半等级显示广告按钮）
		private void AdRefresh()
		{
			if (!NetworkUtils.IsNetworkReachability())
			{
				m_view.m_type.selectedIndex = 0;
				m_view.m_isAd.selectedIndex = 0;
				return;
			}

			var lvMax = data.level >= data.maxlv;
			//广告配置条件（次数，cd时间）
			var isCanPlay = DataCenter.AdUtil.IsAdCanPlay(AdType.Table.ToString());
			//出现条件
			var isCondition = data.level >= (int)(data.maxlv * AdModule.Instance.AD_MACHINE_RATION) &&
				data.level >= AdModule.Instance.AD_MACHINE_UP;
			var isShow = isCanPlay && isCondition && !lvMax;
			m_view.m_isAd.selectedIndex = isShow ? 1 : 0;

			m_view.m_type.selectedIndex = 0;
			if (isShow)
			{
				int adAddLv = Mathf.Min(AdModule.Instance.AD_MACHINE_NUM, data.maxlv - data.level);
				UIListener.SetText(m_view.m_adBtn, string.Format("+{0} LEVELS", adAddLv.ToString()));
				UIListener.SetControllerSelect(m_view.m_adBtn, "hasIcon", 0);
				UIListener.SetControllerSelect(m_view.m_adBtn, "gray", 0);
				UIListener.SetControllerSelect(m_view.m_adBtn, "iconImage", 2);
				m_view.m_type.selectedIndex = 2;
			}
		}

		private void RefreshClick()
		{
			if (m_view == null) return;
			var lvmax = data.maxlv <= data.level;
			if (!lvmax)
			{
				var state = true;
				if (info.type == 1)
					state = DataCenter.MachineUtil.CheckCanActiveMachine(info.mid) == 0;
				else
					state = DataCenter.MachineUtil.CheckCanUpLevel(data) == 0;


				UIListener.SetControllerSelect(m_view.m_click, "limit", 0);
				UIListener.SetControllerSelect(m_view.m_click, "gray", state ? 0 : 1);
			}
		}

		private void SetStarInfo(int index, GObject star)
		{
			UIListener.SetControllerSelect(star, "type", stars[index]);
		}


		private int GetOffset(float3 pos)
		{
			var p = SGame.UIUtils.WorldPosToUI(GRoot.inst, pos);
			var hs = m_view.size.x * 0.5f + 10;
			var left = p.x - hs < 0;
			var right = p.x + hs > GRoot.inst.width;

			if (left) return 1;
			else if (right) return 2;
			return 0;
		}

		private void Unlock()
		{
			switch (DataCenter.MachineUtil.CheckCanActiveMachine(info.mid))
			{
				case Error_Code.MACHINE_DEPENDS_NOT_ENABLE:
					//"@tips_unlock_fail".Tips();
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					//"@tips_unlock_item_not_enough".Tips();
					break;
				case 0:
					DataCenter.MachineUtil.AddMachine(info.mid);
					PropertyManager.Instance.UpdateByArgs(true, data.cfg.GetUnlockPriceArray());
					SGame.UIUtils.CloseUIByID(__id);
					break;
			}
		}

		public bool UpLevel()
		{
			switch (DataCenter.MachineUtil.CheckCanUpLevel(data))
			{
				case Error_Code.LV_MAX:
					Debug.Log($"{info.id} Lv Max!!!");
					//"@tips_lv_max".Tips();
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					Debug.Log($"{info.id} 升级道具不足!!!");
					//"@tips_uplv_item_not_enough".Tips();
					break;
				case 0:
					DataCenter.MachineUtil.UpdateLevel(info.id, 0);
					LevelRefresh();
					ShowUplevelEffect();
					EffectSystem.Instance.AddEffect(2, m_view.m_click);
					return true;
			}
			pressFlag = false;
			return false;
		}

		void ShowUplevelEffect()
		{
			var ls = new List<GObject>();
			if (data.addMachine > 0)
			{
				var item = m_view.m_tips.GetFromPool(null) as UI_Tips;
				ls.Add(item.SetTextByKey("tips_main_btn_upgrade_3", data.addMachine));
			}
			if (data.addProfit > 0)
			{
				var item = m_view.m_tips.GetFromPool(null) as UI_Tips;
				ls.Add(item.SetTextByKey("tips_main_btn_upgrade_2", data.addProfit));
			}
			AddItem(ls.ToArray()).Start();
		}

		System.Collections.IEnumerator AddItem(params GObject[] items)
		{
			var i = 0;
			while (i < items.Length && m_view != null && m_view.m_tips != null)
			{
				m_view.m_tips.AddChild(items[i++]);
				m_view.m_tips.ScrollToView(m_view.m_tips.numItems - 1, true);
				yield return new WaitForSeconds(0.2f);
			}
		}

		partial void OnClickClick(EventContext data)
		{
			if (m_view == null) return;
			if (this.info.type == 1)
			{
				Unlock();
			}
			else if (this.info.type == 2)
			{
				UpLevel();
			}
		}

		partial void OnClickBtnClick(EventContext data)
		{
			if (DataCenter.Instance.guideData.isGuide)
			{
				if (this.info.type == 1)
				{
					Unlock();
				}
				else if (this.info.type == 2)
				{
					UpLevel();
				}
			}
		}

		partial void OnAdBtnClick(EventContext data)
		{
			AdModule.PlayAd(AdType.Table.ToString(), (state) => AdUpdateLevel(state));
		}

		//广告升级
		void AdUpdateLevel(bool state)
		{
			if (state)
			{
				var upNum = AdModule.Instance.AD_MACHINE_NUM;
				var level = Mathf.Min(upNum, data.maxlv - data.level);
				DataCenter.MachineUtil.UpdateLevel(info.id, 0, level);
				LevelRefresh();
			}
		}
	}
}
