
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
    using libx;
    using SGame.UI.Common;
    using System.Collections;
    using SGame.Http;
    using global::Http;

    public partial class UIRankMain
	{
		RankData m_Data = DataCenter.Instance.rankData;
		partial void InitLogic(UIContext context){
	
			m_view.m_list.itemRenderer = OnRendererItem;
			m_view.m_list.SetVirtual();
			m_view.m_list.onClickItem.Add(OnClickRank);

			//m_view.SetText(UIListener.Local())


			LoadTestData();
			//FiberCtrl.Pool.Run(Run());
		}

		IEnumerator Run() 
		{
			HttpPackage pkg = new HttpPackage();
			Score score = new Score() { tips = 10 };
			pkg.data = JsonUtility.ToJson(score);
			var result = HttpSystem.Instance.Post("http://192.168.10.109:8080/rank", pkg.ToJson());
			yield return result;
			if (!string.IsNullOrEmpty(result.error))
			{
				log.Error("rank data fail=" + result.error);
				yield break;
			}
			pkg = JsonUtility.FromJson<HttpPackage>(result.data);
			DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(pkg.data);
			m_view.m_list.numItems = m_Data.rankDatas.Count;
		}

		public void LoadSelfRankData() 
		{
			RankModule.Instance.GetSelfData(out RankItemData data, out int rank);
			if (data != null) UpdateItem(m_view.m_self, data, rank);
		}

		public void LoadTestData() 
		{
			const string fileName = "Assets/BuildAsset/Json/TestRankData.txt.bytes";
			var req = Assets.LoadAsset(fileName, typeof(TextAsset));
			var data = (req.asset as TextAsset).text;
			DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(data);
			m_view.m_list.numItems = m_Data.rankDatas.Count;
		}

		public void OnRendererItem(int index, GObject gObject)
		{
			var data = m_Data.rankDatas[index];
			int rank = index + 1;

			UpdateItem(gObject, data, rank);
		}

		void UpdateItem(GObject gObject, RankItemData data, int rank) 
		{
			var item = (UI_RankItem)gObject;
			item.data = data.player_id;
			item.m_rank.text = rank.ToString();
			item.m_rankIndex.selectedIndex = rank > 3 ? 3 : rank - 1;
			item.m_name.text = data.name;
			(item.m_head as UI_HeadBtn).SetHeadIcon(data.icon_id, data.frame_id);
			item.m_value.text = data.score.tips.ToString();

			var rankConfig = RankModule.Instance.GetRankConfig(1, rank);
			if (rankConfig.IsValid())
			{
				var list = Utils.GetArrayList(rankConfig.GetReward1Array, rankConfig.GetReward2Array, rankConfig.GetReward3Array);
				item.m_list.itemRenderer = (int index, GObject gObject) =>
				{
					var reward = (UI_RankReward)gObject;
					var d = list[index];
					reward.SetIcon(Utils.GetItemIcon(d[0], d[1]));
					reward.SetText(Utils.ConvertNumberStr(d[2]), false);
				};
				item.m_list.numItems = list.Count;
			}
		}

		void OnClickRank(EventContext context)
		{
			var clickBtn = context.data as UI_RankItem;
			if (clickBtn == null) return;

			var player_id = (int)clickBtn.data;
			SGame.UIUtils.OpenUI("frienddetail", new UIParam() { Value = player_id });
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
