
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
	
	public partial class UIGoodReputation
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			m_view.m_markState.onChanged.Add(new EventCallback1(_OnMarkStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			m_view.m_markState.onChanged.Remove(new EventCallback1(_OnMarkStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void _OnMarkStateChanged(EventContext data){
			OnMarkStateChanged(data);
		}
		partial void OnMarkStateChanged(EventContext data);
		void SwitchMarkStatePage(int index)=>m_view.m_markState.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
		void SetInfoText(string data)=>UIListener.SetText(m_view.m_info,data);
		string GetInfoText()=>UIListener.GetText(m_view.m_info);

	}
}
