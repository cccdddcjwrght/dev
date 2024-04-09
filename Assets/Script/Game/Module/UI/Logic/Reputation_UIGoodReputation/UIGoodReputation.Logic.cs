
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
			m_view.m_list.numItems = m_RoomLikeIds.Count;

			RefreshTime();
			RefreshText();
		}

		void OnRendererItem(int index, GObject gObject) 
		{
			var item = gObject as UI_ReputationItem;
			int roomLikeId = m_RoomLikeIds[index];
			if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(roomLikeId, out var roomLikeData)) 
			{
				item.m_icon.SetIcon(roomLikeData.BuffIcon);
				item.m_info.SetText(UIListener.Local(roomLikeData.BuffDesc));
			}
			item.m_inforce.selectedIndex = roomLikeId == DataCenter.Instance.reputationData.cfgId ? 1 : 0;
		}

		//buff效果倒计时
		void RefreshTime() 
		{
			var validTime = DataCenter.ReputationUtils.GetBuffValidTime();
			m_view.m_progress.fillAmount = (float)DataCenter.Instance.reputationData.progress / ReputationModule.Instance.maxLikeNum;
			m_view.m_state.selectedIndex = validTime > 0 ? 1 : 0;
			m_view.SetIcon(ReputationModule.Instance.icon);
			if (validTime > 0) 
			{
				timer = Utils.Timer(validTime, () =>
				{
					validTime = DataCenter.ReputationUtils.GetBuffValidTime();
					m_view.m_time.SetText(Utils.FormatTime(validTime));
				}, completed: ()=> OnTimeFinish());
			}
		}

		void OnTimeFinish() 
		{
			m_view.m_state.selectedIndex = 0;
			m_view.m_progress.fillAmount = 0;
			m_view.m_list.numItems = m_RoomLikeIds.Count;
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
