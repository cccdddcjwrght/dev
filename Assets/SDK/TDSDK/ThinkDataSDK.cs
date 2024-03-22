using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

using ThinkingData.Analytics;
using TDAPI = ThinkingData.Analytics.TDAnalytics;
using TDTOKEN = ThinkingData.Analytics.TDConfig;
using TDDynamicSuperProperties = ThinkingData.Analytics.TDDynamicSuperPropertiesHandler;
using TDAutoTrackEventCallback = ThinkingData.Analytics.TDAutoTrackEventHandler;
using TDEvent = ThinkingData.Analytics.TDEventModel;
using TDFirstEvent = ThinkingData.Analytics.TDFirstEventModel;
using TDUpdatableEvent = ThinkingData.Analytics.TDUpdatableEventModel;
using TDOverwritableEvent = ThinkingData.Analytics.TDOverwritableEventModel;


using TDMODE = ThinkingData.Analytics.TDMode;
using TDTIME = ThinkingData.Analytics.TDTimeZone;
using TDNetworkType = ThinkingData.Analytics.TDNetworkType;
using TDAutoTrackEventType = ThinkingData.Analytics.TDAutoTrackEventType;

namespace SDK.TDSDK
{
	public enum TDEventEnum
	{

		Normal,
		First,
		Update,
		Overwrite,

	}

	public partial class ThinkDataSDK : TDDynamicSuperProperties, TDAutoTrackEventCallback
	{

		private bool _isInited = false;
		private bool _isStarted = false;
		private Func<string, string> _configProxy;
		private TDAPI _api;

		public void Init(GameObject gameObject = null, Func<string, string> configProxy = null, bool autoStart = false)
		{

			if (_isInited) return;
			_configProxy = configProxy;

			var api = gameObject?.GetComponent<TDAPI>()
				?? GameObject.FindObjectOfType<TDAPI>();

			if (api == null)
			{
				gameObject = gameObject ?? new GameObject("ThinkingAnalytics");
				api = gameObject.AddComponent<TDAPI>();
			}

			var tokens = api.configs == null || api.configs.Length == 0 ? new TDTOKEN[1] : api.configs;
			var tk = tokens[0] == null || tokens[0].appId == null ? new TDTOKEN("",serverUrl: "") : tokens[0];
			var appid = GetCfg<string>("td_appid", tk.appId);
			var url = GetCfg<string>("td_url", tk.serverUrl);

			if (string.IsNullOrEmpty(appid) || string.IsNullOrEmpty(url))
			{
				Debug.LogError("没有定义ThinkingData APPID 或 URL！！！");
				return;
			}

			tk.appId = appid;
			tk.serverUrl = url;
			tk.mode = GetCfg<TDMODE>("td_mode", tk.mode);
			tk.timeZone = GetCfg<TDTIME>("td_zone", tk.timeZone);

			tokens[0] = tk;
			api.configs = tokens;
			api.networkType = GetCfg<TDNetworkType>("td_net", api.networkType);
			api.enableLog = GetCfg<bool>("td_log", api.enableLog);
			api.startManually = GetCfg<bool>("td_auto", api.startManually);

			var superproperties = default(Dictionary<string, object>);
			DoGetCommonSuperProperties(ref superproperties);
			TDAPI.SetSuperProperties(superproperties);
			TDAPI.SetDynamicSuperProperties(this);
			TDAPI.EnableAutoTrack(TDAutoTrackEventType.All, this);
			_isInited = true;
			_api = api;
			if (autoStart || api.startManually)
				StartRun();
		}

		public void StartRun(Func<string, string> configProxy = null)
		{
			if (_isStarted) return;
			if (!_isInited)
				Init(null, configProxy, false);
			if (_isInited && !_isStarted && _api)
			{
				_isStarted = true;
				IEnumerator Run()
				{
					OnAwake();
					yield return null;
					if (!_api.startManually)
						TDAPI.Init();
					OnStart();
				}
				_api.StartCoroutine(Run());
			}
		}


		#region Data

		public void UpdateData<T>(string key, T val, bool once = false)
		{
			UpdateDatas(once, key, val);
		}

		public void AddData(string key, double val)
		{
			if (!string.IsNullOrEmpty(key))
				TDAPI.UserAdd(key, val);
		}

		public void DeleteData(params string[] ks)
		{
			if (ks != null && ks.Length > 0)
				TDAPI.UserUnset(new List<string>(ks));
		}

		public void UpdateDatas(bool once = false, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				var ps = ArgsToDic(args);
				if (ps == null || ps.Count == 0) return;
				UpdateDatas(ps, once);
			}
		}

		#endregion

		#region Event


		public void TrackNormal(string eventName, params object[] args)
		{
			Track(eventName, TDEventEnum.Normal, args);
		}

