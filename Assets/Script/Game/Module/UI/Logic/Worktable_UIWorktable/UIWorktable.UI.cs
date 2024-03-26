﻿
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
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick));
			m_view.m_panel.m_type.onChanged.Add(new EventCallback1(_OnWorktablePanelTypeChanged));
			m_view.m_panel.m_pos.onChanged.Add(new EventCallback1(_OnWorktablePanelPosChanged));
			UIListener.Listener(m_view.m_panel.m_clickBtn, new EventCallback1(_OnWorktablePanelClickBtnClick));
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnWorktablePanelClickClick));
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			UIListener.ListenerClose(m_view.m_close, new EventCallback1(DoCloseUIClick),remove:true);
			m_view.m_panel.m_type.onChanged.Remove(new EventCallback1(_OnWorktablePanelTypeChanged));
			m_view.m_panel.m_pos.onChanged.Remove(new EventCallback1(_OnWorktablePanelPosChanged));
			UIListener.Listener(m_view.m_panel.m_clickBtn, new EventCallback1(_OnWorktablePanelClickBtnClick),remove:true);
			UIListener.Listener(m_view.m_panel.m_click, new EventCallback1(_OnWorktablePanelClickClick),remove:true);
			UIListener.ListenerIcon(m_view.m_panel, new EventCallback1(_OnPanelClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void _OnWorktablePanelTypeChanged(EventContext data){
			OnWorktablePanelTypeChanged(data);
		}
		partial void OnWorktablePanelTypeChanged(EventContext data);
		void SwitchWorktablePanelTypePage(int index)=>m_view.m_panel.m_type.selectedIndex=index;
		void _OnWorktablePanelPosChanged(EventContext data){
			OnWorktablePanelPosChanged(data);
		}
		partial void OnWorktablePanelPosChanged(EventContext data);
		void SwitchWorktablePanelPosPage(int index)=>m_view.m_panel.m_pos.selectedIndex=index;
		void SetWorktablePanelLevelText(string data)=>UIListener.SetText(m_view.m_panel.m_level,data);
		string GetWorktablePanelLevelText()=>UIListener.GetText(m_view.m_panel.m_level);
		void SetWorktablePanelProgressValue(float data)=>UIListener.SetValue(m_view.m_panel.m_progress,data);
		float GetWorktablePanelProgressValue()=>UIListener.GetValue(m_view.m_panel.m_progress);
		void SetWorktablePanelProgressText(string data)=>UIListener.SetText(m_view.m_panel.m_progress,data);
		string GetWorktablePanelProgressText()=>UIListener.GetText(m_view.m_panel.m_progress);
		void SetWorktablePanelRewardText(string data)=>UIListener.SetText(m_view.m_panel.m_reward,data);
		string GetWorktablePanelRewardText()=>UIListener.GetText(m_view.m_panel.m_reward);
		void SetWorktablePanelTimeText(string data)=>UIListener.SetText(m_view.m_panel.m_time,data);
		string GetWorktablePanelTimeText()=>UIListener.GetText(m_view.m_panel.m_time);
		void SetWorktablePanelPriceText(string data)=>UIListener.SetText(m_view.m_panel.m_price,data);
		string GetWorktablePanelPriceText()=>UIListener.GetText(m_view.m_panel.m_price);
		void SetWorktablePanelUnlockText(string data)=>UIListener.SetText(m_view.m_panel.m_unlock,data);
		string GetWorktablePanelUnlockText()=>UIListener.GetText(m_view.m_panel.m_unlock);
		void _OnWorktablePanelClickBtnClick(EventContext data){
			OnWorktablePanelClickBtnClick(data);
		}
		partial void OnWorktablePanelClickBtnClick(EventContext data);
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
		void SetPanelText(string data)=>UIListener.SetText(m_view.m_panel,data);
		string GetPanelText()=>UIListener.GetText(m_view.m_panel);

	}
}
