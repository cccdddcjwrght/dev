
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
    using System.Collections.Generic;

    public partial class UIRankResult
	{
		int index = 0;
		RankReward[] rankRewards;
		List<int[]> list;
		partial void InitLogic(UIContext context){
			var objs = (object[])context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			var param = (UIParam)objs[0];
			rankRewards = (RankReward[])param.Value;

			var rewardData = rankRewards[0];
			SetRankReward(rewardData);
			m_view.onClick.Add(OnClickClose);
		}

		public void SetRankReward(RankReward rankReward) 
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.RankConfigRowData>((r) => r.RankingId == rankReward.id, out var data))
			{
				var rankConfig = RankModule.Instance.GetRankConfig(data.RankingMarker, rankReward.rank);
				list = Utils.GetArrayList(rankConfig.GetReward1Array, rankConfig.GetReward2Array, rankConfig.GetReward3Array);
				for (int i = 0; i < list.Count; i++)
					PropertyManager.Instance.Insert2Cache(new List<double[]>() { new double[] { list[i][0], list[i][1], list[i][2] } });

				m_view.m_list.itemRenderer = (int index, GObject gObject) =>
				{
					var reward = (UI_ResultReward)gObject;
					var d = list[index];
					reward.SetIcon(Utils.GetItemIcon(d[0], d[1]));
					reward.SetText(Utils.ConvertNumberStr(d[2]), false);
				};
				m_view.m_list.numItems = list.Count;
				m_view.m_title.SetText(string.Format(UIListener.Local("ui_ranking_4"), rankReward.rank));
				m_view.m_tip.SetText(string.Format(UIListener.Local("ui_ranking_3"), rankReward.rank));
			}
		}

		public void OnClickClose() 
		{
			index++;
			if (index < rankRewards.Length) 
			{
				SetRankReward(rankRewards[index]);
				return;
			}
			SGame.UIUtils.CloseUIByID(__id);
		}

		partial void UnInitLogic(UIContext context){
			PropertyManager.Instance.CombineCache2Items();
		}
	}
}
