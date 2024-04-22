using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGame;
using GameConfigs;
using FairyGUI;

partial class UIListenerExt
{
	public static void SetBuffItem(this GObject gObject, int[] cfg, int quality, bool islock = false, int type = 0, int add = 0)
	{
		if (gObject != null && cfg != null && cfg.Length > 0 && ConfigSystem.Instance.TryGet<BuffRowData>(cfg[0], out var buff))
		{
			double v = cfg[1];
			if (add != 0) v = Utils.ToInt(v * (100 + add) * 0.01f);
			gObject.SetText(buff.Describe.Local(null, v), false);
			UIListener.SetControllerSelect(gObject, "quality", quality, false);
			UIListener.SetControllerSelect(gObject, "lock", islock ? 1 : 0, false);
			UIListener.SetControllerSelect(gObject, "type", type, false);

		}

	}
}
