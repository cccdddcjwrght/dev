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


	public class WaitHttp
	{

		private string _url;
		private string _data;
		private HttpMethod _method;
		private string _api;
		private string _token;

		private int _version;

		private Http.HttpResult _result;
		private System.Action<WaitHttp, string> _onSuccess;
		private System.Action<string> _onFail;


		public int State { get; private set; }
		public bool Completed { get { return State != 0; } }

		static public WaitHttp Get(string url, HttpMethod method = HttpMethod.GET, string token = null)
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

		public WaitHttp SetData(string data)
		{
			this._data = data;
			return this;
		}

		public WaitHttp EncodeData<T>(T data, int msgID = 0)
		{
			_data = HttpProtocol.Encode(data, msgID).ToJson();
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

		public IEnumerator Wait()
		{
			return _result;
		}

		public WaitHttp Run()
		{
			if (_result == null)
			{
				if (string.IsNullOrEmpty(_url) && string.IsNullOrEmpty(_api))
				{
					Error("url is null");
					return this;
				}
				var url = _url;
				if (!string.IsNullOrEmpty(_api))
					url += _api;
				State = 0;
				_version++;
				if (!string.IsNullOrEmpty(_token))
					Http.HttpSystem.Instance.SetToken(_token);
				switch (_method)
				{
					case HttpMethod.GET:
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
				FiberCtrl.Pool.Run(DoWait());
			}
			return this;
		}

		public bool TryGet<T>(out T msg)
		{
			msg = default;
			if (State == 1)
			{
				try
				{
					var package = HttpPackage.FromJson(_result.data);
					msg = HttpProtocol.Dencode<T>(package);

				}
				catch (System.Exception e)
				{
					Error(e.Message);
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
			yield return _result;
			if (_version == ver)
			{
				var state = string.IsNullOrEmpty(_result.error);
				try
				{
					if (state) Success(_result.data);
					else Error(_result.error);
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

		private void Error(string error)
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