
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.GrowGift;
	
	public partial class UIGrowGift
	{
		private int m_goodsID = 0;
		private const string RISING_STAR_TEXT		= "progresspack_mission_1";
		private const string RISING_STAR_GREYTEXT	= "progresspack_mission_1_1";
		
		private GrowGiftData m_datas;
		private EventHandleContainer m_eventContainer;
		
		
		partial void InitEvent(UIContext context)
		{
			var entityManager = context.gameWorld.GetEntityManager();
			var uiParam = entityManager.GetComponentObject<UIParam>(context.entity);

			var myparam = uiParam.Value as object[];
			int index = (int)myparam[0];


			m_goodsID = GrowGiftModule.Instance.GetActiveGoodID(index);
			m_datas = GrowGiftModule.Instance.GetGiftData(m_goodsID);

			m_view.m_listRewards.itemRenderer = OnRenderRewards;
			m_view.m_listRewards.numItems = m_datas.m_rewards.Count;
			
			m_view.m_btnBuy.onClick.Set(OnClickBuy);
			m_view.m_btnCollect.onClick.Set(OnClickOneKeyTakeAll);
			m_eventContainer = new EventHandleContainer();
			m_eventContainer += EventManager.Instance.Reg((int)GameEvent.GROW_GIFT_REFRESH, RefreshUI);
			
			// 倒计时刷新
			GTween.To(0, 1, GetLeftTime())
				.SetTarget(m_view, TweenPropType.None)
				.OnUpdate(UpdateTimeText)
				.OnComplete(() =>
				{
					// 解锁关闭UI
					SGame.UIUtils.CloseUIByID(__id);
				});
			UpdateTimeText();
			RefreshUI();
		}
		
		/// <summary>
		/// 获得活动剩余时间
		/// </summary>
		/// <returns></returns>
		int GetLeftTime()
		{
			int currentTime = GameServerTime.Instance.serverTime;
			return ActiveTimeSystem.Instance.GetLeftTime(m_datas.activeID, currentTime);
		}

		/// <summary>
		/// 刷新时间UI
		/// </summary>
		void UpdateTimeText()
		{
			var timeStr = string.Format(LanagueSystem.Instance.GetValue("ui_progresspack_tips"), Utils.FormatTime(GetLeftTime()));

			m_view.m_lblTime.text = timeStr;
		}

		void OnClickBuy()
		{
			GrowGiftModule.Instance.BuyGoods(m_goodsID);
		}

		void OnClickOneKeyTakeAll()
		{
			foreach (var item in m_datas.m_rewards)
			{
				if (item.GetState() == GiftReward.State.CANTAKE)
					GrowGiftModule.Instance.TakedReward(item.configID);
			}

			RefreshUI();
		}

		void RefreshUI()
		{
			// 设置购买状态
			if (m_datas.IsAllBuyed())
				m_view.m_buy.selectedIndex = 1;
			else
				m_view.m_buy.selectedIndex = 0;
			
			m_view.m_btnBuy.text = DataCenter.ShopUtil.GetGoodsPriceStr(m_datas.shopID, m_datas.price);
			
			// 对数据进行排序
			m_datas.m_rewards.Sort((a, b) =>
			{
				var state1 = a.GetState();
				var state2 = b.GetState();
				if (state1 == GiftReward.State.FINISH)
					return 1;
				if (state2 == GiftReward.State.FINISH)
					return -1;

				return 0;
			});
			
			m_view.m_listRewards.numItems = m_datas.m_rewards.Count;
		}
		

		/// <summary>
		/// 获得数据
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		GiftReward GetData(int index)
		{
			return m_datas.m_rewards[index];
		}

		void OnClickTakeReward(EventContext fcontext)
		{
			var index = (int)(fcontext.sender as GObject).data;
			log.Info("click take reward =" + index);

			if (GrowGiftModule.Instance.TakedReward(m_datas.m_rewards[index].configID))
			{
				RefreshUI();
			}
		}

		void OnRenderRewards(int index, GObject gObject)
		{
			var item = gObject as UI_GiftItem;
			var data = GetData(index);
			
			// 显示进图条
			item.m_progress.min		= 0;
			item.m_progress.max		= data.conditionValue;
			item.m_progress.value	= data.GetConditionProgress();
			var text_value = string.Format("{0}/{1}", item.m_progress.value, item.m_progress.max);
			item.m_progress.text = text_value;

			// 显示状态
			var state = data.GetState();
			item.m_state.selectedIndex = (int)state;
			
			// 设置奖励获取
			item.data = index;
			item.onClick.Set(OnClickTakeReward);
			
			// ICON 数量
			var icon = Utils.GetItemIcon((int)data.reward.type, data.reward.id); //data[0], data[1]);
			item.m_gift_icon.SetIcon(icon);
			item.m_gift_icon.m_title.text = ((int)data.reward.num).ToString();

			// 变灰
			if (state == GiftReward.State.FINISH)
			{
				item.grayed = true;
				item.title = string.Format(LanagueSystem.Instance.GetValue(RISING_STAR_GREYTEXT), data.conditionValue);
			}
			else
			{
				item.grayed = false;
				item.title = string.Format(LanagueSystem.Instance.GetValue(RISING_STAR_TEXT), data.conditionValue);

			}
		}
		
		partial void UnInitEvent(UIContext context){
			m_eventContainer.Close();
			m_eventContainer = null;
		}
	}
}
