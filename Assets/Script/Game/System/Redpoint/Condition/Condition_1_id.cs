using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：1
	/// 商城里面是否有免费商品购买
	/// </summary>
	public class Condition_1_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, string args)
		{
			if(args.Length > 1 && int.TryParse(args.Substring(1) , out var id) && DataCenter.Instance.shopData.goodDic.TryGetValue(id , out var g))
			{
				return g.free > 0;
			}
			return false;
		}
	}

}