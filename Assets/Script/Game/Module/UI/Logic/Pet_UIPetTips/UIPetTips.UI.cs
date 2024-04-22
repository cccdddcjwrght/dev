
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Pet;
	
	public partial class UIPetTips
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_quality.onChanged.Add(new EventCallback1(_OnQualityChanged));
			m_view.m_lock.onChanged.Add(new EventCallback1(_OnLockChanged));
			m_view.m_step.onChanged.Add(new EventCallback1(_OnStepChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick));
			UIListener.Listener(m_view.m_free, new EventCallback1(_OnFreeClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			m_view.m_lock.onChanged.Remove(new EventCallback1(_OnLockChanged));
			m_view.m_step.onChanged.Remove(new EventCallback1(_OnStepChanged));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerIcon(m_view.m_model, new EventCallback1(_OnModelClick),remove:true);
			UIListener.Listener(m_view.m_free, new EventCallback1(_OnFreeClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnLockChanged(EventContext data){
			OnLockChanged(data);
		}
		partial void OnLockChanged(EventContext data);
		void SwitchLockPage(int index)=>m_view.m_lock.selectedIndex=index;
		void _OnStepChanged(EventContext data){
			OnStepChanged(data);
		}
		partial void OnStepChanged(EventContext data);
		void SwitchStepPage(int index)=>m_view.m_step.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnModelClick(EventContext data){
			OnModelClick(data);
		}
		partial void OnModelClick(EventContext data);
		void _OnFreeClick(EventContext data){
			OnFreeClick(data);
		}
		partial void OnFreeClick(EventContext data);
		void SetFreeText(string data)=>UIListener.SetText(m_view.m_free,data);
		string GetFreeText()=>UIListener.GetText(m_view.m_free);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);

	}
}
