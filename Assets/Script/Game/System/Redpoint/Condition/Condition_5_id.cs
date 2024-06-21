using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：5
	/// 新装备
	/// </summary>
	public class Condition_5_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			var items = DataCenter.Instance.equipData.items;
			var l = items.Count;
			if (DataCenter.Instance.equipData.canMerge) return true;
			for (int i = 0; i < l; i++)
			{
				if (items[i].isnew == 1) return true;
			}
			return false;
		}
	}

}