
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;

	public partial class UIEquipPreview
	{
		private BaseEquip equip;
		private int quality;

		partial void InitLogic(UIContext context)
		{

			var arges = context.GetParam().Value as object[];
			equip = arges.Val<BaseEquip>(0);
			quality = arges.Val<int>(1);

			var next = equip.Clone().UpQuality(quality);

			m_view.m_equip.SetEquipInfo(equip, true);
			m_view.m_nextequip.SetEquipInfo(next, true);

			SetChangeInfo(m_view.m_level, equip.qcfg.LevelMax.ToString(), next.qcfg.LevelMax.ToString());
			SetChangeInfo(m_view.m_main, equip.GetAttrVal(true, equip.qcfg.LevelMax) + "%", next.GetAttrVal(true, equip.qcfg.LevelMax) + "%");

			var buff = DataCenter.EquipUtil.GetQualityUnlockBuff(equip.cfg, quality);
			m_view.m_attr.SetBuffItem(buff, next.qType, false , type:3, usecolor: false);

		}

		void SetChangeInfo(UI_EquipUpLabel upLabel, string current, string next)
		{
			if (upLabel != null)
			{
				upLabel.m_val.text = current;
				upLabel.m_next.text = next;
			}
		}

		partial void UnInitLogic(UIContext context)
		{


		}
	}
}
