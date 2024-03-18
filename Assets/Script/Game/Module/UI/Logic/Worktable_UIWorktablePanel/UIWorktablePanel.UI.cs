
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
			UIListener.Listener(m_view.m_click, new EventCallback1(_OnClickClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_type.onChanged.Remove(new EventCallback1(_OnTypeChanged));
			m_view.m_pos.onChanged.Remove(new EventCallback1(_OnPosChanged));
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
		void SetProgressValue(float data)=>UIListener.SetValue(m_view.m_progress,data);
		float GetProgressValue()=>UIListener.GetValue(m_view.m_progress);
		void SetProgressText(string data)=>UIListener.SetText(m_view.m_progress,data);
		string GetProgressText()=>UIListener.GetText(m_view.m_progress);
		void SetRewardText(string data)=>UIListener.SetText(m_view.m_reward,data);
		string GetRewardText()=>UIListener.GetText(m_view.m_reward);
		void SetTimeText(string data)=>UIListener.SetText(m_view.m_time,data);
		string GetTimeText()=>UIListener.GetText(m_view.m_time);
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
