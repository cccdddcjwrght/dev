//*************************************************************************
//	创建日期:	2024-1-8
//	文件名称:	HttpSystem.cs
//  创 建 人:    Silekey
//	版权所有:	群星
//	说    明:	封装了对HTTP的请求
// 范例:
//              HttpResult result =  HttpSystem.Instance.Get("http://xxx.xxx.xxx/xxx?=1)
//              yield return result;
//              if (string.IsNullOrEmpty(result.error)) 
//                 Debug.Log(result.data);
//*************************************************************************

using Unity.Entities;
namespace SGame.Http
{
    [UpdateAfter(typeof(HttpSystemSpawn))]
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class HttpSystem : SystemBase
    {
        private string m_token;
        
        public static HttpSystem Instance
        {
            get { return World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<HttpSystem>(); }
        }

        protected override void OnCreate()
        {
            m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        public void SetToken(string value)
        {
            m_token = value;
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            
            // 将结果存入返回值对象中
            Entities.ForEach((Entity e, HttpData data, HttpResult result) =>
            {
                // 将返回值存入结果中去
                if (data.result.isDone)
                {
                    result.isDone = true;
                    result.error = null;
                    if (string.IsNullOrEmpty(data.request.error))
                    {
                        if (data.isGet)
                        {
                            result.data = data.request.downloadHandler.text;
                        }
                    }
                    else
                    {
                        result.error = data.request.error;
                    }
                    commandBuffer.DestroyEntity(e);
                }
            }).WithoutBurst().Run();
        }

        /// <summary>
        /// post 请求结束后会自动销毁
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResult Post(string url, string data)
        {
            HttpRequest request = new HttpRequest() { url = url, post = data, isGet = false, token = m_token};
            HttpResult result = new HttpResult() { isDone = false, data = null, error = null };
            var e = EntityManager.CreateEntity();
            EntityManager.AddComponentObject(e, request);
            EntityManager.AddComponentObject(e, result);
            return null;
        }
        
        /// <summary>
        /// get 请求后
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public HttpResult Get(string url)
        {
            HttpRequest request = new HttpRequest() { url = url, post = null, isGet = true, token = m_token};
            HttpResult result = new HttpResult() { isDone = false, data = null, error = null };   
            var e = EntityManager.CreateEntity();
            EntityManager.AddComponentObject(e, request);
            EntityManager.AddComponentObject(e, result);
            return result;
        }
        
        
        ////// 数据 /////////
        private EndSimulationEntityCommandBufferSystem m_commandBuffer;
    }
}