using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;

namespace SGame.UI.Explore
{
	public static class UIExploreExt
	{

		static private Action<GObject, object> __cacheCall;

		static private void OnAddAttr(int index, object data, GObject view)
		{
			var val = data as int[];
			if (val != null)
			{
				var k = ((EnumAttribute)val[0]);
				var v = k < EnumAttribute.Dodge ? val[1].ToString() : (ConstDefine.C_PER_SCALE * val[1]).Round() + "%";
				view.SetTextByKey($"ui_fight_attr_{k.ToString().ToLower()}");
				UIListener.SetTextWithName(view, "val", v);
				__cacheCall?.Invoke(view, data);
			}
		}

		static public GObject SetFightAttrList(this GObject gObject, List<int[]> attrs, Action<GObject, object> call = null, string listName = "attrs")
		{
			if (gObject != null && !string.IsNullOrEmpty(listName))
			{
				var ls = gObject.asList ?? gObject.asCom?.GetChild(listName).asList;
				if (ls != null)
				{
					ls.RemoveChildrenToPool();

					if (attrs?.Count > 0)
					{
						__cacheCall = call;
						SGame.UIUtils.AddListItems(ls, attrs, OnAddAttr);
						__cacheCall = null;
					}
				}
			}
			return gObject;
		}
	}
}
