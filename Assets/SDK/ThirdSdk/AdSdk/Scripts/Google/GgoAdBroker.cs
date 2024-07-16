/***************************
 * 作者:      #Nicholas#
 * 最后修改时间:
 * 描述:
 * 
**/
#if USE_AD
using GoogleMobileAds.Api;
using System.Collections.Generic;
using ThirdSdk;

namespace Ad
{

	public class GgoAdBroker
	{
		/// <summary>
		/// 生成facebook广告单元
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		internal static ADUnitBase GenUnit(AdUnitRawData info)
		{
			switch (info.adType)
			{
				case AD_TYPE.BANNER:
					return GgoBannerView.GenUint(info);
				case AD_TYPE.INTERSTITIAL:
					return GgoInterstitialAd.GenUint(info);
				case AD_TYPE.VIDEO:
					return GgoRewardBasedVideoAd.GenUint(info);
				default:
					break;
			}
			return null;
		}

		// Returns an ad request with custom ad targeting.
		internal static AdRequest CreateAdRequest()
		{
			return new AdRequest();
		}

		internal static void InitMob(AdCfg cfg)
		{
			//TODO:在此添加测试设备
			List<string> deviceIds = new List<string>();
			deviceIds.Add("C476DE84E3DAA3A1F7785F0070D7CF91");
			RequestConfiguration reqConfig = new RequestConfiguration();
			reqConfig.TestDeviceIds = deviceIds;
			MobileAds.SetRequestConfiguration(reqConfig);

			// Initialize the Google Mobile Ads SDK.
			MobileAds.RaiseAdEventsOnUnityMainThread = true;
			MobileAds.Initialize(HandleInitCompleteAction);
		}

