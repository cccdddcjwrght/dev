
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.PiggyBank;
	
	public partial class UIPiggyBank
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_state.onChanged.Add(new EventCallback1(_OnStateChanged));
			m_view.m_stage.onChanged.Add(new EventCallback1(_OnStageChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick));
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick));
			UIListener.Listener(m_view.m_tipBtn, new EventCallback1(_OnTipBtnClick));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			UIListener.Listener(m_view.m_buyBtn, new EventCallback1(_OnBuyBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_state.onChanged.Remove(new EventCallback1(_OnStateChanged));
			m_view.m_stage.onChanged.Remove(new EventCallback1(_OnStageChanged));
			UIListener.ListenerClose(m_view.m_mask, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.ListenerClose(m_view.m_body, new EventCallback1(DoCloseUIClick),remove:true);
			UIListener.Listener(m_view.m_tipBtn, new EventCallback1(_OnTipBtnClick),remove:true);
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			UIListener.Listener(m_view.m_buyBtn, new EventCallback1(_OnBuyBtnClick),remove:true);

		}
		void _OnStateChanged(EventContext data){
			OnStateChanged(data);
		}
		partial void OnStateChanged(EventContext data);
		void SwitchStatePage(int index)=>m_view.m_state.selectedIndex=index;
		void _OnStageChanged(EventContext data){
			OnStageChanged(data);
		}
		partial void OnStageChanged(EventContext data);
		void SwitchStagePage(int index)=>m_view.m_stage.selectedIndex=index;
		void DoCloseUIClick(EventContext data){
			 bool __closestate = true;
			 OnUICloseClick(ref __closestate);
			 if(__closestate)SGame.UIUtils.CloseUIByID(__id);
			 
		}
		partial void OnUICloseClick(ref bool state);
		void SetBodyText(string data)=>UIListener.SetText(m_view.m_body,data);
		string GetBodyText()=>UIListener.GetText(m_view.m_body);
		void _OnTipBtnClick(EventContext data){
			OnTipBtnClick(data);
		}
		partial void OnTipBtnClick(EventContext data);
		void SetTipBtnText(string data)=>UIListener.SetText(m_view.m_tipBtn,data);
		string GetTipBtnText()=>UIListener.GetText(m_view.m_tipBtn);
		void SetTipText(string data)=>UIListener.SetText(m_view.m_tip,data);
		string GetTipText()=>UIListener.GetText(m_view.m_tip);
		void SetPiggyBankProgress_MidValueText(string data)=>UIListener.SetText(m_view.m_progress.m_midValue,data);
		string GetPiggyBankProgress_MidValueText()=>UIListener.GetText(m_view.m_progress.m_midValue);
		void SetPiggyBankProgress_MaxValueText(string data)=>UIListener.SetText(m_view.m_progress.m_maxValue,data);
		string GetPiggyBankProgress_MaxValueText()=>UIListener.GetText(m_view.m_progress.m_maxValue);
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void _OnBuyBtnClick(EventContext data){
			OnBuyBtnClick(data);
		}
		partial void OnBuyBtnClick(EventContext data);
		void SetBuyBtnText(string data)=>UIListener.SetText(m_view.m_buyBtn,data);
		string GetBuyBtnText()=>UIListener.GetText(m_view.m_buyBtn);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);

	}
}
