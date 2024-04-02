
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	
	public partial class UIFriendDetail
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick));
			UIListener.Listener(m_view.m_btnDelete, new EventCallback1(_OnBtnDeleteClick));
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick),remove:true);
			UIListener.Listener(m_view.m_btnDelete, new EventCallback1(_OnBtnDeleteClick),remove:true);
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick),remove:true);

		}
		void _OnBtnCloseClick(EventContext data){
			OnBtnCloseClick(data);
		}
		partial void OnBtnCloseClick(EventContext data);
		void SetBtnCloseText(string data)=>UIListener.SetText(m_view.m_btnClose,data);
		string GetBtnCloseText()=>UIListener.GetText(m_view.m_btnClose);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void _OnBtnDeleteClick(EventContext data){
			OnBtnDeleteClick(data);
		}
		partial void OnBtnDeleteClick(EventContext data);
		void SetBtnDeleteText(string data)=>UIListener.SetText(m_view.m_btnDelete,data);
		string GetBtnDeleteText()=>UIListener.GetText(m_view.m_btnDelete);
		void _OnBtnOKClick(EventContext data){
			OnBtnOKClick(data);
		}
		partial void OnBtnOKClick(EventContext data);
		void SetBtnOKText(string data)=>UIListener.SetText(m_view.m_btnOK,data);
		string GetBtnOKText()=>UIListener.GetText(m_view.m_btnOK);

	}
}
