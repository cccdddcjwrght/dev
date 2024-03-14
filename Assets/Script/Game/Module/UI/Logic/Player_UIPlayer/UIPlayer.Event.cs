
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
			EventManager.Instance.Reg(((int)GameEvent.ROLE_EQUIP_CHANGE), SetPlayEquipsInfo);

		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg(((int)GameEvent.EQUIP_REFRESH), OnEquipUpdate);
			EventManager.Instance.UnReg(((int)GameEvent.ROLE_EQUIP_CHANGE), SetPlayEquipsInfo);

		}

		partial void OnAttrbtnClick(EventContext data)
		{
		}

		void OnEquipUpdate()
		{
			SetPlayEquipsInfo();
			OnDataRefresh();
		}

		partial void OnEq1Click(EventContext data)
		{
			OnEqClick(null, DataCenter.Instance.equipData.equipeds[1]);
		}

		partial void OnEq2Click(EventContext data)
		{
			OnEqClick(null, DataCenter.Instance.equipData.equipeds[2]);
		}

		partial void OnEq3Click(EventContext data)
		{
			OnEqClick(null, DataCenter.Instance.equipData.equipeds[3]);
		}

		partial void OnEq4Click(EventContext data)
		{
			OnEqClick(null, DataCenter.Instance.equipData.equipeds[4]);
		}

		partial void OnEq5Click(EventContext data)
		{
			OnEqClick(null, DataCenter.Instance.equipData.equipeds[5]);
		}
	}
}
