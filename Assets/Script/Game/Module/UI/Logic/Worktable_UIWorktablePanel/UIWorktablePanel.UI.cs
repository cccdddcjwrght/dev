
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Worktable;
	
	public partial class UIWorktablePanel
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_type.onChanged.Add(new EventCallback1(_OnTypeChanged));
			m_view.m_pos.onChanged.Add(new EventCallback1(_OnPosChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick));
			UIListener.ListenerIcon(m_view.m_time, new EventCallback1(_OnTimeClick));
			UIListener.ListenerIcon(m_view.m_price, new EventCallback1(_OnPriceClick));
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_pos.onChanged.Remove(new EventCallback1(_OnPosChanged));
			UIListener.ListenerIcon(m_view.m_progress, new EventCallback1(_OnProgressClick),remove:true);
			UIListener.ListenerIcon(m_view.m_time, new EventCallback1(_OnTimeClick),remove:true);
			UIListener.ListenerIcon(m_view.m_price, new EventCallback1(_OnPriceClick),remove:true);
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick),remove:true);

		}
		void _OnTypeChanged(EventContext data){
			OnTypeChanged(data);
		}
		partial void OnTypeChanged(EventContext data);
		void SwitchTypePage(int index)=>m_view.m_type.selectedIndex=index;
		void _OnPosChanged(EventContext data){
			OnPosChanged(data);
		}
		partial void OnPosChanged(EventContext data);
		void SwitchPosPage(int index)=>m_view.m_pos.selectedIndex=index;
		void SetLevelText(string data)=>UIListener.SetText(m_view.m_level,data);
		string GetLevelText()=>UIListener.GetText(m_view.m_level);
		void SetTitleText(string data)=>UIListener.SetText(m_view.m_title,data);
		string GetTitleText()=>UIListener.GetText(m_view.m_title);
		void SetProgress_ProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgress_ProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void _OnProgressClick(EventContext data){
			OnProgressClick(data);
		}
		partial void OnProgressClick(EventContext data);
		void SetProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void SetRewardText(string data)=>UIListener.SetText(m_view.m_reward,data);
		string GetRewardText()=>UIListener.GetText(m_view.m_reward);
		void SetLabel2_TitleText(string data)=>UIListener.SetText(m_view.m_time.m_title,data);
		string GetLabel2_TitleText()=>UIListener.GetText(m_view.m_time.m_title);
		void _OnTimeClick(EventContext data){
			OnTimeClick(data);
		}
		partial void OnTimeClick(EventContext data);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
		void SetLabel_TitleText(string data)=>UIListener.SetText(m_view.m_price.m_title,data);
		string GetLabel_TitleText()=>UIListener.GetText(m_view.m_price.m_title);
		void _OnPriceClick(EventContext data){
			OnPriceClick(data);
		}
		partial void OnPriceClick(EventContext data);
		void SetPriceText(string data)=>UIListener.SetText(m_view.m_price,data);
		string GetPriceText()=>UIListener.GetText(m_view.m_price);
		void SetUnlockText(string data)=>UIListener.SetText(m_view.m_unlock,data);
		string GetUnlockText()=>UIListener.GetText(m_view.m_unlock);
		void _OnClickClick(EventContext data){
			OnClickClick(data);
		}
		partial void OnClickClick(EventContext data);
		void SetClickText(string data)=>UIListener.SetText(m_view.m_click,data);
		string GetClickText()=>UIListener.GetText(m_view.m_click);

	}
}
