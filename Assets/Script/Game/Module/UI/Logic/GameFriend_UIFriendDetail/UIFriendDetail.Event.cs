
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
		private FirendItemData m_friend;
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
			m_view.m_btnOK.onClick.Add(OnClickHire);
			
			m_playerID = (int)context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			UpdateUIInfo();

			// 确认取消
			m_view.m_comfirmDialog.m_btnOK.onClick.Add(OnClickComfirm);
			m_view.m_comfirmDialog.m_btnCancle.onClick.Add(OnClickCancle);
		}

		void ShowComfirm(string name)
		{
			m_uiEquip.m_holder.visible = false;
			m_view.m_comfirmDialog.m_titleName.text = name;
			m_view.m_comfirm.selectedIndex = 1;
		}

		/// <summary>
		/// 确认
		/// </summary>
		void OnClickComfirm()
		{
			FriendModule.Instance.RemoveFriend(m_playerID);
			m_view.m_comfirm.selectedIndex = 0;
			SGame.UIUtils.CloseUIByID(__id);
		}

		/// <summary>
		/// 取消
		/// </summary>
		void OnClickCancle()
		{
			m_uiEquip.m_holder.visible = true;
			m_view.m_comfirm.selectedIndex = 0;
		}

		void UpdateUIInfo()
		{
			var item = FriendModule.Instance.GetFriendItem(m_playerID);
			m_friend = item;
			m_view.m_recomment.selectedIndex = item.state == (int)FIREND_STATE.RECOMMEND ? 1 : 0;
			
			m_view.m_title.text = item.name;
			log.Info("UIFriendDetail OPEN FRIEND =" + m_playerID);
			m_uiEquip.Init(null);
			m_uiEquip.SetInfo(GetRoleData(m_playerID))
				.SetEquipInfo()
				.RefreshModel();

			var scale = GlobalDesginConfig.GetFloat("friend_model_scale", 1.0f);
			var yoffset = GlobalDesginConfig.GetFloat("friend_model_yoffset", 0);
			m_uiEquip.m_holder.scale = new Vector2(scale, scale);
			m_uiEquip.m_holder.y += yoffset;
			m_view.m_btnOK.grayed = !FriendModule.Instance.CanHire(m_playerID);
		}
		

		/// <summary>
		/// 好友结构转角色结构
		/// </summary>
		/// <param name="playerID"></param>
		/// <returns></returns>
		RoleData GetRoleData(int playerID)
		{
			return FriendModule.Instance.GetRoleData(playerID);
		}
		
		partial void UnInitEvent(UIContext context){
			m_uiEquip.Uninit();
		}

		void OnClickUnFriend()
		{
			ShowComfirm(m_friend.name);
			//FriendModule.Instance.RemoveFriend(m_playerID);
			//SGame.UIUtils.CloseUIByID(__id);
		}

		void OnClickHire()
		{
			if (!FriendModule.Instance.CanHire(m_playerID))
				return;
			
			FriendModule.Instance.HireFriend(m_playerID);
			SGame.UIUtils.CloseUIByID(__id);
		}

		void OnMaskClick()
		{
			SGame.UIUtils.CloseUIByID(__id);
		}
	}
}
