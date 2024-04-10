
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UITotalBoost
	{
		private EventHandleContainer m_eventContainer = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.ROOM_BUFF_RESET, TimeDoFinish);
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.ROOM_BUFF_ADD, RefreshTotalList);
		}
		partial void UnInitEvent(UIContext context){
			m_eventContainer.Close();
			m_eventContainer = null;
		}
	}
}
