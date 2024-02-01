
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	using Unity.Mathematics;
	using System.Collections.Generic;

	public partial class UIWorktablePanel
	{
		private SGame.Worktable data;
		private WorktableInfo info;
		private int[] stars;

		partial void InitLogic(UIContext context)
		{
			if (context.GetParam()?.Value is WorktableInfo info)
			{
				this.info = info;
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
			UIListener.SetText(m_view.m_time, data.GetWorkTime().ToString());
			m_view.m_list.itemRenderer = SetStarInfo;
			LevelRefresh();
		}

		private void LevelRefresh()
		{
			stars = DataCenter.MachineUtil.GetWorktableStarInfo(data.id);
			var lvmax = data.maxlv <= data.level;
			var state = PropertyManager.Instance.CheckCountByArgs(data.lvcfg.GetUpgradePriceArray());
			if (lvmax)
			{
				UIListener.SetControllerSelect(m_view.m_click, "limit", 1);
				UIListener.SetTextByKey(m_view.m_click, "ui_main_btn_upgrademax");
			}
			else
			{
				UIListener.SetControllerSelect(m_view.m_click, "limit", 0);
				UIListener.SetControllerSelect(m_view.m_click, "gray", state ? 0 : 1);
				UIListener.SetText(m_view.m_click, SGame.Utils.ConvertNumberStr(data.lvcfg.UpgradePrice(2)));
			}
			UIListener.SetText(m_view, data.cfg.MachineName);
			UIListener.SetText(m_view.m_price, SGame.Utils.ConvertNumberStr(data.GetPrice()));
			SetLevelText(UIListener.LocalFormat("ui_main_btn_upgradelevel", data.level));
			SetProgressValue(data.lvcfg.MachineStar == stars.Length ? 100 : DataCenter.MachineUtil.GetStarProgress(data.id));

			m_view.m_list.RemoveChildrenToPool();
			m_view.m_list.numItems = stars.Length;

		}

		private void SetStarInfo(int index, GObject star)
		{
			UIListener.SetControllerSelect(star, "type", stars[index]);
		}


		private int GetOffset(float3 pos)
		{
			var p = SGame.UIUtils.WorldPosToUI(GRoot.inst, pos);
			var left = p.x - 100 < 0;
			var right = p.x + 100 > Screen.width;

			if (left) return 1;
			else if (right) return 2;
			return 0;
		}

		private void Unlock()
		{
			switch (DataCenter.MachineUtil.CheckCanActiveMachine(info.mid))
			{
				case Error_Code.MACHINE_DEPENDS_NOT_ENABLE:
					Debug.Log("前置条件不满足，无法解锁");
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					Debug.Log("消耗道具不足");
					break;
				case 0:
					var id = info.mid;
					DataCenter.MachineUtil.AddMachine(id);
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
					break;
				case Error_Code.ITEM_NOT_ENOUGH:
					Debug.Log($"{info.id} 升级道具不足!!!");
					break;
				case 0:
					DataCenter.MachineUtil.UpdateLevel(info.id, 0);
					Debug.Log($"{info.id} : Lv -> {data.level}");
					LevelRefresh();
					ShowUplevelEffect();
					return true;
			}
			return false;
		}

		void ShowUplevelEffect()
		{
			var ls = new List<GObject>();
			if (data.addMachine > 0)
			{
				var item = m_view.m_tips.GetFromPool(null) as UI_Tips;
				UIListener.SetTextByKey(item , "tips_main_btn_upgrade_3", data.addMachine);
				ls.Add(item);
			}
			if (data.addProfit > 0)
			{
				var item = m_view.m_tips.GetFromPool(null) as UI_Tips;
				UIListener.SetTextByKey(item, "tips_main_btn_upgrade_2", data.addMachine);
				ls.Add(item);
			}
			AddItem(ls.ToArray()).Start();
		}

		System.Collections.IEnumerator AddItem(params GObject[] items)
		{
			var i = 0;
			while (i < items.Length)
			{
				m_view.m_tips.AddChild(items[i++]);
				m_view.m_tips.ScrollToView(m_view.m_tips.numItems - 1 , true);
				yield return new WaitForSeconds(0.3f);
			}
		}

		partial void OnClickClick(EventContext data)
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
}
