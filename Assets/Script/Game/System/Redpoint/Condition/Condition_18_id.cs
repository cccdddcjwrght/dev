using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：18
	/// 商城里面是否有免费商品购买
	/// </summary>
	public class Condition_18_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.ShopUtil.HasFree();
		}
	}

}