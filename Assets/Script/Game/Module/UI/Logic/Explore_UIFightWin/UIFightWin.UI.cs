
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightWin
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_getBtn, new EventCallback1(_OnGetBtnClick));
			UIListener.Listener(m_view.m_adBtn, new EventCallback1(_OnAdBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_getBtn, new EventCallback1(_OnGetBtnClick),remove:true);
			UIListener.Listener(m_view.m_adBtn, new EventCallback1(_OnAdBtnClick),remove:true);

		}
		void _OnGetBtnClick(EventContext data){
			OnGetBtnClick(data);
		}
		partial void OnGetBtnClick(EventContext data);
		void SetGetBtnText(string data)=>UIListener.SetText(m_view.m_getBtn,data);
		string GetGetBtnText()=>UIListener.GetText(m_view.m_getBtn);
		void _OnAdBtnClick(EventContext data){
			OnAdBtnClick(data);
		}
		partial void OnAdBtnClick(EventContext data);
		void SetAdBtnText(string data)=>UIListener.SetText(m_view.m_adBtn,data);
		string GetAdBtnText()=>UIListener.GetText(m_view.m_adBtn);

	}
}
