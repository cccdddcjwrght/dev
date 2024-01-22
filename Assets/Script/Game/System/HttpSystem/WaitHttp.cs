using System.Collections;
using System.Collections.Generic;
using Http;
using UnityEngine;

namespace SGame
{
	public enum HttpMethod
	{
		GET,
		POST,
	}

	/// <summary>
	/// Http请求封装
	/// 发送
	/// WaitHttp.Request(<url or api>:string, method:HttpMethod , token:string).Run()
	/// 发送并等待
	/// WaitHttp.Request(<url or api>:string, method:HttpMethod , token:string).RunAndWait()
	/// </summary>
	public class WaitHttp
	{

		private string _url;
		private string _data;
		private HttpMethod _method;
		private string _api;
		private string _token;
		private float _time = 5f;

		private int _version;

		private Http.HttpResult _result;
		private System.Action<WaitHttp, string> _onSuccess;
		private System.Action<string> _onFail;


		public int State { get; private set; }
		public bool Completed { get { return State != 0; } }
		public string Error { get { return _result?.error; } }
		public string Data { get { return _result?.data; } }

		static public WaitHttp Request(string url, HttpMethod method = HttpMethod.GET, string token = null)
		{
			return new WaitHttp().SetUrl(url).SetMethod(method).SetToken(token);
		}


		public WaitHttp()
		{
		}

		public WaitHttp SetUrl(string url)
		{
			this._url = url;
			return this;
		}

		public WaitHttp SetToken(string token)
		{
			_token = token;
			return this;
		}

		/// <summary>
		/// 添加上报数据
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public WaitHttp SetData(string data)
		{
			this._data = data;
			return this;
		}

		/// <summary>
		/// 编码并添加上报数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="msgID"></param>
		/// <returns></returns>
		public WaitHttp EncodeData<T>(T data, int msgID = 0)
		{
			_data = HttpProtocol.Encode(data, msgID).ToJson();
			return this;
		}

		/// <summary>
		/// 键值对数据
		/// </summary>
		/// <param name="msgId"></param>
		/// <param name="args">k1,v1,k2,v2......</param>
		/// <returns></returns>
		public WaitHttp SetData(int msgId, params object[] args)
		{
			if (args != null && args.Length % 2 == 0)
			{
				var dic = new Hashtable();
				for (int i = 0; i < args.Length - 1; i += 2)
				{
					var k = args[i];
					if (k == null) continue;
					dic[k.ToString()] = args[i + 1];
				}
				EncodeData(dic, msgId);
			}
			else
				Debug.LogError("参数不是成对出现");
			return this;
		}

		public WaitHttp SetMethod(HttpMethod method)
		{
			this._method = method;
			return this;
		}

		public WaitHttp SetApi(string api)
		{
			this._api = api;
			return this;
		}

		public WaitHttp OnSuccess(System.Action<WaitHttp, string> success)
		{
			_onSuccess = success;
			return this;
		}

		public WaitHttp OnFail(System.Action<string> fail)
		{
			_onFail = fail;
			return this;
		}

		public WaitHttp Timeout(float time)
		{
			this._time = time;
			return this;
		}

		public IEnumerator Wait()
		{
			if (!Completed && _result != null)
				yield return DoWait();
		}

		/// <summary>
		/// 执行请求
		/// </summary>
		/// <returns></returns>
		public WaitHttp Run()
		{
			if (_result == null)
			{
				if (string.IsNullOrEmpty(_url) && string.IsNullOrEmpty(_api))
				{
					Fail("url is null");
					return this;
				}
				//拼装url和api
				//eg: http://www.xxx.com/[api]
				var url = _url + _api;
				State = 0;
				_version++;
				if (!string.IsNullOrEmpty(_token))
					Http.HttpSystem.Instance.SetToken(_token);
				switch (_method)
				{
					case HttpMethod.GET:
						//拼装数据
						//eg: http://www.xxx.com/[api]?[data]
						if (!string.IsNullOrEmpty(_data))
						{
							if (!url.Contains("?"))
								url += "?" + _data;
							else
								url += _data;
						}
						_result = Http.HttpSystem.Instance.Get(url);
						break;
					case HttpMethod.POST:
						_result = Http.HttpSystem.Instance.Post(url, _data);
						break;
				}
			}
			return this;
		}

		public WaitHttp RunAndWait()
		{
			Run();
			FiberCtrl.Pool.Run(Wait());
			return this;
		}

		/// <summary>
		/// 解析为对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="msg"></param>
		/// <returns></returns>
		public bool TryGet<T>(out T msg)
		{
			msg = default;
			if (State == 1)
			{
				try
				{
					msg = HttpProtocol.Dencode<T>(new HttpPackage().FromJson(_result.data));
					return true;
				}
				catch (System.Exception e)
				{
					Fail(e.Message);
				}
			}
			return false;
		}

		public bool TryGetOverride<T>(ref T msg)
		{
			msg = default;
			if (State == 1)
			{
				try
				{
					HttpProtocol.DencodeOverride<T>(new HttpPackage().FromJson(_result.data), ref msg);
					return true;
				}
				catch (System.Exception e)
				{
					Fail(e.Message);
				}
			}
			return false;
		}

		/// <summary>
		/// 解析数据为字典
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		public bool TryGetTable(out Hashtable msg)
		{
			msg = default;
			if (State == 1)
			{
				try
				{
					msg = HttpProtocol.Dencode(new HttpPackage().FromJson(_result.data));
					return msg != null;
				}
				catch (System.Exception e)
				{
					Fail(e.Message);
				}
			}
			return false;
		}

		public WaitHttp Flush()
		{
			_result = null;
			State = 0;
			return this;
		}

		public WaitHttp Clear()
		{
			_result = null;
			_onFail = null;
			_onSuccess = null;
			_url = _api = _data = null;
			return this;
		}

		private IEnumerator DoWait()
		{
			var ver = _version;
			var t = _time < 0 ? 0 : _time;
			var timeout = false;
			if (t == 0) yield return _result;
			else
			{
				while (t > 0)
				{
					if (_result.isDone) break;
					else yield return null;
					t -= Time.deltaTime;
					if (t <= 0) timeout = true;
				}
			}

			if (_version == ver)
			{
				var state = !timeout && string.IsNullOrEmpty(_result.error);
				try
				{
					if (state) Success(_result.data);
					else Fail(timeout ? "timeout!!!" : _result.error);
				}
				catch (System.Exception e)
				{
					Debug.LogException(e);
				}
			}
		}

		private void Success(string data)
		{
			State = 1;
			_onSuccess?.Invoke(this, data);
		}

		private void Fail(string error)
		{
			State = -1;
			if (!string.IsNullOrEmpty(error))
			{
				UnityEngine.Debug.LogError("http error:" + error);
			}
			_onFail?.Invoke(error ?? "http error");
		}

	}

}