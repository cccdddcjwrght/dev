using System;
using GameConfigs;
using SGame.Firend;
using SGame.UI.Common;
using SGame.UI.Main;
using Unity.Entities;

namespace SGame.UI
{
	using FairyGUI;
	using SGame;
	using System.Collections.Generic;

	public partial class UIMain
	{
		private EventHandleContainer m_handles = new EventHandleContainer();
		private GList leftList;
		private GList m_rightList;
		private SetData m_setData;
		private Entity  m_taskEffect = Entity.Null; // 任务特效对象
		private Entity m_totalEffect = Entity.Null;	//店铺特效

		Action<bool> timer;

		private const int RIGHT_ITEM_NUM = 7;
		private const int INVEST_SHOW_EFFECTID = 57; // 广告特效ID

		/// <summary>
		/// 区域定义
		/// </summary>
		public enum AREA : int
		{
			LEFT = 101,
			RIGHT = 102,
		}

		private List<CheckingManager.CheckItem> m_RightIconDatas;
		private List<CheckingManager.CheckItem> m_LeftIconDatas;
		
		private CheckingManager.CheckItem m_treasureItem;


		partial void InitEvent(UIContext context)
		{
			m_setData = DataCenter.Instance.setData;
			leftList = m_view.m_leftList.m_right;
			m_rightList = m_view.m_rightList.m_right;
			leftList.opaque = false;
			m_rightList.opaque = false;
			RegisterUIState();
			var headBtn = m_view.m_head;
			leftList.itemRenderer = (index, gobject) => RenderNormalItem(m_LeftIconDatas, index, gobject, true);//+= RenderListItem;
			m_rightList.itemRenderer = (index, gobject) => RenderNormalItem(m_RightIconDatas, index, gobject, false);

			headBtn.onClick.Add(OnheadBtnClick);
			m_view.m_buff.onClick.Add(OnBuffShowTipClick);
			m_view.m_buff.onFocusOut.Add(OnBuffFoucsOutClick);
			m_view.m_likeBtn.onClick.Add(OnRoomLikeClick);
			m_view.m_taskBtn.GetChild("icon").onClick.Add(OnOpenTaskClick);
			m_view.m_taskBtn.m_click.onClick.Add(OnTaskFinishClick);
			m_view.m_taskBtn.m_taskProgress.onClick.Add(OnTaskFinishClick);


			m_view.m_Gold.onClick.Add(OnOpenTotalClick);
			m_view.m_totalBtn.onClick.Add(OnOpenTotalClick);
			m_view.m_btnShop.onClick.Set(()=>((int)FunctionID.SHOP).Goto());
			m_view.m_explore.onClick.Set(()=>((int)FunctionID.EXPLORE).Goto());

			m_view.m_equipBtn.onClick.Add(() => OpenUI(FunctionID.ROLE_EQUIP));
			m_view.m_recipeBtn.onClick.Add(()=> OpenUI(FunctionID.RECIPE));
			m_view.m_petBtn.onClick.Add(()=>OpenUI(FunctionID.PET));
			m_view.m_hotFoodBtn.onClick.Add(() => OpenUI(FunctionID.HOT_FOOD));
			//m_view.m_leftList.m_treasureBtn.onClick.Add(()=> OpenUI(FunctionID.TREASURE));

			m_handles += EventManager.Instance.Reg((int)GameEvent.PROPERTY_GOLD, OnEventGoldChange);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_MAIN_REFRESH, OnEventRefreshItem);
			m_handles += EventManager.Instance.Reg(((int)GameEvent.SETTING_UPDATE_HEAD), OnHeadSetting);
			//m_handles += EventManager.Instance.Reg((int)GameEvent.ROOM_START_BUFF, OnRefeshBuffTime);
			m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ROOM_LIKE_ADD, OnRefreshLikeCount);
			m_handles += EventManager.Instance.Reg((int)GameEvent.PIGGYBANK_UPDATE, OnUpdatePiggyProgress);
			m_handles += EventManager.Instance.Reg<int, int>((int)GameEvent.TECH_LEVEL, (id, level) => RefreshAdBtn());
			m_handles += EventManager.Instance.Reg<bool>((int)GameEvent.APP_PAUSE, AppPasueRefresh);
			m_handles += EventManager.Instance.Reg((int)GameEvent.TOTAL_REFRESH, OnRefreshTotalState);
			m_handles += EventManager.Instance.Reg((int)GameEvent.HOTFOOD_REFRESH, OnRefreshHotFood);
			m_handles += EventManager.Instance.Reg<int>((int)GameEvent.MAIN_TASK_UPDATE, (t)=> RefreshTaskDes());
			m_handles += EventManager.Instance.Reg((int)GameEvent.MAIN_TASK_PROGRESS_CHANGE, ()=> RefreshTaskState());

