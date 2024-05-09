
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
	
	public partial class UIRankMain
	{
		EventHandleContainer m_EventHandle = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.RANK_UPDATE, OnUpdateRankData);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
