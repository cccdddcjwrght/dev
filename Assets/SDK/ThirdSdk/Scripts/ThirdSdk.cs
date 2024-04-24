using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdSdk
{
    public class ThirdSDK
    {
        #region Inst
        private static readonly ThirdSDK _inst = new ThirdSDK();
        static ThirdSDK() { }
        public static ThirdSDK inst { get { return _inst; } }
        #endregion

        /// <summary>
        /// 第三方初始化接口
        /// </summary>
        /// <param name="rootObj">第三方SDK挂接的父对象</param>
        public void Init(GameObject rootObj)
        {
#if USE_THIRD_SDK
            InternalSdkManager.inst.Init(rootObj);
#else
            ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_THIRD_SDK_INIT_COMPLETE, true);
#endif
        }


        /// <summary>
        /// SDK激活, 在OnApplicationPause的参数为false的时候调用
        /// </summary>
        public void OnActive()
        {
#if USE_THIRD_SDK
            InternalSdkManager.inst.OnActive();
#endif
        }

        /// <summary>
        /// 更新游戏时间，用于处理广告冷却
        /// </summary>
        public void Update()
        {
#if USE_THIRD_SDK
            InternalAdManager.inst.Update();
#endif
        }


        /// <summary>
        /// 添加事件
        /// </summary>
        public void AddListener(THIRD_EVENT_TYPE eventType, Action<object> listener)
        {
            ThirdEvent.inst.AddListener(eventType, listener);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        public void RemoveListener(THIRD_EVENT_TYPE eventType, Action<object> listener)
        {
            ThirdEvent.inst.RemoveListener(eventType, listener);
        }

        /// <summary>
        /// 发送数据分析记录事件
        /// </summary>
        public void SendAnalyticsEvent(string eventName, IDictionary<string, object> dicParam = null)
        {
#if USE_THIRD_SDK
            FirebaseSDK.inst.SendAnalyticsEvent(eventName, dicParam);
#endif
        }


        public void LoadAd(string sceneName)
        {
#if USE_THIRD_SDK
            InternalAdManager.inst.LoadAd(sceneName);
#endif
        }


        /// 是否可用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsAdAvaiable(string name)
        {
#if USE_THIRD_SDK
            return InternalAdManager.inst.IsAdAvaiable(name);
#else
            return false;
#endif
        }

        /// <summary>
        /// 播放插屏广告
        /// </summary>
        /// <param name="sceneName"></param>
        public void ShowInterAd(string sceneName)
        {
#if USE_THIRD_SDK
            InternalAdManager.inst.ShowInterAd(sceneName);
#endif
        }

        /// <summary>
        /// 播放视频广告
        /// </summary>
        /// <param name="sceneName"></param>
        public void ShowVideoAd(string sceneName, ADExternalHandleDef.calleeBool2Void calleeVideoClose)
        {
#if USE_THIRD_SDK
            InternalAdManager.inst.ShowVideoAd(sceneName, calleeVideoClose);
#endif
        }
    }
}