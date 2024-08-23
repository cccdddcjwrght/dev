using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：30
	/// 探索
	/// </summary>
	public class Condition_30_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return PropertyManager.Instance.GetItem(ConstDefine.EXPLORE_ITEM, 1).num > 0;
		}
	}

}