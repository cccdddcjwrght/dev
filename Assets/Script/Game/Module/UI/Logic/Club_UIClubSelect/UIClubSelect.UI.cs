
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubSelect
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_tab.onChanged.Add(new EventCallback1(_OnTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_clubIcon, new EventCallback1(_OnClubIconClick));
			UIListener.Listener(m_view.m_iconTab, new EventCallback1(_OnIconTabClick));
			UIListener.Listener(m_view.m_frameTab, new EventCallback1(_OnFrameTabClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_tab.onChanged.Remove(new EventCallback1(_OnTabChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_clubIcon, new EventCallback1(_OnClubIconClick),remove:true);
			UIListener.Listener(m_view.m_iconTab, new EventCallback1(_OnIconTabClick),remove:true);
			UIListener.Listener(m_view.m_frameTab, new EventCallback1(_OnFrameTabClick),remove:true);

		}
		void _OnTabChanged(EventContext data){
			OnTabChanged(data);
		}
		partial void OnTabChanged(EventContext data);
		void SwitchTabPage(int index)=>m_view.m_tab.selectedIndex=index;
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
		void _OnIconTabClick(EventContext data){
			OnIconTabClick(data);
		}
		partial void OnIconTabClick(EventContext data);
		void SetIconTabText(string data)=>UIListener.SetText(m_view.m_iconTab,data);
		string GetIconTabText()=>UIListener.GetText(m_view.m_iconTab);
		void _OnFrameTabClick(EventContext data){
			OnFrameTabClick(data);
		}
		partial void OnFrameTabClick(EventContext data);
		void SetFrameTabText(string data)=>UIListener.SetText(m_view.m_frameTab,data);
		string GetFrameTabText()=>UIListener.GetText(m_view.m_frameTab);

	}
}
