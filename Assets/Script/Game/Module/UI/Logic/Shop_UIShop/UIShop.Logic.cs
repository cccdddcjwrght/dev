
namespace SGame.UI
{
	using System;
	using FairyGUI;
	using UnityEngine;
	using SGame;
	using SGame.UI.Shop;
	using System.Collections.Generic;
	using System.Linq;
	using Cs;
	using GameConfigs;

	public partial class UIShop
	{
		private const int _ad_goods_id = 1;

		private int _currentLevel = 0;
		private List<ShopGoods> _gifts;

		private Dictionary<int, Action> _refreshCall = new Dictionary<int, Action>();

		partial void BeforeInit(UIContext context)
		{
			DataCenter.ShopUtil.Refresh();
			m_view.m_content.m_gifts.itemRenderer = SetGiftItem;
		}

		partial void InitLogic(UIContext context)
		{
			Refresh();
		}

		partial void UnInitLogic(UIContext context)
		{
			_refreshCall?.Clear();
		}

		void Refresh()
		{
			RefreshAdGoods();
			RefreshGifts();
			RefreshGoods();
		}

		void RefreshAdGoods()
		{
			var goods = DataCenter.Instance.shopData.goodDic[_ad_goods_id];
			m_view.m_content.m_adgood.m_saled.selectedIndex = DataCenter.IsIgnoreAd() ? 1 : 0;
			m_view.m_content.m_adgood.m_click.SetText(goods.pricestr);
		}

		void RefreshGifts()
		{
			_gifts = DataCenter.ShopUtil.GetShopGoodsByArea(2);
			_gifts.RemoveAll(g => g.IsSaled());
			m_view.m_content.m_gifts.RemoveChildrenToPool();
			m_view.m_content.m_page.RemoveChildrenToPool();
			m_view.m_content.m_pages.ClearPages();
			m_view.m_content.m_gifts.numItems = _gifts.Count;
			m_view.m_content.m_page.numItems = _gifts.Count;
			_gifts.ForEach(g => m_view.m_content.m_pages.AddPage(g.id.ToString()));
			m_view.m_pages.selectedIndex = 0;
		}

		void RefreshGoods()
		{
			var list = m_view.m_content.m_goods;
			list.RemoveChildrenToPool();
			for (var i = EnumShopArea.Item; i <= EnumShopArea.Diamond; i++)
			{
				var goods = DataCenter.ShopUtil.GetShopGoodsByArea((int)i);
				if (goods?.Count > 0)
				{
					var div = list.AddItemFromPool("ui://Shop/Div");
					div.text = i.ToString().ToLower().Local("ui_shop_div_");
					for (int j = 0; j < goods.Count; j++)
						SGame.UIUtils.AddListItem(list, SetGoodsInfo, goods[j]);
				}
			}
		}

		void SetGiftItem(int index, GObject gObject)
		{

			var g = _gifts[index];
			var v = gObject as UI_BigGoods;
			v.m_left_state.selectedIndex = !string.IsNullOrEmpty(g.cfg.MarkValue) ? 1 : 0;
			v.SetIcon(g.cfg.Icon);
			v.SetTextByKey(g.cfg.ShopName);
			v.m_left.SetText(g.cfg.MarkValue, false);
			v.m_desc.SetTextByKey(g.cfg.ShopName);
			v.m_click.SetText(g.pricestr);
			v.m_click.onClick?.Clear();
			v.m_click.onClick.Add(() => OnGoodsClick(g));
			v.m_items.RemoveChildrenToPool();
			SGame.UIUtils.AddListItems(v.m_items, DataCenter.ShopUtil.GetGoodsItems(g.id), OnSetGoodsItem);

			if (g.cfg.LimitType != 0)
			{
				_refreshCall[g.id] = () =>
				{
					v.m_count.SetText((g.cfg.LimitNum - g.buy) + "/" + g.cfg.LimitNum, false);
					v.m_saled.selectedIndex = g.buy >= g.cfg.LimitNum ? 1 : 0;
				};
				_refreshCall[g.id].Invoke();
			}
			else
				v.m_count.visible = false;
		}

		void SetGoodsInfo(int index, object data, GObject gObject)
		{
			var g = data as ShopGoods;
			var v = gObject as UI_Goods;
			v.SetTextByKey(g.cfg.ShopName);
			v.SetIcon(g.cfg.Icon, "Icon");
			v.m_desc.SetTextByKey(g.cfg.ShopDes);
			v.m_type.selectedIndex = g.type - 1;

			v.onClick.Clear();
			v.onClick.Add(() => OnGoodsClick(g));

			v.m_tips.onClick.Clear();
			v.m_tips.onClick.Add((e) =>
			{
				e.StopPropagation();
				OnTipsClick(g);
			});

			DoRefreshGoodsInfo(data, gObject);

			_refreshCall[g.id] = () => DoRefreshGoodsInfo(data, gObject);
		}

		void DoRefreshGoodsInfo(object data, GObject gObject)
		{
			var g = data as ShopGoods;
			var v = gObject as UI_Goods;
			var time = g.CDTime();

			v.m_cd.selectedIndex = time > 0 ? 1 : 0;
			v.m_saled.selectedIndex = g.IsSaled() ? 1 : 0;

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
				v.m_cd.selectedIndex = 1;
				v.m_time.SetText(Utils.FormatTime(time), false);
				Utils.Timer(time, () =>
				{
					v.m_time.SetText(Utils.FormatTime(g.CDTime()), false);
				}, gObject, completed: () => DoRefreshGoodsInfo(data, gObject));
			}
		}

		void OnGoodsClick(ShopGoods goods)
		{
			RequestExcuteSystem.BuyGoods(goods.id);
		}


		void OnSetGoodsItem(int index, object data, GObject gObject)
		{
			var val = data as int[];
			gObject.SetText("X" +Utils.ConvertNumberStr(val[2]));
			gObject.SetIcon(Utils.GetItemIcon(val[0], val[1]));
		}

		void OnTipsClick(ShopGoods goods)
		{
			var rates = goods.cfg.GetChestInfoArray();
			m_view.m_rate_2.m_list.RemoveChildrenToPool();

			SGame.UIUtils.AddListItems<int>(m_view.m_rate_2.m_list, rates, (index, v, g) =>
			{
				UIListener.SetControllerSelect(g, "color", index);
				g.SetText(string.Format("{0:p2}", (int)v * 0.0001f), false);
			});
			m_view.m_rate.selectedIndex = 1;
		}
	}
}
