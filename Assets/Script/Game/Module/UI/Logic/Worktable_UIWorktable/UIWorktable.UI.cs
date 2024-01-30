
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
			m_view.m_panel.m_type.onChanged.Add(new EventCallback1(_OnPanel_TypeChanged));
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnPanel_ClickClick));
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_panel.m_type.onChanged.Remove(new EventCallback1(_OnPanel_TypeChanged));
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnPanel_ClickClick),remove:true);
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnPanel_TypeChanged(EventContext data){
			OnPanel_TypeChanged(data);
		}
		partial void OnPanel_TypeChanged(EventContext data);
		void SwitchPanel_TypePage(int index)=>m_view.m_panel.m_type.selectedIndex=index;
		void SetPanel_LevelText(string data)=>UIListener.SetText(m_view.m_panel.m_level,data);
		string GetPanel_LevelText()=>UIListener.GetText(m_view.m_panel.m_level);
		void SetPanel_ProgressValue(float data)=>UIListener.SetValue(m_view.m_panel.m_progress,data);
		float GetPanel_ProgressValue()=>UIListener.GetValue(m_view.m_panel.m_progress);
		void SetPanel_ProgressText(string data)=>UIListener.SetText(m_view.m_panel.m_progress,data);
		string GetPanel_ProgressText()=>UIListener.GetText(m_view.m_panel.m_progress);
		void SetPanel_TimeText(string data)=>UIListener.SetText(m_view.m_panel.m_time,data);
		string GetPanel_TimeText()=>UIListener.GetText(m_view.m_panel.m_time);
		void SetPanel_PriceText(string data)=>UIListener.SetText(m_view.m_panel.m_price,data);
		string GetPanel_PriceText()=>UIListener.GetText(m_view.m_panel.m_price);
		void _OnPanel_ClickClick(EventContext data){
			OnPanel_ClickClick(data);
		}
		partial void OnPanel_ClickClick(EventContext data);
		void SetPanel_ClickText(string data)=>UIListener.SetText(m_view.m_panel.m_click,data);
		string GetPanel_ClickText()=>UIListener.GetText(m_view.m_panel.m_click);
		void _OnPanelClick(EventContext data){
			OnPanelClick(data);
		}
		partial void OnPanelClick(EventContext data);
		void SetPanelText(string data)=>UIListener.SetText(m_view.m_panel,data);
		string GetPanelText()=>UIListener.GetText(m_view.m_panel);

	}
}
