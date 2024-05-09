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
using Unity.Collections;

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
		private EntityQuery m_query;
		protected override void OnCreate()
		{
			m_query = GetEntityQuery(typeof(HttpRequest));
		}

		protected override void OnUpdate()
		{
			var entities = m_query.ToEntityArray(Allocator.Temp);
			if (entities.Length > 0)
			{
				var requests = m_query.ToComponentDataArray<HttpRequest>();
				for (int i = 0; i < entities.Length; i++)
				{
					var req = requests[i];
					var e = entities[i];
					HttpData data = new HttpData() { isGet = req.isGet , isBuffer = req.buffer };
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
					EntityManager.AddComponentData(e, data);
					EntityManager.RemoveComponent<HttpRequest>(e);
				}
			}
		}

		////// 数据 /////////////////////////////////////////////////
		private EndSimulationEntityCommandBufferSystem m_commandBuffer;
	}
}