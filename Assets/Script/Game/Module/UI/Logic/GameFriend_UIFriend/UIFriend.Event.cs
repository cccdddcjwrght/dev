
using SGame.UI.Common;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	using SGame.Firend;
	
	public partial class UIFriend
	{
		private EventHanle m_eventUpdate;
		
		partial void InitEvent(UIContext context)
		{
			m_eventUpdate = EventManager.Instance.Reg((int)GameEvent.FRIEND_DATE_UPDATE, OnFirendUpdate);
			m_view.m_listFirends.SetVirtual();
			m_view.m_listRecomment.SetVirtual();
			m_view.m_listFirends.itemRenderer	= ItemRenderFriend;
			m_view.m_listRecomment.itemRenderer = ItemRenderRecomment;

			OnFirendUpdate();
		}
		
		partial void UnInitEvent(UIContext context){
			m_eventUpdate.Close();
			m_eventUpdate = null;
		}
		

		/// <summary>
		/// 刷新好友列表
		/// </summary>
		void OnFirendUpdate()
		{
			FriendModule.Instance.UpdateFriends();
			m_view.m_listFirends.numItems = FriendModule.Instance.GetDatas().Friends.Count;
			m_view.m_listRecomment.numItems = FriendModule.Instance.GetDatas().RecommendFriends.Count;
			//m_view.m_listFirends.RefreshVirtualList();
			//m_view.m_listRecomment.RefreshVirtualList();
		}

		/// <summary>
		/// 点击好友详情
		/// </summary>
		/// <param name="context"></param>
		void OnClickFriend(EventContext context)
		{
			var clickBtn = context.sender as UI_FriendItem;
			if (clickBtn == null)
			{
				return;
			}
			
			var player_id = (int)clickBtn.data;
			log.Info("open friend =" + player_id);
			SGame.UIUtils.OpenUI("frienddetail", new UIParam() {Value = player_id});
		}

		/// <summary>
		/// 按下雇佣键
		/// </summary>
		/// <param name="context"></param>
		void OnClickHire(EventContext context)
		{
			context.StopPropagation();
			
			var clickBtn = context.sender as GComponent;
			if (clickBtn == null)
			{
				log.Error("OnClickHire Btn Is Null");
				return;
			}
			
			var player_id = (int)clickBtn.data;
			var friendItem = FriendModule.Instance.GetFriendItem(player_id);
			if (friendItem == null)
			{
				log.Error("friend Item Is Null=" + player_id);
				return;
			}
			
			if (friendItem.state != (int)FIREND_STATE.CAN_HIRE)
			{
				log.Warn("now state=" + friendItem.state);
				return;
			}
			
			FriendModule.Instance.HireFriend(friendItem.player_id);
		}

		/// <summary>
		/// 按下邀请键
		/// </summary>
		/// <param name="context"></param>
		void OnClickYesRecommend(EventContext context)
		{
			context.StopPropagation();

			var component = context.sender as GComponent;
			if (component == null)
			{
				log.Error("OnClickYesRecommend component is null");
				return;
			}
			
			var playerId = (int)component.data;//as FirendItemData;
			FriendModule.Instance.AddFriend(playerId);
		}

		/// <summary>
		/// 按下不邀请键
		/// </summary>
		/// <param name="context"></param>
		void OnClickNoRecommend(EventContext context)
		{
			context.StopPropagation();

			var component = context.sender as GComponent;
			if (component == null)
			{
				log.Error("OnClickNoRecommend component is null");
				return;
			}
			
			var playerId = (int)component.data; //as FirendItemData;
			FriendModule.Instance.RemoveFriend(playerId);
		}

		void OnClickHiring(EventContext context)
		{
			context.StopPropagation();
		}
		
		private void ItemRenderFriend(int index, GObject item)
		{
			var view = (UI_FriendItem)item;
			var data = FriendModule.Instance.GetDatas().Friends[index]; 
			view.SetData(data);

			view.m_btnHire.data = data.player_id;
			view.m_btnHire.onClick.Set(OnClickHire);
			view.m_btnHiring.onClick.Set(OnClickHiring);

			view.data = data.player_id;
			view.onClick.Set(OnClickFriend);
		}

		private void ItemRenderRecomment(int index, GObject item)
		{
			var view = (UI_FriendItem)item;
			var data = FriendModule.Instance.GetDatas().RecommendFriends[index]; 
			view.SetData(data);

			view.m_btnYES.data = data.player_id;
			view.m_btnNO.data = data.player_id;
			view.m_btnYES.onClick.Set(OnClickYesRecommend);
			view.m_btnNO.onClick.Set(OnClickNoRecommend);
			view.m_btnHiring.onClick.Set(OnClickHiring);

			view.data = data.player_id;
			view.onClick.Set(OnClickFriend);
		}
	}
}
