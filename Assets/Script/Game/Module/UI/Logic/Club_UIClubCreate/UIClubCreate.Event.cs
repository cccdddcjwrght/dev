
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubCreate
	{
		EventHandleContainer m_EventHandle = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.CLUB_HEAD_SELECT, UpdateClubHead);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
