using Unity.Entities;
using UnityEngine.Networking;

namespace SGame.Http
{
    /// <summary>
    /// HTTP 请求数据
    /// </summary>
    public class HttpData : IComponentData
    {
        public UnityWebRequest                  request;
        public UnityWebRequestAsyncOperation    result;
        public bool                             isGet;
    }
}