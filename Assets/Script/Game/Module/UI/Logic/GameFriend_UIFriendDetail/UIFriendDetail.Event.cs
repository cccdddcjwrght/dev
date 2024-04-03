
using GameConfigs;
using SGame.Firend;
using SGame.UI.Player;
using Unity.Entities;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	using System.Collections.Generic;
		
	public partial class UIFriendDetail
	{
		private int m_playerID;
		private UI_EquipPage m_uiEquip
		{
			get
			{
				return m_view.m_Equip as UI_EquipPage;
			}
		}
		partial void InitEvent(UIContext context){
			context.window.AddEventListener("OnMaskClick", OnMaskClick);
			m_view.m_btnClose.onClick.Add(OnMaskClick);
			m_view.m_btnDelete.onClick.Add(OnClickUnFriend);

			m_playerID = (int)context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			log.Info("UIFriendDetail OPEN FRIEND =" + m_playerID);
			m_uiEquip.Init(null);
			m_uiEquip.SetInfo(GetRoleData(m_playerID))
				.SetEquipInfo()
				.RefreshModel();

			var scale = GlobalDesginConfig.GetFloat("friend_model_scale", 1.0f);
			m_uiEquip.m_holder.scale = new Vector2(scale, scale);
		}

		RoleData GetRoleData(int playerID)
		{
			var item = FirendModule.Instance.GetFriendItem(playerID);

			List<BaseEquip> equips = new List<BaseEquip>();

			foreach (var e in item.equips)
			{
				BaseEquip equip = new BaseEquip()
				{
					cfgID = e.id,
					level = e.level,
					quality = e.quality,
				};
				equip.Refresh();
				equips.Add(equip);
			}

			RoleData roleData = new RoleData()
			{
				roleTypeID = item.roleID,
				isEmployee = true,
				equips = equips
			};
			return roleData;
		}
		
		partial void UnInitEvent(UIContext context){
			m_uiEquip.Uninit();
		}

		void OnClickUnFriend()
		{
			FirendModule.Instance.RemoveFriend(m_playerID);
			SGame.UIUtils.CloseUIByID(__id);
		}

		void OnMaskClick()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}
	}
}
