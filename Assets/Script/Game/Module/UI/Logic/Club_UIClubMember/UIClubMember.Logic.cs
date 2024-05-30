
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
			m_view.m_list.SetVirtual();
			m_view.m_list.itemRenderer = OnMemberItemRenderer;
			RefreshMemberList();
		}

		public void RefreshMemberList() 
		{
			memberDatas = DataCenter.ClubUtil.GetClubMemberList();
			memberDatas.Sort((m1, m2) => 
			{
				int p1 = m1.player_id == DataCenter.ClubUtil.GetCreatePlayerId() ? 1 : 0;
				int p2 = m2.player_id == DataCenter.ClubUtil.GetCreatePlayerId() ? 1 : 0;
				if (p1 > p2) return -1;
				else if (p1 == p2) 
				{
					p1 = m1.player_id == DataCenter.Instance.accountData.playerID ? 1 : 0;
					p2 = m2.player_id == DataCenter.Instance.accountData.playerID ? 1 : 0;
					if (p1 > p2) return -1;
					else if (p1 == p2) 
					{
						if (m1.score > m2.score)
							return -1;
					}
				}
				return 1;
			});

			memberDatas.Find((m) =>
			{
				if (m.player_id == DataCenter.Instance.accountData.playerID)
				{
					m.name = DataCenter.Instance.accountData.playerName;
					m.score = (int)PropertyManager.Instance.GetItem(DataCenter.ClubUtil.GetClubCurrencyId()).num;
					return true;
				}
				return false;
			});

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
			if (item.m_isSelf.selectedIndex == 1)
			{
				item.onClick.Set(() => SGame.UIUtils.OpenUI("clubdetail", new UIParam() { Value = data.player_id }));
			}

			var currencyId = DataCenter.ClubUtil.GetClubCurrencyId();
			item.m_currencyIcon.SetIcon(Utils.GetItemIcon(1, currencyId));
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
