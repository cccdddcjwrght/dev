using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ad;

public class ADTrader
{
    #region Inst
    private static readonly ADTrader _inst = new ADTrader();
    static ADTrader() { }
    public static ADTrader inst { get { return _inst; } }
    #endregion

    /// <summary>
    /// 广告配置
    /// </summary>
    internal AdCfg _cfgAd { get; private set; }

    /// <summary>
    /// 装配部署广告组
    /// </summary>
    /// <param name="strCfg">广告组信息</param>
    public void Deploy(string cfgJson)
    {
        _cfgAd = JsonUtility.FromJson<AdCfg>(cfgJson);
        ADUtil.FormatAdCfg(_cfgAd);
        InitMobs();
        ADCenter.DeployAd(_cfgAd);
    }


    /// <summary>
    /// 显示广告
    /// </summary>
    /// <param name="sceneName"></param>
    public void Show(string sceneName)
    {
        //获取广告组
        ADSceneBase scene = ADSceneBase.FetchScene(sceneName);
        if (scene == null)
        {
            return;
        }
        if (ADUtil.LimitByPurchase(scene))
        {
            return;
        }
        scene.Show();
    }

    /// <summary>
    /// 显示广告
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="calleeClose"></param>
    public void Show(string sceneName, ADExternalHandleDef.calleeVoid2Void calleeClose)
    {
        //获取广告组
        ADSceneBase scene = ADSceneBase.FetchScene(sceneName);
        if (scene == null)
        {
            return;
        }
        if (ADUtil.LimitByPurchase(scene))
        {
            return;
        }
        scene.calleeClose = calleeClose;
        scene.Show();
    }

    /// <summary>
    /// 显示激励视频
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="calleeVideoClose"></param>
    public void Show(string sceneName, ADExternalHandleDef.calleeBool2Void calleeVideoClose)
    {
        //获取广告组
        ADSceneBase scene = ADSceneBase.FetchScene(sceneName);
        if (scene == null)
        {
            return;
        }
        if (ADUtil.LimitByPurchase(scene))
        {
            return;
        }
        scene.calleeVideoClose = calleeVideoClose;
        scene.Show();
    }

    /// <summary>
    /// 加载广告
    /// </summary>
    /// <param name="sceneName"></param>
    public void Load(string sceneName)
    {
        ADSceneBase scene = ADSceneBase.FetchScene(sceneName);
        if (scene == null)
        {
            return;
        }
        if (ADUtil.LimitByPurchase(scene))
        {
            return;
        }
        scene.Load();
    }

    /// <summary>
    /// <summary>
    /// 隐藏Banner
    /// </summary>
    /// <param name="sceneName"></param>
    public void HideBanner(string sceneName = "")
    {
        foreach (var item in ADUnitBase._dictUnit.Values)
        {
            //仅banner可以隐藏，并且当前状态已经是显示状态
            if (!ADUtil.isBuyNoAd && (item is IBanner) && item.showing)
            {
                (item as IBanner).HideBanner();
            }
        }
    }

    /// <summary>
    /// 显示Banner
    /// </summary>
    /// <param name="sceneName"></param>
    public void ShowBanner(string sceneName = "")
    {
        foreach (var item in ADUnitBase._dictUnit.Values)
        {
            //仅banner可以恢复显示，并且当前状态已经是显示状态
            if (!ADUtil.isBuyNoAd && (item is IBanner) && item.showing)
            {
                (item as IBanner).ShowBanner();
            }
        }
    }
   

    /// 是否可用
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsAvaiable(string name)
    {
        ADSceneBase scene = ADSceneBase.FetchScene(name);
        if (scene == null)
        {
            return false;
        }

        return scene.isAvaiable;
    }

    /// <summary>
    /// 广告是否可以（检测去广告影响）
    /// </summary>
    /// <param name="name">场景名</param>
    /// <param name="checkBuyNoAd">是否检测去广告影响</param>
    /// <returns></returns>
    public bool IsAvaiable(string name, bool checkBuyNoAd)
    {
        ADSceneBase scene = ADSceneBase.FetchScene(name);
        if (scene == null)
        {
            return false;
        }

        if (checkBuyNoAd && ADUtil.LimitByPurchase(scene))
        {
            return false;
        }
        return scene.isAvaiable;
    }

