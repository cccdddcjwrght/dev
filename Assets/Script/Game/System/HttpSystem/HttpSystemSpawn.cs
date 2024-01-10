//*************************************************************************
//	创建日期:	2024-1-8
//	文件名称:	HttpSystemSpawn.cs
//  创 建 人:    Silekey
//	版权所有:	群星
//	说    明:	执行HTTP 的请求
//*************************************************************************

using UnityEngine.Networking;
using Unity.Entities;
using System.Text;

namespace SGame.Http
{
	class DefultCertificateHandler : CertificateHandler
	{

		readonly static public DefultCertificateHandler Current = new DefultCertificateHandler();

		protected override bool ValidateCertificate(byte[] certificateData) => true;

	}

	[UpdateInGroup(typeof(GameLogicAfterGroup))]
	public partial class HttpSystemSpawn : SystemBase
	{
		protected override void OnCreate()
		{
			m_commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
		}

		protected override void OnUpdate()
		{
			var commandBuffer = m_commandBuffer.CreateCommandBuffer();

			/// 1. 创建HTTP 请求
			Entities.ForEach((Entity e, HttpRequest req) =>
			{
				HttpData data = new HttpData() { isGet = req.isGet };
				if (req.isGet)
				{
					data.request = UnityWebRequest.Get(req.url);
				}
				else
				{
					data.request = UnityWebRequest.Post(req.url, req.post);
					data.request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
				}
				data.request.SetRequestHeader("Authorization", "token " + req.token);
				data.request.certificateHandler = DefultCertificateHandler.Current;
				data.result = data.request.SendWebRequest();
				commandBuffer.AddComponent(e, data);
				commandBuffer.RemoveComponent<HttpRequest>(e);
			}).WithoutBurst().Run();
		}

		////// 数据 /////////////////////////////////////////////////
		private EndSimulationEntityCommandBufferSystem m_commandBuffer;
	}
}