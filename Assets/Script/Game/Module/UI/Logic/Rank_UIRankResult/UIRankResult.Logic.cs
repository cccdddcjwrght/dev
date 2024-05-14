
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
    using System.Collections.Generic;

    public partial class UIRankResult
	{
		List<int[]> list;
		partial void InitLogic(UIContext context){

			var param = (object[])context.gameWorld.GetEntityManager().GetComponentObject<UIParam>(context.entity).Value;
			var data = (RankUIParam)param[0];
			var rankConfig = RankModule.Instance.GetRankConfig(data.marker, data.rank);

			list = Utils.GetArrayList(rankConfig.GetReward1Array, rankConfig.GetReward2Array, rankConfig.GetReward3Array);
            for (int i = 0; i < list.Count; i++)
				PropertyManager.Instance.Insert2Cache(new List<double[]>() { new double[] { list[i][0], list[i][1], list[i][2] }});

			m_view.m_list.itemRenderer = (int index, GObject gObject) =>
			{
				var reward = (UI_ResultReward)gObject;
				var d = list[index];
				reward.SetIcon(Utils.GetItemIcon(d[0], d[1]));
				reward.SetText(Utils.ConvertNumberStr(d[2]), false);
			};
			m_view.m_list.numItems = list.Count;
			m_view.m_title.SetText(string.Format(UIListener.Local("ui_ranking_4"), data.rank));
			m_view.m_tip.SetText(string.Format(UIListener.Local("ui_ranking_3"), data.rank));

			m_view.onClick.Add(() => SGame.UIUtils.CloseUIByID(__id));
		}

		partial void UnInitLogic(UIContext context){
			PropertyManager.Instance.CombineCache2Items();
		}
	}
}