		private static void HandleInitCompleteAction(InitializationStatus initstatus)
		{
			ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_ADMOB_INIT_COMPLETE);

#if USE_ADMOB_INSOECTOR
            MobileAds.OpenAdInspector((AdInspectorError error)=> { });
#endif
		}
	}

	/// <summary>
	/// 封装底条
	/// </summary>
	internal class GgoBannerView : ADUnitBase, IBanner
	{
		private BannerView bannerView;

		/// <summary>
		/// 生成广告单元
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public static GgoBannerView GenUint(AdUnitRawData info)
		{
			GgoBannerView banner = new GgoBannerView();
			banner.Init(info);
			return banner;
		}

		internal override void Dispose()
		{
			bannerView.Destroy();
			base.Dispose();
		}

		internal override void DisposeRawUnit()
		{
			base.DisposeRawUnit();
			DestroyBanner();
		}

		internal override void RebuildRawUnit()
		{
			base.RebuildRawUnit();
			DestroyBanner();
		}

		private void Init(AdUnitRawData info)
		{
			//初始化单元信息 TODO:"game"改到外部
			this.InitUint(info);
		}

		private void DestroyBanner()
		{
			if (bannerView != null)
			{
				bannerView.Hide();
				bannerView.Destroy();
				bannerView = null;
			}
		}

		private void InitBanner()
		{
			if (bannerView != null)
			{
				bannerView.Hide();
				bannerView.Destroy();
			}

			// Create a banner at the bottom of the screen.
			AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
			this.bannerView = new BannerView(_key, adaptiveSize, AdPosition.Bottom);
			// Register for ad events.
			this.bannerView.OnBannerAdLoaded += this.HandleAdLoaded;
			this.bannerView.OnBannerAdLoadFailed += this.HandleAdFailedToLoad;
			this.bannerView.OnAdPaid += this.HandleAdPaidEvent;
		}

		protected override void Show()
		{
			base.Show();
			bannerView.Show();
		}

		internal override void LoadAd()
		{
			if (_curState == AD_UNIT_STATE.CLOSED)
			{
				OnLoadFeedback();
				return;
			}
			InitBanner();
			base.LoadAd();
			bannerView.LoadAd(GgoAdBroker.CreateAdRequest());
		}

		public void CloseOnShow()
		{
			//防止反复重建
			if (_curState == AD_UNIT_STATE.INIT)
			{
				return;
			}
			RebuildRawUnit();
		}

		public void HideBanner()
		{
			if (bannerView != null)
				bannerView.Hide();
		}

		public void ShowBanner()
		{
			if (bannerView != null)
				bannerView.Show();
		}

		//begin 底条广告
		private void HandleAdLoaded()
		{
			OnLoaded();
			OnImpression();
		}

		private void HandleAdFailedToLoad(LoadAdError args)
		{
			string errMsg = args.GetMessage();
			OnError(errMsg);
		}


		private void HandleAdPaidEvent(AdValue args)
		{
			OnPaid((int)args.Precision, args.Value);
		}
	}

	/// <summary>
	/// 封装插屏
	/// </summary>
	internal class GgoInterstitialAd : ADUnitBase
	{
		private InterstitialAd interstitial;

		/// <summary>
		/// 生成广告单元
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public static GgoInterstitialAd GenUint(AdUnitRawData info)
		{
			GgoInterstitialAd ad = new GgoInterstitialAd();
			ad.Init(info);
			return ad;
		}

		internal override void Dispose()
		{
			DestroyAd();
			base.Dispose();
		}

		internal override void LoadAd()
		{
			base.LoadAd();
			DestroyAd();
			InterstitialAd.Load(_key, GgoAdBroker.CreateAdRequest(), HandleInterstitialLoaded);
		}

		private void DestroyAd()
		{
			if (this.interstitial == null)
				return;

			this.interstitial.Destroy();
			this.interstitial = null;
		}

		protected override void Show()
		{
			ShowInterstitial();
		}

		/// <summary>
		/// 抽成接口 方便需要时可以进行延迟调用
		/// </summary>
		private void ShowInterstitial()
		{
			if (this.interstitial != null && this.interstitial.CanShowAd())
			{
				base.Show();
				this.interstitial.Show();
				OnImpression();
			}
			else
			{
				OnError("check show error");
			}
		}

		private void Init(AdUnitRawData info)
		{
			InitUint(info);
		}

#region Interstitial callback handlers

		private void HandleInterstitialLoaded(InterstitialAd ad, LoadAdError args)
		{
			ProcessInterstitialLoaded(ad, args);
		}

		private void ProcessInterstitialLoaded(InterstitialAd ad, LoadAdError args)
		{
			if (args == null && ad != null)
			{
				this.interstitial = ad;
				RegisterEventHandlers();
				OnLoaded();
			}
			else
			{
				string errMsg = args.GetMessage();
				OnError(errMsg);
			}
		}


		private void RegisterEventHandlers()
		{
			if (this.interstitial == null)
				return;

			this.interstitial.OnAdPaid += HandleInterstitialPaidEvent;
			this.interstitial.OnAdFullScreenContentClosed += HandleInterstitialClosed;
			this.interstitial.OnAdFullScreenContentFailed += HandleInterstitialShowFailed;
		}


		private void HandleInterstitialClosed()
		{
			OnClose();
		}

		private void HandleInterstitialPaidEvent(AdValue args)
		{
			OnPaid((int)args.Precision, args.Value);
		}

		private void HandleInterstitialShowFailed(AdError args)
		{
			OnError(args.GetMessage());
		}

#endregion
	}

	internal class GgoRewardBasedVideoAd : ADUnitBase
	{
		//control
		private RewardedAd rewardAd;

		/// <summary>
		/// 生成广告单元
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public static GgoRewardBasedVideoAd GenUint(AdUnitRawData info)
		{
			GgoRewardBasedVideoAd ad = new GgoRewardBasedVideoAd();
			ad.Init(info);
			return ad;
		}

		internal override void Dispose()
		{
			DestroyAd(); ;
			base.Dispose();
		}

		private void DestroyAd()
		{
			if (this.rewardAd == null)
				return;

			this.rewardAd.Destroy();
			this.rewardAd = null;
		}

		internal override void LoadAd()
		{
			base.LoadAd();
			DestroyAd();
			UnityEngine.Debug.LogWarning($"[ad]{_key} sdkload");
			RewardedAd.Load(_key, GgoAdBroker.CreateAdRequest(), HandleRewardVideoLoaded);
		}

		protected override void Show()
		{
			if (this.rewardAd != null && this.rewardAd.CanShowAd())
			{
				base.Show();
				this.rewardAd.Show(HandleRewardVideoEarnedRewarded);
				OnImpression();
			}
			else
			{
				OnError("check show error");
				ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_AD_VIDEO_STATE_CHANGE, isAvaiable);
			}
		}

		private void Init(AdUnitRawData info)
		{
			InitUint(info);
		}



		//begin 激励视频回调
		private void HandleRewardVideoLoaded(RewardedAd ad, LoadAdError args)
		{
			ProcessRewardVideoLoaded(ad, args);
			ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_AD_VIDEO_STATE_CHANGE, isAvaiable);
		}

		private void ProcessRewardVideoLoaded(RewardedAd ad, LoadAdError args)
		{
			UnityEngine.Debug.LogWarning($"[ad]{_key} sdkload end:{args?.ToString()} ==== {args?.GetMessage()}");
			if (args == null && ad != null)
			{
				this.rewardAd = ad;
				RegisterEventHandlers();
				OnLoaded();
			}
			else
			{
				string errMsg = args.GetMessage();
				OnError(errMsg);
			}
		}

		private void RegisterEventHandlers()
		{
			if (this.rewardAd == null)
				return;

			this.rewardAd.OnAdPaid += HandleRewardVideoPaidEvent;
			this.rewardAd.OnAdFullScreenContentClosed += HandleRewardVideoClosed;
			this.rewardAd.OnAdFullScreenContentFailed += HandleRewardVideoFailedToShow;
		}

		private void HandleRewardVideoFailedToShow(AdError args)
		{
			string errMsg = args.GetMessage();
			OnError(errMsg);
			ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_AD_VIDEO_STATE_CHANGE, isAvaiable);
		}


		private void HandleRewardVideoClosed()
		{
			OnClose();
			ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_AD_VIDEO_STATE_CHANGE, isAvaiable);
		}

		private void HandleRewardVideoEarnedRewarded(Reward args)
		{
			OnVideoReward();
		}

		private void HandleRewardVideoPaidEvent(AdValue args)
		{
			OnPaid((int)args.Precision, args.Value);
		}
	}
}
#endif