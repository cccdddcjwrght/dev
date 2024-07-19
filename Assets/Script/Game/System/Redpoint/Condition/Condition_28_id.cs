using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// 客人图鉴是否有新的
	/// </summary>
	public class Condition_28_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return BuffShopModule.Instance.CheckBuffShopIsFree();
		}
	}
}