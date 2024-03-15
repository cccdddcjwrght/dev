using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using GameConfigs;
using SGame;
using UnityEngine;

partial class UIListenerExt
{
	static public void SetEquipInfo(this GObject gObject, EquipmentRowData cfg)
	{
		if (gObject != null && cfg.IsValid())
		{
			if (gObject is GComponent com)
			{
				com.SetTextByKey(cfg.Name);
				com.SetIcon(cfg.Icon);
				UIListener.SetControllerSelect(com, "quality", cfg.Quality);
				UIListener.SetTextWithName(com, "level", cfg.Level.ToString());
				UIListener.SetTextWithName(com, "qname", $"ui_quality_name_{cfg.Quality}".Local()  );

			}
		}
	}

	static public void SetEquipInfo(this GObject gObject, EquipItem equip)
	{
		if (equip == null || equip.cfgID <= 0)
			UIListener.SetControllerSelect(gObject, "eq", 0, false);
		else
		{
			UIListener.SetControllerSelect(gObject, "eq", 1, false);
			SetEquipInfo(gObject, equip.cfg);
			UIListener.SetTextWithName(gObject, "level", equip.level.ToString());
			UIListener.SetControllerSelect(gObject, "__redpoint", equip.isnew, false);
		}
	}

}