using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	}

	public static class HttpProtocol
	{

		static public HttpPackage Encode<T>(T data, int msgID = 0)
		{
			string d = (data is Hashtable h) ? MiniJSON.jsonEncode(h) : JsonUtility.ToJson(data);

			return new HttpPackage()
			{
				code = msgID,
				data = d,
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

		static public void DencodeOverride<T>(HttpPackage package, ref T data)
		{
			if (package.data != null && package.data.Length > 0)
			{
				JsonUtility.FromJsonOverwrite(package.data, data);
			}
		}

		static public Hashtable Dencode(HttpPackage package)
		{
			if (package.data != null && package.data.Length > 0)
			{
				return MiniJSON.jsonDecode(package.data) as Hashtable;
			}
			return default;
		}

		static public string ToJson(this HttpPackage package)
		{
			return JsonUtility.ToJson(package);
		}

		public static HttpPackage FromJson(this HttpPackage package, string json)
		{
			var p = JsonUtility.FromJson<HttpPackage>(json);
			package.code = p.code;
			package.msg = p.msg;
			package.data = p.data;
			package.version = p.version;
			return package;
		}

	}

}