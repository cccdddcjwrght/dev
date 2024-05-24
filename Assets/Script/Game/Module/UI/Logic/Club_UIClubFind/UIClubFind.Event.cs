
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubFind
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.CLUB_LIST_UPDATE, ()=>RefreshClubList());
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
