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

using GameConfigs;
using Unity.Entities;
namespace SGame.Http
{
	[UpdateAfter(typeof(HttpSystemSpawn))]
	[UpdateInGroup(typeof(GameLogicAfterGroup))]
	public partial class HttpSystem : SystemBase
	{
		private string m_token;
		private string m_baseUrl;

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

		/// <summary>
		/// 设置基础Url，当请求传递的url只是简单的接口名的时候，就用这个url补全
		/// </summary>
		/// <param name="url"></param>
		public void SetBaseUrl(string url)
		{
			m_baseUrl = url;
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

					if (data.request.responseCode == 200 && string.IsNullOrEmpty(data.request.error))
					{
						if (data.isBuffer)
							result.buffer = data.request.downloadHandler.data;
						else
							result.data = data.request.downloadHandler.text;
					}
					else
					{
						result.error = string.IsNullOrEmpty(data.request.downloadHandler.text) ? data.request.error : data.request.downloadHandler.text;
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
		public HttpResult Post(string url, string data, bool isbuffer = false)
		{
			if (!url.Contains("://"))
				url = GetBaseUrl() + url;

			HttpRequest request = new HttpRequest() { url = url, post = data, isGet = false, token = m_token,buffer = isbuffer };
			HttpResult result = new HttpResult() { isDone = false, data = null, error = null };
			var e = EntityManager.CreateEntity();
			EntityManager.AddComponentObject(e, request);
			EntityManager.AddComponentObject(e, result);
			return result;
		}

		/// <summary>
		/// get 请求后
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public HttpResult Get(string url , bool isbuffer = false)
		{
			if (!url.Contains("://"))
				url = GetBaseUrl() + url;

			HttpRequest request = new HttpRequest() { url = url, post = null, isGet = true, token = m_token , buffer = isbuffer };
			HttpResult result = new HttpResult() { isDone = false, data = null, error = null };
			var e = EntityManager.CreateEntity();
			EntityManager.AddComponentObject(e, request);
			EntityManager.AddComponentObject(e, result);
			return result;
		}

		private string GetBaseUrl()
		{
			if (m_baseUrl == null)
			{
				SetBaseUrl(GlobalConfig.GetStr("svr_url") ?? "");
				SetToken(GlobalConfig.GetStr("svr_token") ?? "1");
			}
			return m_baseUrl;
		}

		////// 数据 /////////
		private EndSimulationEntityCommandBufferSystem m_commandBuffer;
	}
}