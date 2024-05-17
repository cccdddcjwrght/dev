using FlatBuffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
	public class Condition_20_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.TaskUtil.CheckHasTaskIsGet() || DataCenter.TaskUtil.CheckIsHasExchange();
		}
	}
}
