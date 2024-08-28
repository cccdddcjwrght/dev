
namespace SGame.UI{
	using FairyGUI;
	using SGame;
    using SGame.UI.Explore;
    using System.Collections.Generic;
    using System.Linq;

    public partial class UIFightWin
	{
		private List<int[]> awardList;
		private List<int[]> adAwardList;

		partial void InitLogic(UIContext context){
			m_view.m_rewardList1.itemRenderer = OnRewardItemRenderer;
			m_view.m_rewardList2.itemRenderer = OnAdRewardItemRenderer;
			var level = DataCenter.Instance.battleLevelData.showLevel;
			ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(level, out var config);
			awardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId1Array(), config.GetRewardNum1Array());
			adAwardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId2Array(), config.GetRewardNum2Array());
			m_view.m_rewardList1.numItems = awardList.Count;
			m_view.m_rewardList2.numItems = adAwardList.Count;
		}

		void OnRewardItemRenderer(int index, GObject gObject) 
		{
			var item = gObject as UI_FightReward;
			item.SetData(awardList[index][1], awardList[index][2]);
		}

		void OnAdRewardItemRenderer(int index, GObject gObject) 
		{
			var item = gObject as UI_FightReward;
			item.SetData(adAwardList[index][1], adAwardList[index][2]);
		}

        partial void OnGetBtnClick(EventContext data)
        {
			RequestExcuteSystem.BattleAward();
			SGame.UIUtils.CloseUIByID(__id);
        }

        partial void OnAdBtnClick(EventContext data)
        {
			RequestExcuteSystem.BattleAdAward();
			SGame.UIUtils.CloseUIByID(__id);
		}

        partial void UnInitLogic(UIContext context){

		}
	}


	
}

namespace SGame.UI.Explore 
{
	public partial class UI_FightReward
	{
		public void SetData(int id, int val)
		{
			this.SetIcon(Utils.GetItemIcon(1, id));
			this.SetText(val.ToString());
		}
	}
}

