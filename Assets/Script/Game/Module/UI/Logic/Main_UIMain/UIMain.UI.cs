
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Main;
	
	public partial class UIMain
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_main.onChanged.Add(new EventCallback1(_OnMainChanged));
			UIListener.ListenerIcon(m_view.m_rightList, new EventCallback1(_OnRightListClick));
			UIListener.ListenerIcon(m_view.m_leftList, new EventCallback1(_OnLeftListClick));
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick));
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick));
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick));
			m_view.m_buff.m_markState.onChanged.Add(new EventCallback1(_OnBuffBtn_MarkStateChanged));
			m_view.m_buff.m_isTime.onChanged.Add(new EventCallback1(_OnBuffBtn_IsTimeChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick));
			UIListener.Listener(m_view.m_levelBtn, new EventCallback1(_OnLevelBtnClick));
			UIListener.Listener(m_view.m_taskRewardBtn, new EventCallback1(_OnTaskRewardBtnClick));
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_main.onChanged.Remove(new EventCallback1(_OnMainChanged));
			UIListener.ListenerIcon(m_view.m_rightList, new EventCallback1(_OnRightListClick),remove:true);
			UIListener.ListenerIcon(m_view.m_leftList, new EventCallback1(_OnLeftListClick),remove:true);
			UIListener.Listener(m_view.m_head, new EventCallback1(_OnHeadClick),remove:true);
			UIListener.Listener(m_view.m_Gold, new EventCallback1(_OnGoldClick),remove:true);
			UIListener.Listener(m_view.m_Diamond, new EventCallback1(_OnDiamondClick),remove:true);
			m_view.m_buff.m_markState.onChanged.Remove(new EventCallback1(_OnBuffBtn_MarkStateChanged));
			m_view.m_buff.m_isTime.onChanged.Remove(new EventCallback1(_OnBuffBtn_IsTimeChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick),remove:true);
			UIListener.Listener(m_view.m_levelBtn, new EventCallback1(_OnLevelBtnClick),remove:true);
			UIListener.Listener(m_view.m_taskRewardBtn, new EventCallback1(_OnTaskRewardBtnClick),remove:true);
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick),remove:true);

		}
		void _OnMainChanged(EventContext data){
			OnMainChanged(data);
		}
		partial void OnMainChanged(EventContext data);
		void SwitchMainPage(int index)=>m_view.m_main.selectedIndex=index;
		void _OnRightListClick(EventContext data){
			OnRightListClick(data);
		}
		partial void OnRightListClick(EventContext data);
		void _OnLeftListClick(EventContext data){
			OnLeftListClick(data);
		}
		partial void OnLeftListClick(EventContext data);
		void _OnHeadClick(EventContext data){
			OnHeadClick(data);
		}
		partial void OnHeadClick(EventContext data);
		void SetHeadText(string data)=>UIListener.SetText(m_view.m_head,data);
		string GetHeadText()=>UIListener.GetText(m_view.m_head);
		void _OnGoldClick(EventContext data){
			OnGoldClick(data);
		}
		partial void OnGoldClick(EventContext data);
		void SetGoldText(string data)=>UIListener.SetText(m_view.m_Gold,data);
		string GetGoldText()=>UIListener.GetText(m_view.m_Gold);
		void _OnDiamondClick(EventContext data){
			OnDiamondClick(data);
		}
		partial void OnDiamondClick(EventContext data);
		void SetDiamondText(string data)=>UIListener.SetText(m_view.m_Diamond,data);
		string GetDiamondText()=>UIListener.GetText(m_view.m_Diamond);
		void _OnBuffBtn_MarkStateChanged(EventContext data){
			OnBuffBtn_MarkStateChanged(data);
		}
		partial void OnBuffBtn_MarkStateChanged(EventContext data);
		void SwitchBuffBtn_MarkStatePage(int index)=>m_view.m_buff.m_markState.selectedIndex=index;
		void _OnBuffBtn_IsTimeChanged(EventContext data){
			OnBuffBtn_IsTimeChanged(data);
		}
		partial void OnBuffBtn_IsTimeChanged(EventContext data);
		void SwitchBuffBtn_IsTimePage(int index)=>m_view.m_buff.m_isTime.selectedIndex=index;
		void SetBuffBtn_TimeText(string data)=>UIListener.SetText(m_view.m_buff.m_time,data);
		string GetBuffBtn_TimeText()=>UIListener.GetText(m_view.m_buff.m_time);
		void _OnBuffClick(EventContext data){
			OnBuffClick(data);
		}
		partial void OnBuffClick(EventContext data);
		void SetBuffText(string data)=>UIListener.SetText(m_view.m_buff,data);
		string GetBuffText()=>UIListener.GetText(m_view.m_buff);
		void _OnLevelBtnClick(EventContext data){
			OnLevelBtnClick(data);
		}
		partial void OnLevelBtnClick(EventContext data);
		void SetLevelBtnText(string data)=>UIListener.SetText(m_view.m_levelBtn,data);
		string GetLevelBtnText()=>UIListener.GetText(m_view.m_levelBtn);
		void _OnTaskRewardBtnClick(EventContext data){
			OnTaskRewardBtnClick(data);
		}
		partial void OnTaskRewardBtnClick(EventContext data);
		void SetTaskRewardBtnText(string data)=>UIListener.SetText(m_view.m_taskRewardBtn,data);
		string GetTaskRewardBtnText()=>UIListener.GetText(m_view.m_taskRewardBtn);
		void _OnAdBtnClick(EventContext data){
			OnAdBtnClick(data);
		}
		partial void OnAdBtnClick(EventContext data);
		void SetAdBtnText(string data)=>UIListener.SetText(m_view.m_AdBtn,data);
		string GetAdBtnText()=>UIListener.GetText(m_view.m_AdBtn);

	}
}
