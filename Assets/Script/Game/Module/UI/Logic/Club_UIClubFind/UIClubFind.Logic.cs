
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
    using System.Collections.Generic;

    public partial class UIClubFind
	{
        List<ClubData> clubList = new List<ClubData>();

		partial void InitLogic(UIContext context){
            RequestExcuteSystem.Instance.ClubListDataReq().Start();
            m_view.m_list.itemRenderer = OnClubItemRenderer;
            m_view.m_input.promptText = UIListener.Local("ui_club_tips1");
        }

        public void RefreshClubList(bool isFind = false) 
        {
            if(!isFind) clubList = DataCenter.ClubUtil.GetClubList();
            m_view.m_state.selectedIndex = 0;
            if (clubList?.Count > 0)
            {
                m_view.m_list.numItems = clubList.Count;
                m_view.m_state.selectedIndex = 1;
            }
        }

        public void OnClubItemRenderer(int index, GObject gObject) 
        {
            UI_ClubItem item = (UI_ClubItem)gObject;

            var data = clubList[index];
            item.m_clubIcon.SetData(data.icon_id, data.frame_id);
            item.m_name.SetText(data.title);
            item.m_ID.SetText("ID:" + data.id);

            if (data.member >= data.limit) item.m_count.SetText(string.Format("[color=#FA4A4A]{0}[/color]/{1}", data.member, data.limit));
            else item.m_count.SetText(string.Format("[color=#06DD6B]{0}[/color]/{1}", data.member, data.limit));

            item.m_joinBtn.onClick.Set(() =>
            {
                RequestExcuteSystem.Instance.JoinClubReq(data.id).Start();
            });
        }

        partial void OnFindBtnClick(EventContext data)
        {
            clubList = DataCenter.ClubUtil.GetFindClubList(m_view.m_input.text);
            RefreshClubList(true);
        }

        partial void OnCreateBtnClick(EventContext data)
        {
            SGame.UIUtils.OpenUI("clubcreate");
        }

        partial void OnJoinBtnClick(EventContext data)
        {
            RequestExcuteSystem.Instance.JoinClubReq(0).Start();
        }

        partial void UnInitLogic(UIContext context){

		}
	}
}
