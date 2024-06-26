using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using Microsoft.SqlServer.Server;
using SGame;
using SGame.UI;
using SGame.UI.Player;
using Unity.VisualScripting;
using UnityEngine;

partial class UIListenerExt
{
	static public void SetEquipInfo(this GObject gObject, EquipmentRowData cfg, bool setq = true)
	{
		if (gObject != null && cfg.IsValid())
		{
			if (gObject is GComponent com)
			{
				com.SetTextByKey(cfg.Name);
				com.SetIcon(cfg.Icon);
				UIListener.SetTextWithName(gObject, "level", "ui_equip_lv_format".Local(null, cfg.Level), false);

				if (setq)
				{
					UIListener.SetControllerSelect(com, "quality", cfg.Quality);
					UIListener.SetTextWithName(com, "qname", $"ui_quality_name_{cfg.Quality}".Local());
				}
				UIListener.SetControllerSelect(com, "part", cfg.Type, false);

			}
		}
	}

	static public bool SetEquipInfo(
		this GObject gObject,
		BaseEquip equip,
		bool hidered = false, int part = 0, double needcount = 0, bool showmask = false,
		string lvformat = null,
		bool hideother = true,
		int merge = 0
	)
	{
		var root = gObject;
		if (root is UI_EqPos eqPos)
		{
			gObject = eqPos.m_body;
			if (!hideother)
				eqPos.SetEquipUpState(equip);
		}
		UIListener.SetControllerSelect(gObject, "merge", 0, false);
		if (equip == null || equip.cfgID <= 0)
		{
			UIListener.SetControllerSelect(gObject, "type", 0, false);
			UIListener.SetControllerSelect(gObject, "part", part, false);
			UIListener.SetTextWithName(gObject, "count", "");
		}
		else
		{
			var issuit = equip.type < 100 && equip.level == 0;//Ì××°Í¼Ö½
			if (issuit || equip.type < 100)
			{
				lvformat = lvformat ?? "ui_equip_lv_format";
				UIListener.SetControllerSelect(gObject, "type", 1, false);
				UIListener.SetControllerSelect(gObject, "lvstate", 1, false);
				SetEquipInfo(gObject, equip.cfg, false);
				UIListener.SetTextWithName(gObject, "level", lvformat == "-1" ? null : lvformat.Local(null, equip.level, equip.qcfg.LevelMax), false);

				UIListener.SetControllerSelect(gObject, "quality", equip.qType, false);
				UIListener.SetControllerSelect(gObject, "step", equip.qStep, false);

				UIListener.SetTextWithName(gObject, "qname", $"ui_quality_name_{equip.quality}".Local());
				UIListener.SetTextWithName(gObject, "attribute", "+" + equip.GetAttrVal() + "%", false);
				UIListener.SetControllerSelect(gObject, "suitmat", issuit ? 1 : 0, false);
				UIListener.SetTextWithName(gObject, "count", equip.count == 0 ? "" : Utils.ConvertNumberStr(equip.count), false);

				if (!hidered)
					UIListener.SetControllerSelect(root, "__redpoint", (equip.isnew == 1 || equip.CheckMats()) ? 1 : 0, false);
				if (equip.qcfg.IsValid())
					UIListener.SetTextWithName(gObject, labelName: "levelpstr", "ui_level_progress".Local(null, equip.level, equip.qcfg.LevelMax), false);

			}
			else
			{
				gObject.SetIcon(equip.icon);
				UIListener.SetControllerSelect(gObject, "type", 2, false);
				UIListener.SetControllerSelect(gObject, "partstate", 0);
				if (!hidered)
					UIListener.SetControllerSelect(root, "__redpoint", equip.isnew, false);
				var c = Utils.ConvertNumberStr(equip.count);
				if (needcount <= 0)
					UIListener.SetTextWithName(gObject, "count", equip.count > 0 ? c : $"[color=#ff0000]{c}[/color]");
				else
				{
					var f = equip.count >= needcount;
					var n = Utils.ConvertNumberStr(needcount);
					var s = (equip.count >= needcount ? "ui_equip_mat_tips1" : "ui_equip_mat_tips2").Local(null, c, n);
					UIListener.SetTextWithName(gObject, "count", s, false);
					if (showmask)
						UIListener.SetControllerSelect(gObject, "mask", needcount > equip.count ? 1 : 0, false);
					return f;
				}

			}
		}
		return false;

	}

