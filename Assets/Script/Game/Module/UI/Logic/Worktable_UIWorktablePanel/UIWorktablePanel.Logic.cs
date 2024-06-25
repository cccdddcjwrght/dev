
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using Unity.Mathematics;
	using System.Collections.Generic;
	using GameConfigs;
	using System.Linq;
	using System;

	public partial class UIWorktablePanel
	{
		private SGame.Worktable data;
		private WorktableInfo info;
		private int[] stars;
		private List<CookBookItem> books;

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

				m_view.m_pos.selectedIndex = 0;
				m_view.m_type.selectedIndex = 0;
				m_view.m_maxlv.selectedIndex = 0;
				this.data = DataCenter.MachineUtil.GetWorktable(info.id);
				switch (info.type)
				{
					case 1:
						SetUnlockInfo();
						break;
					case 2:
						SetUplevelInfo();
						break;
					case 3:
						SetAreaUnlock();
						break;
					case 4:
						SetUplevelInfo(3);
						break;
				}
				m_view.m_pos.selectedIndex = GetOffset(info.target);
				m_view.m_foods.onClickItem.Add(OnFoodClick);
			}
		}

		partial void UnInitLogic(UIContext context)
		{
			books?.Clear();
			books = null;
			data = null;
			info = default;
		}

		private void SetUnlockInfo()
		{
			m_view.m_type.selectedIndex = 1;
			if (!data.isTable)
			{
				books = data.cfg.GetItemIdArray().Select(id => DataCenter.CookbookUtils.GetBook(id)).ToList();

				SGame.UIUtils.AddListItems(m_view.m_foods, books, OnSetFoodItemInfo);
				m_view.m_foods.selectedIndex = Utils.FindCompareIndex(books, (a, b) => a.GetPrice().CompareTo(b.GetPrice()), b => b.IsEnable(), def: 0);
				OnFoodClick();
				SetUnlockBtn(data.GetUnlockPrice(), books.Any(b => b.IsEnable()));
			}
			else
			{
				UIListener.SetControllerSelect(m_view.m_click, "hasIcon", 1);
				UIListener.SetTextByKey(m_view.m_click, "ui_unlock_tips");
			}
		}

		private void SetUplevelInfo(int type = 0)
		{
			m_view.m_type.selectedIndex = type;
			UIListener.SetText(m_view.m_time, data.GetWorkTime().ToString() + "s");
			m_view.m_list.itemRenderer = SetStarInfo;
			LevelRefresh();
			if (type == 0) AdRefresh();

		}

		private void SetAreaUnlock()
		{

			if (ConfigSystem.Instance.TryGet<RoomAreaRowData>(info.id, out var row))
				SetUnlockBtn(row.GetCostArray());

			m_view.m_type.selectedIndex = 1;
		}

		private void LevelRefresh()
		{
			if (m_view == null) return;

			stars = DataCenter.MachineUtil.GetWorktableStarInfo(data.id);
			var cost = data.GetUpCost(out var cty, out var cid);
			var lvmax = data.IsMaxLv();
			var maxStar = data.GetMaxStar();
			if (lvmax)
			{
				UIListener.SetControllerSelect(m_view.m_click, "limit", 1);
				UIListener.SetTextByKey(m_view.m_click, "ui_main_btn_upgrademax");
				m_view.m_rewardlist?.RemoveChildrenToPool();
				m_view.m_maxlv.selectedIndex = 1;
			}
			else
			{
				RefreshClick();
				SetUpStarRewards();
				UIListener.SetText(m_view.m_click, SGame.Utils.ConvertNumberStr(cost));
				m_view.m_maxlv.selectedIndex = 0;
				if (data.objLvCfg.IsValid() && data.objLvCfg.Condition > 0)
				{
					if (!DataCenter.MachineUtil.IsAreaEnable(data.objLvCfg.Condition))
						m_view.m_maxlv.selectedIndex = 2;
				}
			}
			UIListener.SetText(m_view, data.foodName,false);
			UIListener.SetText(m_view.m_price, SGame.Utils.ConvertNumberStr(data.GetPrice()));
			m_view.m_level.SetText(UIListener.LocalFormat("ui_main_btn_upgradelevel", data.level));
			SetRoleChangeInfo();
			if (data.lvcfg.IsValid()) m_view.m_progress.value = data.lvcfg.MachineStar == maxStar ? 100 : DataCenter.MachineUtil.GetStarProgress(data.id);
			m_view.m_list.RemoveChildrenToPool();
			if (stars != null) m_view.m_list.numItems = stars.Length;
			if (m_view.m_isAd.selectedIndex == 1) AdRefresh();
		}

		private void SetUnlockBtn(float[] cost, bool enable = true)
		{
			var state = enable && PropertyManager.Instance.CheckCountByArgs(cost);
			UIListener.SetText(m_view.m_click, SGame.Utils.ConvertNumberStr(cost[2]));
			UIListener.SetControllerSelect(m_view.m_click, "limit", 0);
			UIListener.SetControllerSelect(m_view.m_click, "gray", state ? 0 : 1);
			m_view.m_btnty.selectedIndex = state ? 0 : 1;
			if (!state) m_view.m_click.GetChild("bg").icon = null;
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

		private void SetRoleChangeInfo()
		{
			if (!data.objCfg.IsValid()) return;
			m_view.m_level.SetTextByKey(data.name);
			var role = data.objLvCfg.PartNum;
			var next = data.objNextLvCfg.IsValid() ? data.objNextLvCfg.PartNum : 0;
			if (role == 0)
			{
				role = data.objLvCfg.CustomerNum;
				next = data.objNextLvCfg.IsValid() ? data.objNextLvCfg.CustomerNum : 0;
				if (role == 0)
				{
					role = data.objLvCfg.ChefNum + data.objLvCfg.WaiterNum;
					if (data.objNextLvCfg.IsValid())
						next = data.objNextLvCfg.ChefNum + data.objNextLvCfg.WaiterNum;
				}
			}
			if (data.objLvCfg.PartNum > 0)
			{
				m_view.m_typeicon.SetIcon(data.objCfg.Icon);
				m_view.m_typeicon2.SetIcon(data.objCfg.Icon);
			}
			else if (data.objNextLvCfg.IsValid())
			{
				if (data.objNextLvCfg.ChefNum > 0) m_view.m_roleType.selectedIndex = 0;
				else if (data.objNextLvCfg.WaiterNum > 0) m_view.m_roleType.selectedIndex = 1;
				else if (data.objNextLvCfg.CustomerNum > 0) m_view.m_roleType.selectedIndex = 2;
			}
			else
			{
				if (data.objLvCfg.ChefNum > 0) m_view.m_roleType.selectedIndex = 0;
				else if (data.objLvCfg.WaiterNum > 0) m_view.m_roleType.selectedIndex = 1;
				else if (data.objLvCfg.CustomerNum > 0) m_view.m_roleType.selectedIndex = 2;
			}

			m_view.m_now.SetText(role.ToString());
			m_view.m_now1.SetText(role.ToString());
			m_view.m_next.SetText(next.ToString());
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
			if (m_view == null || data == null) return;
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
				m_view.m_click.touchable = state;
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
			if (!data.isTable)
			{
				var book = books[m_view.m_foods.selectedIndex];
				if (!book.IsEnable()) return;
			}
			switch (DataCenter.MachineUtil.CheckCanActiveMachine(info.mid))
			{
				case Error_Code.MACHINE_DEPENDS_NOT_ENABLE:
					//"@tips_unlock_fail".Tips();
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					//"@tips_unlock_item_not_enough".Tips();
					break;
				case 0:
					data.SetFood(m_view.m_foods.selectedIndex);
					DataCenter.MachineUtil.AddMachine(info.mid);
					PropertyManager.Instance.UpdateByArgs(true, data.GetUnlockPrice());
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

		void OnFoodClick()
		{
			var index = m_view.m_foods.selectedIndex;
			var book = books[index];
			UIListener.SetText(m_view, "ui_worktable_name".Local(null, book.cfg.Name.Local()), false);
			SetFoodInfo(book.id);
			if (!book.IsEnable())
				SGame.UIUtils.OpenUI("cookbookup", book);
		}

		void SetFoodInfo(int id)
		{
			UIListener.SetText(m_view.m_price, SGame.Utils.ConvertNumberStr(data.GetPrice(id, true)));
			UIListener.SetText(m_view.m_time, data.GetWorkTime(id).ToString() + "s");
		}

		void OnSetFoodItemInfo(int index, object data, GObject gObject)
		{
			var view = gObject as UI_SelectFoodItem;
			var book = data as CookBookItem;
			if (book != null && view != null)
			{
				view.SetIcon(book.cfg.Icon);
				UIListener.SetControllerSelect(gObject, "lock", book.IsEnable() ? 0 : 1);
			}
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
			else if (this.info.type == 3)
			{
				RequestExcuteSystem.UnlockArea(this.info.id);
				SGame.UIUtils.CloseUIByID(__id);
			}
			else if (this.info.type == 4)
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
