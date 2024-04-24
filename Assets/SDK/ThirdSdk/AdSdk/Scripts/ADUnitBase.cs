using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ad
{
    public enum AD_CHANNEL
    {
        GOOGLE,
        //�ݴ����� ��Զ����ĩβ
        NONE,
    }

    public enum AD_TYPE
    {
        BANNER,
        INTERSTITIAL,
        VIDEO,
        //�ݴ����� ��Զ����ĩβ
        NONE,
    }

    /// <summary>
    /// ��浥Ԫ����
    /// </summary>
    public class ADUnitBase
    {
        //UnitPool
        internal static Dictionary<string, ADUnitBase> _dictUnit = new Dictionary<string, ADUnitBase>();

        /// <summary>
        /// ���������
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
        /// ��ȡ��浥Ԫ
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
        /// ����Ԫ
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

        private AD_CHANNEL _ch;  //����
        internal AD_TYPE _type { get; private set; }  //����
        protected string _key { get; private set; }     //ƽ̨key
        internal string _code { get; private set; }        //��λ����
        protected string _strCh { get; private set; }          //��������
        private int _gameName = -1;                             //��Ϸ���������Ϸ����ʱʹ��
        //model
        internal AD_UNIT_STATE _curState;        //״̬
        private bool _abandoned;            //��Ԫ������ �����������ˢ��ʱ �еĵ�Ԫ����չʾ ���ܼ�ʱ�ͷ� ֻ�ܴ��ϱ�� ���ض��ص�����ʱ�����ͷ� 
        private float _reqStartTime = -1;  //����ʼʱ���
        private float _reqItvSec;       //���õ�ʧ��������
        private bool _canBeRewared;     //���Ը��轱�� ��������Ƶ��ֵ�б仯 �������͹���Ϊfalse
        private float _lastErrorTime = -1;      //��һ��ʧ��ʱ��
        private float _lastLoadedTime = -1;    //���һ���������ʱ��

        internal string _lastGroupShown { get; private set; }      //�Դ˵�Ԫ����show������
        private string _lastSceneShown;                            //�Դ˵�Ԫ����show�ĳ�����

        internal ADExternalHandleDef.calleeUnitBack calleeClose;
        internal ADExternalHandleDef.calleeUnitBack calleeFail;
        internal ADExternalHandleDef.calleeUnitBack calleeLoadFeedback;

        //develop
        internal string devLastError;
        internal float devLoadSec;

        /// <summary>
        /// ����
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
        /// ��浥Ԫ��Ч
        /// </summary>
        public bool invalidate { get { return _curState == AD_UNIT_STATE.CLOSED || _curState == AD_UNIT_STATE.ERROR; } }

        /// <summary>
        /// ���չʾ��
        /// </summary>
        public bool showing { get { return _curState == AD_UNIT_STATE.SHOWUP || _curState == AD_UNIT_STATE.IMPRESSION; } }

        /// <summary>
        /// ����������ȴ��
        /// </summary>
        public bool onReqCD { get { return _lastErrorTime != -1 && (ADUtil.realtimeSinceStartup - _lastErrorTime) < _reqItvSec; } }

        /// <summary>
        /// �����Ƿ�ʱ
        /// </summary>
        internal bool isLoadOvertime { get { return isLoading && ADUtil.IsUnitLoadOvertime(_reqStartTime); } }
        /// <summary>
        /// ������Ƶ���Ը��轱��
        /// </summary>
        internal bool canVideoReward { get { return _canBeRewared; } }
        /// <summary>
        /// ��ռ�ã�������Ĺ�浥Ԫ���б�ռ�õ������
        /// </summary>
        internal virtual bool occupied { get { return false; } }
        
        /// <summary>
        /// ������ع��
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
        /// ���ڵ���������������
        /// </summary>
        virtual internal void LoadAd()
        {
            _reqStartTime = ADUtil.realtimeSinceStartup;
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.REQUEST, _strCh, _code, "", game: _gameName);
        }

        /// <summary>
        /// �ؽ�ʵ���浥Ԫ
        /// </summary>
        virtual internal void RebuildRawUnit()
        {
            _curState = AD_UNIT_STATE.INIT;
        }

        /// <summary>
        /// �Ƴ����ʵ��
        /// </summary>
        virtual internal void DisposeRawUnit()
        {
            _curState = AD_UNIT_STATE.CLOSED;
        }

        /// <summary>
        /// չʾ
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

        /// ��������
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
        /// ����
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
            //���Ǳ������Ĺ�浥Ԫ �������������ᱻ����
            if (calleeFail != null)
            {
                CodePipeline.Push4Invoke( ()=> { calleeFail(this); } );
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        protected void OnLoaded()
        {
            _curState = AD_UNIT_STATE.LOADED;
            OnLoadFeedback();
            _lastLoadedTime = ADUtil.realtimeSinceStartup;
            //����banner���Զ����м��� ���Է��ֶ����õļ��ز������ʱ
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
        /// �����Ϊ��Чչʾ ����������֧�� ����Ҫ�ڵ���Show��ͬʱ���ô˽ӿ�
        /// </summary>
        protected void OnImpression()
        {
            _curState = AD_UNIT_STATE.IMPRESSION;
            float durSec = ADUtil.realtimeSinceStartup - _lastLoadedTime;
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.SHOWED, _strCh, _code, adType: (int)_type, game: _gameName, scene:_lastSceneShown, group:_lastGroupShown, loadSec:durSec);
        }

        /// <summary>
        /// ������¼�
        /// </summary>
        protected void OnClick()
        {
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.CLICK, _strCh, _code, game: _gameName);
        }

        /// <summary>
        /// ��Ƶ�ص��ɸ�����
        /// </summary>
        protected void OnVideoReward()
        {
            _canBeRewared = true;
        }

        protected void OnPaid(int paidType, long paidValue)
        {
            //����ֵ����100��
            double paidCalcVal = (double)paidValue / GetPaidParam();
            ADUtil.calleeADDataCollect(AD_UNIT_EVT.PAID, _strCh, _code, adType: (int)_type, paidType: paidType, paidValue: paidCalcVal, scene: _lastSceneShown, group: _lastGroupShown);
        }

        protected float GetPaidParam()
        {
            return 1000000.0f;
        }

        /// <summary>
        /// ���ر�
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

        //��������ͻ������ɼ�֮
        internal void DetectRequestConflict()
        {
            //�ɼ��ظ�����
            if (_curState == AD_UNIT_STATE.LOADING)
            {
                ADUtil.calleeADDataCollect(AD_UNIT_EVT.ERROR, _strCh, _code, "Request On Loading", game: _gameName);
            }
        }

        //���ط���
        protected void OnLoadFeedback()
        {
            if (calleeLoadFeedback != null)
            {
                CodePipeline.Push4Invoke(() => calleeLoadFeedback.Invoke(this));
            }
        }

        //��ⱻ����ʱ���������ͷ�
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
        //ͨ���¼�
        public const string REQUEST = "ad_request";
        public const string ERROR = "ad_error";
        public const string LOADED = "ad_loaded";
        public const string SHOWED = "ad_showed";
        public const string CLICK = "ad_clicked";
        public const string CLOSE = "ad_close";
        public const string CHECK = "ad_check";
        public const string PAID = "ad_paid";

        //�����¼�
        public const string SCENE_SHOW = "ad_scene_show";
    }

    internal enum AD_UNIT_STATE
    {
        INIT,
        LOADING,
        LOADED,
        ERROR,
        SHOWUP,     //��չʾ
        IMPRESSION,
        CLOSED,
    }
}
