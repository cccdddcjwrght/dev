using System;
using System.Collections.Generic;

namespace ThirdSdk
{
    public class ThirdEvent
    {
        #region Inst
        private static readonly ThirdEvent _inst = new ThirdEvent();
        static ThirdEvent() { }
        public static ThirdEvent inst { get { return _inst; } }
        #endregion

        private Dictionary<THIRD_EVENT_TYPE, Action<object>> _list = new Dictionary<THIRD_EVENT_TYPE, Action<object>>();

        /// <summary>
        /// 发送事件消息
        /// </summary>
        /// <param name="modelEvent"></param>
        /// <param name="param"></param>
        public void SendEvent(THIRD_EVENT_TYPE eventType, object param = null)
        {
            CodePipeline.Push4Invoke( () =>
            {
                if (_list.ContainsKey(eventType))
                {
                    Action<object> callback = _list[eventType];
                    callback(param);
                }
            });
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="modelEvent"></param>
        /// <param name="listener"></param>
        public void AddListener(THIRD_EVENT_TYPE eventType, Action<object> listener)
        {
            if (_list.ContainsKey(eventType))
            {
                _list[eventType] -= listener;
                _list[eventType] += listener;
            }
            else
            {
                _list[eventType] = listener;
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="modelEvent"></param>
        /// <param name="listener"></param>
        public void RemoveListener(THIRD_EVENT_TYPE eventType, Action<object> listener)
        {
            if (!_list.ContainsKey(eventType))
                return;

            _list[eventType] -= listener;

            if (_list[eventType] == null)
                _list.Remove(eventType);
        }
    }
}
