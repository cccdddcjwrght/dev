
namespace SGame.UI
{
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.MonsterHunter;
	using SGame.UI.Shop;
	using System.Collections.Generic;

	public partial class UITempShop
	{
		private List<ShopGoods> _goodsList;

		partial void InitLogic(UIContext context)
		{

			var actSubID = context.GetParam().Value.To<object[]>().Val<int>(0);
			if (actSubID <= 0)
				DoCloseUIClick(null);
			else
			{
				_goodsList = DataCenter.ShopUtil.GetShopGoodsByType(actSubID);
				RefreshGoods();
			}

		}

		partial void UnInitLogic(UIContext context)
		{

		}

		void RefreshGoods()
		{
			m_view.m_list.RemoveChildrenToPool();
			for (int j = 0; j < _goodsList.Count; j++)
				SGame.UIUtils.AddListItem(m_view.m_list, SetGoodsInfo, _goodsList[j]);
		}

		void SetGoodsInfo(int index, object data, GObject gObject)
		{
			var g = data as ShopGoods;
			var v = gObject as UI_Goods;
			var ty = g.type - 1;
			gObject.name = g.id.ToString();
			v.SetTextByKey(g.cfg.ShopName);
			v.SetIcon(g.cfg.Icon, "Icon");
			v.m_desc.SetTextByKey(g.cfg.ShopDes);
			v.m_type.selectedIndex = ty > 4 ? 1 : ty;
			v.m_hidebottom.selectedIndex = g.type == (int)EnumShopArea.Eq ? 0 : 1;
			v.m_click.onClick.Clear();
			v.m_click.onClick.Add((context) => OnGoodsClick(g, context, gObject));
			DoRefreshGoodsInfo(data, gObject);
		}

		void DoRefreshGoodsInfo(object data, GObject gObject)
		{
			var g = data as ShopGoods;
			var v = gObject as UI_Goods;
			var time = g.CDTime();

			v.m_saled.selectedIndex = 0;
			if (g.free > 0)
			{
				v.m_currency.selectedIndex = 0;
				v.m_price.SetTextByKey("ui_shop_free");
			}
			else
			{
				switch (g.cfg.PurchaseType)
				{
					case 1:
						v.enabled = NetworkUtils.IsNetworkReachability();
						v.m_currency.selectedIndex = 3;
						v.m_price.SetText((g.cfg.LimitNum - g.buy) + "/" + g.cfg.LimitNum);
						break;
					case 2:
						v.m_currency.selectedIndex = 2;
						v.m_price.SetText(g.cfg.Price.ToString());
						break;
					case 3:
						v.m_currency.selectedIndex = 4;
						v.m_price.SetText(g.cfg.Price.ToString());
						break;
				}
			}
			if (time > 0)
			{
				v.m_time.SetText(Utils.FormatTime(time), false);
				Utils.Timer(time, () =>
				{
					v.m_time.SetText(Utils.FormatTime(g.CDTime()), false);
				}, gObject, completed: () => DoRefreshGoodsInfo(data, gObject));
			}

			v.m_cd.selectedIndex = time > 0 ? 1 : 0;
			v.m_saled.selectedIndex = g.IsSaled() ? 1 : 0;
		}

		void OnGoodsClick(ShopGoods goods, EventContext context, GObject gObject)
		{
			GObject btn = context.sender as GObject;
			RequestExcuteSystem.BuyGoods(goods.id, (state) =>
			{
				if (!state) return;
				DoRefreshGoodsInfo(goods, gObject);
			});
		}

	}
}
