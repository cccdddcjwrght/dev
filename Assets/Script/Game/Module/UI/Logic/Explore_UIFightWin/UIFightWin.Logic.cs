
using FairyGUI;

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
			48.ToAudioID().PlayAudio();
			m_view.m_rewardList1.itemRenderer = OnRewardItemRenderer;
			m_view.m_rewardList2.itemRenderer = OnAdRewardItemRenderer;
			var level = DataCenter.Instance.battleLevelData.showLevel;
			ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(level, out var config);
			awardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId1Array(), config.GetRewardNum1Array(), config.GetRewardodds1Array());
			adAwardList = DataCenter.BattleLevelUtil.GetShowReward(config.GetRewardId2Array(), config.GetRewardNum2Array(), config.GetRewardodds2Array());
			m_view.m_rewardList1.numItems = awardList.Count;
			m_view.m_rewardList2.numItems = adAwardList.Count;
		}

		void OnRewardItemRenderer(int index, GObject gObject) 
		{
			var item = gObject as UI_FightReward;
			item.SetData(awardList[index][1], awardList[index][2], awardList[index][3]);
		}

		void OnAdRewardItemRenderer(int index, GObject gObject) 
		{
			var item = gObject as UI_FightReward;
			item.SetData(adAwardList[index][1], adAwardList[index][2], adAwardList[index][3]);
		}

        partial void OnGetBtnClick(EventContext data)
        {
			RequestExcuteSystem.BattleAward();
			SGame.UIUtils.CloseUIByID(__id);
        }

        partial void OnAdBtnClick(EventContext data)
        {
			AdModule.PlayAd("explore_battle_ad", (s) =>
			{
				if (s)
				{
					RequestExcuteSystem.BattleAdAward();
					SGame.UIUtils.CloseUIByID(__id);
				}
			});
		}

        partial void UnInitLogic(UIContext context){

		}
	}


	
}

namespace SGame.UI.Explore 
{
	public partial class UI_FightReward
	{
		public void SetData(int id, int val, int odds = 100)
		{
			this.SetIcon(Utils.GetItemIcon(1, id));
			this.SetText(val.ToString());

			m_isTip.selectedIndex = odds < 100 ? 1 : 0;
			this.onClick.Set(()=> 
			{
				if (odds < 100) 
				{
					var obj = UIPackage.CreateObject("Explore", "FightTip");
					obj.onRemovedFromStage.Add(() => obj.Dispose());
					GRoot.inst.ShowPopup(obj, this, PopupDirection.Up);

					var xy = obj.xy + new UnityEngine.Vector2(80, 50);
					obj.xy = xy;
					obj.SetText(odds + "%");

					if (obj.x + obj.width > GRoot.inst.width)
					{
						obj.SetPivot(1, 0, true);
						UIListener.SetControllerSelect(obj, "state", 1);
						obj.xy = xy;
					}
				}
			});
		}
	}
}

