
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubDetail
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick));
			m_view.m_remove.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_remove, new EventCallback1(_OnRemoveClick));
			UIListener.Listener(m_view.m_btnOK, new EventCallback1(_OnBtnOKClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick),remove:true);
			m_view.m_remove.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_remove, new EventCallback1(_OnRemoveClick),remove:true);
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
		void _OnIconBtn_ColorChanged(EventContext data){
			OnIconBtn_ColorChanged(data);
		}
		partial void OnIconBtn_ColorChanged(EventContext data);
		void SwitchIconBtn_ColorPage(int index)=>m_view.m_remove.m_color.selectedIndex=index;
		void _OnRemoveClick(EventContext data){
			OnRemoveClick(data);
		}
		partial void OnRemoveClick(EventContext data);
		void SetRemoveText(string data)=>UIListener.SetText(m_view.m_remove,data);
		string GetRemoveText()=>UIListener.GetText(m_view.m_remove);
		void _OnBtnOKClick(EventContext data){
			OnBtnOKClick(data);
		}
		partial void OnBtnOKClick(EventContext data);
		void SetBtnOKText(string data)=>UIListener.SetText(m_view.m_btnOK,data);
		string GetBtnOKText()=>UIListener.GetText(m_view.m_btnOK);

	}
}
