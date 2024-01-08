
using Unity.Entities;

namespace SGame.Http
{
    /// <summary>
    /// 创建HTTP 请求
    /// </summary>
    public class HttpRequest : IComponentData
    {
        public string url;   // 请求的URL
        public string post;  // 推送数据
        public bool   isGet; // 是否是GET
    }
}