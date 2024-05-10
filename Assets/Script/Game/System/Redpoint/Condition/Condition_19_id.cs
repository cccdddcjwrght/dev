using FlatBuffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
	public class Condition_19_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return RankModule.Instance.IsRedDot();
		}
    }
}
