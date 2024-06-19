
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;

    public partial class UILuckLike
	{
		int m_LikeCfgId;        //好评奖励Id
		int m_HiddenCfgId;		//隐藏奖励Id
		List<LikeRewardData> m_RewardData;
		List<int> m_HideCfgIds = new List<int>();

		int m_TotalNum = 6;		//抽奖类型数量
		float m_Height = 200;   //抽奖item高度


		partial void InitLogic(UIContext context){

			InitLotteryList();
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

			m_view.m_BigLuckShow.m_list.itemRenderer = OnBigRewardItemRenderer;
			m_view.m_BigLuckShow.onClick.Add(() =>
			{
				m_view.m_BigLuckShow.visible = false;
				RefreshRewardList();
			});
		}

		public void RefreshRewardList() 
		{
			m_RewardData = DataCenter.Instance.likeData.likeRewardDatas;
			m_view.m_reward.selectedIndex = m_RewardData.Count > 0 ? 1 : 0;
			m_view.m_rewardList.numItems = m_RewardData.Count;
		}

		public void PlayLottery() 
		{
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

        partial void OnBtnClick(EventContext data)
        {
			PlayLottery();
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


		partial void UnInitLogic(UIContext context){
			List<int[]> list = new List<int[]>();
			var rewardDatas = DataCenter.Instance.likeData.likeRewardDatas;

			if (rewardDatas.Count > 0)
			{
				rewardDatas.Foreach((r) =>{ list.Add(new int[] { 1, r.id, r.num });});
				Utils.ShowRewards(list, updatedata: true);
				rewardDatas.Clear();
			}
		}
	}
}
