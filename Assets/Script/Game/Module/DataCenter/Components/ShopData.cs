using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;

namespace SGame
{
	partial class Error_Code
	{
		/// <summary>
		/// 商品处于cd中
		/// </summary>
		public const int SHOP_IS_CD = 201;
		/// <summary>
		/// 商品购买上限
		/// </summary>
		public const int SHOP_BUY_LIMIT = 202;
	}

	partial class DataCenter
	{

		public ShopData shopData = new ShopData();

		/// <summary>
		/// 免广告
		/// </summary>
		/// <returns></returns>
		public static bool IsIgnoreAd()
		{
			return ShopUtil.IsIgnoreAd();
		}

		public static class ShopUtil
		{
			static private ShopData _data { get { return Instance.shopData; } }

			static public void InitShop()
			{
				var level = Instance.roomData.current.id;
				var olds = _data.goods.ToDictionary(s => s.id);
				var cfgs = GetShopCfgs(level, 1 << 1 | 1 << 2 | 1 << 3 | 1 << 4 | 1 << 5, 9999);
				var list = new List<ShopGoods>();
				if (cfgs != null && cfgs.Count > 0)
				{
					for (int i = 0; i < cfgs.Count; i++)
					{
						var cfg = cfgs[i];
						if (!olds.TryGetValue(cfg.Id, out var goods))
						{
							goods = new ShopGoods()
							{
								id = cfg.Id,
								free = cfg.FreeTime,
								type = cfg.Area,
							};
						}
						RefreshGoods(goods, cfg);
						list.Add(goods);
					}
				}
				_data.goodDic?.Clear();
				_data.goods = list;
				_data.isInited = true;
				_data.goodDic = list.ToDictionary(g => g.id);
				Refresh();
			}

			static public void Refresh()
			{
				if (!_data.isInited)
					InitShop();
				else if (_data.day != GameServerTime.Instance.serverDay)
				{
					_data.day = GameServerTime.Instance.serverDay;
					_data.goods.ForEach((g) =>
					{
						RefreshGoods(g, default);
						g.cd = 0;
						g.free = g.cfg.FreeTime;
						//永久限购
						if (g.cfg.LimitType != 2) g.buy = 0;
					});
					EventManager.Instance.Trigger(((int)GameEvent.SHOP_REFRESH));
				}
			}

			static public List<ShopGoods> GetShopGoodsByArea(int area)
			{
				return _data.goods.FindAll(g => g.type == area);
			}

			static public List<int[]> GetGoodsItems(int goods)
			{
				if (_data.goodDic.TryGetValue(goods, out var g))
				{
					var ls = new List<int[]>();
					if (g.cfg.Item1Length > 0)
						ls.Add(g.cfg.GetItem1Array());
					if (g.cfg.Item2Length > 0)
						ls.Add(g.cfg.GetItem2Array());
					if (g.cfg.Item3Length > 0)
						ls.Add(g.cfg.GetItem3Array());
					if (g.cfg.Item4Length > 0)
						ls.Add(g.cfg.GetItem4Array());
					return ls;
				}
				return default;
			}

			static public int IsCanBuy(int id)
			{
				if (_data.goodDic.TryGetValue(id, out var goods))
				{
					if (goods.cd > GameServerTime.Instance.serverTime)
						return Error_Code.SHOP_IS_CD;
					else if (goods.cfg.LimitNum > 0 && goods.buy >= goods.cfg.LimitNum)
						return Error_Code.SHOP_BUY_LIMIT;
					else if (goods.cfg.PurchaseType == 2 && !PropertyManager.Instance.CheckCount(2, goods.cfg.Price, 1))
						return Error_Code.ITEM_DIAMOND_NOT_ENOUGH;
				}
				return default;
			}

			static public ShopGoods RecordBuyGoods(int id)
			{
				if (_data.goodDic.TryGetValue(id, out var goods))
				{
					if (goods.free > 0)
					{
						goods.free--;
						if (goods.free > 0) goods.cd = GameServerTime.Instance.serverTime + goods.cfg.FreeCd;
					}
					else if (goods.cfg.LimitNum > 0)
					{
						goods.buy += 1;
						goods.cd = goods.buy >= goods.cfg.LimitNum ? GameServerTime.Instance.nextDayTime : GameServerTime.Instance.serverTime +  goods.cfg.Cd;
					}
				}
				return goods;
			}

			/// <summary>
			/// 免广告
			/// </summary>
			/// <returns></returns>
			static public bool IsIgnoreAd()
			{
				return _data.goods[0].buy > 0;
			}

			static public List<ShopRowData> GetShopCfgs(int level, int type, int count = 1)
			{
				if (type > 0)
					return ConfigSystem.Instance.Finds<ShopRowData>((s) => (count--) >= 0 && (1 << s.Area).IsInState(type) && (level < 0 || (s.Unlock(0) <= level && s.Unlock(1) >= level)));
				return default;
			}

			static public string GetGoodsPriceStr(int id, float price = 0)
			{
				return price.ToString();
			}

			static private void RefreshGoods(ShopGoods goods, ShopRowData cfg)
			{
				if (cfg.IsValid() || ConfigSystem.Instance.TryGet<ShopRowData>(goods.id, out cfg))
				{
					goods.cfg = cfg;
					goods.price = cfg.Price;
					goods.pricestr = cfg.PurchaseType == 3 ? GetGoodsPriceStr(cfg.Id, cfg.Price) : default;
				}
			}

		}

	}

	[System.Serializable]
	public class ShopData
	{

		public int day;
		public List<ShopGoods> goods = new List<ShopGoods>();

		[System.NonSerialized]
		public bool isInited;
		[System.NonSerialized]
		public Dictionary<int, ShopGoods> goodDic;
	}


	[System.Serializable]
	public class ShopGoods
	{
		public int id;
		public int type;
		public float price;
		public string pricestr;

		public int free;//免费次数
		public int buy;//非免费购买次数
		public int cd;//下次购买时间

		[System.NonSerialized]
		public ShopRowData cfg;

		public int CDTime()
		{
			return cd - GameServerTime.Instance.serverTime;
		}

		public bool IsSaled()
		{
			return cfg.LimitNum > 0 && buy >= cfg.LimitNum;
		}

	}


}