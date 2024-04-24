using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdSdk
{
    //内部事件代码接口
    public class InternalEventManager
    {
        #region Inst
        private static readonly InternalEventManager _inst = new InternalEventManager();
        static InternalEventManager() { }
        public static InternalEventManager inst { get { return _inst; } }
        #endregion

#if BAN_EVT_FILTER
        private const string IGNORE_EVT_LIST = "";
#else
        private const string IGNORE_EVT_LIST = "ad_check,ad_request,ad_error,ad_scene_show";
#endif

        private double _prevAccumulatePaid = 0.0f;
        private bool _isInitComplete = false;
        private List<Dictionary<string, object>> _lstEventDicCache = new List<Dictionary<string, object>>();
        private HashSet<string> _ignoreEvents = new HashSet<string>();

        public void Init()
        {
            if (_isInitComplete)
                return;

            _isInitComplete = true;
            InitControl();
            LoadAdSavePaid();
            InitIgnoreEvents();
        }

        private void InitIgnoreEvents()
        {
            _ignoreEvents.Clear();
            foreach (var item in IGNORE_EVT_LIST.Split(','))
            {
                _ignoreEvents.Add(item);
            }
        }


        private void LoadAdSavePaid()
        {
            if (PlayerPrefs.HasKey(InternalPrefsDef.ACCUMULATE_PAID))
                _prevAccumulatePaid = PlayerPrefs.GetFloat(InternalPrefsDef.ACCUMULATE_PAID, 0);

        }

        private Dictionary<string, object> GetEventDicFromCache()
        {
            Dictionary<string, object> dict;
            if (_lstEventDicCache.Count <= 0)
            {
                dict = new Dictionary<string, object>();
            }
            else
            {
                dict = _lstEventDicCache[0];
                _lstEventDicCache.RemoveAt(0);
            }
            dict.Clear();
            return dict;
        }

        private void PushEventDicToCache(Dictionary<string, object> dict)
        {
            if (dict != null)
                _lstEventDicCache.Add(dict);
        }

        /// <summary>
        /// 采集广告信息
        /// </summary>
        /// <param name="strEvent"></param>
        /// <param name="source"></param>
        /// <param name="unit"></param>
        /// <param name="reason"></param>
        /// <param name="loadSec"></param>
        /// <param name="game"></param>
        public void CollectAdData(string strEvent, string source, string unit,
                                   string reason = "", double loadSec = -1, long game = -1,
                                   long fcnt = 0, long ftotal = 0, string scene = "", string group = "",
                                   int adType = -1, int paidType = 0, double paidValue = 0.0)
        {
            if (_ignoreEvents.Contains(strEvent))
                return;

            Dictionary<string, object> dict = GetEventDicFromCache();
            dict.Add("source", source);
            dict.Add("unit", unit);
            if (reason != "")
                dict.Add("reason", reason);

            if (loadSec != -1)
                dict.Add("load_sec", loadSec);

            if (game != -1)
                dict.Add("game_name", game);

            if (fcnt != 0)
                dict.Add("fcnt", fcnt);

            if (ftotal != 0)
                dict.Add("ftotal", ftotal);

            if (!string.IsNullOrEmpty(scene))
                dict.Add("scene", scene);

            if (!string.IsNullOrEmpty(group))
                dict.Add("group", group);

            if (adType != -1)
            {
                dict.Add("adType", adType);
            }

            if (paidType != 0)
            {
                dict.Add("paidType", paidType);
                dict.Add("paidValue", paidValue);
                CheckSendAdAccumulatePaid(paidValue);
            }

            SendAnalyticsEvent(strEvent, dict);
        }


        private void CheckSendAdAccumulatePaid(double paidValue)
        {
            _prevAccumulatePaid += paidValue;
            if (_prevAccumulatePaid >= 0.01)
            {
                Dictionary<string, object> dict = GetEventDicFromCache();
                dict.Add("currency", "USD");
                dict.Add("value", _prevAccumulatePaid);
                SendAnalyticsEvent("Total_Ads_Revenue_001", dict);
                _prevAccumulatePaid = 0.0;
            }

            PlayerPrefs.SetFloat(InternalPrefsDef.ACCUMULATE_PAID, (float)_prevAccumulatePaid);
        }

        private void InitControl()
        {
            ThirdSDK.inst.AddListener(THIRD_EVENT_TYPE.TET_FB_DEEP_LINK_URL, OnFbDeepLinkGetEvent);
        }

        private void OnFbDeepLinkGetEvent(object obj)
        {
            string deepLinkUrl = obj as string;
            if (string.IsNullOrEmpty(deepLinkUrl))
                return;

            deepLinkUrl = FormatDLinkUrl(deepLinkUrl);
            Dictionary<string, object> dict = GetEventDicFromCache();
            dict.Add("source", deepLinkUrl);
            dict.Add("name", deepLinkUrl);
            dict.Add("ad_network", "fb");
            SendAnalyticsEvent("from_ad", dict);
        }

        private string FormatDLinkUrl(string url)
        {
            string scheme = "://";
            int idx = url.IndexOf(scheme);
            if (idx != -1)
            {
                url = url.Substring(idx + scheme.Length);
            }
            return url;
        }

        private void SendAnalyticsEvent(string eventName, Dictionary<string, object> dict = null)
        {
            ThirdSDK.inst.SendAnalyticsEvent(eventName, dict);
            PushEventDicToCache(dict);
        }
    }
}