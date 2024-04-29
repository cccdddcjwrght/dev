#if USE_THIRD_SDK
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDK.THSDK;

namespace SGame
{
	partial class Utils
	{
		static float c_ad_timeout = 0;

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

			if (c_ad_timeout == 0)
				c_ad_timeout = GameConfigs.GlobalDesginConfig.GetFloat("ad_timeout", 5);

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
					c = AdWait(() => flag, key, c_ad_timeout);
				UILockManager.Instance.Require(key);
				THSdk.Instance.PlayAd(type, key, (s) =>
				{
					UILockManager.Instance.Release(key);
					if (s) "-1".Tips();
					c?.Invoke(true);
					flag = true;
					Debug.Log("[ad]end!!!");
					try { complete?.Invoke(s, null); }
					catch (Exception e) { log.Error(e.Message + "-" + e.StackTrace); }
				}, timeout: c_ad_timeout);
			}
		}

		static partial void DoCheckPlayAd(string ad, ref bool state)
		{
			state = THSdk.Instance.IsAdLoaded(ad);
		}

		static Action<bool> AdWait(Func<bool> cacncel, string key = null, float wait = 0)
		{
			wait = wait > 0 ? wait : 10;
			Action<bool> timer = null;
			"@ui_ad_waiting".Tips(null, wait);
			timer = Timer(wait, () =>
			{
				//ÍøÂçÒì³£
				if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					timer.Invoke(obj: true);
					"@ui_net_error".Tips();
				}
				else if (cacncel())
					timer?.Invoke(true);
			}, key, completed: () => UILockManager.Instance.Release(key));

			return timer;
		}

	}

}
#endif