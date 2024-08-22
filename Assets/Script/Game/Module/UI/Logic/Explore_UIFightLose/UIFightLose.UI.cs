
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightLose
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_confirmBtn, new EventCallback1(_OnConfirmBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_confirmBtn, new EventCallback1(_OnConfirmBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnConfirmBtnClick(EventContext data){
			OnConfirmBtnClick(data);
		}
		partial void OnConfirmBtnClick(EventContext data);
		void SetConfirmBtnText(string data)=>UIListener.SetText(m_view.m_confirmBtn,data);
		string GetConfirmBtnText()=>UIListener.GetText(m_view.m_confirmBtn);

	}
}
