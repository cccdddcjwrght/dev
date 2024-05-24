
//请别手动修改该文件
//该文件每次导入界面的时候会自动生成
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
	
	public partial class UIClubReward
	{
		private int __id;

		partial void InitUI(UIContext context){
			__id = context.configID;
			m_view.m_clubItem.m_state.onChanged.Add(new EventCallback1(_OnClubItem_StateChanged));
			UIListener.ListenerIcon(m_view.m_clubItem.m_clubIcon, new EventCallback1(_OnClubItem_ClubIconClick));
			m_view.m_clubItem.m_joinBtn.m_color.onChanged.Add(new EventCallback1(_OnClubBtn_ClubItejoinBtn_colorChanged));
			UIListener.Listener(m_view.m_clubItem.m_joinBtn, new EventCallback1(_OnClubItem_JoinBtnClick));
			m_view.m_clubItem.m_leaveBtn.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_ClubIteleaveBtn_colorChanged));
			UIListener.Listener(m_view.m_clubItem.m_leaveBtn, new EventCallback1(_OnClubItem_LeaveBtnClick));
			UIListener.ListenerIcon(m_view.m_clubItem, new EventCallback1(_OnClubItemClick));
			m_view.m_memberBtn.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_memberBtn, new EventCallback1(_OnMemberBtnClick));
			m_view.m_taskBtn.m_color.onChanged.Add(new EventCallback1(_OnIconBtn_TaskBtn_colorChanged));
			UIListener.Listener(m_view.m_taskBtn, new EventCallback1(_OnTaskBtnClick));

		}
		partial void UnInitUI(UIContext context){
			m_view.m_clubItem.m_state.onChanged.Remove(new EventCallback1(_OnClubItem_StateChanged));
			UIListener.ListenerIcon(m_view.m_clubItem.m_clubIcon, new EventCallback1(_OnClubItem_ClubIconClick),remove:true);
			m_view.m_clubItem.m_joinBtn.m_color.onChanged.Remove(new EventCallback1(_OnClubBtn_ClubItejoinBtn_colorChanged));
			UIListener.Listener(m_view.m_clubItem.m_joinBtn, new EventCallback1(_OnClubItem_JoinBtnClick),remove:true);
			m_view.m_clubItem.m_leaveBtn.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_ClubIteleaveBtn_colorChanged));
			UIListener.Listener(m_view.m_clubItem.m_leaveBtn, new EventCallback1(_OnClubItem_LeaveBtnClick),remove:true);
			UIListener.ListenerIcon(m_view.m_clubItem, new EventCallback1(_OnClubItemClick),remove:true);
			m_view.m_memberBtn.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_ColorChanged));
			UIListener.Listener(m_view.m_memberBtn, new EventCallback1(_OnMemberBtnClick),remove:true);
			m_view.m_taskBtn.m_color.onChanged.Remove(new EventCallback1(_OnIconBtn_TaskBtn_colorChanged));
			UIListener.Listener(m_view.m_taskBtn, new EventCallback1(_OnTaskBtnClick),remove:true);

		}
		void _OnClubItem_StateChanged(EventContext data){
			OnClubItem_StateChanged(data);
		}
		partial void OnClubItem_StateChanged(EventContext data);
		void SwitchClubItem_StatePage(int index)=>m_view.m_clubItem.m_state.selectedIndex=index;
		void _OnClubItem_ClubIconClick(EventContext data){
			OnClubItem_ClubIconClick(data);
		}
		partial void OnClubItem_ClubIconClick(EventContext data);
		void _OnClubBtn_ClubItejoinBtn_colorChanged(EventContext data){
			OnClubBtn_ClubItejoinBtn_colorChanged(data);
		}
		partial void OnClubBtn_ClubItejoinBtn_colorChanged(EventContext data);
		void SwitchClubBtn_ClubItejoinBtn_colorPage(int index)=>m_view.m_clubItem.m_joinBtn.m_color.selectedIndex=index;
		void _OnClubItem_JoinBtnClick(EventContext data){
			OnClubItem_JoinBtnClick(data);
		}
		partial void OnClubItem_JoinBtnClick(EventContext data);
		void _OnIconBtn_ClubIteleaveBtn_colorChanged(EventContext data){
			OnIconBtn_ClubIteleaveBtn_colorChanged(data);
		}
		partial void OnIconBtn_ClubIteleaveBtn_colorChanged(EventContext data);
		void SwitchIconBtn_ClubIteleaveBtn_colorPage(int index)=>m_view.m_clubItem.m_leaveBtn.m_color.selectedIndex=index;
		void _OnClubItem_LeaveBtnClick(EventContext data){
			OnClubItem_LeaveBtnClick(data);
		}
		partial void OnClubItem_LeaveBtnClick(EventContext data);
		void _OnClubItemClick(EventContext data){
			OnClubItemClick(data);
		}
		partial void OnClubItemClick(EventContext data);
		void _OnIconBtn_ColorChanged(EventContext data){
			OnIconBtn_ColorChanged(data);
		}
		partial void OnIconBtn_ColorChanged(EventContext data);
		void SwitchIconBtn_ColorPage(int index)=>m_view.m_memberBtn.m_color.selectedIndex=index;
		void _OnMemberBtnClick(EventContext data){
			OnMemberBtnClick(data);
		}
		partial void OnMemberBtnClick(EventContext data);
		void _OnIconBtn_TaskBtn_colorChanged(EventContext data){
			OnIconBtn_TaskBtn_colorChanged(data);
		}
		partial void OnIconBtn_TaskBtn_colorChanged(EventContext data);
		void SwitchIconBtn_TaskBtn_colorPage(int index)=>m_view.m_taskBtn.m_color.selectedIndex=index;
		void _OnTaskBtnClick(EventContext data){
			OnTaskBtnClick(data);
		}
		partial void OnTaskBtnClick(EventContext data);

	}
}