	static public void SetMerge(this GObject gObject, BaseEquip equip)
	{
		if (gObject != null)
		{
			var merge = equip.qcfg.AdvanceType;
			UIListener.SetControllerSelect(gObject, "merge", merge);
			switch (merge)
			{
				case 1:
					UIListener.SetControllerSelect(gObject, "type", 0);
					UIListener.SetControllerSelect(gObject, "quality", equip.qType, false);
					UIListener.SetControllerSelect(gObject, "step", equip.qStep, false);
					UIListener.SetControllerSelect(gObject, "state", 0);
					UIListener.SetControllerSelect(gObject, "rightbottom", 0);
					UIListener.SetControllerSelect(gObject, "partstate", 0);
					break;
				case 2:
					UIListener.SetControllerSelect(gObject, "type", 0);
					UIListener.SetControllerSelect(gObject, "quality", equip.qType, false);
					UIListener.SetControllerSelect(gObject, "step", equip.qStep, false);
					UIListener.SetControllerSelect(gObject, "state", 0);
					UIListener.SetControllerSelect(gObject, "rightbottom", 0);
					UIListener.SetControllerSelect(gObject, "partstate", 1);
					UIListener.SetControllerSelect(gObject, "part", equip.cfg.Type);

					break;
				case 4:
					UIListener.SetControllerSelect(gObject, "type", 1);
					UIListener.SetControllerSelect(gObject, "quality", equip.qType, false);
					UIListener.SetControllerSelect(gObject, "step", equip.qStep, false);
					UIListener.SetControllerSelect(gObject, "rightbottom", 1);
					UIListener.SetControllerSelect(gObject, "partstate", 0);
					break;
			}

			UIListener.SetControllerSelect(gObject, "lvstate", 0);
		}
	}

	static public void RefreshLevel(this GObject gObject, BaseEquip equip)
	{
		if (equip == null || gObject == null) return;
		UIListener.SetTextWithName(gObject, "level", equip.level.ToString());
		if (equip.qcfg.IsValid())
			UIListener.SetTextWithName(gObject, labelName: "levelpstr", "ui_level_progress".Local(null, equip.level, equip.qcfg.LevelMax));
	}

	static public void SetEquipUpState(this UI_EqPos pos, BaseEquip equip)
	{
		if (pos != null)
		{
			if (equip == null || !equip.cfg.IsValid()) pos.m_state.selectedIndex = 0;
			else if (equip.IsMaxLv()) pos.m_state.selectedIndex = 2;
			else
			{
				pos.m_state.selectedIndex = 1;
				pos.m_currency.SetIcon(Utils.GetItemIcon(1, ConstDefine.EQUIP_UPLV_MAT));
				pos.SetText(Utils.ConvertNumberStr(equip.upLvCost), false);
				pos.m_attr.SetText("+" + equip.qcfg.MainBuffAdd + "%", false);
			}
		}
	}
}


namespace SGame.UI.Player
{
	partial class UI_attrlabel
	{
		public UI_attrlabel SetInfo(BaseEquip equip, int quality = 0, bool checklock = false)
		{
			quality = (quality == 0 ? equip.quality : quality);
			if (equip.cfgID > 0 && quality >= 0)
			{
				var index = quality - 2;
				if (index >= 0)
					SetInfo(equip.GetEffects()[index], quality, checklock ? quality > equip.quality : false);
			}
			return this;
		}

		public UI_attrlabel SetInfo(int[] cfg, int quality, bool islock = false)
		{
			if (ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
			{
				m_quality.selectedIndex = quality;
				this.SetTextByKey(buff.Describe, cfg[1]);
				m_lock.selectedIndex = islock ? 1 : 0;
			}
			return this;

		}
	}
}