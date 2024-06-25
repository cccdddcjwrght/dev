
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
			m_view.m_item2.m_btnInfo.onClick.Add(OnClickShowProbility);
			
			//UIListener.ListenerClose(probabilityUI.m_bg, new EventCallback0(OnRateClose));
			probabilityUI.m_bg.onClick.Add(OnRateClose);
			probabilityUI.m_bg.GetChild("close").onClick.Add(OnRateClose);


			m_view.m_timegroup.visible = TomorrowGiftModule.Instance.time > 0;
			//m_view.m_btnOK.grayed = TomorrowGiftModule.Instance.time > 0;
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

			UpdateItemNums();
			UpdateTimeText();
		}

		/// <summary>
		/// 初始化道具数量
		/// </summary>
		void UpdateItemNums()
		{
			if (!ConfigSystem.Instance.TryGet(TomorrowGiftModule.GOOD_ITEM_ID, out ShopRowData config))
			{
				log.Error("good id not found=" + TomorrowGiftModule.GOOD_ITEM_ID);
				return;
			}

			if (config.Item1Length != 3 || config.Item2Length != 3)
			{
				log.Error("item num not match!");
				return;
			}
            
			var itemGroup = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			m_view.m_item1.m_title.text = config.Item1(2).ToString();
			m_view.m_item2.m_title.text = config.Item2(2).ToString();
			var icon1 = Utils.GetItemIcon(config.Item1(0), config.Item1(1));
			var icon2 = Utils.GetItemIcon(config.Item2(0), config.Item2(1));
			m_view.m_item1.SetIcon(icon1);
			m_view.m_item2.SetIcon(icon2);
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
