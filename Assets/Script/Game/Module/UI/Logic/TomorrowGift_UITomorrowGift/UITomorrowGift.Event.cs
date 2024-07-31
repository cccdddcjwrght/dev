
using SGame.UI.Shop;
using SGame.VS;
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	using GameConfigs;
	
	public partial class UITomorrowGift
	{
		UI_Probability probabilityUI
		{
			get { return m_view.m_probablity as UI_Probability; }
		}
		partial void InitEvent(UIContext context){
			m_view.m_btnOK.onClick.Add(OnClickTakeGift);
			probabilityUI.m_bg.onClick.Add(OnRateClose);
			probabilityUI.m_bg.GetChild("close").onClick.Add(OnRateClose);
			m_view.m_timegroup.visible = TomorrowGiftModule.Instance.time > 0;

			if (TomorrowGiftModule.Instance.time > 0)
			{
				GTween.To(0, 1, TomorrowGiftModule.Instance.time)
					.SetTarget(m_view, TweenPropType.None)
					.OnUpdate(UpdateTimeText)
					.OnComplete(() =>
					{
						m_view.m_timegroup.visible = false;
					});
			}

			UpdateTimeText();
			
			m_view.m_gift.Initalize(TomorrowGiftModule.GOOD_ITEM_ID, OnClickShowProbility);
		}

		void UpdateTimeText()
		{
			m_view.m_time.text = Utils.FormatTime((int)TomorrowGiftModule.Instance.time);
		}
		
		partial void UnInitEvent(UIContext context){

		}

		void OnClickTakeGift()
		{
			if (TomorrowGiftModule.Instance.time > 0)
			{
				HudModule.Instance.SystemTips("ui_tomorrowgift_tips1".Local());
				return;
			}
			
			if (TomorrowGiftModule.Instance.TakeGift())
			{
				SGame.UIUtils.CloseUIByID(__id);
				EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
			}
		}

		void OnClickShowProbility()
		{
			ConfigSystem.Instance.TryGet(TomorrowGiftModule.GOOD_ITEM_ID, out ShopRowData config);
			//m_view.m_probablity.visible = true;
			probabilityUI.SetRates(config.GetChestInfoArray());
			probabilityUI.SetIcon(config.ChestOpen);
			probabilityUI.m_show.selectedIndex = 1;
		}

		void OnRateClose()
		{
			probabilityUI.m_show.selectedIndex = 0;
		}
	}
}
