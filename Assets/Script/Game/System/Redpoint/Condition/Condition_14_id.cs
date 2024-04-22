using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：14
	/// 宠物更新
	/// </summary>
	public class Condition_14_id : IConditonCalculator
	{
		private PetData petData
		{
			get { return DataCenter.Instance.petData; }
		}

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return petData.pets.Any(p => p.isnew == 1 || p.evo > 0);
		}
	}

}