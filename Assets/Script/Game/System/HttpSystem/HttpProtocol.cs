using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Serialization.Json;
using UnityEngine;

namespace Http
{
	public struct HttpPackage
	{
		public int code;
		public int version;
		public string msg;
		public string data;
	}


	public static class HttpProtocol
	{
		static public JsonSerializationParameters Parameters = new JsonSerializationParameters()
		{
			Minified = true,

		};

		static public HttpPackage Encode<T>(T data, int msgID = 0)
		{
			return new HttpPackage()
			{
				code = msgID,
				data = JsonSerialization.ToJson(data, Parameters),
			};
		}

		static public T Dencode<T>(HttpPackage package)
		{
			if (package.data != null && package.data.Length > 0)
			{
				return JsonSerialization.FromJson<T>(package.data);
			}
			return default;
		}

		static public Dictionary<string, object> Dencode(HttpPackage package)
		{
			if (package.data != null && package.data.Length > 0)
			{
				return JsonSerialization.FromJson<Dictionary<string, object>>(package.data);
			}
			return default;
		}

		static public string ToJson(this HttpPackage package)
		{
			return JsonSerialization.ToJson(package, Parameters);
		}

		public static HttpPackage FromJson(this HttpPackage package, string json)
		{
			var p = JsonSerialization.FromJson<HttpPackage>(json, Parameters);
			package.code = p.code;
			package.msg = p.msg;
			package.data = p.data;
			package.version = p.version;
			return package;
		}

	}

}