﻿
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using GameConfigs;

	public partial class UICookbookUp
	{
		private CookBookItem data;
		private int[] stars;

		#region Init

		partial void InitLogic(UIContext context)
		{
			data = context.GetParam()?.Value.To<object[]>().Val<CookBookItem>(0);
			if (data == null) { DoCloseUIClick(null); return; }
			m_view.m_stars.itemRenderer = SetStarInfo;

			SetInfo();
		}

		partial void UnInitLogic(UIContext context)
		{
			data = default;
			stars = default;
		}

		#endregion

		void SetInfo()
		{
			m_view.GetChild("icon").SetIcon(data.cfg.Icon);
			m_view.SetText(data.cfg.Name);
			m_view.m_tips.SetTextByKey(data.cfg.Description);
			m_view.m_stars.RemoveChildrenToPool();

			SetChangeInfo();
		}

		private void SetChangeInfo()
		{
			SetUpState();
			SetStar();
			SetUpChangeInfo();
		}

		private void SetUpState()
		{
			var flag = data.CanUpLv(out var scenelimit);
			var cost = data.GetCost(out _, out var currency);
			m_view.m_type.selectedIndex = data.IsMaxLv() ? 2 : flag ? 0 : 1;
			m_view.m_cost.SetText(Utils.ConvertNumberStr(cost), false);
			m_view.m_currency.selectedIndex = currency;
			if (!flag)
			{
				if (scenelimit)
				{
					ConfigSystem.Instance.TryGet(data.lvCfg.Map, out RoomRowData room);
					m_view.m_limit.SetText("ui_cookbook_condition_level".Local(null, room.Name.Local()));
				}
				else
				{
					switch (data.lvCfg.ConditionType)
					{
						case 1:
							ConfigSystem.Instance.TryGet(data.lvCfg.ConditionValue, out MachineRowData cfg);
							m_view.m_limit.SetText("ui_cookbook_condition_machine".Local(null, cfg.MachineName.Local()));
							break;
						case 2:
							m_view.m_limit.SetTextByKey("ui_cookbook_condition_area");
							break;
					}
				}
			}
		}

		private void SetStar()
		{
			stars = DataCenter.MachineUtil.CalcuStarList(data.maxStar, data.lvCfg.Star);
			if (stars != null) m_view.m_stars.numItems = stars.Length;
		}

		private void SetUpChangeInfo()
		{
			var f = data.nextLvCfg.IsValid();
			SetPropertInfo(
				m_view.m_pros.GetChildAt(0) as UI_PropertyUpdate,
				"ui_cookbook_star",
				data.lvCfg.Star,
				f ? data.nextLvCfg.Star : 0
			);

			SetPropertInfo(
				m_view.m_pros.GetChildAt(1) as UI_PropertyUpdate,
				"ui_cookbook_time",
				data.lvCfg.Time,
				f ? data.nextLvCfg.Time : 0,
				"s"
			);

			SetPropertInfo(
				m_view.m_pros.GetChildAt(2) as UI_PropertyUpdate,
				"ui_cookbook_price",
				(0.01d * data.lvCfg.Price * data.cfg.Price(2)).ToInt(),
				f ? (0.01d * data.nextLvCfg.Price * data.cfg.Price(2)).ToInt() : 0
			);
		}

		private void SetStarInfo(int index, GObject star)
		{
			UIListener.SetControllerSelect(star, "type", stars[index]);
		}

		private void SetPropertInfo(UI_PropertyUpdate view, string name, double val, double next, string ext = null)
		{
			if (view != null)
			{
				view.SetTextByKey(name);
				view.m_state.selectedIndex = next > 0 && next != val ? 0 : 1;
				if (ext != null)
				{
					view.m_val.SetText(val.ToString() + ext, false);
					view.m_next.SetText(next.ToString() + ext, false);
				}
				else
				{
					view.m_val.SetText(Utils.ConvertNumberStr(val), false);
					view.m_next.SetText(Utils.ConvertNumberStr(next), false);
				}
			}
		}

		partial void OnClickClick(EventContext data)
		{
			if (this.data.CanUpLv(out _))
			{
				DataCenter.CookbookUtils.UpLv(this.data.id);
				SetChangeInfo();
			}
		}

	}
}
