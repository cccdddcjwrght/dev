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
            InitInternalEventSdk();
            InitFirebaseSdk();
            InitFacebookSdk();
        }

        private void InitInternalEventSdk()
        {
            CodePipeline.Push4Invoke(() => InternalEventManager.inst.Init());
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
            CodePipeline.Push4Invoke(() => FacebookSDK.inst.Init());
        }

        private void CheckThirdInitComplete()
        {
            if (!_isFirebaseInit)
                return;

            CodePipeline.Push4Invoke(() => ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_THIRD_SDK_INIT_COMPLETE, true));
            CodePipeline.Push4Invoke(() => InternalAdManager.inst.InitAd());
        }

    }
}