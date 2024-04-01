using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ThirdSdk
{
    /// <summary>
    /// 事件采集缓冲器
    /// </summary>
    public class InternalAnalysticsCacher
    {
        private List<string> _lstCache = new List<string>();

        public void AppendCache(string name, IDictionary<string, object> dictParams = null)
        {
            if (dictParams != null)
            {
                Dictionary<string, object> dicSaveParams = new Dictionary<string, object>(dictParams);
                _lstCache.Add(InternalAnalysticsEvtPair.GenEvtPairString(name, dicSaveParams));
            }
            else
            {
                _lstCache.Add(InternalAnalysticsEvtPair.GenEvtPairString(name, null));
            }
        }

        public bool HasCache()
        {
            return _lstCache.Count > 0;
        }

        public InternalAnalysticsEvtPair PopCache()
        {
            if (_lstCache.Count <= 0)
                return null;

            string cacheStr = _lstCache[0];
            _lstCache.RemoveAt(0);
            return InternalAnalysticsEvtPair.GenEvtPairObj(cacheStr);
        }
    }

    /// <summary>
    /// 数据采集事件键值对
    /// </summary>
    public class InternalAnalysticsEvtPair
    {
        public string nameEvt;
        public Dictionary<string, object> dictParams;

        private static InternalAnalysticsEvtPair _tmp;
        /// <summary>
        /// 生成json字符串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dictParams"></param>
        /// <returns></returns>
        public static string GenEvtPairString(string name, Dictionary<string, object> dictParams)
        {
            if (_tmp == null)
            {
                _tmp = new InternalAnalysticsEvtPair();
            }
            _tmp.nameEvt = name;
            _tmp.dictParams = dictParams;
            return LitJson.JsonMapper.ToJson(_tmp);
        }

        public static InternalAnalysticsEvtPair GenEvtPairObj(string strJson)
        {
            return LitJson.JsonMapper.ToObject<InternalAnalysticsEvtPair>(strJson);
        }
    }
}