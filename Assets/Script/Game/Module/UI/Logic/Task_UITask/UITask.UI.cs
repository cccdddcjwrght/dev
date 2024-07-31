
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Task;
	
	public partial class UITask
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			UIListener.Listener(m_view.m_btn, new EventCallback1(_OnBtnClick));

		}
		partial void UnInitUI(UIContext context){
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			UIListener.Listener(m_view.m_btn, new EventCallback1(_OnBtnClick),remove:true);

		}
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void SetDesText(string data)=>UIListener.SetText(m_view.m_des,data);
		string GetDesText()=>UIListener.GetText(m_view.m_des);
		void SetTaskProgress_ValueText(string data)=>UIListener.SetText(m_view.m_progress.m_value,data);
		string GetTaskProgress_ValueText()=>UIListener.GetText(m_view.m_progress.m_value);
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void _OnBtnClick(EventContext data){
			OnBtnClick(data);
		}
		partial void OnBtnClick(EventContext data);
		void SetBtnText(string data)=>UIListener.SetText(m_view.m_btn,data);
		string GetBtnText()=>UIListener.GetText(m_view.m_btn);

	}
}
