using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：31
	/// 日常任务
	/// </summary>
	public class Condition_31_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.DailyTaskUtil.CheckRed();
		}
	}

}