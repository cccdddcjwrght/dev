
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	using System.Linq;

	public partial class UIPlayer
	{
		private EventHandleContainer handleContainer = new EventHandleContainer();

		partial void InitEvent(UIContext context)
		{
			handleContainer += EventManager.Instance.Reg<int>(((int)GameEvent.ROLE_EQUIP_PUTON), OnEquipPut);
			handleContainer += EventManager.Instance.Reg(((int)GameEvent.EQUIP_REFRESH), OnEquipUpdate);
			handleContainer += EventManager.Instance.Reg(((int)GameEvent.ROLE_EQUIP_CHANGE), OnPlayerEquipChange);
			handleContainer += EventManager.Instance.Reg(((int)GameEvent.ROLE_PROPERTY_REFRESH), RefreshProperty);

		}

		partial void UnInitEvent(UIContext context)
		{
			handleContainer.Close();
			handleContainer = null;
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

		private void OnEquipPut(int id)
		{
			if (id > 0)
			{
				var eq = DataCenter.Instance.equipData.equipeds.FirstOrDefault(e => e.cfgID == id);
				if (eq == null) return;
				var eqview = m_view.m_EquipPage.GetChildByPath($"eq{eq.pos}.body")?.asCom;
				if (eqview != null)
				{
					var eMgr = EffectSystem.Instance;
					eMgr.AddEffect(54, eqview);
					eMgr.AddEffect(53, m_view.m_EquipPage.m_modeleffect);

					var last = DataCenter.Instance.equipData.recordLastAttr;
					var currnt = DataCenter.EquipUtil.GetRoleEquipAddValue();
					m_view.m_EquipPage.m_upeffect.z = -100;
					if (currnt > last)
						eMgr.AddEffect(55, m_view.m_EquipPage.m_upeffect);
				}
			}
		}

	}
}
