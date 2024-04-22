using System.Collections;
using System.Collections.Generic;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：13
	/// 孵化
	/// </summary>
	public class Condition_13_id : IConditonCalculator
	{
		private PetData petData
		{
			get { return DataCenter.Instance.petData; }
		}

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return DataCenter.PetUtil.EggCanBorn();
		}
	}

}