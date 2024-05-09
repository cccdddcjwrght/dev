
using SGame.UI.Common;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GameFriend;
	using SGame.Firend;
	
	public partial class UIFriend
	{
		private EventHandleContainer m_eventContainer = new EventHandleContainer();
		
		partial void InitEvent(UIContext context)
		{
			m_eventContainer  += EventManager.Instance.Reg((int)GameEvent.FRIEND_DATE_UPDATE, OnFirendUpdate); // 好友数据更新
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.CROSS_DAY, OnFirendUpdate);		   // 跨天更新
			m_view.m_listFirends.SetVirtual();
			m_view.m_listRecomment.SetVirtual();
			m_view.m_listFirends.itemRenderer	= ItemRenderFriend;
			m_view.m_listRecomment.itemRenderer = ItemRenderRecomment;

			FriendModule.Instance.RefreshRecommend();
			OnFirendUpdate();
		}
		
		partial void UnInitEvent(UIContext context){
			m_eventContainer.Close();
			m_eventContainer = null;
		}

		void UpdateFrame(UIContext context)
		{
			// 跨天刷新
			var datas = FriendModule.Instance.GetDatas();
			var currentTime = GameServerTime.Instance.serverTime;
			if (datas.nextHireTime != 0 && currentTime >= datas.nextHireTime)
			{
				// 跨天就更新界面
				OnFirendUpdate();
			}
		}
		
		/// <summary>
		/// 刷新好友列表
		/// </summary>
		void OnFirendUpdate()
		{
			FriendModule.Instance.UpdateFriends();
			var friends = FriendModule.Instance.GetDatas();
			
			m_view.m_friendGroup.relations.ClearAll();
			if (friends.RecommendFriends.Count > 0)
			{
				m_view.m_empty.selectedIndex = 0;
				m_view.m_friendGroup.relations.Add(m_view.m_listRecomment, RelationType.Top_Bottom);
				m_view.m_friendGroup.y = m_view.m_listRecomment.y + m_view.m_listRecomment.height + 30;
			}
			else
			{
				m_view.m_empty.selectedIndex = 1;
				m_view.m_friendGroup.y = m_view.m_topBar.y - 4;
			}
			
			m_view.m_listFirends.numItems = friends.Friends.Count;
			m_view.m_listRecomment.numItems = friends.RecommendFriends.Count;

			if (friends.Friends.Count > 0)
			{
				m_view.m_emptyFriend.selectedIndex = 0;
				m_view.m_listRecomment.ResizeToFit(3);
			}
			else
			{
				m_view.m_emptyFriend.selectedIndex = 1;
				m_view.m_listRecomment.ResizeToFit(5);
			}

			m_view.m_titleCount.text = string.Format("({0}/{1})", friends.Friends.Count, 100);
			
			// 显示倒计时
			GTween.Kill(m_view);
			int time = FriendModule.Instance.coldTime;
			if (time > 0)
			{
				m_view.m_titleTime.text = Utils.FormatTime(time);
				GTween.To(time, 0, time).OnUpdate(t =>
				{
					m_view.m_titleTime.text =  Utils.FormatTime(FriendModule.Instance.coldTime);
				}).SetTarget(m_view).OnComplete(() =>
				{
					m_view.m_titleTime.text = "";
					EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
				});
			}
			else
			{
				m_view.m_titleTime.text = "";
			}
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
			
			if (FriendModule.Instance.CanHire(friendItem.player_id))
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