    /// <summary>
    /// 初始化第三方广告平台入口
    /// </summary>
    private void InitMobs()
    {
#if USE_AD
        GgoAdBroker.InitMob(_cfgAd);
#endif
    }
}

/// <summary>
/// 外部接口
/// </summary>
public class ADExternalHandleDef
{
    public delegate void calleeCollectAD(string strEvent, string source, string unit,
                                          string reason = "", double loadSec = -1, long game = -1,
                                          long fcnt = 0, long ftotal = 0, string scene = "", string group = "",
                                          int adType = -1, int paidType = 0, double paidValue = 0.0);

    //from DelegateDef
    public delegate void calleeInt2Void(int param);
    public delegate void calleeBool2Void(bool param);
    public delegate void calleeBytes2Void(byte[] param);
    public delegate void calleeStr2Void(string param);
    public delegate void calleeVoid2Void();
    public delegate int calleeString2Int(string param);
    public delegate bool calleeVoid2Bool();
    public delegate int calleeVoid2Int();
    public delegate void calleePipelineInovke(Action param);
    public delegate void calleeUnitBack(ADUnitBase unit);
}

namespace Ad
{
    internal class ADUtil
    {
        internal static bool isBuyNoAd;
        //从游戏启动时过去的秒数
        internal static float realtimeSinceStartup { get; private set; }

        //全局的广告数据采集接口
        internal static ADExternalHandleDef.calleeCollectAD calleeADDataCollect;
        internal static ADExternalHandleDef.calleeVoid2Void calleeMainTicker;

