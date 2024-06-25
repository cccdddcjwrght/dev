
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Cookbook;
	using GameConfigs;
	using System.Collections.Generic;
	using SGame.UI.Common;
	using Unity.Entities;
	using System.Collections;
	using System;

	public partial class UICookbookUp
	{
		private CookBookItem data;
		private int[] stars;
		private EventHandleContainer m_events = new EventHandleContainer();

		private bool isEnable;
		private Entity effect;

		#region Init

		partial void InitLogic(UIContext context)
		{
			data = context.GetParam()?.Value.To<object[]>().Val<CookBookItem>(0);
			if (data == null) { DoCloseUIClick(null); return; }
			SetInfo();
			m_events.Add(EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnEvent));
			m_events.Add(EventManager.Instance.Reg<double, double>((int)GameEvent.PROPERTY_GOLD_CHANGE, (a, b) => SetChangeInfo()));
		}

		partial void UnInitLogic(UIContext context)
		{
			data = default;
			stars = default;
			m_events.Close();
			if (effect != default)
				EffectSystem.Instance.ReleaseEffect(effect);
		}

		#endregion

		void SetInfo()
		{
			m_view.GetChild("icon").SetIcon(data.cfg.Icon);
			m_view.SetTextByKey(data.cfg.Name);
			m_view.m_tips.SetTextByKey(data.cfg.Description);


			SetChangeInfo();
		}

		private void SetChangeInfo()
		{
			m_view.m_level.SetTextByKey("ui_common_lv1", data.level);

			isEnable = data.IsEnable();
			if (isEnable)
				m_view.m_item.SetIcon(Utils.GetItemIcon(1, data.lvCfg.Cost(1)));
			else if (data.lvCfg.ConditionType == 3)
				m_view.m_item.SetIcon(Utils.GetItemIcon(1, data.lvCfg.ConditionValue(0)));

			m_view.m_mode.selectedIndex = isEnable ? 0 : 1;
			m_view.m_click.SetTextByKey(isEnable ? "ui_cookbook_up" : "ui_cookbook_unlock");
			SetUpState();
			SetUpChangeInfo();
		}

		private void SetUpState()
		{
			var flag = data.CanUpLv(out var scenelimit, out var itemnot, isEnable);
			var cost = data.GetCost(out _, out var currency);
			var count = PropertyManager.Instance.GetItem(currency).num;
			m_view.m_type.selectedIndex = data.IsMaxLv() ? 2 : flag ? 0 : itemnot ? 3 : 1;
			m_view.m_cost.SetText(Utils.ConvertNumberStr(count) + "[color=#000000]/" + Utils.ConvertNumberStr(cost), false);
			if (!flag)
			{
				if (scenelimit)
				{
					ConfigSystem.Instance.TryGet(data.lvCfg.Map, out RoomRowData room);
					m_view.m_limit.SetText("ui_cookbook_condition_level".Local(null, room.Name.Local()));
				}
				else if (!itemnot)
				{
					switch (data.lvCfg.ConditionType)
					{
						case 1:
							ConfigSystem.Instance.TryGet(data.lvCfg.ConditionValue(0), out MachineRowData cfg);
							m_view.m_limit.SetText("ui_cookbook_condition_machine".Local(null, cfg.MachineName.Local()));
							break;
						case 2:
							m_view.m_limit.SetTextByKey("ui_cookbook_condition_area");
							break;
					}
				}
			}
		}


		private void SetUpChangeInfo(GList list = null , int fcolor = 0 , bool neednext = true)
		{
			var f = neednext && isEnable && data.nextLvCfg.IsValid();
			list = list ?? m_view.m_pros;
			SetPropertInfo(
				list.GetChildAt(0) as UI_PropertyUpdateIcon,
				"ui_cookbook_time",
				data.lvCfg.Time,
				f ? data.nextLvCfg.Time : 0,
				"s" , fcolor
			);

			SetPropertInfo(
				list.GetChildAt(1) as UI_PropertyUpdateIcon,
				"ui_cookbook_price",
				(0.01d * data.lvCfg.Price * data.cfg.Price(2)).ToInt(),
				f ? (0.01d * data.nextLvCfg.Price * data.cfg.Price(2)).ToInt() : 0,
				fcolor: fcolor
			);

			SetPropertInfo(
				list.GetChildAt(2) as UI_PropertyUpdateIcon,
				"ui_cookbook_price",
				DataCenter.MachineUtil.GetWorkPriceByItem(data.id), 0,
				fcolor:fcolor
			);
		}

		private void SetStarInfo(int index, GObject star)
		{
			UIListener.SetControllerSelect(star, "type", stars[index]);
		}

		private void SetPropertInfo(UI_PropertyUpdateIcon view, string name, double val, double next, string ext = null , int fcolor = 0)
		{
			if (view != null)
			{
				view.SetTextByKey(name);
				view.m_state.selectedIndex = next > 0 && next != val ? 0 : 1;
				view.m_fcolor.selectedIndex = fcolor;
				if (ext != null)
				{
					view.m_val.SetText(val.ToString() + ext, false);
					view.m_val2.SetText(val.ToString() + ext, false);
					view.m_next.SetText(next.ToString() + ext, false);
				}
				else
				{
					view.m_val.SetText(Utils.ConvertNumberStr(val), false);
					view.m_val2.SetText(Utils.ConvertNumberStr(val), false);
					view.m_next.SetText(Utils.ConvertNumberStr(next), false);
				}
			}
		}

		partial void OnClickClick(EventContext data)
		{

			var state = isEnable;
			if ( DataCenter.CookbookUtils.UpLv(this.data.id))
			{
				ShowGetNewBook(SetChangeInfo, state);
			}
		}

		void ShowGetNewBook(Action call = null, bool isuplv = false)
		{
			IEnumerator Run(GLoader loader)
			{
				effect = EffectSystem.Instance.AddEffect(35, loader.parent);
				yield return new WaitForSeconds(1.5f);
				if (m_view == null) yield break;
				loader.url = "ui://Cookbook/NewBook";
				var panel = loader.component as UI_NewBook;
				if (panel == null) yield break;
				panel.SetIcon(this.data.cfg.Icon);
				panel.m_uplv.selectedIndex = isuplv ? 1 : 0;
				SetUpChangeInfo(panel.m_pros , 1 , false);
				call?.Invoke();
			}

			Utils.ShowRewards(title: "@ui_cookbook_unlock_title", contentCall: (view) =>
			{
				Run(view.m_loader).Start();
			});
		}

		void OnEvent(int id, int a, int b, int c) => SetUpState();
	}
}
