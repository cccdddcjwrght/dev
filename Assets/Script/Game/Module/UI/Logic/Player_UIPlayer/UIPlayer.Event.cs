
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;

	public partial class UIPlayer
	{


		partial void InitEvent(UIContext context)
		{

			EventManager.Instance.Reg(((int)GameEvent.EQUIP_REFRESH), OnEquipUpdate);
			EventManager.Instance.Reg(((int)GameEvent.ROLE_EQUIP_CHANGE), OnPlayerEquipChange);
			EventManager.Instance.Reg(((int)GameEvent.ROLE_PROPERTY_REFRESH), RefreshProperty);


		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg(((int)GameEvent.EQUIP_REFRESH), OnEquipUpdate);
			EventManager.Instance.UnReg(((int)GameEvent.ROLE_EQUIP_CHANGE), OnPlayerEquipChange);
			EventManager.Instance.UnReg(((int)GameEvent.ROLE_PROPERTY_REFRESH), RefreshProperty);
		}


		void OnEquipUpdate()
		{
			if (m_view.m_eqTab.selectedIndex == 2)
				OnEquipSuitPage();
			else
			{
				m_view.m_EquipPage.SetEquipInfo();
				SetEquipList();
			}
			RefreshMergeState();
		}

		private void OnPlayerEquipChange()
		{
			SetPlayerEquipsInfo();
		}

		private void RefreshProperty()
		{
			m_view.m_EquipPage.SetEquipInfo();
		}

	}
}
