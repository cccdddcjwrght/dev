using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SGame;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SDK.TDSDK
{
	public enum TDEvent
	{
		new_device,
		register,
		login,
		update,
		enter_game,
		guide_step,
	}

	public class PItem : IEquatable<PItem>
	{
		public string name;
		public List<string> events;
		public Func<string, object> get;
		public object val;
		public bool once;
		public bool upload;
		private bool saved;


		public object GetVal()
		{
			var ret = get == null ? val : get(name);
			return ret;
		}

		public bool Check(out object ret, Dictionary<string, object> properties = null)
		{
			ret = GetVal();
			if (get == null || !System.Object.Equals(ret, val))
			{
				val = ret;
				if (properties != null && upload && (!once || !saved))
				{
					saved = true;
					properties[name] = val;
				}
				return true;
			}
			return false;
		}

		public void Set(object val)
		{
			this.val = val;
			saved = false;
		}

		public void Clear()
		{
			events?.Clear();
			get = null;
			name = null;
			val = null;
		}

		public bool Equals(PItem other)
		{
			return other.name == name;
		}

		public PItem LinkEvent(params string[] events)
		{
			if (events != null && events.Length > 0)
			{
				this.events = this.events ?? new List<string>();
				this.events.AddRange(events);
			}
			return this;
		}

		public PItem LinkEvent(params TDEvent[] events)
		{
			if (events != null && events.Length > 0)
			{
				this.events = this.events ?? new List<string>();
				this.events.AddRange(events.Select(e => e.ToString()));
			}
			return this;
		}

	}

	public class ItemCache
	{

		static public ItemCache Item = new ItemCache();

		public int type;
		public int id;
		public int num;
		public int after;
		public string reason;

	}

	[Serializable]
	public class ItemUseInfo
	{
		static public ItemUseInfo Info = new ItemUseInfo();

		public List<ItemInfo> items = new List<ItemInfo>();
	}

	[Serializable]
	public struct ItemInfo
	{
		public int id;
		public int count;
	}

	partial class ThinkDataSDK
	{

		#region Member
		static ILog log = LogManager.GetLogger("TD.Common");

		private DataCenter appData { get { return DataCenter.Instance; } }


		private Dictionary<string, PItem> _properties = new Dictionary<string, PItem>();
		private Dictionary<string, object> _caches = new Dictionary<string, object>();
		private Dictionary<string, object> _onceCaches = new Dictionary<string, object>();

		private string netType;

		#endregion

		#region Partial

		partial void DoAwake()
		{
			netType = Application.internetReachability.ToString();
			RegisterProperties();
			RegisterEvent();
			if (IsFirstStartApp())
				TrackFirst("new_device", GetProperty("device_id"));
		}

		partial void DoGetCommonSuperProperties(ref Dictionary<string, object> properties)
		{
			properties = properties ?? new Dictionary<string, object>();
		}

		partial void DoGetDynamicSuperProperties(ref Dictionary<string, object> properties)
		{
			properties = properties ?? new Dictionary<string, object>();
			properties["network_type"] = netType;
		}

		partial void DoFillEventProperties(string eventName, ref Dictionary<string, object> properties)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				var ps = properties ?? new Dictionary<string, object>();
				_properties.Values.Where(p => p.events != null && p.events.Contains(eventName)).All((p) =>
				{
					ps[p.name] = p.GetVal();
					return true;
				});
				properties = ps;
			}
		}

		#endregion

		#region Method

		private void RegisterEvent()
		{
			//通用打点接口
			EventManager.Instance.Reg<string, object[]>(-1, TrackNormal);
			EventManager.Instance.Reg(((int)GameEvent.LOGIN_COMPLETE), () =>
			{
				OnLogined(DataCenter.Instance.accountData.playerID.ToString());
			});

			EventManager.Instance.Reg(((int)GameEvent.ENTER_GAME), () => TrackNormal(TDEvent.enter_game.ToString()));
			EventManager.Instance.Reg<int>(((int)GameEvent.GUIDE_STEP), (a) => TrackNormal(TDEvent.guide_step.ToString(), "step", a));

		}

		private void RegisterProperties()
		{
			RegisterProperty("device_id", SystemInfo.deviceUniqueIdentifier, once: true);
			RegisterProperty("instancing", get: (k) => SystemInfo.supportsInstancing ? 1 : 0, upload: false).LinkEvent(TDEvent.login);
		}

		#endregion

		#region Event


		public void OnLogined(string id)
		{
			var time = System.DateTime.Now;
			Login(id);
			if (DataCenter.IsFirstLogin)
			{
				TrackNormal(TDEvent.register.ToString());
				UpdateData("register_time", time, true);
				UpdateData("first_login_time", time, true);
			}
			UpdateData("last_login_time", time);
			TrackNormal("login");
		}

		public void OnDataRefresh()
		{
			netType = Application.internetReachability.ToString();
			if (_properties != null && _properties.Count > 0)
			{
				_caches = _caches ?? new Dictionary<string, object>();
				_onceCaches = _onceCaches ?? new Dictionary<string, object>();

				_caches.Clear();
				_onceCaches.Clear();

				foreach (var item in _properties)
				{
					var v = item.Value;
					if (!v.once)
						v.Check(out _, _caches);
					else
						v.Check(out _, _onceCaches);
				}
				if (_caches.Count > 0)
					UpdateDatas(_caches, false);
				if (_onceCaches.Count > 0)
					UpdateDatas(_onceCaches, true);
			}
		}


		#endregion

		#region Private

		private PItem RegisterProperty(string name, object val = null, System.Func<string, object> get = null, bool once = false, bool upload = true)
		{
			if (string.IsNullOrEmpty(name)) return default;
			if (!_properties.ContainsKey(name))
				_properties[name] = new PItem() { name = name, val = val, get = get, once = once, upload = upload };
			return _properties[name];
		}

		private object GetPropertyVal(string name)
		{
			if (!string.IsNullOrEmpty(name) && _properties.TryGetValue(name, out var p))
			{
				return p.GetVal();
			}
			return default;
		}

		private object[] GetProperty(string name)
		{
			return new object[] { name, GetPropertyVal(name) };
		}

		public static bool IsFirstStartApp()
		{
			const string k = "__install__";
			var flag = System.Reflection.Assembly.GetCallingAssembly().GetHashCode();
			if (!PlayerPrefs.HasKey(k))
				PlayerPrefs.SetInt(k, flag);
			var v = PlayerPrefs.GetInt(k, flag);
			return v == flag;
		}

		#endregion

	}
}
