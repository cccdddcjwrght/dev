using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：3
	/// 工作台
	/// </summary>
	public class Condition_3_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if(target is GameObject go)
			{
				var mono = go.GetComponent<RegionHit>();
				var region = DataCenter.MachineUtil.GetWorktable(mono.region);
				if (region != null)
				{
					if (mono.transform.Find("scene_grid"))
						return DataCenter.MachineUtil.CheckCanActiveMachine(mono.place, false) == 0;
					else
						return region.level>0 && DataCenter.MachineUtil.CheckCanUpLevel(region.id, 0) == 0;
				}
				return false;
			}

			return false;
		}
	}

}