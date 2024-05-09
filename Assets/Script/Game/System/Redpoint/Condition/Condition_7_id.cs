using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using SGame.Dining;
using SGame.Firend;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：5
	/// 新装备
	/// </summary>
	public class Condition_7_id : IConditonCalculator
	{
		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return FriendModule.Instance.RedDot();
		}
	}

}