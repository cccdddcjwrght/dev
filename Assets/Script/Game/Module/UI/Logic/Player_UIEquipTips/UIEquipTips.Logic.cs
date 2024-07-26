
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;
	using System.Collections.Generic;
	using System.Collections;
	using System;

	public partial class UIEquipTips
	{
		private bool showBtn;
		private EquipItem equip;
		private BuffRowData buff;
		private List<int[]> effects;

		private float checktime;
		private float interval;
		private float mininterval;
		private int maxchangeval;

		private double itemcount = -1;
		private int changeVal;

		private LongPressGesture pressHandler;
		private bool pressFlag;

		private bool needRefreshPlayUI;

		const string c_color = "[color=#0bb65c]{0}";


		partial void InitLogic(UIContext context)
		{

			checktime = GlobalDesginConfig.GetFloat("eqtips_longpress_check_time", 0.5f);
			interval = GlobalDesginConfig.GetFloat("eqtips_longpress_interval", 0.1f);
			mininterval = GlobalDesginConfig.GetFloat("eqtips_longpress_interval_min", 0.05f);
			maxchangeval = GlobalDesginConfig.GetInt("eqtips_max_addval", 20);

			pressHandler = new LongPressGesture(m_view.m_up) { once = false, interval = interval, trigger = checktime };
			pressHandler.onAction.Add(OnUpClick);
			pressHandler.onEnd.Add(() => pressFlag = false);


			m_view.z = -500;
			var eq = (context.GetParam().Value as object[]).Val<BaseEquip>(0);
			if (eq is EquipItem) equip = eq as EquipItem;
			else { equip = new EquipItem() { cfgID = eq.cfgID, level = eq.level, quality = eq.quality, progress = 0 }; equip.Refresh(); }
			showBtn = (context.GetParam().Value as object[]).Val<bool>(1, true);

			m_view.m_flag.selectedIndex = showBtn ? 0 : 1;
			SetInfo();
			SetEffectsInfo();
		}

		partial void DoShow(UIContext context)
		{
		}

		void SetInfo()
		{
			ConfigSystem.Instance.TryGet<BuffRowData>(equip.attrID, out buff);
			m_view.SetTextByKey(equip.name);
			m_view.SetEquipInfo(equip);
			m_view.GetChild("icon").SetEquipInfo(equip);
			m_view.m_qualitytips.SetTextByKey("ui_quality_name_" + equip.quality);
			m_view.m_click.SetTextByKey(equip.pos == 0 ? "ui_equip_on" : "ui_equip_off");
			m_view.m_click2.SetText(m_view.m_click.text);

			if (m_view.m_lvmax.selectedIndex == 0)
			{
				itemcount = itemcount >= 0 ? itemcount : PropertyManager.Instance.GetItem(ConstDefine.EQUIP_UPLV_MAT).num;
				m_view.m_item.SetIcon(Utils.GetItemIcon(1, ConstDefine.EQUIP_UPLV_MAT));
			}
			SetSimpleInfo(true);
		}

		void SetEffectsInfo()
		{
			m_view.m_attrs.RemoveChildrenToPool();
			var buffs = equip.GetEffects();
			if (buffs?.Count > 0)
			{
				m_view.m_attrs.scrollPane.touchEffect = buffs.Count > 5;
				SGame.UIUtils.AddListItems(m_view.m_attrs, buffs, SetEffect);
			}
		}

		void SetEffect(int index, object data, GObject gObject)
		{
			var q = DataCenter.EquipUtil.GetBuffUnlockQuality(index);
			DataCenter.EquipUtil.ConvertQuality(q, out var qt, out var qs);
			gObject.SetBuffItem(data as int[], qt, equip.quality < q, usecolor: false);
			(gObject as UI_attrlabel).m_info.onClick.Clear();
			(gObject as UI_attrlabel).m_info.onClick.Add(() => SGame.UIUtils.OpenUI("eqpreview", equip, q));
		}

		void SetSimpleInfo(bool uplv = true)
		{
			m_view.m_lvmax.selectedIndex = equip.IsMaxLv() ? 1 : 0;
			m_view.m_funcType.selectedIndex = equip.level > 1 || equip.progress > 0 ? 0 : 1;
			m_view.RefreshLevel(equip);
			if (buff.IsValid())
			{
				var v = "+" + equip.attrVal + "%";
				m_view.m_attr.SetText(v, false);
				m_view.m___attr.SetText(v, false);
			}
			if (m_view.m_lvmax.selectedIndex == 0)
			{
				m_view.m_cost.SetText("x" + Utils.ConvertNumberStr(equip.upLvCost));
				m_view.m_up.grayed = itemcount < equip.upLvCost;
				if (uplv)
					m_view.m_nextlvattr.SetText("+" + equip.GetNextAttrVal() + "%", false);

			}
			if (!showBtn)
				m_view.m_hide.selectedIndex = 1;
		}

		void OnSetEffect(int index, GObject gObject)
		{
			var q = index + 1;
			var g = gObject as UI_attrlabel;
			var cfg = effects[index];
			var lockstate = q >= equip.quality;
			g.m_quality.selectedIndex = q + 1;
			g.m_lock.selectedIndex = lockstate ? 1 : 0;
			if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
			{
				var v = cfg[1];
				gObject.SetTextByKey(buff.Describe, lockstate ? v : string.Format(c_color, v));
			}

		}

		partial void OnClickClick(EventContext data)
		{
			if (equip.pos == 0)
				DataCenter.EquipUtil.PutOn(equip);
			else
				DataCenter.EquipUtil.PutOff(equip);
			DoCloseUIClick(null);
		}

		partial void OnClick2Click(EventContext data)
		{
			OnClickClick(null);
		}

		partial void OnFuncClick(EventContext data)
		{
			var eq = equip;
			SGame.UIUtils.OpenUI("equipreset", eq, 0);
			DoCloseUIClick(null);
		}

		partial void OnUpClick(EventContext data)
		{
			if (itemcount <= 0)
			{
				"@ui_equip_uplv_error1".Tips();
				return;
			}
			needRefreshPlayUI = true;
			itemcount -= RequestExcuteSystem.EquipUpLevel(equip, out var success);

			if (success)
				EffectSystem.Instance.AddEffect(27, m_view);

			SetSimpleInfo();
			if (itemcount < equip.upLvCost)
				pressHandler?.Cancel();

		}

		partial void UnInitLogic(UIContext context)
		{
			equip = null;
			buff = default;
			effects = default;
			pressHandler?.Dispose();
			pressHandler = null;
			if (needRefreshPlayUI)
				EventManager.Instance.Trigger(((int)GameEvent.ROLE_PROPERTY_REFRESH));
		}

	}
}
