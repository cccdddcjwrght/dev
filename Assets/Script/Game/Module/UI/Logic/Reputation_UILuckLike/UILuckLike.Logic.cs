
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;
    using System.Linq;

    public partial class UILuckLike
	{
		private LongPressGesture _press;

		int m_LikeCfgId;        //好评奖励Id
		int m_HiddenCfgId;		//隐藏奖励Id
		List<LikeRewardData> m_RewardData;  //储存的奖励
		List<LikeRewardData> m_DropRewardData;
		List<ItemData.Value> m_DropItem;	//随机的掉落物品

		List<int> m_HideCfgIds = new List<int>();

		int m_TotalNum = 6;		//抽奖类型数量
		float m_Height = 200;   //抽奖item高度

		bool isHaveBox = false;		//是否有装备宝箱
		bool m_IsPlaying = false;	//是否抽奖中
		bool m_Auto = false;		//是否自动抽奖

		EventHandleContainer m_Event = new EventHandleContainer();
		partial void InitLogic(UIContext context){
			context.onUpdate += OnUpdate;
			m_Event += EventManager.Instance.Reg<int>((int)GameEvent.ROOM_LIKE_ADD, (num)=> RefreshLikeNum());

			InitLotteryList();
			RefreshLikeNum();
			RefreshRewardList();
		}

		public void InitLotteryList() 
		{
			m_view.m_rewardList.itemRenderer = OnRewardItemRenderer;
			m_view.m_list1.SetVirtualAndLoop();
			m_view.m_list2.SetVirtualAndLoop();

			m_view.m_list1.itemRenderer = OnItem1Renderer;
			m_view.m_list2.itemRenderer = OnItem2Renderer;

			m_view.m_list1.numItems = m_TotalNum;
			m_view.m_list2.numItems = m_TotalNum;

			_press = new LongPressGesture(m_view.m_startBtn)
			{
				once = true,
			};
			_press.onBegin.Add(() => {
				m_Auto = true;
				RefreshAutoState();
			});

			m_view.m_BigLuckShow.m_list.itemRenderer = OnBigRewardItemRenderer;
			m_view.m_BigLuckShow.onClick.Add(() =>
			{
				m_view.m_BigLuckShow.visible = false;
				RefreshRewardList();
			});
		}

		public void RefreshAutoState() 
		{
			m_view.m_auto.selectedIndex = m_Auto ? 1 : 0;
			Debug.Log(m_view.m_auto.selectedIndex);
		}

		public void RefreshLikeNum() 
		{
			m_view.m_count.text = DataCenter.Instance.likeData.likeNum.ToString();
		}

		public void RefreshRewardList() 
		{
			m_RewardData = DataCenter.Instance.likeData.likeRewardDatas;
			m_view.m_reward.selectedIndex = m_RewardData.Count > 0 ? 1 : 0;
			m_view.m_rewardList.numItems = m_RewardData.Count;
		}

		public void PlayLottery() 
		{
            if (DataCenter.Instance.likeData.likeNum <= 0)
            {
                "@ui_likes_tips".Tips();
                return;
            }

            if (m_IsPlaying) return;
			m_IsPlaying = true;
			//UILockManager.Instance.Require("LuckLike");
			DataCenter.Instance.likeData.likeNum--;
			EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, 0);

			m_LikeCfgId = DataCenter.LikeUtil.GetLotteryIndex();
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_RewardsRowData>(m_LikeCfgId, out var config)) 
			{
				int a = config.ResultShow(0);
				int b = config.ResultShow(1);
				LotteryAnim(a, m_view.m_list1, 1f);
				LotteryAnim(b, m_view.m_list2, 1f, LotteryFinish, 0.2f);

				if (config.ResultType == 1)
				{
					PropertyManager.Instance.Insert2Cache(new List<int[]>() { new int[] { 1, config.Reward(0), config.Reward(1) } });
				}
				else if (config.ResultType == 2)
				{
					DataCenter.LikeUtil.AddRewardData(config.Reward(0), config.Reward(1));
				}
				else if (config.ResultType == 3) 
				{
					//抽到隐藏大奖需要到隐藏奖配置再随机一次
					m_HiddenCfgId = DataCenter.LikeUtil.GetHiddenRewardIndex();
					if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_JackpotRowData>(m_HiddenCfgId, out var cfg)) 
					{
						DataCenter.LikeUtil.AddRewardData(cfg.Reward(0), cfg.Reward(1));
					}
				}
			}
		}

		public void LotteryAnim(int index, GList list, float duration, GTweenCallback callback = null, float delay = 0) 
		{
			float pos_y = m_TotalNum * m_Height * 2 + (index - 1) * m_Height;
			GTween.To(0, pos_y, duration).SetTarget(m_view).SetDelay(delay).OnUpdate((t) =>
			{
				list.scrollPane.SetPosY((float)t.value.d, false);
			}).OnComplete(callback);
		}

		
		public void LotteryFinish() 
		{
			m_IsPlaying = false;
			//UILockManager.Instance.Release("LuckLike");
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_RewardsRowData>(m_LikeCfgId, out var config)) 
			{
				if (config.ResultType == 1)
				{
					PropertyManager.Instance.CombineCache2Items();
					TransitionModule.Instance.PlayFlight(m_view.m_list1, config.Reward(0), m_view.m_list1.width * 0.5f);
				}
				else if (config.ResultType == 2)
				{
					RefreshRewardList();
				}
				else if (config.ResultType == 3) 
				{
					m_HideCfgIds.Clear();
					if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_JackpotRowData>(m_HiddenCfgId, out var cfg))
					{
						m_view.m_BigLuckShow.visible = true;
						m_view.m_BigLuckShow.m_show.Play();
						m_HideCfgIds.Add(cfg.Reward(0));

						m_view.m_BigLuckShow.m_list.numItems = m_HideCfgIds.Count;
					}
				}
			}
		}

		private void OnUpdate(UIContext context)
		{
			if (m_Auto) 
			{
				if (!m_IsPlaying) 
				{
					if (DataCenter.Instance.likeData.likeNum > 0)
						PlayLottery();
					else
					{
						m_Auto = false;
						RefreshAutoState();
					}
				}
			}
		}

        partial void OnStartBtnClick(EventContext data)
        {
			PlayLottery();
        }

        partial void OnStopBtnClick(EventContext data)
        {
			m_Auto = false;
			RefreshAutoState();
        }

        void OnRewardItemRenderer(int index, GObject gObject) 
		{
			var data = m_RewardData[index];

			gObject.SetIcon(Utils.GetItemIcon(1, data.id));
			gObject.SetText("x" + Utils.ConvertNumberStr(data.num));
		}

        void OnItem1Renderer(int index, GObject gObject) 
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_IconRowData>(index + 1, out var config))
				gObject.SetIcon(config.Icon);
		}

		void OnItem2Renderer(int index, GObject gObject) 
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_IconRowData>(index + 1, out var config))
				gObject.SetIcon(config.Icon);
		}

		void OnBigRewardItemRenderer(int index, GObject gObject) 
		{
			var cfgId = m_HideCfgIds[index];
			gObject.SetIcon(Utils.GetItemIcon(1, cfgId));
		}

		//打开碎片宝箱
		void OpenFramentUI() 
		{
			if (m_DropItem == null || m_DropItem.Count <= 0) return;

			if (isHaveBox)
			{
				DelayExcuter.Instance.OnlyWaitUIClose("eqgiftui", () =>
				{
					SGame.UIUtils.OpenUI("frament", m_DropItem);
				});
			}
			else SGame.UIUtils.OpenUI("frament", m_DropItem);
		}

		partial void UnInitLogic(UIContext context){
			context.onUpdate -= OnUpdate;
			_press?.Dispose();
			m_Event.Close();
			m_Event = null;

			PropertyManager.Instance.CombineCache2Items();
			List<int[]> list = new List<int[]>();

			if (m_RewardData.Count > 0)
			{
				//如果有食谱碎片宝箱，先加上
				m_DropRewardData = m_RewardData.Where((r) => r.itemType == (int)EnumItemType.ChestKey).ToList();
				isHaveBox = m_RewardData.Find((r) => r.itemType == (int)EnumItemType.Chest) !=null;
				m_RewardData.RemoveAll((r) => r.itemType == (int)EnumItemType.ChestKey);
				for (int i = 0; i < m_DropRewardData.Count; i++)
				{
					var data = m_DropRewardData[i];
					m_DropItem = DataCenter.LikeUtil.GetItemDrop(data.typeId, data.num, i == 0);
				}
				m_DropItem.Foreach((d) => PropertyManager.Instance.Update(d.type, d.id, d.num));

				if (m_RewardData.Count > 0)
				{
					//关闭界面的时候打开领取普通大奖获得的奖励
					m_RewardData.Foreach((r) => { list.Add(new int[] { 1, r.id, r.num }); });
					Utils.ShowRewards(list, OpenFramentUI, updatedata: true);
				}
				else 
				{
					OpenFramentUI();
				}
				m_RewardData.Clear();
			}
		}
	}
}
