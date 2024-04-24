using UnityEngine;
using System.Collections.Generic;

#if USE_AD
using Ad;
#endif

namespace ThirdSdk
{
    public class InternalAdManager
    {
#region Inst
        private static readonly InternalAdManager _inst = new InternalAdManager();
        static InternalAdManager() { }
        public static InternalAdManager inst { get { return _inst; } }
#endregion

        private bool _hasInit = false;
        private bool _hasInitComplete = false;

        /// <summary>
        /// 初始化广告
        /// </summary>
        public void InitAd()
        {
#if USE_AD
            if (_hasInit)
                return;

            _hasInit = true;
            ThirdSDK.inst.AddListener(THIRD_EVENT_TYPE.TET_ADMOB_INIT_COMPLETE, OnAdMobInitComplete);
            ADUtil.Init();
            //部署广告
            DeployAd();
#endif
        }

        /// <summary>
        /// 更新游戏时间，用于处理广告冷却
        /// </summary>
        public void Update()
        {
#if USE_AD
            ADUtil.OnUpdate(Time.realtimeSinceStartup);
#endif
        }

        private void OnAdMobInitComplete(object obj)
        {
            _hasInitComplete = true;
            ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_AD_INIT_COMPLETE);
        }

        /// <summary>
        /// 部署广告
        /// </summary>
        private void DeployAd()
        {
#if USE_AD
            //根据项目需求读取本地配置文件
            string strCfgJson = ReadAdCfg();
            ADTrader.inst.Deploy(strCfgJson);
#endif
        }

        private string ReadAdCfg()
        {
#if USE_AD
            TextAsset cfg = Resources.Load("cfg/AdCfg") as TextAsset;
            return cfg.text;
#else
            return "";
#endif
        }

        public void LoadAd(string sceneName)
        {
            if (!_hasInitComplete)
                return;

#if USE_AD
            ADTrader.inst.Load(sceneName);
#endif
        }


        /// 是否可用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsAdAvaiable(string name)
        {
            if (!_hasInitComplete)
                return false;

#if USE_AD
            return ADTrader.inst.IsAvaiable(name);
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
            if (!_hasInitComplete)
                return;

#if USE_AD
            ADTrader.inst.Show(sceneName, () => {
                ADTrader.inst.Load(sceneName);
            });
#endif
        }

        /// <summary>
        /// 播放视频广告
        /// </summary>
        /// <param name="sceneName"></param>
        public void ShowVideoAd(string sceneName, ADExternalHandleDef.calleeBool2Void calleeVideoClose)
        {
            if (!_hasInitComplete)
                return;

#if USE_AD
            ADTrader.inst.Show(sceneName, isReward =>
            {
                ADTrader.inst.Load(sceneName);
                calleeVideoClose(isReward);
                AdMonitor.SetGlobalGroupShowSec();
            });
#endif
        }
    }
}