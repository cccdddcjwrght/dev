using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdSdk
{
    public class InternalSdkManager
    {
        #region Inst
        private static readonly InternalSdkManager _inst = new InternalSdkManager();
        static InternalSdkManager() { }
        public static InternalSdkManager inst { get { return _inst; } }
        #endregion

        private bool _isFirebaseInit = false;


        public void Init(GameObject rootObj)
        {
            CodePipeline.Init(rootObj);
            InitSdk();

        }


        public void OnActive()
        {
            FirebaseSDK.inst.OnActive();
            FacebookSDK.inst.OnActive();
        }


        private void InitSdk()
        {
            InitFirebaseSdk();
            InitFacebookSdk();
        }

        private void InitFirebaseSdk()
        {
            ThirdSDK.inst.AddListener(THIRD_EVENT_TYPE.TET_FIREBASE_INIT_COMPLETE, OnFirebaseInitComplete);
            ThirdSDK.inst.AddListener(THIRD_EVENT_TYPE.TET_FIREBASE_INIT_FAILURE, OnFirebaseInitFail);
            CodePipeline.Push4Invoke(() => FirebaseSDK.inst.Init());
        }

        private void OnFirebaseInitComplete(object obj)
        {
            _isFirebaseInit = true;
            CheckThirdInitComplete();
        }

        private void OnFirebaseInitFail(object obj)
        {
            _isFirebaseInit = true;
            CheckThirdInitComplete();
        }


        private void InitFacebookSdk()
        {
            ThirdSDK.inst.AddListener(THIRD_EVENT_TYPE.TET_FB_DEEP_LINK_URL, OnFbDeepLinkGetEvent);
            CodePipeline.Push4Invoke(() => FacebookSDK.inst.Init());
        }

        private void CheckThirdInitComplete()
        {
            if (!_isFirebaseInit)
                return;

            CodePipeline.Push4Invoke(() => ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_THIRD_SDK_INIT_COMPLETE, true));
        }

        private void OnFbDeepLinkGetEvent(object obj)
        {
            string deepLinkUrl = obj as string;
            if (string.IsNullOrEmpty(deepLinkUrl))
                return;

            deepLinkUrl = FormatDLinkUrl(deepLinkUrl);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("source", deepLinkUrl);
            dict.Add("ad_network", "fb");
            ThirdSDK.inst.SendAnalyticsEvent("from_ad", dict);
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
    }
}