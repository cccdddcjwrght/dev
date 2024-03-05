using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：2
	/// 是否有关卡科技可以购买
	/// </summary>
	public class Condition_2_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			var techs = DataCenter.Instance.roomData?.current?.roomTechs;
			if (techs?.Count > 0)
				return techs.Values.Any(t => PropertyManager.Instance.CheckCountByArgs(t.GetCostArray()));
			return false;
		}
	}

}