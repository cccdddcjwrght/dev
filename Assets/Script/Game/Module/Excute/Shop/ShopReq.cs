﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameConfigs;
using libx;
using UnityEngine;

namespace SGame
{
	public partial class RequestExcuteSystem
	{
		private static readonly string c_shop_cfg_path = Assets.updatePath + "/Shop.bytes";

		static private string shop_lock = "shop";
		static private int[] G_GAME_BUFF;

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
			var code = DataCenter.ShopUtil.IsCanBuy(id);
			if (code == 4) "shop".Goto();
			switch (code)
			{
				case 0:
					UILockManager.Instance.Require(shop_lock);
					SceneCameraSystem.Instance.disableTouch = true;
					var goods = DataCenter.Instance.shopData.goodDic[id];
					if (goods != null)
					{
						var cfg = goods.cfg;
						var pt = goods.free > 0 ? 0 : cfg.PurchaseType;
						switch (pt)
						{
							case 1:
#if !UNITY_EDITOR
								AdModule.PlayAd(cfg.Price.ToString(), (s) => DoBuyGoods(goods, s, call));
#else
								Utils.PlayAd(cfg.Price.ToString(), (s, t) => DoBuyGoods(goods, s, call));
#endif
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
				default: 
					code.ToString().ErrorTips();
					call?.Invoke(false);
					break;
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
				/*for (int i = 0; i < items.Count; i++)
					PropertyManager.Instance.Update(items[i][0], items[i][1], items[i][2]);*/

				Utils.ShowRewards(items);
				EventManager.Instance.Trigger(((int)GameEvent.SHOP_GOODS_BUY_RESULT), id);
				if(id == 1)
				{

				}
			}
			call?.Invoke(state);
			UILockManager.Instance.Release(shop_lock);
			SceneCameraSystem.Instance.disableTouch = false;


		}

	}
}
