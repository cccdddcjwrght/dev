
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIWorktable
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_panel.m_type.onChanged.Add(new EventCallback1(_OnWorktablePanelTypeChanged));
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnWorktablePanelClickClick));
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_panel.m_type.onChanged.Remove(new EventCallback1(_OnWorktablePanelTypeChanged));
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnWorktablePanelClickClick),remove:true);
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnWorktablePanelTypeChanged(EventContext data){
			OnWorktablePanelTypeChanged(data);
		}
		partial void OnWorktablePanelTypeChanged(EventContext data);
		void SwitchWorktablePanelTypePage(int index)=>m_view.m_panel.m_type.selectedIndex=index;
		void SetWorktablePanelLevelText(string data)=>UIListener.SetText(m_view.m_panel.m_level,data);
		string GetWorktablePanelLevelText()=>UIListener.GetText(m_view.m_panel.m_level);
		void SetWorktablePanelProgressValue(float data)=>UIListener.SetValue(m_view.m_panel.m_progress,data);
		float GetWorktablePanelProgressValue()=>UIListener.GetValue(m_view.m_panel.m_progress);
		void SetWorktablePanelProgressText(string data)=>UIListener.SetText(m_view.m_panel.m_progress,data);
		string GetWorktablePanelProgressText()=>UIListener.GetText(m_view.m_panel.m_progress);
		void SetWorktablePanelTimeText(string data)=>UIListener.SetText(m_view.m_panel.m_time,data);
		string GetWorktablePanelTimeText()=>UIListener.GetText(m_view.m_panel.m_time);
		void SetWorktablePanelPriceText(string data)=>UIListener.SetText(m_view.m_panel.m_price,data);
		string GetWorktablePanelPriceText()=>UIListener.GetText(m_view.m_panel.m_price);
		void _OnWorktablePanelClickClick(EventContext data){
			OnWorktablePanelClickClick(data);
		}
		partial void OnWorktablePanelClickClick(EventContext data);
		void SetWorktablePanelClickText(string data)=>UIListener.SetText(m_view.m_panel.m_click,data);
		string GetWorktablePanelClickText()=>UIListener.GetText(m_view.m_panel.m_click);
		void _OnPanelClick(EventContext data){
			OnPanelClick(data);
		}
		partial void OnPanelClick(EventContext data);

	}
}
