
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	
	public partial class UIEnterScene
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_show.onChanged.Add(new EventCallback1(_OnShowChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_btnGO, new EventCallback1(_OnBtnGOClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_show.onChanged.Remove(new EventCallback1(_OnShowChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_btnGO, new EventCallback1(_OnBtnGOClick),remove:true);

		}
		void _OnShowChanged(EventContext data){
			OnShowChanged(data);
		}
		partial void OnShowChanged(EventContext data);
		void SwitchShowPage(int index)=>m_view.m_show.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);
		void SetTitle2Text(string data)=>UIListener.SetText(m_view.m_title2,data);
		string GetTitle2Text()=>UIListener.GetText(m_view.m_title2);
		void SetTitle3Text(string data)=>UIListener.SetText(m_view.m_title3,data);
		string GetTitle3Text()=>UIListener.GetText(m_view.m_title3);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void _OnBtnGOClick(EventContext data){
			OnBtnGOClick(data);
		}
		partial void OnBtnGOClick(EventContext data);
		void SetBtnGOText(string data)=>UIListener.SetText(m_view.m_btnGO,data);
		string GetBtnGOText()=>UIListener.GetText(m_view.m_btnGO);
		void SetTipsText(string data)=>UIListener.SetText(m_view.m_tips,data);
		string GetTipsText()=>UIListener.GetText(m_view.m_tips);

	}
}
