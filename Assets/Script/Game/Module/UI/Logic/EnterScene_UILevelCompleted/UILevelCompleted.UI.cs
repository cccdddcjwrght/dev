
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.EnterScene;
	
	public partial class UILevelCompleted
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.Listener(m_view.m_body.m_click, new EventCallback1(_OnLevelCompletedBody_ClickClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.Listener(m_view.m_body.m_click, new EventCallback1(_OnLevelCompletedBody_ClickClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);

		}
		void _OnLevelCompletedBody_ClickClick(EventContext data){
			OnLevelCompletedBody_ClickClick(data);
		}
		partial void OnLevelCompletedBody_ClickClick(EventContext data);
		void SetLevelCompletedBody_Body_clickText(string data)=>UIListener.SetText(m_view.m_body.m_click,data);
		string GetLevelCompletedBody_Body_clickText()=>UIListener.GetText(m_view.m_body.m_click);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);

	}
}
