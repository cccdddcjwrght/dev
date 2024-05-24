
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubFind
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			m_view.m_findBtn.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_findBtn, new EventCallback1(_OnFindBtnClick));
			m_view.m_createBtn.m_color.onChanged.Add(new EventCallback1(_OnClubBtn_ColorChanged));
			UIListener.Listener(m_view.m_createBtn, new EventCallback1(_OnCreateBtnClick));
			m_view.m_joinBtn.m_color.onChanged.Add(new EventCallback1(_OnClubBtn_JoinBtn_colorChanged));
			UIListener.Listener(m_view.m_joinBtn, new EventCallback1(_OnJoinBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_findBtn.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_findBtn, new EventCallback1(_OnFindBtnClick),remove:true);
			m_view.m_createBtn.m_color.onChanged.Remove(new EventCallback1(_OnClubBtn_ColorChanged));
			UIListener.Listener(m_view.m_createBtn, new EventCallback1(_OnCreateBtnClick),remove:true);
			m_view.m_joinBtn.m_color.onChanged.Remove(new EventCallback1(_OnClubBtn_JoinBtn_colorChanged));
			UIListener.Listener(m_view.m_joinBtn, new EventCallback1(_OnJoinBtnClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetInputText(string data)=>UIListener.SetText(m_view.m_input,data);
		string GetInputText()=>UIListener.GetText(m_view.m_input);
		void _OnIconBtn_ColorChanged(EventContext data){
			OnIconBtn_ColorChanged(data);
		}
		partial void OnIconBtn_ColorChanged(EventContext data);
		void SwitchIconBtn_ColorPage(int index)=>m_view.m_findBtn.m_color.selectedIndex=index;
		void _OnFindBtnClick(EventContext data){
			OnFindBtnClick(data);
		}
		partial void OnFindBtnClick(EventContext data);
		void SetFindBtnText(string data)=>UIListener.SetText(m_view.m_findBtn,data);
		string GetFindBtnText()=>UIListener.GetText(m_view.m_findBtn);
		void _OnClubBtn_ColorChanged(EventContext data){
			OnClubBtn_ColorChanged(data);
		}
		partial void OnClubBtn_ColorChanged(EventContext data);
		void SwitchClubBtn_ColorPage(int index)=>m_view.m_createBtn.m_color.selectedIndex=index;
		void _OnCreateBtnClick(EventContext data){
			OnCreateBtnClick(data);
		}
		partial void OnCreateBtnClick(EventContext data);
		void SetCreateBtnText(string data)=>UIListener.SetText(m_view.m_createBtn,data);
		string GetCreateBtnText()=>UIListener.GetText(m_view.m_createBtn);
		void _OnClubBtn_JoinBtn_colorChanged(EventContext data){
			OnClubBtn_JoinBtn_colorChanged(data);
		}
		partial void OnClubBtn_JoinBtn_colorChanged(EventContext data);
		void SwitchClubBtn_JoinBtn_colorPage(int index)=>m_view.m_joinBtn.m_color.selectedIndex=index;
		void _OnJoinBtnClick(EventContext data){
			OnJoinBtnClick(data);
		}
		partial void OnJoinBtnClick(EventContext data);
		void SetJoinBtnText(string data)=>UIListener.SetText(m_view.m_joinBtn,data);
		string GetJoinBtnText()=>UIListener.GetText(m_view.m_joinBtn);

	}
}
