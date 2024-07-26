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

		static public bool EquipUpQuality(EquipItem equip, List<EquipItem> mats, out double count, bool trigger = true)
		{
			count = 0;
			if (equip == null || (equip.qcfg.AdvanceType != 3 && mats.Count == 0)) return false;
			var eqs = mats.ToArray();
			count = DataCenter.EquipUtil.RecycleEquip(equip, false, false);
			if (equip.qcfg.AdvanceType != 3)
				count += DataCenter.EquipUtil.RecycleEquips(false, false, eqs);
			else
				PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPQUALITY_MAT, equip.qcfg.AdvanceValue, true);

			if (count > 0)
				PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPLV_MAT, count);

			equip.progress = 0;
			equip.UpQuality();
			EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EQUIP_STAGE, 1);
			EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_merge", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
			DataCenter.EquipUtil.RemoveEquips(trigger, eqs);
			log.Info($"[equip] recycle mat:{count}");
			return true;

		}

		static public bool EquipAutoUpQuality(out List<BaseEquip> equips, out double recycle)
		{
			equips = null;
			recycle = 0D;
			if (DataCenter.Instance.equipData.canAutoMerge)
			{
				var list = DataCenter.EquipUtil.GetCombineList(-DataCenter.EquipUtil.c_max_auto_merge_quality);
				if (list?.Count > 0)
				{
					equips = new List<BaseEquip>();
					foreach (var item in list)
					{
						if (item.Count > 0)
						{
							var eq = item[0];
							item.RemoveAt(0);
							if (EquipUpQuality(eq as EquipItem, item, out recycle, trigger: false))
								equips.Add(eq);
						}
					}
					DataCenter.EquipUtil.CheckCanMerge();
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					return equips.Count > 0;
				}
			}
			return false;
		}

		static public double EquipUpLevel(EquipItem equip, out bool success)
		{
			success = false;
			if (equip != null && !equip.IsMaxLv() && Utils.CheckItemCount(ConstDefine.EQUIP_UPLV_MAT, equip.upLvCost))
			{
				25.ToAudioID().PlayAudio();
				success = true;
				var cost = equip.upLvCost;
				PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPLV_MAT, equip.upLvCost, true);
				equip.level++;
				equip.progress = 0;
				equip.Refresh();
				EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
				EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.EQUIP_LEVEL, 1);
				EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_upgrade", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
				return cost;
			}
			return 0;
		}

		static public void EquipRemake(EquipItem equip)
		{
			if (equip != null && equip.cfg.IsValid() && (equip.level > 1 || equip.progress > 0))
			{
				var count = DataCenter.EquipUtil.RecycleEquip(equip, false, true);
				if (count > 0)
				{
					EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_reset", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
					equip.level = 1;
					equip.progress = 0;
					equip.Refresh();
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					Utils.ShowRewards(updatedata: false).Append(ConstDefine.EQUIP_UPLV_MAT, count, 1);
				}
			}
		}

		static public void EquipDecompose(EquipItem equip)
		{
			if (equip != null)
			{
				if (equip.pos > 0) DataCenter.EquipUtil.PutOff(equip, true);
				var count = DataCenter.EquipUtil.RecycleEquip(equip, true, false);
				EventManager.Instance.Trigger((int)GameEvent.EQUIP_BURYINGPOINT, "equipment_decompose", equip.cfgID, equip.level, equip.quality, equip.cfg.Type);
				Utils.ShowRewards(updatedata: true)
					.Append(ConstDefine.EQUIP_UPLV_MAT, count, 1, ignorezero: true)
					.Append(equip.qcfg.GetBreakRewardArray());

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
