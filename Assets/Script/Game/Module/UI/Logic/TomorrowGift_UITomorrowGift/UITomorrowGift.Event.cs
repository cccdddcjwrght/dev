
namespace SGame.UI{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.TomorrowGift;
	using GameConfigs;
	
	public partial class UITomorrowGift
	{
		partial void InitEvent(UIContext context){
			m_view.m_btnOK.onClick.Add(OnClickTakeGift);

			m_view.m_timegroup.visible = TomorrowGiftModule.Instance.time > 0;
			m_view.m_btnOK.grayed = TomorrowGiftModule.Instance.time > 0;
			if (TomorrowGiftModule.Instance.time > 0)
			{
				GTween.To(0, 1, TomorrowGiftModule.Instance.time)
					.SetTarget(m_view, TweenPropType.None)
					.OnUpdate(UpdateTimeText)
					.OnComplete(() =>
					{
						m_view.m_timegroup.visible = false;
						m_view.m_btnOK.grayed = false;
					});
			}

			UpdateItemNums();
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
		}

		void UpdateTimeText()
		{
			m_view.m_time.text = Utils.FormatTime((int)TomorrowGiftModule.Instance.time);
		}
		
		partial void UnInitEvent(UIContext context){

		}

		void OnClickTakeGift()
		{
			if (TomorrowGiftModule.Instance.TakeGift())
			{
				SGame.UIUtils.CloseUIByID(__id);
				EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
			}
		}
	}
}
