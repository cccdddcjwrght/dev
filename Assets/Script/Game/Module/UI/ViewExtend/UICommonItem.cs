using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using SGame;
using UnityEngine;

partial class UIListenerExt
{
	static public GObject SetCommonItem(this GObject gObject, string format = null, params int[] item)
	{
		if (gObject != null && item?.Length > 0)
		{
			string icon = null;
			var count = 0;
			if (item.Length >= 3)
			{
				icon = Utils.GetItemIcon(item.Val(0), item.Val(1));
				count = item.Val(2);
			}
			else
			{
				icon = Utils.GetItemIcon(1, item.Val(0));
				count = item.Val(1);
			}

			gObject.SetIcon(icon);
			gObject.SetText(format == null ? "X" + count.ToString() : string.Format(format, count), false);

		}
		return gObject;
	}
}

