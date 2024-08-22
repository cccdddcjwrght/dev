using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
using Unity.Entities.UniversalDelegates;

namespace SGame.UI.Explore
{
	public static class UIExploreExt
	{

		static private Action<GObject, object> __cacheCall;
		static private FightEquip __compareEquip;

		static private void OnAddAttr(int index, object data, GObject view)
		{
			var val = data as int[];
			if (val != null)
			{
				if (val[0] > 0)
				{
					var k = ((EnumAttribute)val[0]);
					var v = k < EnumAttribute.Dodge ? val[1].ToString() : (ConstDefine.C_PER_SCALE * val[1]).Round() + "%";
					view.SetTextByKey($"ui_fight_attr_{k.ToString().ToLower()}");
					UIListener.SetTextWithName(view, "val", v);
					UIListener.SetControllerSelect(view, "uptype", 0);
					if (__compareEquip != null)
					{
						var otherval = __compareEquip.GetAttrVal(id: val[0]);
						if (otherval != val[0])
							UIListener.SetControllerSelect(view, "uptype", val[1] > otherval ? 1 : 2, false);
					}
					__cacheCall?.Invoke(view, data);
				}
				else
				{
					view.SetText(null, false);
					UIListener.SetTextWithName(view, "val", null, false);
				}
			}
		}

		static public GObject SetFightAttrList(this GObject gObject, List<int[]> attrs, Action<GObject, object> call = null, string listName = "attrs", FightEquip compare = null, int addnull = 0)
		{
			if (gObject != null && !string.IsNullOrEmpty(listName))
			{
				var ls = gObject.asList ?? gObject.asCom?.GetChild(listName).asList;
				if (ls != null)
				{
					ls.RemoveChildrenToPool();

					if (attrs?.Count > 0)
					{
						if (addnull > 0)
						{
							if (attrs.Count % 2 == 1)
								attrs.Add(new int[] { 0, 0 });
						}
						__cacheCall = call;
						__compareEquip = compare;
						SGame.UIUtils.AddListItems(ls, attrs, OnAddAttr);
						__compareEquip = default;
						__cacheCall = null;
					}
				}
			}
			return gObject;
		}

		static public GObject SetFightEquipInfo(this GObject gObject, FightEquip equip, FightEquip other = null)
		{
			if (gObject == null) return null;
			var eq = gObject?.asCom?.GetChild("eq") ?? gObject;
			UIListener.SetControllerSelect(eq, "strongstate", equip.strong, false);
			UIListener.SetTextWithName(gObject, "name", equip.name);
			eq.SetEquipInfo(equip);
			gObject.SetFightAttrList(equip.GetEffects(), compare: other);
			var upstate = 0;
			if (other != null && equip.isnew == 1)
			{
				var v = equip.power - other.power;
				if (v != 0)
				{
					upstate = v > 0 ? 1 : 2;
					UIListener.SetTextWithName(gObject, "power", (v > 0 ? "+" : "") + v);
				}
			}
			UIListener.SetControllerSelect(gObject, "new", equip.isnew, false);
			UIListener.SetControllerSelect(gObject, "upstate", upstate, false);

			return gObject;
		}

	}

	partial class UI_FightEquipTipsBody
	{
	}

}
