
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


		partial void InitLogic(UIContext context)
		{

			checktime = GlobalDesginConfig.GetFloat("eqtips_longpress_check_time", 0.5f);
			interval = GlobalDesginConfig.GetFloat("eqtips_longpress_interval", 0.1f);
			mininterval = GlobalDesginConfig.GetFloat("eqtips_longpress_interval_min", 0.05f);
			maxchangeval = GlobalDesginConfig.GetInt("eqtips_max_addval", 20);

			pressHandler = new LongPressGesture(m_view.m_up) { once = false, interval = interval, trigger = checktime };
			pressHandler.onBegin.Add(OnBegin);
			pressHandler.onEnd.Add(() => pressFlag = false);


			m_view.z = -500;
			equip = (context.GetParam().Value as object[]).Val<EquipItem>(0);
			showBtn = (context.GetParam().Value as object[]).Val<bool>(1 , true);

			m_view.m_list.itemRenderer = OnSetEffect;
			m_view.m_click.visible = m_view.m_click2.visible = m_view.m_up.visible = showBtn;

			SetInfo();
			SetEffectsInfo();
		}

		partial void DoShow(UIContext context)
		{
		}

		void SetInfo()
		{
			ConfigSystem.Instance.TryGet<BuffRowData>(equip.attrID, out buff);

			m_view.SetEquipInfo(equip);
			m_view.m_qualitytips.SetTextByKey("ui_quality_name_" + equip.cfg.Quality);
			m_view.m_click.SetTextByKey(equip.pos == 0 ? "ui_equip_on" : "ui_equip_off");
			m_view.m_click2.SetText(m_view.m_click.text);

			if (m_view.m_lvmax.selectedIndex == 0)
			{
				itemcount = itemcount >= 0 ? itemcount : PropertyManager.Instance.GetGroup(1).GetNum(ConstDefine.EQUIP_UPLV_MAT);
				m_view.m_item.SetIcon(Utils.GetItemIcon(1, ConstDefine.EQUIP_UPLV_MAT));
			}
			SetSimpleInfo(true);
		}

		void SetEffectsInfo()
		{
			effects = DataCenter.EquipUtil.GetEquipEffects(equip, null, false);
			m_view.m_list.numItems = (int)effects?.Count;
		}

		void SetSimpleInfo(bool uplv = true)
		{
			m_view.m_lvmax.selectedIndex = equip.IsMaxLv() ? 1 : 0;
			if (buff.IsValid()) m_view.m_attr.SetTextByKey(buff.Describe, equip.attrVal);
			m_view.m_level.SetText(equip.level.ToString(), false);
			if (m_view.m_lvmax.selectedIndex == 0)
			{
				m_view.m_progress.value = equip.progress;
				m_view.m_progress.max = equip.upLvCost;
				m_view.m_cost.SetText("x" + Utils.ConvertNumberStr(itemcount));
				m_view.m_up.grayed = itemcount <= 0;
				if (uplv)
					m_view.m_nextlvattr.SetText("ui_equip_nextlv_tips".Local(null, equip.GetNextAttrVal()), false);

			}
			else
			{
				m_view.m_progress.value = 1;
				m_view.m_progress.max = 1;
				m_view.m_nextlvattr.SetTextByKey("ui_equip_lvmax");
			}

		}

		void OnSetEffect(int index, GObject gObject)
		{

			var cfg = effects[index];
			if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
				gObject.SetTextByKey(buff.Describe, cfg[1]);

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

		partial void OnUpClick(EventContext data)
		{
			if (itemcount <= 0)
			{
				"@ui_equip_uplv_error1".Tips();
				return;
			}

			itemcount -= RequestExcuteSystem.EquipAddExp(equip, Math.Clamp(changeVal, 1, (int)itemcount), out var state);
			PropertyManager.Instance.GetGroup(1).SetNum(ConstDefine.EQUIP_UPLV_MAT, Math.Max(0, itemcount));
			if (state)
			{
				changeVal = 1;
				needRefreshPlayUI = true;
				RequestExcuteSystem.EquipUpLevel(equip);
			}
			SetSimpleInfo(state);

		}

		void OnBegin()
		{

			void AddExp() => OnUpClick(null);

			IEnumerator Run()
			{
				var interval = this.interval;
				var need = equip.upLvCost;
				var max = 1;
				var add = changeVal = 1;
				while (pressFlag && itemcount > 0 && !equip.IsMaxLv())
				{
					AddExp();
					need = equip.upLvCost;
					max = (int)Math.Ceiling(need * Math.Max(1, maxchangeval) * 0.01f);
					yield return new WaitForSeconds(interval);
					if (interval >= mininterval)
						interval -= (interval - mininterval) * 0.1f;
					if (changeVal < max)
						changeVal = Math.Min(changeVal + (add *= 3), max);
				}
				changeVal = 1;
				SetInfo();
			}


			if (itemcount > 0)
			{
				pressFlag = true;
				Run().Start();
			}

		}

		partial void UnInitLogic(UIContext context)
		{
			if (needRefreshPlayUI)
				EventManager.Instance.Trigger(((int)GameEvent.ROLE_PROPERTY_REFRESH));
		}

	}
}
