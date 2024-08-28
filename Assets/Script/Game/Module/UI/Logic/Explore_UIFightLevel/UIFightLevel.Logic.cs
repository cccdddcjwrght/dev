
namespace SGame.UI{
	using FairyGUI;
	using SGame;
    using SGame.UI.Explore;

    public partial class UIFightLevel
	{
		private int level;
		private BattleAttritube attribute = new BattleAttritube();
		partial void InitLogic(UIContext context){
			level = (int)context.GetParam()?.Value.To<object[]>().Val<int>(0);

			m_view.m_tip.onClick.Add(OnTipBtnClick);
			m_view.m_reward1.onClick.Add(OnRewardPreBtnClick);
			m_view.m_reward2.onClick.Add(OnRewardPreAdBtnClick);

			UpdateInfo();
		}

		public void UpdateInfo() 
		{
			ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(level, out var config);
			if (config.IsValid()) 
			{
				ConfigSystem.Instance.TryGet<GameConfigs.BattleRoleRowData>(config.Monster, out var roleConfig);
				m_view.m_name.SetText(UIListener.Local(roleConfig.Name));
				m_view.SetIcon(roleConfig.Icon);
				attribute.ReadAttribute(config.Monster);

				var fightAttr = attribute.GetFightAttr();
				m_view.m_list.SetFightAttrList(fightAttr);
				var fightValue = DataCenter.ExploreUtil.CaluPower(fightAttr.ToArray());
				m_view.m_meet.selectedIndex = DataCenter.Instance.exploreData.explorer.GetPower() > fightValue ? 0 : 1;
				m_view.m_fight.SetText(Utils.ConvertNumberStr(fightValue));
			}
		}

		void OnTipBtnClick() 
		{
			SGame.UIUtils.OpenUI("fightinfo", attribute.GetFightAttr());
		}

		void OnRewardPreBtnClick() 
		{
			SGame.UIUtils.OpenUI("fightrewardpre", false);
		}

		void OnRewardPreAdBtnClick()
		{
			SGame.UIUtils.OpenUI("fightrewardpre", true);
		}

		partial void OnBattleBtnClick(EventContext data)
        {
			RequestExcuteSystem.BattleBegin();
			SGame.UIUtils.CloseUIByID(__id);
        }

        partial void UnInitLogic(UIContext context){

		}
	}
}
