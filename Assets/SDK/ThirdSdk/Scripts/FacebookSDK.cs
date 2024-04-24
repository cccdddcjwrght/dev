using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if USE_THIRD_SDK
using Facebook.Unity;
#endif

namespace ThirdSdk
{
    public class FacebookSDK
    {
#region Inst
        private static readonly FacebookSDK _inst = new FacebookSDK();
        static FacebookSDK() { }
        public static FacebookSDK inst { get { return _inst; } }
        #endregion

        private InternalAnalysticsCacher _eventCache = new InternalAnalysticsCacher();

        /// <summary>
        /// SDK初始化
        /// </summary>
        public void Init()
        {
#if USE_THIRD_SDK
            if (!FB.IsInitialized)
            {
                FB.Init(OnInitCallback);
            }
            else
            {
                ActivateAppHandle(true);
            }
#endif
        }


        public void OnActive()
        {
#if USE_THIRD_SDK
            if (FB.IsInitialized)
            {
                ActivateAppHandle(false);
            }
#endif
        }


        /// <summary>
        /// 初始化回调
        /// </summary>
        private void OnInitCallback()
        {
#if USE_THIRD_SDK
            if (FB.IsInitialized)
            {
                ActivateAppHandle(true);
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
#endif
        }

        private void ActivateAppHandle(bool isInit)
        {
#if USE_THIRD_SDK
            FB.ActivateApp();
            CheckSendCacheEvent();
            ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_FB_INIT_COMPLETE);
            CheckFetchDeferredAppLinkData(isInit);
#endif
        }

        private void CheckFetchDeferredAppLinkData(bool isInit)
        {
#if USE_THIRD_SDK
            if (!isInit)
                return;

            if (PlayerPrefs.HasKey(InternalPrefsDef.FB_APP_LINK_CACHE))
                return;

            FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);
#endif
        }

        /// <summary>
        /// 深度链接回调
        /// </summary>
#if USE_THIRD_SDK
        void DeepLinkCallback(IAppLinkResult result)
        {
            string deepLinkUrl = string.IsNullOrEmpty(result.TargetUrl) ? "" : result.TargetUrl;
            PlayerPrefs.SetString(InternalPrefsDef.FB_APP_LINK_CACHE, deepLinkUrl);
            ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_FB_DEEP_LINK_URL, deepLinkUrl);
        }
#endif

        /// <summary>
        /// 发送数据分析记录事件
        /// </summary>
        public void SendAnalyticsEvent(string eventName, IDictionary<string, object> dicParam)
        {
#if USE_THIRD_SDK
            SendAnalyticsEventDirect(eventName, dicParam);
#endif
        }

        /// <summary>
        /// 发送数据分析记录事件
        /// </summary>
        public void SendAnalyticsEventDirect(string eventName, IDictionary<string, object> dicParam)
        {
#if USE_THIRD_SDK
            if (!FB.IsInitialized)
            {
                 _eventCache.AppendCache(eventName, dicParam);
                return;
            }

            if (dicParam != null)
            {
                Dictionary<string, object> lstParams = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> entry in dicParam)
                {
                    if (entry.Value != null)
                        lstParams.Add(entry.Key, entry.Value.ToString());
                    else
                        lstParams.Add(entry.Key, "");
                }
                FB.LogAppEvent(eventName, 0.0f, lstParams);
            }
            else
            {
                FB.LogAppEvent(eventName);
            }
#endif
        }

        private void CheckSendCacheEvent()
        {
#if USE_THIRD_SDK
            CodePipeline.Push4Invoke(OnProcessTickerSendCache);
#endif
        }

        private void OnProcessTickerSendCache()
        {
#if USE_THIRD_SDK
            //全部处理完毕
            if (!_eventCache.HasCache())
            {
                return;
            }
            InternalAnalysticsEvtPair evtPair = _eventCache.PopCache();
            if (evtPair != null)
                SendAnalyticsEventDirect(evtPair.nameEvt, evtPair.dictParams);

            //如果还有未处理的事件 加入下一帧处理
            if (_eventCache.HasCache())
            {
                CodePipeline.Push4Invoke(OnProcessTickerSendCache);
            }
#endif
        }
    }
}