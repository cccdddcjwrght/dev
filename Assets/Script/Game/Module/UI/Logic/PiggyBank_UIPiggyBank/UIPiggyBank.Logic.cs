
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

			m_view.m_progress.m_midValue.SetText(DataCenter.PiggyBankUtils.PIGGYBANK_MID.ToString());
			m_view.m_progress.m_maxValue.SetText(DataCenter.PiggyBankUtils.PIGGYBANK_MAX.ToString());
			m_view.m_progress.max = DataCenter.PiggyBankUtils.PIGGYBANK_MAX;
			m_view.m_progress.m_valueIcon.x = m_view.m_progress.width * ((float)DataCenter.PiggyBankUtils.PIGGYBANK_MID / DataCenter.PiggyBankUtils.PIGGYBANK_MAX);

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
			PlayHammerTranslation();
		}

		public void PlayHammerTranslation() 
		{
			m_view.m_hammer.visible = true;
			bool isReset = DataCenter.Instance.piggybankData.stage == 0;
			m_view.m_hammer.m_hammer.Play(() =>
			{
				if (isReset) ResetEffect();
				else RefreshAll();
			});
			Utils.Timer(0.5f, null, m_view, completed: () => EffectSystem.Instance.AddEffect(200001, m_view.m___effect));
			Utils.Timer(1.75f, null, m_view, completed: () => m_view.m_hammer.visible = false);
		}

		/// <summary>
		/// 存钱罐重置特效
		/// </summary>
		public void ResetEffect() 
		{
			//破损状态
			Utils.Timer(0.2f, null, m_view, completed: () => m_view.m_stage.selectedIndex = 6);
			Utils.Timer(1f, null, m_view, completed: () =>
			{
				TransitionModule.Instance.PlayFlight(m_view.m_icon, (int)FlightType.DIAMOND);
				EffectSystem.Instance.AddEffect(200002, m_view.m___effect);
				Utils.Timer(0.5f, null, m_view, completed: () =>
				{
					RefreshAll();
				});
			});
		}


		partial void UnInitLogic(UIContext context){

		}
	}
}
