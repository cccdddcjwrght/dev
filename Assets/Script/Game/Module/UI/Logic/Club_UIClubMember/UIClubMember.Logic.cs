
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
    using System.Collections.Generic;

    public partial class UIClubMember
	{
		List<MemberData> memberDatas;
		partial void InitLogic(UIContext context){
			m_view.m_list.itemRenderer = OnMemberItemRenderer;
			memberDatas = DataCenter.ClubUtil.GetClubMemberList();
			m_view.m_list.numItems = memberDatas.Count;
		}

		public void OnMemberItemRenderer(int index, GObject gObject) 
		{
			UI_ClubMemberItem item = (UI_ClubMemberItem)gObject;
			var data = memberDatas[index];
			(item.m_head as Common.UI_HeadBtn).SetHeadIcon(data.icon_id, data.frame_id);
			item.m_name.SetText(data.name);
			item.m_value.SetText(data.score.ToString());

			var createId = DataCenter.ClubUtil.GetCreatePlayerId();
			item.m_isMember.selectedIndex = createId == data.player_id ? 1 : 0;

			item.m_isSelf.selectedIndex = data.player_id == DataCenter.Instance.accountData.playerID ? 0 : 1;

			var currencyId = DataCenter.ClubUtil.GetClubCurrencyId();
			item.m_currencyIcon.SetIcon(Utils.GetItemIcon(1, currencyId));
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
