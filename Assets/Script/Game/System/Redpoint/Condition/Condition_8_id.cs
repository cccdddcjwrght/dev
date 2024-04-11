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
	/// id：8
	/// 新装备
	/// </summary>
	public class Condition_8_id : IConditonCalculator
	{
		private Dictionary<ItemRowData, List<int[]>> suitMats;

		public bool Do(IFlatbufferObject cfg, object target, string args)
		{
			if (suitMats == null)
			{
				suitMats = new Dictionary<ItemRowData, List<int[]>>();
				if (ConfigSystem.Instance.TryGets<ItemRowData>((c) => c.Type == 6, out var cfgs))
				{
					for (int i = 0; i < cfgs.Count; i++)
					{
						var c = cfgs[i];
						if (ConfigSystem.Instance.TryGet<EquipmentRowData>(c.TypeId, out var e))
						{
							suitMats[c] = Utils.GetArrayList(
								e.GetPart1Array,
								e.GetPart2Array,
								e.GetPart3Array,
								e.GetPart4Array,
								e.GetPart5Array
							);
						}
					}

				};
			}

			if (suitMats.Count > 0)
			{
				var flag = true;
				foreach (var item in suitMats)
				{
					var ms = item.Value;
					for (int i = 0; i < ms.Count; i++)
						flag = PropertyManager.Instance.CheckCountByArgs(ms[i]) && flag;
					if (flag) return true;
				}
			}

			return false;
		}
	}

}