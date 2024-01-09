using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Http
{
	public struct HttpPackage
	{
		public int code;
		public int version;
		public string msg;
		public string data;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public static HttpPackage FromJson(string json)
		{
			return JsonUtility.FromJson<HttpPackage>(json);
		}

	}


	public static class HttpProtocol
	{

		static public HttpPackage Encode<T>(T data, int msgID = 0)
		{
			return new HttpPackage()
			{
				code = msgID,
				data = JsonUtility.ToJson(data),
			};
		}

		static public T Dencode<T>(HttpPackage package)
		{
			if (package.data != null && package.data.Length > 0)
			{
				return JsonUtility.FromJson<T>(package.data);
			}
			return default;
		}

	}

}