﻿using System;
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

		public class RightItem
		{
			public FunctionConfigRowData config; // 配置
			public Func<int>		     second; // 倒计时
		}
		
		private List<RightItem> m_rightOpens = new List<RightItem>();

		/// <summary>
		/// 区域定义
		/// </summary>
		public enum AREA : int
		{
			LEFT = 101,
			RIGHT = 102,
		}

		partial void InitEvent(UIContext context)
		{
			m_setData			= DataCenter.Instance.setData;
			leftList			= m_view.m_leftList.m_left;
			m_rightList			= m_view.m_rightList.m_right;
			leftList.opaque		= false;
			m_rightList.opaque	= false;
			
			var headBtn = m_view.m_head;
			leftList.itemRenderer += RenderListItem;
			leftList.numItems = 4;

			m_rightList.itemRenderer = RenderRightItem;
			
			headBtn.onClick.Add(OnheadBtnClick);
			m_view.m_buff.onClick.Add(OnBuffShowTipClick);
			m_view.m_buff.onFocusOut.Add(OnBuffFoucsOutClick);
			m_view.m_likeBtn.onClick.Add(OnRoomLikeClick);
			m_view.m_totalBtn.onClick.Add(OnOpenTotalClick);

			m_handles += EventManager.Instance.Reg((int)GameEvent.PROPERTY_GOLD, OnEventGoldChange);
			m_handles += EventManager.Instance.Reg((int)GameEvent.GAME_MAIN_REFRESH, OnEventRefreshItem);
			m_handles += EventManager.Instance.Reg(((int)GameEvent.SETTING_UPDATE_HEAD), OnHeadSetting);
			m_handles += EventManager.Instance.Reg((int)GameEvent.ROOM_START_BUFF, OnRefeshBuffTime);
			m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ROOM_LIKE_ADD, OnRefreshLikeTime);

			OnHeadSetting();
			OnEventRefreshItem();
			OnRefeshBuffTime();
			OnRefreshLikeTime(0);
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
				levelBtn.visible = 15.IsOpend(false);
				var leveltechBtn = m_view.m_taskRewardBtn;
				leveltechBtn.visible = 13.IsOpend(false);
			}
		
			var adBtn = m_view.m_AdBtn;
			adBtn.visible = 16.IsOpend(false);
			
			leftList.GetChildAt(0).visible = 10.IsOpend(false);
			leftList.GetChildAt(1).visible = 12.IsOpend(false);
			leftList.GetChildAt(2).visible = 17.IsOpend(false);
			leftList.GetChildAt(3).visible = 14.IsOpend(false);
			
			// 处理好友倒计时间
			var friendItem = leftList.GetChildAt(3) as UI_ShowBtn;
			GTween.Kill(friendItem);
			if (friendItem.visible)
			{
				if (FriendModule.Instance.hiringTime > 0)
				{
					friendItem.m_ctrlTime.selectedIndex = 1;
					GTween.To(0, 1, FriendModule.Instance.hiringTime)
						.SetTarget(friendItem)
						.OnUpdate(() =>
						{
							var t = FriendModule.Instance.hiringTime;
							friendItem.m_content.text = Utils.FormatTime(t);
						})
						.OnComplete(() => friendItem.m_ctrlTime.selectedIndex = 0);
				}
				else
				{
					friendItem.m_ctrlTime.selectedIndex = 0;
				}
			}


			// 处理右列表
			m_rightOpens.Clear();
			var configs = ConfigSystem.Instance.LoadConfig<FunctionConfig>();
			for(int i = 0; i < configs.DatalistLength; i++)
			{
				var item = configs.Datalist(i);
				if (item.Value.Parent == (int)AREA.RIGHT)
				{
					// 明日礼包
					if (item.Value.Id.IsOpend(false))
					{
						TomorrowGiftModule.Instance.UpdateState();
						if (item.Value.Id == 18 && !TomorrowGiftModule.Instance.IsFinished())
						{
							m_rightOpens.Add(new RightItem(){config = item.Value, second = ()=> TomorrowGiftModule.Instance.time});
						}
					}
				}
			}
			m_rightList.numItems = m_rightOpens.Count;
		}

		void OnRighMenuClick(EventContext context)
		{
			//var index = (int)(context.sender as GComponent).data;
			var config = (context.sender as GComponent).data as RightItem;// m_rightOpens[index];
			SGame.UIUtils.OpenUI(config.config.Ui);
			log.Info("click ui=" + config.config.Id);
		}
		
		private void RenderRightItem(int index, GObject item)
		{
			var config = m_rightOpens[index];
			UI_ActBtn ui = item as UI_ActBtn;
			ui.data = config;
			ui.onClick.Set(OnRighMenuClick);
			if (ui == null)
			{
				log.Error("item can not conver=" + config.config.Id);
				return;
			}
			
			// 倒计时
			GTween.Kill(ui);
			var configTime = config.second();
			GTween.To(0, 1, configTime).OnUpdate((GTweener tweener) =>
			{
				ui.m_content.text = Utils.FormatTime(config.second());
			}).SetTarget(ui).OnComplete(() =>
			{
				ui.m_time.visible = false;
				ui.m___redpoint.selectedIndex = 1;
				log.Info("counter dowm zero!");
			});
			
			ui.m_time.visible = configTime > 0;
			ui.m___redpoint.selectedIndex = configTime > 0 ? 0 : 1;
			ui.m_content.text = Utils.FormatTime(configTime);
			ui.icon = config.config.Icon;
		}

		private void RenderListItem(int index, GObject item)
		{
			item.onClick.Add(() =>
			{
				switch (index)
				{
					case 0:
						"shop".Goto();
						break;
					case 1:
						"player".Goto();
						break;
					case 2:
						"technology".Goto();
						break;
					case 3:
						"friend".Goto();
						break;				}
			});
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
				timer = Utils.Timer(time, ()=> 
				{
					time = DataCenter.ExclusiveUtils.GetBuffResiduTime();
					m_view.m_buff.m_time.SetText(Utils.FormatTime(time));
				}, m_view.m_buff, completed: ()=> BuffTimeFinish());
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
			if (validTime > 0)
			{
				timer = Utils.Timer(validTime, () =>
				{
					validTime = DataCenter.ReputationUtils.GetBuffValidTime();
					m_view.m_likeBtn.m_time.SetText(Utils.FormatTime(validTime));
				}, completed: () => OnLikeFinish());
			}

			if (likeNum > 0) 
			{
				m_view.m_likeBtn.m_add.Play();
				m_view.m_likeBtn.m_num.SetText(string.Format("+{0}", likeNum));
			}
			OnRefreshTotalState();
		}

		void OnLikeFinish() 
		{
			m_view.m_likeBtn.m_state.selectedIndex = 0;
			m_view.m_likeBtn.m_progress.fillAmount = 0;
			DataCenter.ReputationUtils.Reset();
			OnRefreshTotalState();
		}

		void OnRefreshTotalState() 
		{
			m_view.m_totalBtn.visible = ReputationModule.Instance.GetVailedBuffList().Count > 0;
			m_view.m_totalBtn.m_num.text = string.Format("X{0}", ReputationModule.Instance.GetTotalValue());
		}

		partial void OnTaskRewardBtnClick(EventContext data)
		{
			"leveltech".Goto();
		}

		partial void OnLevelBtnClick(EventContext data)
		{
			/*
			if (DataCenter.MachineUtil.CheckAllWorktableIsMaxLv())
			{
				SGame.UIUtils.OpenUI("enterscene", DataCenter.Instance.roomData.current.id + 1);
			}
			else
			{
				"@ui_worktable_goto_next_fail".Tips();
			}
			*/
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
