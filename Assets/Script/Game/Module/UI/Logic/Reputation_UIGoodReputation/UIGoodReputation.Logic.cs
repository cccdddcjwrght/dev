﻿
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Reputation;
    using System.Collections.Generic;
    using System;

    public partial class UIGoodReputation
	{
		Action<bool> timer;

		List<int> m_RoomLikeIds;
		partial void InitLogic(UIContext context)
		{
			m_RoomLikeIds = DataCenter.Instance.reputationData.randomBuffs?.Count <= 0 ?
				DataCenter.ReputationUtils.GetRandomBuffList() :
				DataCenter.Instance.reputationData.randomBuffs;
			DataCenter.Instance.reputationData.randomBuffs = m_RoomLikeIds;

			m_view.m_list.itemRenderer = OnRendererItem;

			RefreshTime();
			RefreshText();
		}

		void OnRendererItem(int index, GObject gObject) 
		{
			var item = gObject as UI_ReputationItem;
			int roomLikeId = m_RoomLikeIds[index];
			if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(roomLikeId, out var roomLikeData)) 
			{
				var buffIcon = roomLikeData.BuffIcon;
				if (buffIcon == string.Empty)
				{
					if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(roomLikeData.BuffId, out var buffData)) 
						buffIcon = buffData.Icon;
				}
				item.m_icon.SetIcon(buffIcon);
				item.m_info.SetText(string.Format(UIListener.Local(roomLikeData.BuffDesc),
					roomLikeData.BuffValue == 0 ? roomLikeData.BuffDuration : roomLikeData.BuffValue,
					roomLikeData.BuffDuration));
				item.m_markState.selectedIndex = roomLikeData.BuffMark;
			}
			item.m_inforce.selectedIndex = roomLikeId == DataCenter.Instance.reputationData.cfgId ? 1 : 0;
		}

		//buff效果倒计时
		void RefreshTime() 
		{
			m_view.m_list.numItems = m_RoomLikeIds.Count;
			var validTime = DataCenter.ReputationUtils.GetBuffValidTime();
			m_view.m_progress.fillAmount = (float)DataCenter.Instance.reputationData.progress / ReputationModule.Instance.maxLikeNum;
			m_view.m_state.selectedIndex = validTime > 0 ? 1 : 0;
			m_view.SetIcon(ReputationModule.Instance.icon);
			if (ReputationModule.Instance.roomLikeData.IsValid()) 
				m_view.m_markState.selectedIndex = ReputationModule.Instance.roomLikeData.BuffMark;
			
			if (validTime > 0) 
			{
				timer = Utils.Timer(validTime, () =>
				{
					validTime = DataCenter.ReputationUtils.GetBuffValidTime();
					m_view.m_time.SetText(Utils.FormatTime(validTime));
				});
			}
		}

		void OnTimeFinish() 
		{
			if (DataCenter.ReputationUtils.GetBuffValidTime() <= 0) 
			{
				m_view.m_state.selectedIndex = 0;
				m_view.m_progress.fillAmount = 0;
				m_view.m_list.numItems = m_RoomLikeIds.Count;
			}
		}

		void RefreshText() 
		{
			m_view.m_body.title = UIListener.Local("ui_like_title_1");
			m_view.m_info.SetText(UIListener.Local("ui_like_body_1"));
		}

		partial void UnInitLogic(UIContext context){
			timer?.Invoke(false);
		}
	}
}
