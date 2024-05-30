using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// 食谱是否可升级
	/// </summary>
	public class Condition_25_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.Instance.cookbook.books.Any(Check);
		}

		bool Check(CookBookItem cookBook) => cookBook.CanUpLv(out _);
	}

}