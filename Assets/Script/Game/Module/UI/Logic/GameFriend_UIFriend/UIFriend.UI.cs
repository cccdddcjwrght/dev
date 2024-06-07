
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	
	public partial class UIFriend
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_empty.onChanged.Add(new EventCallback1(_OnEmptyChanged));
			m_view.m_emptyFriend.onChanged.Add(new EventCallback1(_OnEmptyFriendChanged));
			m_view.m_allEmpty.onChanged.Add(new EventCallback1(_OnAllEmptyChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_empty.onChanged.Remove(new EventCallback1(_OnEmptyChanged));
			m_view.m_emptyFriend.onChanged.Remove(new EventCallback1(_OnEmptyFriendChanged));
			m_view.m_allEmpty.onChanged.Remove(new EventCallback1(_OnAllEmptyChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnEmptyChanged(EventContext data){
			OnEmptyChanged(data);
		}
		partial void OnEmptyChanged(EventContext data);
		void SwitchEmptyPage(int index)=>m_view.m_empty.selectedIndex=index;
		void _OnEmptyFriendChanged(EventContext data){
			OnEmptyFriendChanged(data);
		}
		partial void OnEmptyFriendChanged(EventContext data);
		void SwitchEmptyFriendPage(int index)=>m_view.m_emptyFriend.selectedIndex=index;
		void _OnAllEmptyChanged(EventContext data){
			OnAllEmptyChanged(data);
		}
		partial void OnAllEmptyChanged(EventContext data);
		void SwitchAllEmptyPage(int index)=>m_view.m_allEmpty.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetTitle1Text(string data)=>UIListener.SetText(m_view.m_title1,data);
		string GetTitle1Text()=>UIListener.GetText(m_view.m_title1);
		void SetTitle2Text(string data)=>UIListener.SetText(m_view.m_title2,data);
		string GetTitle2Text()=>UIListener.GetText(m_view.m_title2);
		void SetTitleTimeText(string data)=>UIListener.SetText(m_view.m_titleTime,data);
		string GetTitleTimeText()=>UIListener.GetText(m_view.m_titleTime);
		void SetTitleCountText(string data)=>UIListener.SetText(m_view.m_titleCount,data);
		string GetTitleCountText()=>UIListener.GetText(m_view.m_titleCount);

	}
}
