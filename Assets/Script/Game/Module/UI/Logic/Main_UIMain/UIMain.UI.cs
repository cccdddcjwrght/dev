
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
			m_view.m_buff.m_tipState.onChanged.Add(new EventCallback1(_OnBuffBtn_TipStateChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick));
			m_view.m_likeBtn.m_state.onChanged.Add(new EventCallback1(_OnLikeBtn_StateChanged));
			UIListener.Listener(m_view.m_likeBtn, new EventCallback1(_OnLikeBtnClick));
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick));
			UIListener.Listener(m_view.m_levelBtn, new EventCallback1(_OnLevelBtnClick));
			UIListener.Listener(m_view.m_taskRewardBtn, new EventCallback1(_OnTaskRewardBtnClick));
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick));
			UIListener.Listener(m_view.m_skillBtn, new EventCallback1(_OnSkillBtnClick));
			UIListener.Listener(m_view.m_equipBtn, new EventCallback1(_OnEquipBtnClick));
			UIListener.Listener(m_view.m_InvestBtn, new EventCallback1(_OnInvestBtnClick));

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
			m_view.m_buff.m_tipState.onChanged.Remove(new EventCallback1(_OnBuffBtn_TipStateChanged));
			UIListener.Listener(m_view.m_buff, new EventCallback1(_OnBuffClick),remove:true);
			m_view.m_likeBtn.m_state.onChanged.Remove(new EventCallback1(_OnLikeBtn_StateChanged));
			UIListener.Listener(m_view.m_likeBtn, new EventCallback1(_OnLikeBtnClick),remove:true);
			UIListener.Listener(m_view.m_totalBtn, new EventCallback1(_OnTotalBtnClick),remove:true);
			UIListener.Listener(m_view.m_levelBtn, new EventCallback1(_OnLevelBtnClick),remove:true);
			UIListener.Listener(m_view.m_taskRewardBtn, new EventCallback1(_OnTaskRewardBtnClick),remove:true);
			UIListener.Listener(m_view.m_AdBtn, new EventCallback1(_OnAdBtnClick),remove:true);
			UIListener.Listener(m_view.m_skillBtn, new EventCallback1(_OnSkillBtnClick),remove:true);
			UIListener.Listener(m_view.m_equipBtn, new EventCallback1(_OnEquipBtnClick),remove:true);
			UIListener.Listener(m_view.m_InvestBtn, new EventCallback1(_OnInvestBtnClick),remove:true);

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
		void _OnBuffBtn_TipStateChanged(EventContext data){
			OnBuffBtn_TipStateChanged(data);
		}
		partial void OnBuffBtn_TipStateChanged(EventContext data);
		void SwitchBuffBtn_TipStatePage(int index)=>m_view.m_buff.m_tipState.selectedIndex=index;
		void SetBuffBtn_TimeText(string data)=>UIListener.SetText(m_view.m_buff.m_time,data);
		string GetBuffBtn_TimeText()=>UIListener.GetText(m_view.m_buff.m_time);
		void SetBuffBtn_InfoText(string data)=>UIListener.SetText(m_view.m_buff.m_info,data);
		string GetBuffBtn_InfoText()=>UIListener.GetText(m_view.m_buff.m_info);
		void _OnBuffClick(EventContext data){
			OnBuffClick(data);
		}
		partial void OnBuffClick(EventContext data);
		void SetBuffText(string data)=>UIListener.SetText(m_view.m_buff,data);
		string GetBuffText()=>UIListener.GetText(m_view.m_buff);
		void _OnLikeBtn_StateChanged(EventContext data){
			OnLikeBtn_StateChanged(data);
		}
		partial void OnLikeBtn_StateChanged(EventContext data);
		void SwitchLikeBtn_StatePage(int index)=>m_view.m_likeBtn.m_state.selectedIndex=index;
		void SetLikeBtn_ProgressValue(float data)=>UIListener.SetValue(m_view.m_likeBtn.m_progress,data);
		float GetLikeBtn_ProgressValue()=>UIListener.GetValue(m_view.m_likeBtn.m_progress);
		void SetLikeBtn_TimeText(string data)=>UIListener.SetText(m_view.m_likeBtn.m_time,data);
		string GetLikeBtn_TimeText()=>UIListener.GetText(m_view.m_likeBtn.m_time);
		void SetLikeBtn_NumText(string data)=>UIListener.SetText(m_view.m_likeBtn.m_num,data);
		string GetLikeBtn_NumText()=>UIListener.GetText(m_view.m_likeBtn.m_num);
		void SetLikeBtn_InfoText(string data)=>UIListener.SetText(m_view.m_likeBtn.m_info,data);
		string GetLikeBtn_InfoText()=>UIListener.GetText(m_view.m_likeBtn.m_info);
		void _OnLikeBtnClick(EventContext data){
			OnLikeBtnClick(data);
		}
		partial void OnLikeBtnClick(EventContext data);
		void SetLikeBtnText(string data)=>UIListener.SetText(m_view.m_likeBtn,data);
		string GetLikeBtnText()=>UIListener.GetText(m_view.m_likeBtn);
		void SetTotalBtn_NumText(string data)=>UIListener.SetText(m_view.m_totalBtn.m_num,data);
		string GetTotalBtn_NumText()=>UIListener.GetText(m_view.m_totalBtn.m_num);
		void _OnTotalBtnClick(EventContext data){
			OnTotalBtnClick(data);
		}
		partial void OnTotalBtnClick(EventContext data);
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
		void _OnSkillBtnClick(EventContext data){
			OnSkillBtnClick(data);
		}
		partial void OnSkillBtnClick(EventContext data);
		void SetSkillBtnText(string data)=>UIListener.SetText(m_view.m_skillBtn,data);
		string GetSkillBtnText()=>UIListener.GetText(m_view.m_skillBtn);
		void _OnEquipBtnClick(EventContext data){
			OnEquipBtnClick(data);
		}
		partial void OnEquipBtnClick(EventContext data);
		void SetEquipBtnText(string data)=>UIListener.SetText(m_view.m_equipBtn,data);
		string GetEquipBtnText()=>UIListener.GetText(m_view.m_equipBtn);
		void _OnInvestBtnClick(EventContext data){
			OnInvestBtnClick(data);
		}
		partial void OnInvestBtnClick(EventContext data);

	}
}
