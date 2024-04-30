using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libx;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
	public partial class RequestExcuteSystem
	{
		private static readonly string c_shop_cfg_path = Assets.updatePath + "/Shop.bytes";

		static private string shop_lock = "shop";

		[InitCall]
		static void InitShop()
		{

			WaitHttp.Request("shop.bytes").NeedBuffer().OnSuccess((w, t) =>
			{
				File.WriteAllBytes(c_shop_cfg_path, w.GetDatas());
			}).OnFail((s) =>
			{
				log.Info("暂时没有启用远程下载配置");
			}).OnCompleted((s) => DataCenter.ShopUtil.Refresh())
			.RunAndWait();

		}

		static public void BuyGoods(int id, Action<bool> call = null)
		{
			UILockManager.Instance.Require(shop_lock);
			0.Delay(() =>
			{
				UILockManager.Instance.Release(shop_lock);
			}, 200);
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
								AdModule.PlayAd(cfg.Price.ToString(), (s) => DoBuyGoods(goods, s, call));
								break;
							case 3:
								Utils.Pay((cfg.Price == 0 ? cfg.Id : cfg.Price).ToString(), (s, t) => DoBuyGoods(goods, s, call));
								break;
							default:
								DoBuyGoods(goods, true, call);
								break;
						}
					}
					break;
				default: code.ToString().ErrorTips(); break;
			}
		}

		static private void DoBuyGoods(ShopGoods goods, bool state, Action<bool> call = null)
		{

			if (state)
			{
				var cfg = goods.cfg;
				var id = goods.id;
				var free = goods.free;
				var items = DataCenter.ShopUtil.GetGoodsItems(id);

				DataCenter.ShopUtil.RecordBuyGoods(id);
				if (cfg.PurchaseType == 2 && free <= 0)
					PropertyManager.Instance.Update(1, 2, cfg.Price, true);
				for (int i = 0; i < items.Count; i++)
					PropertyManager.Instance.Update(items[i][0], items[i][1], items[i][2]);

				EventManager.Instance.Trigger(((int)GameEvent.SHOP_GOODS_BUY_RESULT), id);
			}
			call?.Invoke(state);
		}

	}
}
