
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.PiggyBank;
    using Unity.Entities;

    public partial class UIPiggyBank
	{
		private UIContext m_context;
		EntityManager EntityManager
		{
			get
			{
				return m_context.gameWorld.GetEntityManager();
			}
		}

		partial void InitLogic(UIContext context){
			m_context = context;

			m_view.m_progress.max = DataCenter.PiggyBankUtils.PIGGYBANK_MAX;
			RefreshAll();
			RefreshText();
		}

		void RefreshAll() 
		{
			RefreshProgress();
			RefreshStage();
			RefreshTime();
		}

		void RefreshProgress() 
		{
			var data = DataCenter.Instance.piggybankData;
			m_view.m_progress.value = data.progress;
			m_view.m_progress.m_value.SetText(data.progress.ToString());
			m_view.m_state.selectedIndex = data.progress >= DataCenter.PiggyBankUtils.PIGGYBANK_MID ?
				data.progress >= DataCenter.PiggyBankUtils.PIGGYBANK_MAX ? 2 : 1 : 0;

			m_view.m_buyBtn.enabled = DataCenter.PiggyBankUtils.CheckBuyPiggyBank();
			string priceStr = DataCenter.PiggyBankUtils.GetNextFreeRefreshTime() > 0 ?
				DataCenter.PiggyBankUtils.shopRowData.Price.ToString() : UIListener.Local("ui_piggybank_btn");
			m_view.m_buyBtn.SetText(priceStr);
		}

		void RefreshStage() 
		{
			m_view.m_stage.selectedIndex = DataCenter.Instance.piggybankData.stage;
		}

		void RefreshTime() 
		{
			int time = DataCenter.PiggyBankUtils.GetNextFreeRefreshTime();
			if (time > 0)
			{
				m_view.m_time.visible = true;
				Utils.Timer(time, () =>
				{
					time = DataCenter.PiggyBankUtils.GetNextFreeRefreshTime();
					m_view.m_time.SetText(string.Format(UIListener.Local("ui_piggybank_btn_tips"), Utils.FormatTime(time)));
				}, m_view, completed: () => { m_view.m_time.visible = false; });
			}
		}

		void RefreshText() 
		{
			m_view.m_body.SetText(UIListener.Local("ui_piggybank_title"));
			m_view.m_tip.SetText(UIListener.Local("ui_piggybank_tips"));
		}

        partial void OnTipBtnClick(EventContext data) 
		{
			UIRequest.Create(EntityManager, SGame.UIUtils.GetUI("usehelp"));
		}

		partial void OnBuyBtnClick(EventContext data) 
		{
			DataCenter.PiggyBankUtils.BuyPiggyBank();
		}

		partial void UnInitLogic(UIContext context){

		}
	}
}
