
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubMain
	{
		public EventHandleContainer m_EventHandle = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.CLUB_MAIN_UPDATE, RefreshAll);
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.CLUB_REWARD_UPDATE, RefreshRewardList);
			m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, (t, v) => RefreshRewardList());
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
