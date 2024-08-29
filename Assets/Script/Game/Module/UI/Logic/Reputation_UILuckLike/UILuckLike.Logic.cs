
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.Entities;
    using System.Collections;

    public partial class UILuckLike
	{
		private LongPressGesture _press;
		private LongPressGesture _effpress;

		int m_LikeCfgId;        //好评奖励Id
		int m_HiddenCfgId;		//隐藏奖励Id
		List<LikeRewardData> m_RewardData;  //储存的奖励
		List<ItemData.Value> m_DropItem;    //随机的掉落物品

		ItemData.Value m_CurReward = new ItemData.Value();     //当前抽中的奖励
		List<int[]> m_CommonRewardList = new List<int[]>();	//普通奖励（不用暂存）

		int m_TotalNum = 6;		//抽奖类型数量
		float m_Height = 200;   //抽奖item高度

		bool isHaveBox = false;		//是否有装备宝箱
		bool m_IsPlaying = false;	//是否抽奖中
		bool m_Auto = false;        //是否自动抽奖

		GTweener m_Tweener;
		float m_AutoCloseTime = 3f; //自动关闭时间
		bool isPop = false;			//是否有奖励弹框

		EventHandleContainer m_Event = new EventHandleContainer();
		partial void InitLogic(UIContext context){
			context.onUpdate += OnUpdate;
			m_Event += EventManager.Instance.Reg<int>((int)GameEvent.ROOM_LIKE_ADD, (num)=> RefreshLikeNum());
			m_Event += EventManager.Instance.Reg<bool>((int)GameEvent.APP_PAUSE, (pause) => SetAutoState(false));

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
				if (GuideManager.Instance.IsCoerce) return;
				if (DataCenter.Instance.likeData.likeNum <= 0) return;
				SetAutoState(true);
				EffectSystem.Instance.AddEffect(48, m_view.m___clickeffect);
			});

			_effpress = new LongPressGesture(m_view.m_startBtn)
			{
				once = true,
				trigger = 0.1f,
			};

			Entity effect = Entity.Null;
			_effpress.onBegin.Add(() =>
			{
				if (GuideManager.Instance.IsCoerce) return;
				if (DataCenter.Instance.likeData.likeNum <= 0) return;
				effect = EffectSystem.Instance.AddEffect(49, m_view.m___clickeffect);
			});

			m_view.m_startBtn.onTouchEnd.Add(() =>
			{
				if (effect != Entity.Null) EffectSystem.Instance.CloseEffect(effect);
			});

			m_view.m_closeBg.onClick.Add(DoCloseUIClick);
			m_view.m_LuckShow.onClick.Add(() =>
			{
				isPop = false;
				m_Tweener?.Kill();
				m_view.m_LuckShow.visible = false;
				RefreshRewardList();
			});
			m_view.m_tipBtn.onClick.Add(() => SGame.UIUtils.OpenUI("luckhelp"));

			m_view.m_t1.Play();
		}

		public void RefreshAutoState() 
		{
			m_view.m_auto.selectedIndex = m_Auto ? 1 : 0;
		}

		public void RefreshPlayBtnState() 
		{
			if (m_IsPlaying == m_view.m_startBtn.enabled) 
			{
				m_view.m_startBtn.enabled = !m_IsPlaying;
			}
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

			27.ToAudioID().PlayAudio();
			28.ToAudioID().PlayAudio();
			m_IsPlaying = true;
			m_view.m_t1.Play();
			m_view.m_t0.Stop();
			DataCenter.Instance.likeData.likeNum--;
			EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, 0);

			m_LikeCfgId = DataCenter.LikeUtil.GetLotteryIndex();
			EventManager.Instance.Trigger((int)GameEvent.LIKE_SPIN, m_LikeCfgId);
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_RewardsRowData>(m_LikeCfgId, out var config)) 
			{
				int a = config.ResultShow(0);
				int b = config.ResultShow(1);
				LotteryAnim(a, m_view.m_list1, 1f);
				LotteryAnim(b, m_view.m_list2, 1f, LotteryFinish, 1f);

				if (config.ResultType == 1)
				{
					m_CurReward.id = config.Reward(0);
					m_CurReward.num = config.Reward(1);
					PropertyManager.Instance.Insert2Cache(config.Reward(0), config.Reward(1));
				}
				else if (config.ResultType == 2)
				{
					m_CurReward.id = config.Reward(0);
					m_CurReward.num = config.Reward(1);
					if (config.Reward(0) == 1 || config.Reward(0) == 2) PropertyManager.Instance.Insert2Cache(config.Reward(0), config.Reward(1));
					else DataCenter.LikeUtil.AddRewardData(config.Reward(0), config.Reward(1));
				}
				else if (config.ResultType == 3) 
				{
					//抽到隐藏大奖需要到隐藏奖配置再随机一次
					m_HiddenCfgId = DataCenter.LikeUtil.GetHiddenRewardIndex();
					if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_JackpotRowData>(m_HiddenCfgId, out var cfg)) 
					{
						m_CurReward.id = cfg.Reward(0);
						m_CurReward.num = cfg.Reward(1);
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
			29.ToAudioID().PlayAudio();
			if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_RewardsRowData>(m_LikeCfgId, out var config))
			{
				m_view.m_t1.Stop();
				m_view.m_t0.Play();
				if (config.ResultType == 1)
				{
					PlayCommonShow();
				}
				else if (config.ResultType == 2)
				{
					//特效表现
					EffectSystem.Instance.AddEffect(30, m_view);
					if (config.Reward(0) == 1 || config.Reward(0) == 2)
						PlayCommonShow();
					else PlayLuckShow();
				}
				else if (config.ResultType == 3) 
				{
					if (ConfigSystem.Instance.TryGet<GameConfigs.Likes_JackpotRowData>(m_HiddenCfgId, out var cfg))
					{
						isPop = true;
						30.ToAudioID().PlayAudio();
						m_view.m_BigLuckShow.visible = true;
						m_view.m_BigLuckShow.m_show.Play();
						Utils.Timer(m_AutoCloseTime, null, m_view, completed: () =>
						{
							m_view.m_BigLuckShow.visible = false;
							PlayLuckShow();
						});
						EffectSystem.Instance.AddEffect(31, m_view.m_BigLuckShow.m_effect);
					}
				}
			}
		}

		int[] m_Temp = new int[3];
		void PlayCommonShow() 
		{
			m_CommonRewardList.Clear();

			m_Temp[0] = 1;
			m_Temp[1] = m_CurReward.id;
			m_Temp[2] = (int)m_CurReward.num;
			m_CommonRewardList.Add(m_Temp);

			isPop = true;
			Utils.ShowRewards(m_CommonRewardList, closeCall: () =>
			{
				var ui = SGame.UIUtils.GetUIView("rewardlist");
				TransitionModule.Instance.PlayFlight(ui.Value.contentPane.GetChildByPath("body.list").asList, m_CommonRewardList);
				PropertyManager.Instance.CombineCache2Items();
				isPop = false;
			}, updatedata: false);

			//自动抽奖需要关闭领取奖励界面
			if (m_Auto)
			{
				Utils.Timer(1, null, m_view, completed: () =>
				{
					isPop = false;
					PropertyManager.Instance.CombineCache2Items();
					SGame.UIUtils.CloseUIByName("rewardlist");
				});
			}
		}

		void PlayLuckShow() 
		{
			isPop = true;
			m_view.m_LuckShow.visible = true;
			32.ToAudioID().PlayAudio();
			Vector2 start_pos = new Vector2(m_view.m_list1.x, m_view.m_list1.y - 110);
			Vector2 end_pos = new Vector2(m_view.m_rewardList.x, m_view.m_rewardList.y);

			m_view.m_LuckShow.m_reward.SetIcon(Utils.GetItemIcon(1, m_CurReward.id));
			m_view.m_LuckShow.m_reward.SetText("X" + m_CurReward.num);
			EffectSystem.Instance.AddEffect(36, m_view.m_LuckShow.m_reward);

			m_view.m_LuckShow.m_reward.xy = start_pos;
			m_Tweener = GTween.To(start_pos, end_pos, 1).SetDelay(1).SetTarget(m_view).OnUpdate((t) =>
            {
                m_view.m_LuckShow.m_reward.xy = t.value.vec2;
            }).OnComplete(()=> 
			{
				RefreshRewardList();
				EffectSystem.Instance.AddEffect(37, m_view.m___luckeff);
				m_view.m_LuckShow.visible = false;
				isPop = false;
			});
        }

		private void OnUpdate(UIContext context)
		{
			if (m_Auto) 
			{
				if (!m_IsPlaying) 
				{
					if (isPop) return;

					if (DataCenter.Instance.likeData.likeNum > 0)
						PlayLottery();
					else
					{
						m_Auto = false;
						RefreshAutoState();
					}
				}
			}
			RefreshPlayBtnState();
		}

        partial void OnStartBtnClick(EventContext data)
        {
			PlayLottery();
        }

        partial void OnStopBtnClick(EventContext data)
        {
			SetAutoState(false);
		}

		void SetAutoState(bool state) 
		{
			m_Auto = state;
			RefreshAutoState();
		}

        void OnRewardItemRenderer(int index, GObject gObject) 
		{
			var data = m_RewardData[index];

			gObject.SetIcon(Utils.GetItemIcon(1, data.id));
			gObject.SetText("X" + Utils.ConvertNumberStr(data.num));
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


		partial void UnInitLogic(UIContext context){
			context.onUpdate -= OnUpdate;
			_press?.Dispose();
			_effpress?.Dispose();
			m_Event.Close();
			m_Event = null;

			//AudioSystem.Instance.Stop(SoundType.MASTER);
			PropertyManager.Instance.CombineCache2Items();
			List<int[]> list = new List<int[]>();

			if (m_RewardData.Count > 0)
			{
				IEnumerator OpenReward()
				{
					yield return WaitRewardClose();
					m_RewardData.Foreach((r) => { list.Add(new int[] { 1, r.id, r.num }); });
					Utils.ShowRewards(list, updatedata: true);
					m_RewardData.Clear();
				}
				//关闭界面的时候打开领取普通大奖获得的奖励
				OpenReward().Start();
			}
		}

		IEnumerator WaitRewardClose() 
		{
			while (true) 
			{
				if (!SGame.UIUtils.CheckUIIsOpen("rewardlist")) yield break;
				yield return null;
			}
		}
	}
}
