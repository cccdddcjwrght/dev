
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using GameConfigs;

	public partial class UIUpQualityTip
	{
		partial void InitLogic(UIContext context)
		{

			var equip = (context.GetParam()?.Value as object[])?.Val<BaseEquip>(0);
			var recycle = (context.GetParam()?.Value as object[])?.Val<double>(1);

			if (equip == null)
			{
				DoCloseUIClick(null);
				return;
			}

			m_view.SetEquipInfo(equip, true);
			m_view.m_equip.SetEquipInfo(equip, true);
			m_view.m_addeffect.SetInfo(equip);
			m_view.m_state.selectedIndex = recycle > 0 ? 1 : 0;
			m_view.m_recycle.SetText("ui_equip_upquality_recycle".Local(null, recycle), false);
		}

		partial void UnInitLogic(UIContext context)
		{

		}
	}
}
