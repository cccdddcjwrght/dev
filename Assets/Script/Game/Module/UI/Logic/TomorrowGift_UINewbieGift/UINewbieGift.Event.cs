
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	using SGame;
	using SGame.UI.Shop;
	using GameConfigs;
	
	public partial class UINewbieGift
	{
		UI_Probability probabilityUI
		{
			get { return m_view.m_probablity as UI_Probability; }
		}

		
		partial void InitEvent(UIContext context){
			if (!ConfigSystem.Instance.TryGet(NewbieGiftModule.GOOD_ITEM_ID, out ShopRowData config))
			{
				log.Error("goold id not found=" + NewbieGiftModule.GOOD_ITEM_ID);
				return;
			}

			probabilityUI.m_bg.onClick.Add(OnRateClose);
			probabilityUI.m_bg.GetChild("close").onClick.Add(OnRateClose);
			m_view.m_btnOK.onClick.Add(OnClickTakeGift);

			m_view.m_btnOK.text = LanagueSystem.Instance.GetValue(config.ShopName);
			
			m_view.m_gift.Initalize(NewbieGiftModule.GOOD_ITEM_ID, OnRateOpen);
			
			// 更新每日自动打开时间
			DataCenter.SetIntValue(NewbieGiftModule.DAY_TIME_KEY, GameServerTime.Instance.nextDayTime);
			m_view.m_btnOK.text = DataCenter.ShopUtil.GetGoodsPriceStr(config.Id, config.Price);
			// var daytime = DataCenter.GetIntValue(NewbieGiftModule.DAY_TIME_KEY, 0);
		}
		
		partial void UnInitEvent(UIContext context){

		}
		
		void OnRateOpen()
		{
			ConfigSystem.Instance.TryGet(NewbieGiftModule.GOOD_ITEM_ID, out ShopRowData config);
			probabilityUI.SetRates(config.GetChestInfoArray());
			probabilityUI.m_show.selectedIndex = 1;
		}

		void OnClickTakeGift()
		{
			if (NewbieGiftModule.Instance.TakeGift())
			{
				SGame.UIUtils.CloseUIByID(__id);
			}
		}
		
		void OnRateClose()
		{
			probabilityUI.m_show.selectedIndex = 0;
		}
	}
}
