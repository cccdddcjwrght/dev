using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using SGame;
using SGame.UI;
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
				UIListener.SetTextWithName(com, "level", cfg.Level.ToString());

				if (setq)
				{
					UIListener.SetControllerSelect(com, "quality", cfg.Quality);
					UIListener.SetTextWithName(com, "qname", $"ui_quality_name_{cfg.Quality}".Local());
				}
				UIListener.SetControllerSelect(com, "part", cfg.Type, false);

			}
		}
	}

	static public void SetEquipInfo(this GObject gObject, BaseEquip equip, bool hidered = false, int part = 0, double needcount = 0)
	{
		if (equip == null || equip.cfgID <= 0)
		{
			UIListener.SetControllerSelect(gObject, "type", 0, false);
			UIListener.SetControllerSelect(gObject, "part", part, false);
		}
		else
		{
			if (equip.type < 100)
			{
				UIListener.SetControllerSelect(gObject, "type", 1, false);
				SetEquipInfo(gObject, equip.cfg, false);
				UIListener.SetTextWithName(gObject, "level", equip.level.ToString());
				if (!hidered) UIListener.SetControllerSelect(gObject, "__redpoint", equip.isnew, false);
				UIListener.SetControllerSelect(gObject, "quality", equip.quality, false);
				UIListener.SetTextWithName(gObject, "qname", $"ui_quality_name_{equip.quality}".Local());
				UIListener.SetTextWithName(gObject, "attribute", "+" + equip.GetAttrVal() + "%");
				if (equip.qcfg.IsValid())
					UIListener.SetTextWithName(gObject, labelName: "levelpstr", "ui_level_progress".Local(null, equip.level, equip.qcfg.LevelMax));
			}
			else
			{
				gObject.SetIcon(equip.icon);
				UIListener.SetControllerSelect(gObject, "type", 2, false);
				var c = Utils.ConvertNumberStr(equip.count);
				if (needcount <= 0)
					UIListener.SetTextWithName(gObject, "count", c);
				else
				{
					var n = Utils.ConvertNumberStr(needcount);
					var s = equip.count >= needcount ? "ui_equip_mat_tips1" : "ui_equip_mat_tips2";
					UIListener.SetTextWithName(gObject, "count", s.Local(null, c, n));
				}
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