		public void TrackFirst(string eventName, params object[] args)
		{
			Track(eventName, TDEventEnum.First, args);
		}

		public void TrackUpdate(string eventName, params object[] args)
		{
			Track(eventName, TDEventEnum.Update, args);
		}

		public void Track(string eventName, TDEventEnum type = TDEventEnum.Normal, params object[] args)
		{
			if (string.IsNullOrEmpty(eventName)) return;
			var ps = args == null || args.Length == 0 ? null : ArgsToDic(args);
			DoFillEventProperties(eventName,ref ps);
			switch (type)
			{
				case TDEventEnum.Normal:
					TDAPI.Track(eventName, ps);
					break;
				case TDEventEnum.First:
					var normalEvent = new TDFirstEvent(eventName) { Properties = ps };
					TDAPI.Track(normalEvent);
					break;
				case TDEventEnum.Update:
					var updatableEvent = new TDUpdatableEvent("UPDATABLE_EVENT", eventName) { Properties = ps};
					updatableEvent.SetTime( DateTime.Now , TimeZoneInfo.Local) ;
					TDAPI.Track(updatableEvent);
					break;
				case TDEventEnum.Overwrite:
					var overWritableEvent = new TDOverwritableEvent("OVERWRITABLE_EVENT", eventName) { Properties = ps };
					TDAPI.Track(overWritableEvent);
					break;
			}
		}

		public Action<Dictionary<string, object>> TimeWatch(string eventName)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				TDAPI.TimeEvent(eventName);
				return (ps) => TDAPI.Track(eventName, ps);
			}
			return default;
		}

		#endregion

		#region 公共属性

		public Dictionary<string, object> GetDynamicSuperProperties()
		{
			var ps = new Dictionary<string, object>();
			DoGetDynamicSuperProperties(ref ps);
			return ps;
		}

		public Dictionary<string, object> GetAutoTrackEventProperties(int type, Dictionary<string, object> properties)
		{
			DoAutoTrackEventCallback(type, ref properties);
			return properties;
		}

		#endregion

		#region method

		private void OnAwake() => DoAwake();

		private void OnStart()
		{
			DoStart();
		}

		public void Login(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				Debug.LogError("Login ID Is Null");
				return;
			}
			TDAPI.Login(id);
		}



		public string GetDeviceID()
		{
			return TDAPI.GetDeviceId();
		}

		public string GetLocale()
		{
			return TDAPI.GetLocalRegion();
		}


		private void UpdateDatas(Dictionary<string, object> datas, bool once = false)
		{
			if (once)
				TDAPI.UserSetOnce(datas);
			else
				TDAPI.UserSet(datas);
		}

		private Dictionary<string, object> ArgsToDic(params object[] args)
		{
			if (args != null && args.Length > 0 && args.Length % 2 == 0)
			{
				var ps = new Dictionary<string, object>();
				for (int i = 0; i < args.Length; i += 2)
				{
					var a = args[i];
					if (a != null)
					{
						var k = a.ToString();
						if (!string.IsNullOrEmpty(k))
							ps[k] = args[i + 1];
					}
				}
				return ps;
			}
			else
			{
				Debug.LogError("args为空或者数量不是成对的");
			}
			return null;
		}

		private T GetCfg<T>(string name, T def = default)
		{
			if (_configProxy != null && !string.IsNullOrEmpty(name))
			{
				var val = _configProxy(name);
				if (!string.IsNullOrEmpty(val))
				{
					try
					{
						if (typeof(T) == typeof(bool))
						{
							return (T)(object)(val != null && val.ToLower() != "false");
						}
						else if (typeof(T).IsEnum)
						{
							if (Enum.TryParse(typeof(T), val, true, out var e))
								return (T)e;
						}
						else
							return (T)Convert.ChangeType(_configProxy(name), typeof(T));
					}
					catch
					{

					}
				}
			}
			return def;
		}



		#endregion

		#region 部分方法

		partial void DoAwake();

		partial void DoStart();

		/// <summary>
		/// 动态公共属性，一般用来或许游戏数据
		/// </summary>
		/// <param name="properties"></param>
		partial void DoGetDynamicSuperProperties(ref Dictionary<string, object> properties);

		/// <summary>
		/// 自动采集获取属性
		/// </summary>
		/// <param name="type"></param>
		/// <param name="properties"></param>
		partial void DoAutoTrackEventCallback(int type, ref Dictionary<string, object> properties);

		/// <summary>
		/// 基本app属性
		/// </summary>
		/// <param name="properties"></param>
		partial void DoGetCommonSuperProperties(ref Dictionary<string, object> properties);

		/// <summary>
		/// 填充事件额外属性
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="properties"></param>
		partial void DoFillEventProperties(string eventName, ref Dictionary<string, object> properties);

		#endregion

	}
}
