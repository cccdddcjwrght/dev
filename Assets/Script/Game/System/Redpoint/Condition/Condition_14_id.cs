using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatBuffers;
using GameConfigs;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// id：14
	/// 宠物更新
	/// </summary>
	public class Condition_14_id : IConditonCalculator
	{

		private PetData data { get { return DataCenter.Instance.petData; } }

		public Condition_14_id()
		{
			EventManager.Instance.Reg<int, int, int, int>(((int)GameEvent.ITEM_CHANGE_BURYINGPOINT), OnEvent);
		}

		private PetData petData
		{
			get { return DataCenter.Instance.petData; }
		}

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			return petData.pets.Any(p => p.isnew == 1 || p.evo > 0) || data.newegg.Count > 0;
		}

		private void OnEvent(int type, int id, int change, int val)
		{
			if (val >= 0 && DataCenter.PetUtil.IsEgg(id))
			{
				var f = data.newegg.Contains(id);
				if (change > 0)
				{
					if (!f)
						data.newegg.Add(id);
				}
				else if (val == 0 && f)
				{
					data.newegg.Remove(id);
				}
			}
		}

	}

}