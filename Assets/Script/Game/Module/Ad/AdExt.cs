#if USE_AD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDK.THSDK;

namespace SGame
{
	partial class Utils
	{
		static partial void DoPlayAd(string ad, Action<bool, string> complete, ref bool state)
		{
			state = true;
			if (string.IsNullOrEmpty(ad))
			{
				complete?.Invoke(false, "ad is null");
				return;
			}
			var load = ad[0] == '@';
			ad = ad.Replace("@", "");
			if (string.IsNullOrEmpty(ad)) return;

			var key = ad;
			var type = EnumAD.Inner;

			if (ConfigSystem.Instance.TryGet(ad, out GameConfigs.ADConfigRowData cfg))
			{
				type = (EnumAD)cfg.Type;
				key = string.IsNullOrEmpty(cfg.Ad) ? ad : cfg.Ad;
			}

			if (load)
				THSdk.Instance.Preload(type, key);
			else
			{
				var flag = false;
				var c = default(Action<bool>);
				if (type != EnumAD.Banner)
					c = AdWait(() => flag);

				UILockManager.Instance.Require(key);
				THSdk.Instance.PlayAd(type, key, (s) =>
				{
					Debug.Log("[ad]end!!!");
					complete?.Invoke(s, null);
					UILockManager.Instance.Release(key);

					if (type != EnumAD.Banner)
					{ flag = true; c?.Invoke(false); }
				});
			}
		}

		static Action<bool> AdWait(Func<bool> cacncel)
		{
			var time = 0f;
			Action<bool> timer = null;
			timer = Timer(100, () =>
			{
				if (cacncel()) timer?.Invoke(false);
				else if ((time -= Time.deltaTime) < 0)
				{
					time = 5f;
					"@ui_ad_waiting".Tips();
				}
			}, delay: 2f);

			return timer;
		}

	}

}
#endif