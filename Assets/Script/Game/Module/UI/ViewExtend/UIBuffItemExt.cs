﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGame;
using GameConfigs;
using FairyGUI;

partial class UIListenerExt
{
	public static void SetBuffItem(this GObject gObject, int[] cfg, int quality, bool islock = false, int type = 0, int add = 0, bool appendadd = false)
	{
		if (gObject != null && cfg != null && cfg.Length > 0 && ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
		{
			double v = cfg[1];
			gObject.SetText(buff.Describe.Local(null, appendadd ? v + $" [color=#FE725A]+{add}[/color]" : v), false);
			gObject.SetIcon(buff.Icon);
			UIListener.SetTextWithName(gObject, "add", $"+{add}%", false);
			UIListener.SetControllerSelect(gObject, "addstate", add != 0 ? 1 : 0, false);
			UIListener.SetControllerSelect(gObject, "quality", quality, false);
			UIListener.SetControllerSelect(gObject, "lock", islock ? 1 : 0, false);
			UIListener.SetControllerSelect(gObject, "type", type, false);

		}

	}
}
