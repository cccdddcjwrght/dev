
using Unity.Entities;
using System;
using System.Collections;
namespace SGame.Http
{
    /// <summary>
    /// HTTP 返回结果, 支持协程 等待
    /// </summary>
    public class HttpResult : IComponentData, IEnumerator
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isDone;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string error;

        /// <summary>
        /// 返回数据
        /// </summary>
        public string data;

		public byte[] buffer;

        public bool MoveNext() { return !isDone; }

        public object Current { get { return data; } }

        public void Reset() { }
    }
}