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
			return Check()
				|| CustomerBookModule.Instance.HasNew
				|| DataCenter.WorkerDataUtils.Check(0);
		}

		bool Check()
		{
			var data = DataCenter.Instance.cookbook.books;
			for (int i = 0; i < data.Count; i++)
			{
				if (data[i].OnlyCheck()) return true;
			}
			return false;
		}

	}

}