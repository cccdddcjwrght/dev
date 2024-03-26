
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

		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg(((int)GameEvent.EQUIP_REFRESH), OnEquipUpdate);
			EventManager.Instance.UnReg(((int)GameEvent.ROLE_EQUIP_CHANGE), OnPlayerEquipChange);

		}


		void OnEquipUpdate()
		{
			SetEquipList();
		}

		private void OnPlayerEquipChange()
		{
			SetPlayerEquipsInfo();
		}

	}
}
