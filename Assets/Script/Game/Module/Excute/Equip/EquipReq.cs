using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{
	partial class RequestExcuteSystem
	{
		[InitCall]
		static void InitEquip()
		{

			DataCenter.EquipUtil.Init();

		}

		static public void EquipUpQuality(EquipItem equip, List<EquipItem> mats)
		{
			if (equip == null || mats?.Count < ConstDefine.EQUIP_UP_QUALITY_MAT_COUNT) return;
			var ms = new List<EquipItem>(mats); ms.Add(equip);
			var count = DataCenter.EquipUtil.RecycleEquips(false, false, ms.ToArray());
			equip.quality++;
			equip.level = 1;
			equip.progress = 0;
			equip.Refresh();
			DataCenter.EquipUtil.RemoveEquips(mats.ToArray());
			log.Info($"[equip] recycle mat:{count}");
		}

		static public void EquipUpLevel(EquipItem equip)
		{
			if (equip != null && !equip.IsMaxLv())
			{
				equip.level++;
				equip.progress = 0;
				equip.Refresh();
				EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));

			}
		}

		static public int EquipAddExp(EquipItem equip , int exp , out bool uplv)
		{
			uplv = false;
			if(equip!=null && !equip.IsMaxLv())
			{
				var need = equip.upLvCost - equip.progress;
				exp = Math.Min(exp, need);
				uplv = exp >= need;
				if (exp > 0)
				{
					equip.progress += exp;
					return exp;
				}
			}
			return 0;
		}

	}
}
