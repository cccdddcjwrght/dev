using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：4
	/// 下一关
	/// </summary>
	public class Condition_4_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.MachineUtil.CheckAllWorktableIsMaxLv();
		}
	}

}