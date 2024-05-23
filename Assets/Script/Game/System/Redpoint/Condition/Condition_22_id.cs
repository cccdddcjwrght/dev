using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：22
	/// 区域
	/// </summary>
	public class Condition_22_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if(target is GameObject go)
			{
				var s = DataCenter.MachineUtil.IsAreaEnable(go.name);
				
				return !s;
			}

			return false;
		}
	}

}