using GameConfigs;
namespace SGame.UI
{
	using UnityEngine;
	using SGame;
	using SGame.UI.Player;
    using FairyGUI;
    using SGame.Firend;

    public partial class UIClubDetail
	{
		private long m_playerID;
		private UI_EquipPage m_uiEquip
		{
			get
			{
				return m_view.m_Equip as UI_EquipPage;
			}
		}
		partial void InitEvent(UIContext context)
		{
			context.window.AddEventListener("OnMaskClick", OnMaskClick);
			m_view.m_btnClose.onClick.Add(OnMaskClick);

			m_playerID = (long)context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			UpdateUIInfo();
		}

		void UpdateUIInfo()
		{
			var item = DataCenter.ClubUtil.GetMemberData(m_playerID);

			if (FriendModule.Instance.GetFriendItem(m_playerID) == null) m_view.m_btnOK.visible = false;
			else m_view.m_btnOK.visible = true;

			m_view.m_remove.visible = DataCenter.ClubUtil.GetCreatePlayerId() == DataCenter.Instance.accountData.playerID;
			m_view.m_title.text = item.name;
			log.Info("UIClubDetail OPEN PlayerID =" + m_playerID);
			m_uiEquip.Init(null);
			m_uiEquip.SetInfo(GetRoleData(m_playerID))
				.SetEquipInfo()
				.RefreshModel();

			var scale = GlobalDesginConfig.GetFloat("friend_model_scale", 1.0f);
			var yoffset = GlobalDesginConfig.GetFloat("friend_model_yoffset", 0);
			m_uiEquip.m_holder.scale = new Vector2(scale, scale);
			m_uiEquip.m_holder.y += yoffset;
		}

		RoleData GetRoleData(long playerID)
		{
			return DataCenter.ClubUtil.GetRoleData(playerID);
		}

        partial void OnRemoveClick(EventContext data)
        {
			RequestExcuteSystem.Instance.ClubKickReq(DataCenter.ClubUtil.GetCreatePlayerId(), m_playerID);
        }

        partial void UnInitEvent(UIContext context)
		{
			m_uiEquip.Uninit();
		}


		void OnMaskClick()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}
	}
}
