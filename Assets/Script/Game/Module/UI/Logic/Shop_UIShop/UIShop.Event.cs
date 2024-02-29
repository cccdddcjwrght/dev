
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
			UIListener.ListenerClose(m_view.m_rate_2.m_bg, new EventCallback1(OnRateClose) , false);
		}

		partial void OnBigGoods_ClickClick(EventContext data)
		{
			RequestExcuteSystem.BuyGoods(1);
		}

		void OnShopRefresh() { }

		void OnGoodsBuyResult(int id)
		{
			if (id == 1) RefreshAdGoods();
			if (_refreshCall.TryGetValue(id, out var call)) call?.Invoke();
		}

		void OnRateClose(EventContext data)
		{
			m_view.m_rate.selectedIndex = 0;
		}
	}
}
