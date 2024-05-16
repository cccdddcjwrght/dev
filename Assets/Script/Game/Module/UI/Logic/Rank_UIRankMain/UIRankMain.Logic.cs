
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Rank;
    using libx;
    using SGame.UI.Common;
    using System.Collections;

    public partial class UIRankMain
	{
		RankData m_Data = DataCenter.Instance.rankData;
		partial void InitLogic(UIContext context){
	
			m_view.m_list.itemRenderer = OnRendererItem;
			m_view.m_list.SetVirtual();
			m_view.m_list.onClickItem.Add(OnClickRank);

			LoadText();
			LoadTime();

			m_view.m_state.selectedIndex = 2;

			FiberCtrl.Pool.Run(RankModule.Instance.ReqRankList(true));
			FiberCtrl.Pool.Run(RankModule.Instance.ReqRankData());
			//LoadTestData();
		}

		public void LoadText() 
		{
			var config = RankModule.Instance.GetCurRankConfig();
			if (config.IsValid()) 
			{
				m_view.m_body.SetText(UIListener.Local(config.Name));
				m_view.m_tip.SetText(UIListener.Local(config.Tips));
				m_view.m_bg.icon = "ui://Rank/" + config.Icon;
			}
			m_view.m_noRank.SetText(UIListener.Local("ui_ranking_2"));
		}

		public void LoadTime() 
		{
			int time = RankModule.Instance.GetRankTime();
			if (time > 0) 
			{
				Utils.Timer(time, () =>
				{
					time = RankModule.Instance.GetRankTime();
					if (time < 60) m_view.m_time.SetText(string.Format("{0}S", time));
					else m_view.m_time.SetText(Utils.FormatTime(time));
				}, m_view, completed: () => SGame.UIUtils.CloseUIByID(__id));
			}
		}


		public void OnUpdateRankData() 
		{
			LoadSelfRankData();
			m_view.m_list.numItems = DataCenter.Instance.rankData.list.Count;
		}

		public void LoadSelfRankData() 
		{
			RankModule.Instance.GetSelfData(out RankItemData data, out int rank);
			m_view.m_state.selectedIndex = data != null ? 0 : 1;
			if (data == null)
			{ 
				data = new RankItemData()
				{
					icon_id = DataCenter.Instance.accountData.head,
					frame_id = DataCenter.Instance.accountData.frame,
					score = new RankScore(), 
				};
			}
			data.name = DataCenter.Instance.accountData.playerName;
			if (data.name?.Length > 7)
				data.name = data.name.Substring(0, 7) + "...";
			UpdateItem(m_view.m_self, data, rank, true);
		}

		public void LoadTestData() 
		{
			const string fileName = "Assets/BuildAsset/Json/TestRankData.txt.bytes";
			var req = Assets.LoadAsset(fileName, typeof(TextAsset));
			var data = (req.asset as TextAsset).text;
			DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(data);
			m_view.m_list.numItems = DataCenter.Instance.rankData.list.Count;
		}

		public void OnRendererItem(int index, GObject gObject)
		{
			var data = DataCenter.Instance.rankData.list[index];
			int rank = index + 1;

			UpdateItem(gObject, data, rank);
		}

		void UpdateItem(GObject gObject, RankItemData data, int rank, bool isSelf = false) 
		{
			var item = (UI_RankItem)gObject;
			item.data = data.player_id;

			item.m_rank.text = rank == -1 ? "" : rank.ToString();
			//bool isSelf = DataCenter.Instance.accountData.playerID == data.player_id;
			if (isSelf) item.m_rankIndex.selectedIndex = 4;
			else item.m_rankIndex.selectedIndex = rank > 3 ? 3 : rank - 1;

			item.m_name.text = data.name;
#if UNITY_EDITOR
			if (string.IsNullOrEmpty(data.name))
				item.m_name.text = "Player";
#endif	
			(item.m_head as UI_HeadBtn).SetHeadIcon(data.icon_id, data.frame_id);
			item.m_value.text = RankModule.Instance.GetScoreValue(data.score).ToString();

			var rankConfig = RankModule.Instance.GetRankConfig(data.score.type, rank);
			if (rankConfig.IsValid())
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.ItemRowData>(rankConfig.ItemId, out var d)) 
					item.m_tag.SetIcon(d.Icon);

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

			var player_id = (long)clickBtn.data;
			if (player_id == DataCenter.Instance.accountData.playerID)
				return;

			SGame.UIUtils.OpenUI("rankdetail", new UIParam() { Value = player_id });
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
