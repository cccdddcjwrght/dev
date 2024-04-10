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
			if (equip == null || mats.Count == 0) return;
			var eqs = mats.ToArray();
			double count = DataCenter.EquipUtil.RecycleEquip(equip, false, false);
			if (equip.qcfg.AdvanceType < 3)
				count += DataCenter.EquipUtil.RecycleEquips(false, false, eqs);
			else
				PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPQUALITY_MAT, equip.qcfg.AdvanceValue, true);

			if (count > 0)
				PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPLV_MAT, count);

			equip.quality++;
			equip.level = 1;
			equip.progress = 0;
			equip.Refresh();
			DataCenter.EquipUtil.RemoveEquips(eqs);
			log.Info($"[equip] recycle mat:{count}");

			UIUtils.OpenUI("upqualitytips", equip, count);

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

		static public void EquipRemake(EquipItem equip)
		{
			if (equip != null && equip.cfg.IsValid() && (equip.level > 1 || equip.progress > 0))
			{
				DataCenter.EquipUtil.RecycleEquip(equip, false, true);
				equip.level = 1;
				equip.progress = 0;
				equip.Refresh();
				EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
			}
		}

		static public void EquipDecompose(EquipItem equip)
		{
			if (equip != null)
			{
				if (equip.pos > 0) DataCenter.EquipUtil.PutOff(equip, true);
				PropertyManager.Instance.UpdateByArgs(false, equip.qcfg.GetBreakRewardArray());
				DataCenter.EquipUtil.RecycleEquip(equip, true, true);
			}
		}

		static public int EquipAddExp(EquipItem equip, int exp, out bool uplv)
		{
			uplv = false;
			if (equip != null && !equip.IsMaxLv())
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
