using System.Collections.Generic;
using UnityEngine;
using System;

#if USE_THIRD_SDK
using Firebase;
using Firebase.Analytics;
using Firebase.Crashlytics;
#endif


namespace ThirdSdk
{
    public class FirebaseSDK
    {
        #region Inst
        private static readonly FirebaseSDK _inst = new FirebaseSDK();
        static FirebaseSDK() { }
        public static FirebaseSDK inst { get { return _inst; } }
        #endregion

        private bool _firebaseInitComplete = false;  //是否初始化完成
        private InternalAnalysticsCacher _eventCache = new InternalAnalysticsCacher();

        private FirebaseSDK()
        {
        }

        ~FirebaseSDK()
        {
        }

        /// <summary>
        /// SDK初始化
        /// </summary>
        public void Init()
        {
#if USE_THIRD_SDK
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result == DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_FIREBASE_INIT_FAILURE);
                }
            });
#endif
        }


        private void InitializeFirebase()
        {
#if USE_THIRD_SDK
            _firebaseInitComplete = true;
            InitAnalytics();
            ThirdEvent.inst.SendEvent(THIRD_EVENT_TYPE.TET_FIREBASE_INIT_COMPLETE);
#endif
        }

        public void OnActive()
        {
        }

        private void InitAnalytics()
        {
#if USE_THIRD_SDK
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            CheckSendCacheEvent();
#endif
        }

        /// <summary>
        /// 发送数据分析记录事件
        /// </summary>
        public void SendAnalyticsEvent(string eventName, IDictionary<string, object> dicParam)
        {
#if USE_THIRD_SDK
            if (!_firebaseInitComplete)
            {
                _eventCache.AppendCache(eventName, dicParam);
                return;
            }

            SendAnalyticsEventDirect(eventName, dicParam);
#endif
        }

        /// <summary>
        /// 发送数据分析记录事件
        /// </summary>
        public void SendAnalyticsEventDirect(string eventName, IDictionary<string, object> dicParam)
        {
#if USE_THIRD_SDK
            try
            {
                if(dicParam != null)
                {    
                    List<Parameter> lstParams = new List<Parameter>();
                    foreach(KeyValuePair<string, object> entry in dicParam)
                    {
                        if(entry.Value == null)
                            lstParams.Add(new Parameter(entry.Key, "is null value"));
                        if(entry.Value is long)
                            lstParams.Add(new Parameter(entry.Key, (long)entry.Value));
                        else if(entry.Value is int)
                            lstParams.Add(new Parameter(entry.Key, (int)entry.Value));
                        else if (entry.Value is double)
                            lstParams.Add(new Parameter(entry.Key, (double)entry.Value));
                        else if (entry.Value is float)
                            lstParams.Add(new Parameter(entry.Key, (float)entry.Value));
                        else if (entry.Value is string)
                            lstParams.Add(new Parameter(entry.Key, (string)entry.Value));
                        else if (entry.Value is uint)
                            lstParams.Add(new Parameter(entry.Key, (uint)entry.Value));
                        else
                        {
                            lstParams.Add(new Parameter("is_error_type", entry.Key));
                        }
                    }
                    FirebaseAnalytics.LogEvent(eventName, lstParams.ToArray());

				}
				else
                {
                    FirebaseAnalytics.LogEvent(eventName);
                }
            }
            catch (InitializationException e)
            {
                //初始化失败不处理
            }
            catch (Exception exAll)
            {
                Debug.Log(exAll);
            }
#endif
        }

        private void CheckSendCacheEvent()
        {
#if USE_THIRD_SDK 
            CodePipeline.Push4Invoke(() => OnProcessTickerSendCache());
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
                CodePipeline.Push4Invoke(() => OnProcessTickerSendCache());
            }
#endif
        }

    }
}