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
	using UnityEngine;

	public partial class UIMain
	{
		private EventHandleContainer m_handles = new EventHandleContainer();
		private GList leftList;
		private GList m_rightList;
		private SetData m_setData;

		Action<bool> timer;

		private const int RIGHT_ITEM_NUM = 7;
		
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


		partial void InitEvent(UIContext context)
		{
			m_setData			= DataCenter.Instance.setData;
			leftList			= m_view.m_leftList.m_right;
			m_rightList			= m_view.m_rightList.m_right;
			leftList.opaque		= false;
			m_rightList.opaque	= false;
			RegisterUIState();
			var headBtn = m_view.m_head;
			leftList.itemRenderer	 = (index, gobject) => RenderNormalItem(m_LeftIconDatas, index, gobject);//+= RenderListItem;
			m_rightList.itemRenderer = (index, gobject) => RenderNormalItem(m_RightIconDatas, index, gobject);;

			headBtn.onClick.Add(OnheadBtnClick);
			m_view.m_buff.onClick.Add(OnBuffShowTipClick);
			m_view.m_buff.onFocusOut.Add(OnBuffFoucsOutClick);
			m_view.m_likeBtn.onClick.Add(OnRoomLikeClick);
			m_view.m_totalBtn.onClick.Add(OnOpenTotalClick);
			
			m_view.m_skillBtn.onClick.Add(()=>OpenUI(FunctionID.TECH));
			m_view.m_equipBtn.onClick.Add(()=>OpenUI(FunctionID.ROLE_EQUIP));

			m_handles += EventManager.Instance.Reg((int)GameEvent.PROPERTY_GOLD, OnEventGoldChange);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_MAIN_REFRESH, OnEventRefreshItem);
			m_handles += EventManager.Instance.Reg(((int)GameEvent.SETTING_UPDATE_HEAD), OnHeadSetting);
			m_handles += EventManager.Instance.Reg((int)GameEvent.ROOM_START_BUFF, OnRefeshBuffTime);
			m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ROOM_LIKE_ADD, OnRefreshLikeTime);
			m_handles += EventManager.Instance.Reg((int)GameEvent.PIGGYBANK_UPDATE, OnUpdatePiggyProgress);
			m_handles += EventManager.Instance.Reg<int, int>((int)GameEvent.TECH_LEVEL, (id,level) => RefreshAdBtn());
			m_handles += EventManager.Instance.Reg<bool>((int)GameEvent.APP_PAUSE, AppPasueRefresh);

			OnHeadSetting();
			OnEventRefreshItem();
			OnRefeshBuffTime();
			OnRefreshLikeTime(0);
			OnRefreshAdTime();
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
			if (m_funcManager != null)
				return;

			m_funcManager = new CheckingManager();

			//排行榜
			m_funcManager.Register(26, ()=> RankModule.Instance.IsOpen(), ()=> RankModule.Instance.GetRankTime());

			//m_funcManager.Register(28);
			//存钱罐
			m_funcManager.Register(PiggyBankModule.PIGGYBANK_OEPNID, PiggyBankModule.Instance.CanTake);

			// 新手礼包
			m_funcManager.Register(NewbieGiftModule.OPEN_ID, NewbieGiftModule.Instance.CanTake);
			
			// 明日礼包
			m_funcManager.Register(TomorrowGiftModule.OPEN_ID, () =>
			{
				TomorrowGiftModule.Instance.UpdateState();
				return TomorrowGiftModule.Instance.CanShow();
			}, ()=> TomorrowGiftModule.Instance.time);
			
			// 成长礼包
			m_funcManager.Register(GrowGiftModule.OPEND_ID, ()=>GrowGiftModule.Instance.IsOpend(0), ()=>GrowGiftModule.Instance.GetActiveTime(0), 0, "growgift1");
			m_funcManager.Register(GrowGiftModule.OPEND_ID, ()=>GrowGiftModule.Instance.IsOpend(1), ()=>GrowGiftModule.Instance.GetActiveTime(1), 1, "growgift2");
			
			// 左排
			m_funcManager.Register((int)FunctionID.SHOP);
			m_funcManager.Register((int)FunctionID.FRIEND, null, ()=>FriendModule.Instance.hiringTime); // 好友
			m_funcManager.Register((int)24 );
			m_funcManager.Register((int)25, () => ChestItemUtil.CheckEqGiftBag());
			m_funcManager.RegisterAllActFunc();

		}

		void UpdateUIState()
		{
			m_RightIconDatas = m_funcManager.GetDatas((int)AREA.RIGHT);
			m_rightList.numItems = m_RightIconDatas.Count;

			m_LeftIconDatas = m_funcManager.GetDatas((int)AREA.LEFT);
			leftList.numItems = m_LeftIconDatas.Count;

			m_funcManager.UpdateState();
		}

		private void OnHeadSetting()
		{
			var head = m_view.m_head as UI_HeadBtn;
			head.m_headImg.url=string.Format("ui://IconHead/{0}",m_setData.GetHeadFrameIcon(1,DataCenter.Instance.accountData.GetHead()));
			head.m_frame.url=string.Format("ui://IconHead/{0}",m_setData.GetHeadFrameIcon(2,DataCenter.Instance.accountData.GetFrame()));
		}

		private void OnEventRefreshItem()
		{
			if (DataCenter.Instance.guideData.isGuide)
			{
				var levelBtn = m_view.m_levelBtn;
				levelBtn.visible = CheckFuncOpen(FunctionID.MAP);
				var leveltechBtn = m_view.m_taskRewardBtn;
				leveltechBtn.visible = CheckFuncOpen(FunctionID.LEVEL_TECH);
			}
		
			var adBtn = m_view.m_AdBtn;
			adBtn.visible = 16.IsOpend(false);
			RefreshAdBtn();

			m_view.m_likeBtn.visible = 23.IsOpend(false);
			
			m_view.m_skillBtn.visible		= CheckFuncOpen(FunctionID.TECH);
			m_view.m_equipBtn.visible		= CheckFuncOpen(FunctionID.ROLE_EQUIP);
			
			// 处理左右列表
			UpdateUIState();
			OnUpdatePiggyProgress();
		}

		void OnRighMenuClick(EventContext context)
		{
			var item = (context.sender as GComponent).data as CheckingManager.CheckItem;
			item.OpenUI();
		}

		/// <summary>
		/// 显示列表图片
		/// </summary>
		/// <param name="datas"></param>
		/// <param name="index"></param>
		/// <param name="item"></param>
		private void RenderNormalItem(List<CheckingManager.CheckItem> datas, int index, GObject item)
		{
			var config = datas[index];

			UI_ActBtn ui = item as UI_ActBtn;
			ui.data = config;
			
			if (!string.IsNullOrEmpty(config.uiname))
				item.name = config.uiname;
			
			ui.onClick.Set(OnRighMenuClick);
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


		// 金币添加事件
		void OnEventGoldChange()
		{
			SetGoldText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.GOLD)));
			SetDiamondText(Utils.ConvertNumberStr(m_itemProperty.GetNum((int)ItemID.DIAMOND)));
		}
		
		void OnheadBtnClick(EventContext context)
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("setting"));
		}

		Action<bool> m_buffTimer;
		//buff描述tip，应该写个通用的
		void OnBuffShowTipClick(EventContext context) 
		{
			//显示tip
			m_view.m_buff.m_tipState.selectedIndex = (m_view.m_buff.m_tipState.selectedIndex + 1) % 2;

			m_buffTimer?.Invoke(false);
			if(m_view.m_buff.m_tipState.selectedIndex == 1) m_buffTimer = Utils.Timer(3, null, completed: () => { OnBuffFoucsOutClick(null); });
		}

		void OnBuffFoucsOutClick(EventContext context) 
		{
			m_buffTimer?.Invoke(false);
			m_view.m_buff.m_tipState.selectedIndex = 0;
		}

		void OnOpenTotalClick() 
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("totalBoost"));
		}

		void OnRoomLikeClick() 
		{
			Entity popupUI = UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("goodreputation"));
		}

        partial void OnAdBtnClick(EventContext data)
        {
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
        /// 刷新开局局内buff时间
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
				Utils.Timer(time, ()=> 
				{
					time = DataCenter.ExclusiveUtils.GetBuffResiduTime();
					m_view.m_buff.m_time.SetText(Utils.FormatTime(time));
				}, m_view, completed: ()=> BuffTimeFinish());
				OnRefreshTotalState();
			}
		}

		void BuffTimeFinish() 
		{
			m_view.m_buff.m_isTime.selectedIndex = 1;
			m_view.m_buff.visible = false;
			OnRefreshTotalState();
		}

		/// <summary>
		/// 好评buff生效时间
		/// </summary>
		void OnRefreshLikeTime(int likeNum) 
		{
			var validTime = DataCenter.ReputationUtils.GetBuffValidTime();
			m_view.m_likeBtn.m_progress.fillAmount = (float)DataCenter.Instance.reputationData.progress / ReputationModule.Instance.maxLikeNum;
			m_view.m_likeBtn.m_state.selectedIndex = validTime > 0 ? 1 : 0;
			m_view.m_likeBtn.SetIcon(ReputationModule.Instance.icon);
			if (ReputationModule.Instance.roomLikeData.IsValid())
				m_view.m_likeBtn.m_markState.selectedIndex = ReputationModule.Instance.roomLikeData.BuffMark;


			if (validTime > 0)
			{
				Utils.Timer(validTime, () =>
				{
					validTime = DataCenter.ReputationUtils.GetBuffValidTime();
					m_view.m_likeBtn.m_time.SetText(Utils.FormatTime(validTime));
				}, m_view, completed: () => OnLikeFinish());
			}

			if (likeNum > 0) 
			{
				m_view.m_likeBtn.m_add.Play();
				m_view.m_likeBtn.m_num.SetText(string.Format("+{0}", likeNum));
				if (DataCenter.Instance.reputationData.progress >= ReputationModule.Instance.maxLikeNum)
				{
					var data = ReputationModule.Instance.roomLikeData;
					m_view.m_likeBtn.m_info.SetText(string.Format("{0}:{1}", UIListener.Local(data.BuffName),
						string.Format(UIListener.Local(data.BuffDesc),data.BuffValue == 0 ? data.BuffDuration : data.BuffValue,data.BuffDuration)));
					m_view.m_likeBtn.m_play.Play();
				}
			}
			OnRefreshTotalState();
		}

		void OnLikeFinish() 
		{
			m_view.m_likeBtn.m_state.selectedIndex = 0;
			m_view.m_likeBtn.m_progress.fillAmount = 0;
			DataCenter.ReputationUtils.Reset();
			m_view.m_likeBtn.SetIcon(ReputationModule.Instance.icon);
			OnRefreshTotalState();
		}

		void OnRefreshTotalState() 
		{
			m_view.m_totalBtn.visible = ReputationModule.Instance.GetVailedBuffList().Count > 0;
			m_view.m_totalBtn.m_num.text = string.Format("X{0}", ReputationModule.Instance.GetTotalValue());
			EventManager.Instance.Trigger((int)GameEvent.ROOM_BUFF_RESET);
		}

		void OnRefreshAdState() 
		{
			bool networkState = NetworkUtils.IsNetworkReachability();
			m_view.m_AdBtn.enabled = AdModule.Instance.GetBuffTime() > 0 || networkState;
			if (!networkState)
			{
				m_view.m_InvestBtn.visible = false;
				AdModule.Instance.RecordEnterTime(AdType.Invest.ToString());
				return;
			}

			AdModule.Instance.GetAdShowTime(AdType.Invest.ToString(), out bool state, out int time);
			m_view.m_InvestBtn.visible = state;
			if (state) 
			{
				var btn = m_view.m_InvestBtn as UI_InvestMan;
				btn.m_bar.fillAmount = (float)time / DataCenter.AdUtil.GetAdSustainTime(AdType.Invest.ToString());
				AdModule.Instance.GetAdInvestNum(out int itemId, out double num);
				string url = itemId == (int)ItemID.GOLD ? "ui_shop_icon_coin_03" : "ui_shop_icon_gem_02";
				btn.SetText(string.Format("+{0}", Utils.ConvertNumberStr(num)));
				btn.SetIcon(url);
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
			if (time > 0) 
			{
				UIListener.SetTextWithName(m_view.m_AdBtn, "time", Utils.TimeFormat(time));
				timer?.Invoke(false);
				timer = Utils.Timer(time, () =>
				{
					time = AdModule.Instance.GetBuffTime();
					UIListener.SetTextWithName(m_view.m_AdBtn, "time", Utils.TimeFormat(time));
				},m_view, completed: ()=>
				{
					UIListener.SetControllerSelect(m_view.m_AdBtn, "isTime", 0);
					OnRefreshTotalState();
				});
			}
			OnRefreshTotalState();
		}

		void RefreshAdBtn() 
		{
			m_view.m_AdBtn.GetChild("boostTxt").SetText(UIListener.Local("ui_ad_boost") + "x" + AdModule.Instance.GetAdRatio());
			m_view.m_AdBtn.GetChild("timeTxt").SetText("+" + Utils.FormatTime(AdModule.Instance.GetAdDuration(), formats:
				AdModule.Instance.GetAdDuration() % 60 == 0 ? new string[] { "{0}min" } : new string[] { "{0}min{1}s" }));
			OnRefreshTotalState();
		}

		void AppPasueRefresh(bool pause) 
		{
			OnRefeshBuffTime();
			OnRefreshAdTime();
		}

		partial void OnTaskRewardBtnClick(EventContext data)
		{
			"leveltech".Goto();
		}

		partial void OnLevelBtnClick(EventContext data)
		{
			SGame.UIUtils.OpenUI("enterscene", DataCenter.Instance.roomData.current.id + 1);
		}

		partial void UnInitEvent(UIContext context)
		{
			timer?.Invoke(false);
			m_buffTimer?.Invoke(false);
			m_handles.Close();
		}

		partial void OnDiamondClick(EventContext data)
		{
			"shop".Goto();
		}

	}
}
