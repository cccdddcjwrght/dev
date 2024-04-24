using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ad
{
    public enum AD_CHANNEL
    {
        GOOGLE,
        //容错渠道 永远放在末尾
        NONE,
    }

    public enum AD_TYPE
    {
        BANNER,
        INTERSTITIAL,
        VIDEO,
        //容错类型 永远放在末尾
        NONE,
    }

    /// <summary>
    /// 广告单元基类
    /// </summary>
    public class ADUnitBase
    {
        //UnitPool
        internal static Dictionary<string, ADUnitBase> _dictUnit = new Dictionary<string, ADUnitBase>();

        /// <summary>
        /// 构建广告组
        /// </summary>
        internal static void BuildUnits(AdUnitRawData[] arrInfos)
        {
            ClearUnits();

            int len = arrInfos.Length;
            AdUnitRawData info;
            ADUnitBase unit;
            for (int i = 0; i < len; i++)
            {
                info = arrInfos[i];
                unit = ADUtil.GenUnit(info);
                if (unit != null)
                {
                    _dictUnit.Add(info.code, unit);
                }
            }
        }

        /// <summary>
        /// 获取广告单元
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static ADUnitBase FetchUnit(string name)
        {
            if (_dictUnit.ContainsKey(name))
            {
                return _dictUnit[name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 清理单元
        /// </summary>
        internal static void ClearUnits()
        {
            foreach (var item in _dictUnit.Values)
            {
                item.Dispose();
            }
            _dictUnit.Clear();
        }

        //global def
        private static string[] AD_CHANNEL_NAME = { "admob" };

        private AD_CHANNEL _ch;  //渠道
        internal AD_TYPE _type { get; private set; }  //类型
        protected string _key { get; private set; }     //平台key
        internal string _code { get; private set; }        //版位描述
        protected string _strCh { get; private set; }          //渠道代号
        private int _gameName = -1;                             //游戏名，多个游戏类型时使用
        //model
        internal AD_UNIT_STATE _curState;        //状态
        private bool _abandoned;            //单元被遗弃 会出现在配置刷新时 有的单元正在展示 不能及时释放 只能打上标记 待特定回调触发时自行释放 
        private float _reqStartTime = -1;  //请求开始时间戳
        private float _reqItvSec;       //配置的失败请求间隔
        private bool _canBeRewared;     //可以给予奖励 仅激励视频此值有变化 其他类型广告均为false
        private float _lastErrorTime = -1;      //上一次失败时间
        private float _lastLoadedTime = -1;    //最近一次下载完毕时间

        internal string _lastGroupShown { get; private set; }      //对此单元调用show的组名
        private string _lastSceneShown;                            //对此单元调用show的场景名

        internal ADExternalHandleDef.calleeUnitBack calleeClose;
        internal ADExternalHandleDef.calleeUnitBack calleeFail;
        internal ADExternalHandleDef.calleeUnitBack calleeLoadFeedback;

        //develop
        internal string devLastError;
        internal float devLoadSec;

        /// <summary>
        /// 析构
        /// </summary>
        virtual internal void Dispose()
        {
            calleeLoadFeedback = null;
            calleeClose = null;
            calleeFail = null;
        }

        public bool isInit { get { return _curState == AD_UNIT_STATE.INIT; } }
        public bool isLoading { get { return _curState == AD_UNIT_STATE.LOADING; } }
        public bool isAvaiable { get { return IsUnitAvaiable(); } }
        public bool failed { get { return _curState == AD_UNIT_STATE.ERROR; } }
        /// <summary>
        /// 广告单元无效
        /// </summary>
        public bool invalidate { get { return _curState == AD_UNIT_STATE.CLOSED || _curState == AD_UNIT_STATE.ERROR; } }

        /// <summary>
        /// 广告展示中
        /// </summary>
        public bool showing { get { return _curState == AD_UNIT_STATE.SHOWUP || _curState == AD_UNIT_STATE.IMPRESSION; } }

        /// <summary>
        /// 处在请求冷却中
        /// </summary>
        public bool onReqCD { get { return _lastErrorTime != -1 && (ADUtil.realtimeSinceStartup - _lastErrorTime) < _reqItvSec; } }

        /// <summary>
        /// 加载是否超时
        /// </summary>
        internal bool isLoadOvertime { get { return isLoading && ADUtil.IsUnitLoadOvertime(_reqStartTime); } }
        /// <summary>
        /// 激励视频可以给予奖励
        /// </summary>
        internal bool canVideoReward { get { return _canBeRewared; } }
        /// <summary>
        /// 被占用（单例类的广告单元会有被占用的情况）
        /// </summary>
        internal virtual bool occupied { get { return false; } }
        
        /// <summary>
        /// 请求加载广告
        /// </summary>
        /// <param name="isBackUp"></param>
        /// <param name="gameName"></param>
        /// <param name="adId"></param>
        virtual public void Request(bool isBackUp = false, int gameName = -1)
        {
            OnRequest(isBackUp, gameName);
            AdReqCenter.inst.LoadAd(_code);
        }

        /// <summary>
        /// 用于调度器触发广告加载
        /// </summary>
        virtual internal void LoadAd()
        {
            _reqStartTime = ADUtil.realtimeSinceStartup;
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.REQUEST, _strCh, _code, "", game: _gameName);
        }

        /// <summary>
        /// 重建实体广告单元
        /// </summary>
        virtual internal void RebuildRawUnit()
        {
            _curState = AD_UNIT_STATE.INIT;
        }

        /// <summary>
        /// 移除广告实体
        /// </summary>
        virtual internal void DisposeRawUnit()
        {
            _curState = AD_UNIT_STATE.CLOSED;
        }

        /// <summary>
        /// 展示
        /// </summary>
        virtual protected void Show()
        {
            _curState = AD_UNIT_STATE.SHOWUP;
            _canBeRewared = false;
            AdMonitor.OnUnitShow(_type);
        }

        virtual protected bool IsUnitAvaiable()
        {
            return _curState == AD_UNIT_STATE.LOADED;
        }

        internal void GroupShow(string group, string sceneName)
        {
            _lastGroupShown = group;
            _lastSceneShown = sceneName;
            Show();
        }

        /// 设置类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subType"></param>
        protected void InitUint(AdUnitRawData info)
        {
            _curState = AD_UNIT_STATE.INIT;

            _ch = info.adChannel;
            _strCh = AD_CHANNEL_NAME[(int)_ch];

            _type = info.adType;
            _code = info.code;

            _key = info.unit;

            _reqItvSec = info.reqcd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isBackUp"></param>
        protected void OnRequest(bool isBackUp = false, int gameName = -1)
        {
            _curState = AD_UNIT_STATE.LOADING;
            _gameName = gameName;
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="errorMsg"></param>
        protected void OnError(string errorMsg)
        {
            _curState = AD_UNIT_STATE.ERROR;
            devLastError = errorMsg;
            _lastErrorTime = ADUtil.realtimeSinceStartup;
            OnLoadFeedback();
            if(!string.IsNullOrEmpty(errorMsg))
                ADUtil.calleeADDataCollect(AD_UNIT_EVT.ERROR, _strCh, _code, errorMsg);
            DetectAbandoned();
            //若是被遗弃的广告单元 则下列条件不会被触发
            if (calleeFail != null)
            {
                CodePipeline.Push4Invoke( ()=> { calleeFail(this); } );
            }
        }

        /// <summary>
        /// 加载完毕
        /// </summary>
        protected void OnLoaded()
        {
            _curState = AD_UNIT_STATE.LOADED;
            OnLoadFeedback();
            _lastLoadedTime = ADUtil.realtimeSinceStartup;
            //由于banner会自动进行加载 所以非手动调用的加载不参与计时
            if (_reqStartTime != -1)
            {
                float deltaTime = ADUtil.realtimeSinceStartup - _reqStartTime;
                ADUtil.calleeADDataCollect(AD_UNIT_EVT.LOADED, _strCh, _code, loadSec:deltaTime);
                devLoadSec = deltaTime;
            }
            _reqStartTime = -1;
            DetectAbandoned();
        }

        /// <summary>
        /// 被标记为有效展示 部分渠道不支持 则需要在调用Show的同时调用此接口
        /// </summary>
        protected void OnImpression()
        {
            _curState = AD_UNIT_STATE.IMPRESSION;
            float durSec = ADUtil.realtimeSinceStartup - _lastLoadedTime;
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.SHOWED, _strCh, _code, adType: (int)_type, game: _gameName, scene:_lastSceneShown, group:_lastGroupShown, loadSec:durSec);
        }

        /// <summary>
        /// 广告点击事件
        /// </summary>
        protected void OnClick()
        {
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.CLICK, _strCh, _code, game: _gameName);
        }

        /// <summary>
        /// 视频回调可给奖励
        /// </summary>
        protected void OnVideoReward()
        {
            _canBeRewared = true;
        }

        protected void OnPaid(int paidType, long paidValue)
        {
            //最终值除以100万
            double paidCalcVal = (double)paidValue / GetPaidParam();
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.PAID, _strCh, _code, adType: (int)_type, paidType: paidType, paidValue: paidCalcVal, scene: _lastSceneShown, group: _lastGroupShown);
        }

        protected float GetPaidParam()
        {
            return 1000000.0f;
        }

        /// <summary>
        /// 广告关闭
        /// </summary>
        protected void OnClose()
        {
            _curState = AD_UNIT_STATE.CLOSED;
            AdMonitor.OnUnitClose(_type, _canBeRewared);
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.CLOSE, _strCh, _code, game: _gameName);
            if (calleeClose != null)
            {
                CodePipeline.Push4Invoke(()=>
                {
                    calleeClose(this);
                    _lastGroupShown = "";
                    _lastSceneShown = "";
                    _canBeRewared = false;
                    DetectAbandoned();
                });
            }
        }

        //检测请求冲突情况并采集之
        internal void DetectRequestConflict()
        {
            //采集重复加载
            if (_curState == AD_UNIT_STATE.LOADING)
            {
                ADUtil.calleeADDataCollect(AD_UNIT_EVT.ERROR, _strCh, _code, "Request On Loading", game: _gameName);
            }
        }

        //加载反馈
        protected void OnLoadFeedback()
        {
            if (calleeLoadFeedback != null)
            {
                CodePipeline.Push4Invoke(() => calleeLoadFeedback.Invoke(this));
            }
        }

        //检测被遗弃时进行自我释放
        private void DetectAbandoned()
        {
            if (!_abandoned)
            {
                return;
            }
            else
            {
                Dispose();
            }
        }
    }

    internal class AD_UNIT_EVT
    {
        //通用事件
        public const string REQUEST = "ad_request";
        public const string ERROR = "ad_error";
        public const string LOADED = "ad_loaded";
        public const string SHOWED = "ad_showed";
        public const string CLICK = "ad_clicked";
        public const string CLOSE = "ad_close";
        public const string CHECK = "ad_check";
        public const string PAID = "ad_paid";

        //定制事件
        public const string SCENE_SHOW = "ad_scene_show";
    }

    internal enum AD_UNIT_STATE
    {
        INIT,
        LOADING,
        LOADED,
        ERROR,
        SHOWUP,     //被展示
        IMPRESSION,
        CLOSED,
    }
}
