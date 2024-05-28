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
			EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EQUIP_STAGE, 1);
			EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_merge", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
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
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EQUIP_LEVEL, 1);
				EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_upgrade", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
			}
		}

		static public void EquipRemake(EquipItem equip)
		{
			if (equip != null && equip.cfg.IsValid() && (equip.level > 1 || equip.progress > 0))
			{
				DataCenter.EquipUtil.RecycleEquip(equip, false, true);
				EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_reset", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
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
				EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_decompose", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
			}
		}


		static public void PutOnOrOffSuit(EquipItem suit)
		{
			if (suit.pos == 0)
				DataCenter.EquipUtil.PutOn(suit);
			else
				DataCenter.EquipUtil.PutOff(suit);
		}

		static public bool SuitCompose(EquipItem suitmat, List<int[]> mats)
		{
			if (suitmat != null && mats?.Count > 0)
			{
				if (suitmat.count > 0)
				{
					for (int i = 0; i < mats.Count; i++)
					{
						if (!PropertyManager.Instance.CheckCountByArgs(mats[i]))
						{ "@ui_suit_mat_not_enough".Tips(); return false; }
					}
					for (int i = 0; i < mats.Count; i++)
						PropertyManager.Instance.UpdateByArgs(true, mats[i]);
					PropertyManager.Instance.Update(1, suitmat.cfgID, 1, true);
					DataCenter.EquipUtil.AddEquip(suitmat.cfg.Id, 1, cfg: suitmat.cfg);
					return true;
				}
			}
			return false;
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
