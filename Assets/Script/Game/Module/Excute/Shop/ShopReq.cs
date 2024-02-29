using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libx;
using UnityEngine;
using UnityEngine.UIElements;

namespace SGame
{
	public partial class RequestExcuteSystem
	{
		private static readonly string c_shop_cfg_path = Assets.updatePath + "/Shop.bytes";

		[InitCall]
		static void InitShop()
		{

			WaitHttp.Request("shop.bytes").NeedBuffer().OnSuccess((w, t) =>
			{
				File.WriteAllBytes(c_shop_cfg_path, w.GetDatas());
			}).OnFail((s) =>
			{
				log.Info("暂时没有启用远程下载配置");
			}).RunAndWait();

		}

		static public void BuyGoods(int id)
		{
			var code = DataCenter.ShopUtil.IsCanBuy(id);
			switch (code)
			{
				case 0:

					var goods = DataCenter.Instance.shopData.goodDic[id];

					if (goods != null)
					{
						var cfg = goods.cfg;
						var pt = goods.free > 0 ? 0 : cfg.PurchaseType;
						switch (pt)
						{
							case 1:
								if (!DataCenter.IsIgnoreAd())
									Utils.PlayAd(cfg.Price.ToString(), (s, t) => DoBuyGoods(goods));
								else
								{
									log.Info("no ad , free");
									DoBuyGoods(goods);
								}
								break;
							case 3:
								Utils.Pay((cfg.Price == 0 ? cfg.Id : cfg.Price).ToString(), (s, t) => DoBuyGoods(goods));
								break;
							default:
								DoBuyGoods(goods);
								break;
						}
					}
					break;
				default: code.ToString().ErrorTips(); break;
			}
		}

		static private void DoBuyGoods(ShopGoods goods)
		{

			var cfg = goods.cfg;
			var id = goods.id;
			var items = DataCenter.ShopUtil.GetGoodsItems(id);

			DataCenter.ShopUtil.RecordBuyGoods(id);
			if (cfg.PurchaseType == 2)
				PropertyManager.Instance.Update(1, 2, cfg.Price, true);
			for (int i = 0; i < items.Count; i++)
				PropertyManager.Instance.Update(items[i][0], items[i][1], items[i][2]);

			EventManager.Instance.Trigger(((int)GameEvent.SHOP_GOODS_BUY_RESULT), id);

		}

	}
}
