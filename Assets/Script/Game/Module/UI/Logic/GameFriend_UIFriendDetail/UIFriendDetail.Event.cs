
using SGame.Firend;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	
	public partial class UIFriendDetail
	{
		private int m_playerID;
		partial void InitEvent(UIContext context){
			context.window.AddEventListener("OnMaskClick", OnMaskClick);
			m_view.m_btnClose.onClick.Add(OnMaskClick);
			m_view.m_btnDelete.onClick.Add(OnClickUnFriend);

			m_playerID = (int)context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			log.Info("UIFriendDetail OPEN FRIEND =" + m_playerID);
		}
		
		partial void UnInitEvent(UIContext context){

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
