using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using GameConfigs;
using SGame.Dining;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：11
	/// 有可领取的对象
	/// </summary>
	public class Condition_11_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return GrowGiftModule.Instance.RedDot(1);
		}
	}

}