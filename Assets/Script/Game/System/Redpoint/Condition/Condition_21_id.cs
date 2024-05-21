using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：21
	/// 猎宝
	/// </summary>
	public class Condition_21_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if (target is GObject gObject)
			{
				var s = args.Split('_').Last();
				if (int.TryParse(s, out var id))
				{
					var h = DataCenter.HunterUtil.GetHunter(id);
					if (h != null && !h.isClosed && h.monster!=null)
						return PropertyManager.Instance.GetItem(h.itemID).num > 0;
				}
			}
			return false;
		}
	}

}