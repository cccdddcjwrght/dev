using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using FairyGUI;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using SGame;
using UnityEngine;

partial class UIListenerExt
{
	static public GObject SetCommonItem(this GObject gObject, string format = null, params int[] item)
	{
		if (gObject != null && item?.Length > 0)
		{
			if (item.Length >= 3)
				SetCommonItem(gObject, item.Val(1), item.Val(2), item.Val(0), format);
			else
				SetCommonItem(gObject, item.Val(0), item.Val(1), 1, format);
		}
		return gObject;
	}

	static public GObject SetCommonItem(this GObject gObject, string format = null, params double[] item)
	{
		if (gObject != null && item?.Length > 0)
		{
			if (item.Length >= 3)
				SetCommonItem(gObject, (int)item.Val(1), item.Val(2), (int)item.Val(0), format);
			else
				SetCommonItem(gObject, (int)item.Val(0), item.Val(1), 1, format);

		}
		return gObject;
	}

	static public GObject SetCommonItem(this GObject gObject, int id, double count, int type = 1, string format = null)
	{
		if (gObject != null)
		{
			var c = Utils.ConvertNumberStr(count);
			gObject.SetIcon(Utils.GetItemIcon(type, id));
			gObject.SetText(format == null ? "X" + c : string.Format(format, c), false);

		}
		return gObject;
	}


}

