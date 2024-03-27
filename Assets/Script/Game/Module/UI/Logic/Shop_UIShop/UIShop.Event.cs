
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Shop;

	public partial class UIShop
	{
		partial void InitEvent(UIContext context)
		{
			EventManager.Instance.Reg<int>(((int)GameEvent.SHOP_GOODS_BUY_RESULT), OnGoodsBuyResult);
			EventManager.Instance.Reg(((int)GameEvent.SHOP_REFRESH), OnShopRefresh);
			UIListener.ListenerClose(m_view.m_rate_2.m_bg, new EventCallback1(OnRateClose));
		}

		partial void UnInitEvent(UIContext context)
		{
			EventManager.Instance.UnReg<int>(((int)GameEvent.SHOP_GOODS_BUY_RESULT), OnGoodsBuyResult);
			EventManager.Instance.UnReg(((int)GameEvent.SHOP_REFRESH), OnShopRefresh);
			UIListener.ListenerClose(m_view.m_rate_2.m_bg, new EventCallback1(OnRateClose), false);
		}

		partial void OnBigGoods_Content_adgood_clickClick(EventContext data)
		{
			RequestExcuteSystem.BuyGoods(1);
		}

		void OnShopRefresh() { }

		void OnGoodsBuyResult(int id)
		{
			if (id == 1) RefreshAdGoods();
			else
			{
				var goods = DataCenter.Instance.shopData.goodDic[id];
				if (goods.IsSaled() && goods.cfg.LimitNum != 0)
				{
					switch (goods.type)
					{
						case 2:
							RefreshGifts();
							break;
						case 4:
						case 5:
						case 3:
							RefreshGoods();
							break;
					}
					return;
				}
				if (_refreshCall.TryGetValue(id, out var call)) call?.Invoke();
			}
		}

		void OnRateClose(EventContext data)
		{
			m_view.m_rate.selectedIndex = 0;
		}
	}
}
