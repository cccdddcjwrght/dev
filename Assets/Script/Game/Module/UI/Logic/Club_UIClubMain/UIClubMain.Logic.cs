
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Club;
    using System.Collections.Generic;

    public partial class UIClubMain
	{
		UI_ClubRewardBody m_RewardPanel;
		List<GameConfigs.ClubRewardRowData> m_RewardDatas;
		List<int[]> m_TopItems;

		partial void InitLogic(UIContext context){
            RequestExcuteSystem.Instance.GetCurrentClubReq().Start();
			m_RewardPanel = (UI_ClubRewardBody)m_view.m_bodyList.GetChildAt(0);
			m_RewardPanel.m_list.itemRenderer = OnRewardItemRenderer;
			m_RewardPanel.m_topList.itemRenderer = OnTopItemRenderer;
			m_RewardPanel.m_topList.onClick.Add(OnClickGetTopReward);
		}

		public void RefreshAll() 
		{
			RefreshClubInfo();
			RefreshActivityTime();
			RefreshRewardList();
			m_view.m_bodyList.scrollPane.SetPercY(1, false);
		}

		/// <summary>
		/// 刷新俱乐部信息
		/// </summary>
		public void RefreshClubInfo() 
		{
			var data = DataCenter.ClubUtil.GetClubCurrentData();
			m_view.m_clubItem.m_clubIcon.SetData(data.icon_id, data.frame_id);
			m_view.m_clubItem.m_ID.SetText("ID:" + data.id);
			m_view.m_clubItem.m_name.SetText(data.title);
			m_view.m_clubItem.m_count.SetText(data.member_list.Count + "/" + data.limit);
		}

		/// <summary>
		/// 刷新奖励状态
		/// </summary>
		public void RefreshRewardList() 
		{
			var periods = DataCenter.ClubUtil.GetClubPeriods();
			if (periods == 0) return;
			m_RewardDatas = DataCenter.ClubUtil.GetCurClubActivityRewards(periods);

			m_RewardPanel.m_list.numItems = m_RewardDatas.Count - 1;
			m_RewardPanel.m_list.ResizeToFit();

			var topCfg = m_RewardDatas[m_RewardDatas.Count - 1];
			m_TopItems = DataCenter.ClubUtil.GetItemReward(topCfg);
			m_RewardPanel.m_topList.numItems = m_TopItems.Count;

			var topRewardData = DataCenter.ClubUtil.GetClubReward(topCfg.Id);
			if (topRewardData.isGet) m_RewardPanel.m_top.selectedIndex = 2;
			else m_RewardPanel.m_top.selectedIndex = PropertyManager.Instance.CheckCount(DataCenter.ClubUtil.GetClubCurrencyId(), topRewardData.target, 1) ? 1 : 0;

			var currencyId = DataCenter.ClubUtil.GetClubCurrencyId();
			var num = PropertyManager.Instance.GetItem(currencyId).num;
			var total = DataCenter.ClubUtil.GetClubTotalProgress();
			m_view.m_value.SetText(string.Format("{0}/{1}", Mathf.Min((float)num, total), total));
			m_view.m_currencyIcon.SetIcon(Utils.GetItemIcon(1, currencyId));

			var totalHeight = m_RewardPanel.m_barbg.height;
			m_RewardPanel.m_bar.height = (totalHeight * (Mathf.Min((float)num, total) / total));
		}

		/// <summary>
		/// 刷新活动时间
		/// </summary>
		public void RefreshActivityTime() 
		{
			var time = DataCenter.ClubUtil.GetResidueTime();
			if (time > 0) 
			{
				Utils.Timer(time, () =>
				{
					time = DataCenter.ClubUtil.GetResidueTime();
					m_view.m_time.SetText(Utils.FormatTime(time));
				}, m_view);
			}
		}

		public void OnClickGetTopReward() 
		{
			var data = DataCenter.ClubUtil.GetClubReward(m_RewardDatas[m_RewardDatas.Count - 1].Id) ;
			if (data != null)  
			{
				RequestExcuteSystem.Instance.ClubRewardGetReq(data.id);
			}
		}
		public void OnRewardItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_ClubStair)gObject;
			var cfg = m_RewardDatas[m_RewardDatas.Count - 2 - index];
			item.m_dir.selectedIndex = index % 2;
			if (cfg.BuffLength <= 0)
			{
				var vals = DataCenter.ClubUtil.GetItemReward(cfg);
				var val = vals[0];
				item.m_reward.SetIcon(Utils.GetItemIcon(val[0], val[1]));
				item.m_reward.m_value.SetText(val[2].ToString());
			}
			else 
			{
				var buffId = cfg.Buff(0);
				//var buffValue = cfg.Buff(1);
				if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(buffId, out var buffCfg)) 
					item.m_reward.SetIcon(buffCfg.Icon);
			}
			var rewardData = DataCenter.ClubUtil.GetClubReward(cfg.Id);
			if (rewardData.isGet) item.m_reward.m_state.selectedIndex = 2;
			else item.m_reward.m_state.selectedIndex = PropertyManager.Instance.CheckCount(DataCenter.ClubUtil.GetClubCurrencyId(), rewardData.target, 1) ? 1 : 0;

			item.m_reward.onClick.Add(()=> 
			{
				RequestExcuteSystem.Instance.ClubRewardGetReq(cfg.Id);
			});
		}

		public void OnTopItemRenderer(int index, GObject gObject) 
		{
			var item = (UI_ClubRewardIcon)gObject;
			var data = m_TopItems[index];
			item.SetIcon(Utils.GetItemIcon(data[0], data[1]));
			item.m_num.SetText(data[2].ToString());
		}

        partial void OnMemberBtnClick(EventContext data)
        {
			SGame.UIUtils.OpenUI("clubmember");
		}

        partial void OnTaskBtnClick(EventContext data)
        {
			SGame.UIUtils.OpenUI("clubtask");
		}

        partial void OnClubItem_LeaveBtnClick(EventContext data)
        {
			//俱乐部信息
			var currentData = DataCenter.ClubUtil.GetClubCurrentData();
			var createId = DataCenter.ClubUtil.GetCreatePlayerId();
			if (createId == DataCenter.Instance.accountData.playerID)
			{
				//解散俱乐部提示
				SGame.UIUtils.Confirm("@ui_club_dismiss_title", string.Format(UIListener.Local("ui_club_hint_quit"), currentData.title), (index) =>
				{
					if (index == 0) RequestExcuteSystem.Instance.QuitClubReq().Start();
				}, new string[] { "@ui_club_btn_dismiss", "@ui_club_btn_confirm1" });
			}
			else 
			{
				//退出俱乐部提示
				SGame.UIUtils.Confirm("@ui_club_title6", string.Format(UIListener.Local("ui_club_hint_quit"), currentData.title), (index) =>
				{
					if (index == 0) RequestExcuteSystem.Instance.QuitClubReq().Start();
				}, new string[] { "@ui_club_btn_cancel2", "@ui_club_btn_confirm2" });
			}

		}

        partial void UnInitLogic(UIContext context){

		}
	}
}