        /// <summary>
        /// 获取每日播放次数
        /// </summary>
        internal static ADExternalHandleDef.calleeString2Int calleeDailyShowCnt;
        /// <summary>
        /// 每次播放广告是调用外部接口 用于每日计数
        /// </summary>
        internal static ADExternalHandleDef.calleeStr2Void calleeShowOnce4DailySave;
        /// <summary>
        /// 广告单元加载超时
        /// </summary>
        private static float _unitLoadOvertimeSec = 60f;
        /// <summary>
        /// 必须在调用其他广告代码前进行初始化并且必须在主线程
        /// </summary>
        /// <param name="root"></param>
        /// <param name="mono"></param>
        /// <param name="mainCmr"></param>
        public static void Init()
        {
            calleeADDataCollect = DefaultCollectAD;
            calleeMainTicker += AdReqCenter.inst.OnTicker;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static ADUnitBase GenUnit(AdUnitRawData info)
        {
            switch (info.adChannel)
            {

#if USE_AD
                case AD_CHANNEL.GOOGLE:
                    return GgoAdBroker.GenUnit(info);
#endif
                default:
                    return null;
            }
        }

     
        /// <summary>
        /// 组中是否包含底条用于判断底条组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool ContainBanner(AdGroupRawData info)
        {
            ADUnitBase unit;
            foreach (var item in info.units)
            {
                unit = ADUnitBase.FetchUnit(item.unit);
                if (unit != null && (unit._type == AD_TYPE.BANNER))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 广告配置中的渠道名转枚举
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static AD_CHANNEL Name2AD_CHANNEL(string name)
        {
            //name = name.ToLower();
            return AD_CHANNEL.GOOGLE;
        }

        /// <summary>
        /// 名字转广告版位类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static AD_TYPE Name2AD_TYPE(string name)
        {
            name = name.ToLower();
            switch (name)
            {
                case "banner":
                    return AD_TYPE.BANNER;
                case "interstitial":
                    return AD_TYPE.INTERSTITIAL;
                case "reward.video":
                    return AD_TYPE.VIDEO;
                default:
                    return AD_TYPE.NONE;
            }
        }

        /// <summary>
        /// 格式化广告配置信息
        /// </summary>
        /// <param name="cfg"></param>
        internal static void FormatAdCfg(AdCfg cfg)
        {
            if (cfg.unitStandLoadSec > 0)
            {
                _unitLoadOvertimeSec = cfg.unitStandLoadSec;
            }
            foreach (var unit in cfg.units)
            {
                AdUnitRawData.FormatInfo(unit);
            }
        }

        /// <summary>
        /// 主线程update函数
        /// </summary>
        internal static void OnUpdate(float secFromStar)
        {
            realtimeSinceStartup = secFromStar;
            if (calleeMainTicker != null)
            {
                calleeMainTicker.Invoke();
            }
        }

        /// <summary>
        /// 加载是否超时
        /// </summary>
        /// <param name="startSec"></param>
        /// <returns></returns>
        internal static bool IsUnitLoadOvertime(float startSec)
        {
            if (startSec <= 0)
            {
                return false;
            }
            else
            {
                return (realtimeSinceStartup - startSec) > _unitLoadOvertimeSec;
            }
        }

        /// <summary>
        /// 数据采集容错回调
        /// </summary>
        /// <param name="strEvent"></param>
        /// <param name="source"></param>
        /// <param name="unit"></param>
        /// <param name="reason"></param>
        private static void DefaultCollectAD(string strEvent, string source, string unit,
                                          string reason = "", double loadSec = -1, long game = -1,
                                          long fcnt = 0, long ftotal = 0, string scene = "", string group = "",
                                          int adType = 0, int paidType = 0, double paidValue = 0.0)
        {
#if USE_THIRD_SDK
            ThirdSdk.InternalEventManager.inst.CollectAdData(strEvent, source, unit, reason, loadSec, game, fcnt, ftotal, scene, group, adType, paidType, paidValue);
#endif
        }


        /// <summary>
        /// 为去广告用户删除部分广告
        /// </summary>
        internal static void Clear4Purchase()
        {
            isBuyNoAd = true;
            ADCenter.RemoveUnit4Purchase();
        }

        /// <summary>
        /// 关闭所有banner
        /// </summary>
        internal static void CloseAllBanner()
        {
            ADCenter.RemoveAllBanner();
        }

        /// <summary>
        /// 场景被去广告限制了
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        internal static bool LimitByPurchase(ADSceneBase scene)
        {
            return (isBuyNoAd && scene != null && !scene.ignorePurchase);
        }

        /// <summary>
        /// 获取组在场景中每日播放次数
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="groupName"></param>
        internal static int FetchDailyShowCntInScene(string sceneName, string groupName)
        {
            return calleeDailyShowCnt(string.Format("{0}_{1}", sceneName, groupName));
        }

        /// <summary>
        /// 组在场景中展示了一次存在
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="groupName"></param>
        internal static void ShowOnceInScene4DailySave(string sceneName, string groupName)
        {
            calleeShowOnce4DailySave(string.Format("{0}_{1}", sceneName, groupName));
        }

        internal static string GetSceneCheckResult(string newResult, string old)
        {
            if (old == "add" || newResult == "add")
            {
                return "add";
            }

            if (newResult == "cd" || old == "cd")
            {
                return "cd";
            }
            return "exist";
        }
    }

    /// <summary>
    /// 广告请求的调度中心
    /// </summary>
    internal class AdReqCenter
    {
#region Inst
        private static readonly AdReqCenter _inst = new AdReqCenter();
        static AdReqCenter() { }
        internal static AdReqCenter inst { get { return _inst; } }
#endregion
        /// <summary>
        /// 标准上限
        /// </summary>
        private const int STD_MAX_CNT = 5;

        /// <summary>
        /// 并发加载器数量上限
        /// </summary>
        private int _maxCnt = STD_MAX_CNT;

        /// <summary>
        /// 待加载队列
        /// </summary>
        private List<string> _lstPendingUnitCode = new List<string>();
        //TODO:如果需要则要建立加载中的队列

        /// <summary>
        /// 当前加载数量
        /// </summary>
        private int _curLoadingCnt;

        /// <summary>
        /// 最近一次请求时间标记
        /// </summary>
        private float _lastReqTimeMark;

        /// <summary>
        /// 上一次调度函数检测时间
        /// </summary>
        private float _lastProcessCheckTime;

        /// <summary>
        /// 删除待加载队列中的单元
        /// </summary>
        internal void RemovePending4Purchase()
        {
            //删除受去广告影响的单元(后台还不支持到单元级的控制 目前的规则是除视频外全都受广告影响)
            int cnt = _lstPendingUnitCode.Count;
            for (int i = cnt - 1; i >= 0; i--)
            {
                ADUnitBase unit = ADUnitBase.FetchUnit(_lstPendingUnitCode[i]);
                if (unit._type == AD_TYPE.VIDEO)
                {
                    continue;
                }
                _lstPendingUnitCode.Remove(_lstPendingUnitCode[i]);
            }
        }

        /// <summary>
        /// 删除待加载队列中的banner单元
        /// </summary>
        internal void RemovePendingBanner()
        {
            int cnt = _lstPendingUnitCode.Count;
            for (int i = cnt - 1; i >= 0; i--)
            {
                ADUnitBase unit = ADUnitBase.FetchUnit(_lstPendingUnitCode[i]);
                if (unit._type == AD_TYPE.BANNER)
                {
                    _lstPendingUnitCode.Remove(_lstPendingUnitCode[i]);
                }
            }
        }

        /// <summary>
        /// 请求广告
        /// </summary>
        /// <param name="unitCode"></param>
        internal void LoadAd(string unitCode)
        {
            if (_lstPendingUnitCode.Contains(unitCode))
            {
                return;
            }
            _lstPendingUnitCode.Add(unitCode);
            //按加载优先级排序
            SortPendingList();
            Process();
        }

        /// <summary>
        /// 取消广告加载队列
        /// </summary>
        /// <param name="unitCode"></param>
        internal void CancelAdInPending(string unitCode)
        {
            if (!_lstPendingUnitCode.Contains(unitCode))
            {
                return;
            }
            _lstPendingUnitCode.Remove(unitCode);
        }

        /// <summary>
        /// 防卡死定时触发器
        /// </summary>
        internal void OnTicker()
        {
            //如果加载未超时 则不处理
            if (!ADUtil.IsUnitLoadOvertime(_lastProcessCheckTime))
            {
                return;
            }
            _lastProcessCheckTime = ADUtil.realtimeSinceStartup;
            Process();
        }

        private void Process()
        {
            if (_lstPendingUnitCode.Count == 0)
            {
                return;
            }

            //扩容检测 防止某些广告单元请求异常长时间阻塞并队列
            ConcurrentExpandsionCheck();

            if (_curLoadingCnt > _maxCnt)
            {
                return;
            }
            //前面入栈逻辑保证了不会重
            string unitCode = _lstPendingUnitCode[0];
            _lstPendingUnitCode.RemoveAt(0);
            ADUnitBase unit = ADUnitBase.FetchUnit(unitCode);
            unit.calleeLoadFeedback = OnUnitFeedback;
            _lastReqTimeMark = ADUtil.realtimeSinceStartup;
            _curLoadingCnt++;
            unit.LoadAd();
        }

        /// <summary>
        /// 广告单元给加载器的反馈回调
        /// </summary>
        /// <param name="unit"></param>
        private void OnUnitFeedback(ADUnitBase unit)
        {
            //无论成功失败 都将从加载器移除 防止阻塞
            if (_curLoadingCnt == 0)
            {
                //Debug.Log("|||||ADTrader OnUnitFeedback _curLoadingCnt is 0, may be some error occurred");
            }
            else
            {
                _curLoadingCnt--;
            }

            ConcurrentShrinkCheck();
            Process();
        }

        /// <summary>
        /// 并发数扩容检测
        /// </summary>
        private void ConcurrentExpandsionCheck()
        {
            //扩容条件：并发数达上限且最近一次请求已经超时
            if (_curLoadingCnt < _maxCnt || !ADUtil.IsUnitLoadOvertime(_lastReqTimeMark))
            {
                return;
            }
            _maxCnt++;
            //TODO:若需要采集数据请加在这里
        }

        /// <summary>
        /// 并发数收缩检测
        /// </summary>
        private void ConcurrentShrinkCheck()
        {
            //不要一次就降到标准值 如果非要一次性降回标准值 请确保加载队列中的数量不大于标准值
            if (_maxCnt > STD_MAX_CNT)
            {
                _maxCnt--;
            }
        }

        //排序加载队列
        private void SortPendingList()
        {
            if (_lstPendingUnitCode.Count < 2)
            {
                return;
            }
            _lstPendingUnitCode.Sort((key1, key2) => ADCenter.SortReq(key1, key2));
        }
    }

    /// <summary>
    /// 广告监控
    /// </summary>
    internal class AdMonitor
    {
        /// <summary>
        /// 全局组展示间隔
        /// </summary>
        private static Dictionary<int, float> _dictGlobalGroupLastShowSec = new Dictionary<int, float>();


        /// TODO:将数据采集代码也移到此类中？
        //当前处于展示中的激励视频数 设计上该值不应超过1
        internal static int VideoShowingCnt { get; private set; }
        //当前处于展示中的激励视频数 设计上该值不应超过1
        internal static int InterstitialShowingCnt { get; private set; }
        /// <summary>
        /// 已显示的插屏数
        /// </summary>
        internal static int InterstitialShowedCnt { get { return _totalCntInterstitial; } }
        /// <summary>
        /// 已显示的视频数
        /// </summary>
        internal static int VideoShowedCnt { get { return _totalCntVideo; } }
        /// <summary>
        /// 最近视频关闭时间
        /// </summary>
        internal static float LastVideoCloseSec { get; private set; }
        /// <summary>
        /// 上一次视频打开
        /// </summary>
        internal static float LastVideoOpenSec { get; private set; }
        /// <summary>
        /// 上一次插屏展示时间
        /// </summary>
        internal static float LastItstOpenSec { get; private set; }

        /// <summary>
        /// 最近一次关闭插屏时间
        /// </summary>
        internal static float LastItstCloseSec { get; private set; }
        /// <summary>
        /// 可给奖视频计数
        /// </summary>
        internal static int VideoRwdCnt { get; private set; }

        //视频
        private static int _totalCntVideo;
        private static int _totalCntInterstitial;

        /// <summary>
        /// 广告单元被展示
        /// </summary>
        /// <param name="type"></param>
        internal static void OnUnitShow(AD_TYPE type)
        {
            switch (type)
            {
                case AD_TYPE.BANNER:
                    break;
                case AD_TYPE.INTERSTITIAL:
                    InterstitialShowingCnt++;
                    _totalCntInterstitial++;
                    LastItstOpenSec = ADUtil.realtimeSinceStartup;
                    break;
                case AD_TYPE.VIDEO:
                    VideoShowingCnt++;
                    _totalCntVideo++;
                    LastVideoOpenSec = ADUtil.realtimeSinceStartup;
                    break;
                case AD_TYPE.NONE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 广告单元被关闭
        /// </summary>
        /// <param name="type"></param>
        internal static void OnUnitClose(AD_TYPE type, bool canRwd)
        {
            switch (type)
            {
                case AD_TYPE.BANNER:
                    break;
                case AD_TYPE.INTERSTITIAL:
                    InterstitialShowingCnt--;
                    LastItstCloseSec = ADUtil.realtimeSinceStartup;
                    break;
                case AD_TYPE.VIDEO:
                    VideoShowingCnt--;
                    LastVideoCloseSec = ADUtil.realtimeSinceStartup;
                    if (canRwd)
                    {
                        VideoRwdCnt++;
                    }
                    break;
                case AD_TYPE.NONE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        internal static void Reset()
        {
            _totalCntInterstitial = 0;
            _totalCntVideo = 0;

            InterstitialShowingCnt = 0;
            VideoShowingCnt = 0;

            _dictGlobalGroupLastShowSec.Clear();
        }

        /// <summary>
        /// 组初始化cd信息
        /// </summary>
        /// <param name="itvSec"></param>
        internal static void OnGroupInit(int itvSec)
        {
            //TODO:按需求
            if (itvSec > 0)
            {
                _dictGlobalGroupLastShowSec[itvSec] = itvSec;
            }
        }

        /// <summary>
        /// 组展示
        /// </summary>
        /// <param name="itvSec"></param>
        internal static void OnGroupShow(int itvSec)
        {
            if (itvSec > 0)
            {
                _dictGlobalGroupLastShowSec[itvSec] = ADUtil.realtimeSinceStartup;
            }
        }

        /// <summary>
        /// 组展示
        /// </summary>
        /// <param name="itvSec"></param>
        internal static void OnGroupClose(int itvSec)
        {
            if (itvSec > 0)
            {
                _dictGlobalGroupLastShowSec[itvSec] = ADUtil.realtimeSinceStartup;
            }
        }

        /// <summary>
        /// 组处于全局展示cd中
        /// </summary>
        /// <param name="itvSec"></param>
        /// <returns></returns>
        internal static bool InGlobalGroupShowCD(int itvSec)
        {
            if (itvSec > 0)
            {
                if (_dictGlobalGroupLastShowSec.ContainsKey(itvSec))
                {
                    return (ADUtil.realtimeSinceStartup - _dictGlobalGroupLastShowSec[itvSec]) < itvSec;
                }
                else
                {
                    return ADUtil.realtimeSinceStartup < itvSec;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置全局展示时间
        /// </summary>
        /// <param name="startupTimeInSec">启动后的秒数 默认为当前值</param>
        internal static void SetGlobalGroupShowSec(float startupTimeInSec = -1)
        {
            if (startupTimeInSec == -1)
            {
                startupTimeInSec = ADUtil.realtimeSinceStartup;
            }

            var keys = new List<int>(_dictGlobalGroupLastShowSec.Keys);
            foreach (int key in keys)
            {
                if (key > 0)
                {
                    _dictGlobalGroupLastShowSec[key] = startupTimeInSec;
                }
            }
        }

    }

    /// <summary>
    /// 全局管理单元->组->场景关系
    /// </summary>
    internal class ADCenter
    {
        //单元的订阅信息
        private static Dictionary<string, List<string>> _dictUnitSubscribe = new Dictionary<string, List<string>>();
        private static Dictionary<string, AdUnitRawData> _dictUnitInfo = new Dictionary<string, AdUnitRawData>();

        internal static void DeployAd(AdCfg cfg)
        {
            //TODO:建立对应关系？
            _dictUnitInfo.Clear();
            foreach (var info in cfg.units)
            {
                _dictUnitInfo.Add(info.code, info);
            }

            //优先构建单元
            ADUnitBase.BuildUnits(cfg.units);
            //构建组
            ADGroup.BuildGroups(cfg.groups);
            //构建场景
            ADSceneBase.BuildScenes(cfg.scenes);
        }

        /// <summary>
        /// 去广告删除订阅内容
        /// </summary>
        internal static void RemoveUnit4Purchase()
        {
            foreach (var item in ADUnitBase._dictUnit.Values)
            {
                //仅banner需要直接移除 其他版位在释放后不再出现
                if (item._type == AD_TYPE.BANNER)
                {
                    item.DisposeRawUnit();
                }

                //除视频单元外 所有订阅都将被取消 TODO:需要依据场景实际情况处理
                if (item._type != AD_TYPE.VIDEO)
                {
                    _dictUnitSubscribe.Remove(item._code);
                }
            }
            //加载器删除队列
            AdReqCenter.inst.RemovePending4Purchase();
        }

        /// <summary>
        /// 关闭显示中的banner
        /// </summary>
        internal static void RemoveAllBanner()
        {
            foreach (var item in ADUnitBase._dictUnit.Values)
            {
                //仅banner需要直接移除 其他版位在释放后不再出现
                if (item is IBanner)
                {
                    (item as IBanner).CloseOnShow();
                }
            }
            //加载器删除队列
            AdReqCenter.inst.RemovePendingBanner();
        }

        /// <summary>
        /// 订阅某个广告单元
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="unitName"></param>
        internal static void Subscribe(string groupName, string unitName)
        {
            List<string> lstGroupChain;
            if (_dictUnitSubscribe.ContainsKey(unitName))
            {
                lstGroupChain = _dictUnitSubscribe[unitName];
                //若已经订阅
                if (lstGroupChain.Contains(groupName))
                {
                    return;
                }
                else
                {
                    lstGroupChain.Add(groupName);
                }
            }
            else
            {
                lstGroupChain = new List<string>();
                lstGroupChain.Add(groupName);
                _dictUnitSubscribe.Add(unitName, lstGroupChain);
            }
        }

        /// <summary>
        /// 取消订阅某个广告单元
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="unitName"></param>
        internal static void UnSubscribe(string groupName, string unitName)
        {
            //Debug.Log("UnSubscribe G: " + groupName + " uncare " + unitName);
            if (_dictUnitSubscribe.ContainsKey(unitName))
            {
                List<string> lstGroupChain = _dictUnitSubscribe[unitName];
                lstGroupChain.Remove(groupName);
            }
        }

        /// <summary>
        /// 是否订阅某个广告单元
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="unitName"></param>
        internal static bool HasSubscribe(string groupName, string unitName)
        {
            if (_dictUnitSubscribe.ContainsKey(unitName))
            {
                List<string> lstGroupChain = _dictUnitSubscribe[unitName];
                //若已经订阅
                return lstGroupChain.Contains(groupName);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 请求排序
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        internal static int SortReq(string key1, string key2)
        {
            //TODO:如果会挂 则加判断
            AdUnitRawData info1 = _dictUnitInfo[key1];
            AdUnitRawData info2 = _dictUnitInfo[key2];
            return info1.weight > info2.weight ? -1 : 1;
        }
    }

    /// <summary>
    /// banner接口
    /// </summary>
    internal interface IBanner
    {
        void CloseOnShow();
        void ShowBanner();
        void HideBanner();
    }

    public enum GROUP_POLICY
    {
        NONE,
        SEQUENCE,           //顺序策略
        DAILY_CNT_LIMIT,    //按日限次
        END,
    }

    [Serializable]
    public class AdUnitRawData
    {
        public float reqcd;     //请求间隔
        public string code;
        public string unit;         //广告平台key
        public float delay;
        public float delayRatio;
        public int weight;     //加载优先级
        //配置表属性
        public string platform;
        public string format;
        //实际调用属性
        public AD_CHANNEL adChannel { private set; get; }
        public AD_TYPE adType { private set; get; }

        /// <summary>
        /// 格式化数据 便于后续代码访问
        /// </summary>
        /// <param name="info"></param>
        internal static void FormatInfo(AdUnitRawData info)
        {
            info.adChannel = ADUtil.Name2AD_CHANNEL(info.platform);
            info.adType = ADUtil.Name2AD_TYPE(info.format);
        }
    }

    [Serializable]
    public class AdUnitBrief
    {
        public string unit;     //广告单元代号
        public int weight;      //权重
    }

    [Serializable]
    public class AdGroupRawData
    {
        public string code;
        public int policy;
        public int limits;      //每日展示次数上限
        public int cooldown;
        public AdUnitBrief[] units;
    }

    [Serializable]
    public class AdGroupBrief
    {
        public string group;        //广告组代号
        public GROUP_POLICY policy;      //策略类型
        public int weight;      //策略参数
    }

    [Serializable]
    public class AdSceneInfo
    {
        public string code;
        internal string subType { get; private set; }      //
        //public int stParam;
        public int interval;        //场景显示间隔
        public AdGroupBrief[] groups;
        public int ignorePurchase;     //去广告无效
        public AdSceneInfo[] countries;     //国家差异化

        /// <summary>
        /// 覆盖差异化
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="diff"></param>
        internal static void CvtDiff(AdSceneInfo raw, AdSceneInfo diff)
        {
            raw.interval = diff.interval;
            raw.groups = diff.groups;
            raw.ignorePurchase = diff.ignorePurchase;
            raw.countries = null;
        }
    }

    [Serializable]
    public class AdCfg
    {
        public string admobApp;

        /// <summary>
        /// 单元标准加载时长
        /// </summary>
        public float unitStandLoadSec;
        public AdSceneInfo[] scenes;
        public AdGroupRawData[] groups;
        public AdUnitRawData[] units;
    }

    /// <summary>
    /// 原生样式
    /// </summary>
    public enum NATIVE_STYLE
    {
        NONE,
        NORMAL,
        SMALL,
    }
}
