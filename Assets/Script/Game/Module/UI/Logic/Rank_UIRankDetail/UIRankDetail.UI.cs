
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
	
	public partial class UIRankDetail
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_btnClose, new EventCallback1(_OnBtnCloseClick),remove:true);

		}
		void _OnBtnCloseClick(EventContext data){
			OnBtnCloseClick(data);
		}
		partial void OnBtnCloseClick(EventContext data);
		void SetBtnCloseText(string data)=>UIListener.SetText(m_view.m_btnClose,data);
		string GetBtnCloseText()=>UIListener.GetText(m_view.m_btnClose);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);

	}
}
