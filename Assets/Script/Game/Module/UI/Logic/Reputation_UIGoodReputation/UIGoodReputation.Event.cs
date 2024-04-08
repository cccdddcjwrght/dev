
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UIGoodReputation
	{
		private EventHandleContainer m_eventContainer = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.ROOM_LIKE_ADD, OnLikeAdd);
		}

		void OnLikeAdd() 
		{
			RefreshTime();
		}

		partial void UnInitEvent(UIContext context){
			m_eventContainer.Close();
			m_eventContainer = null;
		}
	}
}
