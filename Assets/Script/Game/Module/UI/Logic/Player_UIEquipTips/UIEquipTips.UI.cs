
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIEquipTips
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_quality.onChanged.Add(new EventCallback1(_OnQualityChanged));
			m_view.m_lvmax.onChanged.Add(new EventCallback1(_OnLvmaxChanged));
			m_view.m_hide.onChanged.Add(new EventCallback1(_OnHideChanged));
			m_view.m_progress.m_state.onChanged.Add(new EventCallback1(_Onuplevelprogress_StateChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));
			UIListener.Listener(m_view.m_up, new EventCallback1(_OnUpClick));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_click2, new EventCallback1(_OnClick2Click));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_quality.onChanged.Remove(new EventCallback1(_OnQualityChanged));
			m_view.m_lvmax.onChanged.Remove(new EventCallback1(_OnLvmaxChanged));
			m_view.m_hide.onChanged.Remove(new EventCallback1(_OnHideChanged));
			m_view.m_progress.m_state.onChanged.Remove(new EventCallback1(_Onuplevelprogress_StateChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);
			UIListener.Listener(m_view.m_up, new EventCallback1(_OnUpClick),remove:true);
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_click2, new EventCallback1(_OnClick2Click),remove:true);

		}
		void _OnQualityChanged(EventContext data){
			OnQualityChanged(data);
		}
		partial void OnQualityChanged(EventContext data);
		void SwitchQualityPage(int index)=>m_view.m_quality.selectedIndex=index;
		void _OnLvmaxChanged(EventContext data){
			OnLvmaxChanged(data);
		}
		partial void OnLvmaxChanged(EventContext data);
		void SwitchLvmaxPage(int index)=>m_view.m_lvmax.selectedIndex=index;
		void _OnHideChanged(EventContext data){
			OnHideChanged(data);
		}
		partial void OnHideChanged(EventContext data);
		void SwitchHidePage(int index)=>m_view.m_hide.selectedIndex=index;
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetAttrText(string data)=>UIListener.SetText(m_view.m_attr,data);
		string GetAttrText()=>UIListener.GetText(m_view.m_attr);
		void SetQualitytipsText(string data)=>UIListener.SetText(m_view.m_qualitytips,data);
		string GetQualitytipsText()=>UIListener.GetText(m_view.m_qualitytips);
		void SetNextlvattrText(string data)=>UIListener.SetText(m_view.m_nextlvattr,data);
		string GetNextlvattrText()=>UIListener.GetText(m_view.m_nextlvattr);
		void SetCostText(string data)=>UIListener.SetText(m_view.m_cost,data);
		string GetCostText()=>UIListener.GetText(m_view.m_cost);
		void _Onuplevelprogress_StateChanged(EventContext data){
			Onuplevelprogress_StateChanged(data);
		}
		partial void Onuplevelprogress_StateChanged(EventContext data);
		void Switchuplevelprogress_StatePage(int index)=>m_view.m_progress.m_state.selectedIndex=index;
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);
		void _OnUpClick(EventContext data){
			OnUpClick(data);
		}
		partial void OnUpClick(EventContext data);
		void SetUpText(string data)=>UIListener.SetText(m_view.m_up,data);
		string GetUpText()=>UIListener.GetText(m_view.m_up);
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetCloseText(string data)=>UIListener.SetText(m_view.m_close,data);
		string GetCloseText()=>UIListener.GetText(m_view.m_close);
		void _OnClick2Click(EventContext data){
			OnClick2Click(data);
		}
		partial void OnClick2Click(EventContext data);
		void SetClick2Text(string data)=>UIListener.SetText(m_view.m_click2,data);
		string GetClick2Text()=>UIListener.GetText(m_view.m_click2);

	}
}
