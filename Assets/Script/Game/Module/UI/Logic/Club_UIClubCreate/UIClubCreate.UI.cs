
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubCreate
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_clubIcon, new EventCallback1(_OnClubIconClick));
			m_view.m_reset.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_reset, new EventCallback1(_OnResetClick));
			m_view.m_createBtn.m_color.onChanged.Add(new EventCallback1(_OnClubBtn_ColorChanged));
			UIListener.Listener(m_view.m_createBtn, new EventCallback1(_OnCreateBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_clubIcon, new EventCallback1(_OnClubIconClick),remove:true);
			m_view.m_reset.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_reset, new EventCallback1(_OnResetClick),remove:true);
			m_view.m_createBtn.m_color.onChanged.Remove(new EventCallback1(_OnClubBtn_ColorChanged));
			UIListener.Listener(m_view.m_createBtn, new EventCallback1(_OnCreateBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnClubIconClick(EventContext data){
			OnClubIconClick(data);
		}
		partial void OnClubIconClick(EventContext data);
		void SetClubIconText(string data)=>UIListener.SetText(m_view.m_clubIcon,data);
		string GetClubIconText()=>UIListener.GetText(m_view.m_clubIcon);
		void _OnIconBtn_ColorChanged(EventContext data){
			OnIconBtn_ColorChanged(data);
		}
		partial void OnIconBtn_ColorChanged(EventContext data);
		void SwitchIconBtn_ColorPage(int index)=>m_view.m_reset.m_color.selectedIndex=index;
		void _OnResetClick(EventContext data){
			OnResetClick(data);
		}
		partial void OnResetClick(EventContext data);
		void SetResetText(string data)=>UIListener.SetText(m_view.m_reset,data);
		string GetResetText()=>UIListener.GetText(m_view.m_reset);
		void SetInputText(string data)=>UIListener.SetText(m_view.m_input,data);
		string GetInputText()=>UIListener.GetText(m_view.m_input);
		void SetCurrencyValueText(string data)=>UIListener.SetText(m_view.m_currencyValue,data);
		string GetCurrencyValueText()=>UIListener.GetText(m_view.m_currencyValue);
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

	}
}
