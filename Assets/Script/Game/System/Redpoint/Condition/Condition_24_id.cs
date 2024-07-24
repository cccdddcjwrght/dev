using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// 任务是否可以领取奖励
	/// </summary>
	public class Condition_24_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			DataCenter.TaskMainUtil.RefreshTaskState();
			return false;
		}
	}

}