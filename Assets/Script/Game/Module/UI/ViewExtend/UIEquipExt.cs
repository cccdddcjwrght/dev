using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using SGame;
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
			}
		}
	}

	static public void SetEquipInfo(this GObject gObject, BaseEquip equip, bool hidered = false)
	{
		if (equip == null || equip.cfgID <= 0)
			UIListener.SetControllerSelect(gObject, "eq", 0, false);
		else
		{
			UIListener.SetControllerSelect(gObject, "eq", 1, false);
			SetEquipInfo(gObject, equip.cfg, false);
			UIListener.SetTextWithName(gObject, "level", equip.level.ToString());
			if (!hidered)
				UIListener.SetControllerSelect(gObject, "__redpoint", equip.isnew, false);
			UIListener.SetControllerSelect(gObject, "quality", equip.quality, false);
			UIListener.SetTextWithName(gObject, "qname", $"ui_quality_name_{equip.quality}".Local());

		}
	}

}