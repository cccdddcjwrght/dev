
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Explore;
    using System.Collections.Generic;

    public partial class UIFightRewardPreview
	{
		private List<int[]> _rewardList;
		private bool _isAd;

		partial void InitLogic(UIContext context){
			_isAd = (bool)context.GetParam()?.Value.To<object[]>().Val<bool>(0);

			var level = DataCenter.Instance.battleLevelData.showLevel;
			ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(level, out var config);
			if (config.IsValid())
			{
				if(_isAd) _rewardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId2Array(), config.GetRewardNum2Array());
				else _rewardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId1Array(), config.GetRewardNum1Array());
			}

			m_view.m_list1.itemRenderer = OnItemRenderer;
			m_view.m_list2.itemRenderer = OnItemRenderer;

			m_view.m_ad.selectedIndex = _isAd ? 1 : 0;
			if (_isAd) m_view.m_list2.numItems = _rewardList.Count;
			else m_view.m_list1.numItems = _rewardList.Count;
		}

		void OnItemRenderer(int index, GObject gObject) 
		{
			var item = gObject as UI_FightReward;
			item.SetData(_rewardList[index][1], _rewardList[index][2]);
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