			OnHeadSetting();
			OnEventRefreshItem();
			RefreshTaskDes();
			RefreshTaskState(false);
			//OnRefeshBuffTime();
			RepeatPlayLikeEffect();
			OnRefreshLikeCount(0);
			OnRefreshAdTime();
			OnRefreshTotalState();
			OnRefreshHotFood();
		}

		void OpenUI(FunctionID id)
		{
			int funcID = (int)id;
			if (!ConfigSystem.Instance.TryGet(funcID, out FunctionConfigRowData config))
			{
				log.Error("function id not found=" + funcID);
				return;
			}

			if (!CheckFuncOpen(id, true))
				return;
			SGame.UIUtils.OpenUI(config.Ui);
		}

		bool CheckFuncOpen(FunctionID fid, bool showtips = false)
		{
			return ((int)fid).IsOpend(showtips);
		}

		void RegisterUIState()
		{
			m_treasureItem = CheckingManager.CreateItem((int)FunctionID.TREASURE, () => ChestItemUtil.CheckEqGiftBag())
				.SetIcon(ChestItemUtil.GetIcon)
				.SetTips(() => ChestItemUtil.GetChestCount().ToString());
			
			if (m_funcManager != null)
				return;

			m_funcManager = new CheckingManager();

			// 地图
			m_funcManager.Register((int)FunctionID.MAP).SetOnClick(()=>SGame.UIUtils.OpenUI("enterscenetemp", DataCenter.Instance.roomData.current.id + 1));

			//全局科技
			m_funcManager.Register(((int)FunctionID.TECH));
			//排行榜
			m_funcManager.Register(26, () => RankModule.Instance.IsOpen(), () => RankModule.Instance.GetRankTime());

			///活动商城
			m_funcManager.Register(28, () => DataCenter.TaskUtil.IsOpen(), () => DataCenter.TaskUtil.GetTaskActiveTime());

			//存钱罐
			m_funcManager.Register(PiggyBankModule.PIGGYBANK_OEPNID, PiggyBankModule.Instance.CanTake);

			// 新手礼包
			m_funcManager.Register(NewbieGiftModule.OPEN_ID, NewbieGiftModule.Instance.CanTake);

			// 明日礼包
			m_funcManager.Register(TomorrowGiftModule.OPEN_ID, () =>
			{
				TomorrowGiftModule.Instance.UpdateState();
				return TomorrowGiftModule.Instance.CanShow();
			}, () => TomorrowGiftModule.Instance.time);

			m_funcManager.Register((int)FunctionID.DAILY_TASK, funcTime: ()=> GameServerTime.Instance.nextDayInterval, 
				complete:() => DataCenter.DailyTaskUtil.GenerateTask());

			// 成长礼包
			m_funcManager.Register(GrowGiftModule.OPEND_ID, () => GrowGiftModule.Instance.IsOpend(0), () => GrowGiftModule.Instance.GetActiveTime(0), 0, "growgift1");
			m_funcManager.Register(GrowGiftModule.OPEND_ID, () => GrowGiftModule.Instance.IsOpend(1), () => GrowGiftModule.Instance.GetActiveTime(1), 1, "growgift2");

			// 左排
			//m_funcManager.Register((int)FunctionID.SHOP);
			//m_funcManager.Register((int)FunctionID.TECH);
			
			//俱乐部
			m_funcManager.Register(30, () => DataCenter.ClubUtil.IsOpen());
			m_funcManager.Register(31, () => DataCenter.ClubUtil.CheckIsInClub());

			m_funcManager.Register((int)FunctionID.FRIEND, null, () => FriendModule.Instance.hiringTime); // 好友
			//m_funcManager.Register((int)24);
			
			//m_funcManager.Register((int)FunctionID.TREASURE, () => ChestItemUtil.CheckEqGiftBag());
			//	.SetIcon(ChestItemUtil.GetIcon)
			//	.SetTips(() => ChestItemUtil.GetChestCount().ToString());

			// m_funcManager.Register((int)FunctionID.RECIPE);

			m_funcManager.RegisterAllActFunc();
		}

		void UpdateUIState()
		{
			m_RightIconDatas = m_funcManager.GetDatas((int)AREA.RIGHT);
			m_rightList.numItems = m_RightIconDatas.Count;

			m_LeftIconDatas = m_funcManager.GetDatas((int)AREA.LEFT);
			leftList.numItems = m_LeftIconDatas.Count;
			leftList.ResizeToFit();

			m_funcManager.UpdateState();
		}

		private void OnHeadSetting()
		{
			var head = m_view.m_head as UI_HeadBtn;
			head.m_headImg.url = string.Format("ui://IconHead/{0}", m_setData.GetHeadFrameIcon(1, DataCenter.Instance.accountData.GetHead()));
			head.m_frame.url = string.Format("ui://IconHead/{0}", m_setData.GetHeadFrameIcon(2, DataCenter.Instance.accountData.GetFrame()));
		}

		private void OnEventRefreshItem()
		{
			if (StaticDefine.PAUSE_MAIN_REFRESH) return;

			m_view.m_Diamond.visible = 34.IsOpend(false);
			m_view.m_explore.visible = CheckFuncOpen(FunctionID.EXPLORE);
			var adBtn = m_view.m_AdBtn;
			adBtn.visible = 16.IsOpend(false);
			m_view.m_hotFoodBtn.visible = 37.IsOpend(false);
			RefreshAdBtn();

			m_view.m_likeBtn.visible = 23.IsOpend(false);
			m_view.m_taskBtn.visible = DataCenter.TaskMainUtil.IsShow();

			m_view.m_btnShop.visible = ((int)FunctionID.SHOP).IsOpend(false);

			m_view.m_petBtn.visible = CheckFuncOpen(FunctionID.PET);
			m_view.m_equipBtn.visible = CheckFuncOpen(FunctionID.ROLE_EQUIP);
			m_view.m_recipeBtn.visible = CheckFuncOpen(FunctionID.RECIPE);
			m_view.m_getworker.selectedIndex =  36.IsOpend(false) ? 1 : 0;

			if (m_treasureItem.IsVisible())
			{
				m_view.m_leftList.m_treasureBtn.visible = true;
				SetItemData(m_treasureItem, m_view.m_leftList.m_treasureBtn, true);
			}
			else
			{
				m_view.m_leftList.m_treasureBtn.visible = false;
			}

			// 处理左右列表
			UpdateUIState();
			OnUpdatePiggyProgress();
		}

		void OnRighMenuClick(EventContext context)
		{
			var item = (context.sender as GComponent).data as CheckingManager.CheckItem;
			item.OpenUI();
		}

		void SetItemData(CheckingManager.CheckItem config, GObject item, bool isLeft)
		{
			UI_ActBtn ui = item as UI_ActBtn;
			ui.data = config;
			//if (config.effectID > 0)
			//{
			//	ui.m_side.selectedIndex = 2;
			//}
			//else
			//{
			//ui.m_side.selectedIndex = isLeft ? 2 : 1;
			//}
			config.ShowEffect(ui.m_effect);
			ui.m_redpoint.visible = true;

			if (!string.IsNullOrEmpty(config.uiname))
				item.name = config.uiname;

			ui.onClick.Remove(OnRighMenuClick);
			ui.onClick.Add(OnRighMenuClick);
			if (ui == null)
			{
				log.Error("item can not conver=" + config.config.Id);
				return;
			}

			// 倒计时刷新
			if (config.funcTime != null)
			{
				GTween.Kill(ui);
				var configTime = config.funcTime();
				GTween.To(0, 1, configTime).OnUpdate((GTweener tweener) =>
				{
					var time = config.funcTime();
					if (time > 0)
						ui.m_content.text = Utils.FormatTime(config.funcTime());
					else
					{
						GTween.Kill(ui);
						ui.m_ctrlTime.selectedIndex = 0;
						config.complete?.Invoke();
					}
				}).SetTarget(ui).OnComplete(() =>
				{
					ui.m_ctrlTime.selectedIndex = 0;
					log.Info("counter dowm zero!");
				});
				ui.m_ctrlTime.selectedIndex = configTime > 0 ? 1 : 0;
				ui.m_content.text = Utils.FormatTime(configTime);
			}
			else
			{
				// 没有倒计时
				ui.m_ctrlTime.selectedIndex = 0;
			}

			ui.icon = config.config.Icon;
		}
		
		/// <summary>
		/// 显示列表图片
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="index"></param>
		/// <param name="item"></param>
		private void RenderNormalItem(List<CheckingManager.CheckItem> datas, int index, GObject item, bool isLeft)
		{
			var config = datas[index];
			SetItemData(config, item, isLeft);
		}

		// 金币添加事件
		void OnEventGoldChange()
		{
			m_view.m_Gold.SetText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			m_view.m_Diamond.SetText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
		}

		void OnheadBtnClick(EventContext context)
		{
			SGame.UIUtils.OpenUI("setting");
			//Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("setting"));
		}

		Action<bool> m_buffTimer;
		//buff描述tip，应该写个通用的
		void OnBuffShowTipClick(EventContext context)
		{
			//显示tip
			m_view.m_buff.m_tipState.selectedIndex = (m_view.m_buff.m_tipState.selectedIndex + 1) % 2;

			m_buffTimer?.Invoke(false);
			if (m_view.m_buff.m_tipState.selectedIndex == 1) m_buffTimer = Utils.Timer(3, null, completed: () => { OnBuffFoucsOutClick(null); });
		}

		void OnBuffFoucsOutClick(EventContext context)
		{
			m_buffTimer?.Invoke(false);
			m_view.m_buff.m_tipState.selectedIndex = 0;
		}

		void OnOpenTotalClick()
		{
			if(38.IsOpend(false)) SGame.UIUtils.OpenUI("buffshop");
			else SGame.UIUtils.OpenUI("totalBoost");
		}

		void OnRoomLikeClick()
		{
			SGame.UIUtils.OpenUI("lucklike");
		}

		void OnOpenTaskClick() 
		{
			SGame.UIUtils.OpenUI("task");
		}

		void OnTaskFinishClick() 
		{
			if (DataCenter.TaskMainUtil.CheckIsGet())
			{
				var taskCfgId = DataCenter.TaskMainUtil.GetCurTaskCfgId();
				ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(taskCfgId, out var cfg);
				if (cfg.IsValid())
				{
					var list = Utils.GetArrayList(true, cfg.GetTaskReward1Array, cfg.GetTaskReward2Array, cfg.GetTaskReward3Array);
					Utils.ShowRewards(list, () => SGame.UIUtils.OpenUI("task"));
					DataCenter.TaskMainUtil.FinishTaskId(taskCfgId, false);
				}
			}
			else
			{
				SGame.UIUtils.OpenUI("task");
			}
		}

		partial void OnAdBtnClick(EventContext data)
		{
			if (DataCenter.Instance.AdMainData.freeCount > 0) 
			{
				AdModule.Instance.AddBuff(true);
				DataCenter.Instance.AdMainData.freeCount--;
				OnRefreshAdTime();
				return;
			}

			AdModule.PlayAd(AdType.Buff.ToString(), (state) =>
			{
				if (state)
				{
					AdModule.Instance.AddBuff(true);
					OnRefreshAdTime();
				}
			});
		}

		partial void OnInvestBtnClick(EventContext data)
		{
			EventManager.Instance.Trigger((int)GameEvent.INVEST_CLICK, AdModule.Instance.GetShowTime(AdType.Invest.ToString()));
			AdModule.PlayAd(AdType.Invest.ToString(), (state) =>
			{
				if (state)
				{
					AdModule.Instance.RecordEnterTime(AdType.Invest.ToString());
					AdModule.Instance.GetAdInvestNum(out int itemId, out double num);
					PropertyManager.Instance.Update(1, itemId, num);
					TransitionModule.Instance.PlayFlight(m_view.m_InvestBtn, itemId);
				}
			});
		}

		/// <summary>
		/// 刷新开局局内buff时间(弃用)
		/// </summary>
		void OnRefeshBuffTime()
		{
			bool checkTakeEffect = DataCenter.ExclusiveUtils.CheckBuffTakeEffect();
			m_view.m_buff.visible = checkTakeEffect;
			if (checkTakeEffect)
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(DataCenter.Instance.exclusiveData.cfgId, out var data))
				{
					m_view.m_buff.SetIcon(data.BuffIcon);
					m_view.m_buff.m_info.SetText(string.Format(UIListener.Local(data.BuffDesc),
					data.BuffValue == 0 ? data.BuffDuration : data.BuffValue,
					data.BuffDuration));
				}
				var time = DataCenter.ExclusiveUtils.GetBuffResiduTime();
				m_view.m_buff.m_isTime.selectedIndex = time > 0 ? 0 : 1;
				Utils.Timer(time, () =>
				{
					time = DataCenter.ExclusiveUtils.GetBuffResiduTime();
					m_view.m_buff.m_time.SetText(Utils.FormatTime(time));
				}, m_view, completed: () => BuffTimeFinish());
			}
		}

		void BuffTimeFinish()
		{
			m_view.m_buff.m_isTime.selectedIndex = 1;
			m_view.m_buff.visible = false;
			//OnRefreshTotalState();
		}

		/// <summary>
		/// 刷新好评数量
		/// </summary>
		void OnRefreshLikeCount(int likeNum)
		{
			var curNum = DataCenter.Instance.likeData.likeNum;
			m_view.m_likeBtn.m_count.text = curNum.ToString();
			m_view.m_likeBtn.m_count.visible = curNum > 0;

			if (likeNum > 0)
			{
				if (!m_view.m_likeBtn.m_add.playing) 
				{
					m_view.m_likeBtn.m_add.Play();
					m_view.m_likeBtn.m_zan.Play();
				}
				m_view.m_likeBtn.m_num.SetText(string.Format("+{0}", likeNum));
			}
		}

		GTweener m_TaskTweenr;
		void RefreshTaskDes() 
		{
			m_TaskTweenr?.Kill();
			m_TaskTweenr = null;
			ConfigSystem.Instance.TryGet<MainTaskRowData>(DataCenter.TaskMainUtil.GetCurTaskCfgId(), out var config);
			if (config.IsValid()) m_view.m_taskBtn.SetTextByKey(config.TaskDes);
			m_view.m_taskBtn.m_state.selectedIndex = 0;
			bool isTaskFinish = DataCenter.TaskMainUtil.CheckIsGet();
			m_view.m_taskBtn.m_finish.selectedIndex = isTaskFinish ? 1 : 0;
			UpdateTaskEffect(isTaskFinish);
		}

		void UpdateTaskEffect(bool isTaskFinish)
		{
			if (isTaskFinish)
			{
				if (m_taskEffect == Entity.Null)
				{
					m_taskEffect = EffectSystem.Instance.SpawnUI(56, m_view.m_taskBtn.m_effect);
				}
				return;
			}
			else
			{
				if (m_taskEffect != Entity.Null)
				{
					EffectSystem.Instance.CloseEffect(m_taskEffect);
					m_taskEffect = Entity.Null;
				}
			}
		}

		void RefreshTaskState(bool isPlay = true) 
		{
			m_TaskTweenr?.Kill();
			m_TaskTweenr = null;

			bool isTaskFinish = DataCenter.TaskMainUtil.CheckIsGet();
			m_view.m_taskBtn.m_finish.selectedIndex = isTaskFinish ? 1 : 0;
			if (Utils.GetCurrentTaskProgress(out int value, out int max))
			{
				m_view.m_taskBtn.m_taskProgress.value = value;
				m_view.m_taskBtn.m_taskProgress.max = max;
			}
			UpdateTaskEffect(isTaskFinish);

			if (isTaskFinish)
			{
				m_view.m_taskBtn.m_state.selectedIndex = 1;
				return;
			}
			

			if (!isPlay) return;
			m_view.m_taskBtn.m_state.selectedIndex = 1;
			m_TaskTweenr = GTween.To(0, 1, 2).SetTarget(m_view).OnComplete(()=> 
			{
				m_view.m_taskBtn.m_state.selectedIndex = 0;
			});
		}

		void RepeatPlayLikeEffect() 
		{
			GTween.To(0, 1, 30).SetTarget(m_view).OnComplete(() =>
			{
				if(DataCenter.Instance.likeData.likeNum > 0) EffectSystem.Instance.AddEffect(51, m_view.m_likeBtn);
				RepeatPlayLikeEffect();
			});
		}


		void OnRefreshTotalState()
		{
			var value = ReputationModule.Instance.GetTotalValue();
			m_view.m_totalBtn.m_num.text = string.Format("X{0}", value);

			if (BuffShopModule.Instance.GetHaveBuffVaild())
			{
				if (m_totalEffect == Entity.Null) m_totalEffect = EffectSystem.Instance.AddEffect(60, m_view.m_Gold);
			}	
			else
			{
				if (m_totalEffect != null)
				{
					EffectSystem.Instance.CloseEffect(m_totalEffect);
					m_totalEffect = Entity.Null;
				}
			}
		}


		bool m_OldAdShowState;

		private static string AdTypeInvestStr = AdType.Invest.ToString();
		
		void OnRefreshAdState()
		{
			GameProfiler.BeginSample("refreshad1");
			var hotFoodData = DataCenter.Instance.hotFoodData;
			m_view.m_hotFoodBtn.m_hoting.selectedIndex = hotFoodData.IsForce() ? 1 : 0;
			m_view.m_hotFoodBtn.m_cd.selectedIndex = !hotFoodData.IsForce() && hotFoodData.GetCdTime() > 0 ? 1 : 0;

			bool networkState = NetworkUtils.IsNetworkReachability();
			GameProfiler.BeginSample("refreshad1-1");
			m_view.m_AdBtn.enabled = AdModule.Instance.GetBuffTime() > 0 || networkState;
			GameProfiler.EndSample();

			GameProfiler.EndSample();
			if (!networkState)
			{
				GameProfiler.BeginSample("refreshad1.1");
				m_view.m_InvestBtn.visible = false;
				AdModule.Instance.RecordEnterTime(AdTypeInvestStr);
				GameProfiler.EndSample();
				return;
			}

			GameProfiler.BeginSample("refreshad2");
			AdModule.Instance.GetAdShowTime(AdTypeInvestStr, out bool state, out int time);
			GameProfiler.EndSample();
			//state = true;
			//m_view.m_InvestBtn.visible = state;
			GameProfiler.BeginSample("refreshad3");

			if (m_OldAdShowState != state) 
			{
				m_OldAdShowState = state;
				if (m_OldAdShowState)
				{
					m_view.m_doshow.Play();
					m_view.m_InvestBtn.visible = true;
					
					// 播放特效
					m_view.m_InvestBtn.m_t0.Play();
					EffectSystem.Instance.SpawnUI(INVEST_SHOW_EFFECTID, m_view.m_InvestBtn.m_effect);

				}
				else m_view.m_dohide.Play(()=>m_view.m_InvestBtn.visible = false);
			}
			GameProfiler.EndSample();

			if (state)
			{
				GameProfiler.BeginSample("refreshad4");

				var btn = m_view.m_InvestBtn as UI_InvestMan;
				btn.m_bar.fillAmount = (float)time / DataCenter.AdUtil.GetAdSustainTime(AdType.Invest.ToString());
				AdModule.Instance.GetAdInvestNum(out int itemId, out double num);
				string url = itemId == (int)ItemID.GOLD ? "ui_shop_icon_coin_03" : "ui_shop_icon_gem_02";
				btn.SetText(string.Format("+{0}", Utils.ConvertNumberStr(num)));
				btn.SetIcon(url);
				GameProfiler.EndSample();
			}
		}

		void OnRefreshHotFood() 
		{
			var hotFoodData = DataCenter.Instance.hotFoodData;
			if (hotFoodData.IsForce())
			{
				//m_view.m_hotFoodBtn.m_hoting.selectedIndex = 1;
				m_view.m_hotFoodBtn.SetIcon(Utils.GetItemIcon(1, hotFoodData.foodID));

				Utils.Timer(hotFoodData.GetTime(), () =>
				{
					var t = HotFoodModule.Instance.HotDuration;
					var p = t - hotFoodData.GetTime();
					m_view.m_hotFoodBtn.m_progress.fillAmount = (float)p / t;
				}, m_view, completed : OnRefreshHotFood);
			}
			else 
			{
				//m_view.m_hotFoodBtn.m_cd.selectedIndex = DataCenter.Instance.hotFoodData.GetCdTime() > 0 ? 1 : 0;
				//m_view.m_hotFoodBtn.m_hoting.selectedIndex = 0;
			}
			
		}

		void OnUpdatePiggyProgress()
		{
			if (PiggyBankModule.Instance.CanTake())
			{
				GButton btn = m_rightList.GetChild("piggybank").asButton;
				btn.GetController("ctrlTime").selectedIndex = 1;
				btn.GetChild("content").SetText(string.Format("{0}/{1}",
					DataCenter.Instance.piggybankData.progress,
					DataCenter.PiggyBankUtils.PIGGYBANK_MAX));
			}
		}

		void OnRefreshAdTime()
		{
			var time = AdModule.Instance.GetBuffTime();
			UIListener.SetControllerSelect(m_view.m_AdBtn, "isTime", time > 0 ? 1 : 0);
			UIListener.SetControllerSelect(m_view.m_AdBtn, "isFree", DataCenter.Instance.AdMainData.freeCount > 0 ? 0 : 1);
			if (time > 0)
			{
				UIListener.SetTextWithName(m_view.m_AdBtn, "time", Utils.TimeFormat(time));
				timer?.Invoke(false);
				timer = Utils.Timer(time, () =>
				{
					time = AdModule.Instance.GetBuffTime();
					UIListener.SetTextWithName(m_view.m_AdBtn, "time", Utils.TimeFormat(time));
				}, m_view, completed: () =>
				{
					UIListener.SetControllerSelect(m_view.m_AdBtn, "isTime", 0);
					//OnRefreshTotalState();
				});
			}
			//OnRefreshTotalState();
		}

		void RefreshAdBtn()
		{
			m_view.m_AdBtn.GetChild("boostTxt").SetText(UIListener.Local("ui_ad_boost") + "x" + AdModule.Instance.GetAdRatio());
			m_view.m_AdBtn.GetChild("timeTxt").SetText("+" + Utils.FormatTime(AdModule.Instance.GetAdDuration(), formats:
				AdModule.Instance.GetAdDuration() % 60 == 0 ? new string[] { "{0}min" } : new string[] { "{0}min{1}s" }));
			//OnRefreshTotalState();
		}

		void AppPasueRefresh(bool pause)
		{
			OnRefeshBuffTime();
			OnRefreshAdTime();
		}

		partial void UnInitEvent(UIContext context)
		{
			timer?.Invoke(false);
			m_buffTimer?.Invoke(false);
			m_handles.Close();

			CloseAllEffect();
		}

		void CloseAllEffect()
		{
			if (m_taskEffect != Entity.Null)
			{
				EffectSystem.Instance.CloseEffect(m_taskEffect);
				m_taskEffect = Entity.Null;
			}

			if (m_totalEffect != Entity.Null)
			{
				EffectSystem.Instance.CloseEffect(m_totalEffect);
				m_totalEffect = Entity.Null;
			}
		}

		partial void OnDiamondClick(EventContext data)
		{
			"shop".Goto();
		}

		partial void OnWorkflagClick(EventContext data)
		{
			SceneCameraSystem.Instance.Focus(StaticDefine.G_GET_WORKER_POS , useY:false , time:0.5f);
		}

	}
}
