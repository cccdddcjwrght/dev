
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Exchange;
	
	public partial class UIExchangeTask
	{
		EventHandleContainer m_EventHandle = new EventHandleContainer();
		partial void InitEvent(UIContext context){
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.TASK_UPDATE, RefreshTask);
			m_EventHandle += EventManager.Instance.Reg((int)GameEvent.TASK_BUY_GOOD, RefreshGood);
		}
		partial void UnInitEvent(UIContext context){
			m_EventHandle.Close();
			m_EventHandle = null;
		}
	}
}
