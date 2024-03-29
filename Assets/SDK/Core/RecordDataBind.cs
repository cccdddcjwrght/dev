using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Record
{

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class BindAttr : Attribute
	{
		public string name;
		public object val;

		public BindAttr(string name, object val = null)
		{

			this.name = name;
			this.val = val;

		}

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

		public PItem LinkEvent(params Enum[] events)
		{
			if (events != null && events.Length > 0)
			{
				this.events = this.events ?? new List<string>();
				this.events.AddRange(events.Select(e => e.ToString()));
			}
			return this;
		}

	}

	public abstract class RecordDataBind
	{

		#region Member

		private Dictionary<string, PItem> _properties = new Dictionary<string, PItem>();
		private Dictionary<string, object> _caches = new Dictionary<string, object>();
		private Dictionary<string, object> _onceCaches = new Dictionary<string, object>();

		private Func<string, string> _configProxy;


		private int triggerType;

		#endregion

		#region Method

		public virtual IEnumerator Init(Func<string, string> getCfg = null)
		{
			_configProxy = getCfg;
			BeforeInit();
			InitPropertys();
			InitListening();
			return _DoInit();
		}

		public virtual RecordDataBind SetTriggerType(int type)
		{
			triggerType = type;
			return this;
		}

		public RecordDataBind Trigger(string eventID, params object[] args)
		{
			if (string.IsNullOrEmpty(eventID)) return this;
			TriggerDic(eventID, args?.Length > 1 ? ArgsToDic(args) : null);
			return this;
		}

		public RecordDataBind TriggerDic(string eventID, Dictionary<string, object> args = null)
		{
			if (!string.IsNullOrEmpty(eventID))
			{
				DoFillEventProperties(eventID, ref args);
				DoTrigger(eventID, triggerType, args);
			}
			return this;
		}

		protected void RefreshPropertys()
		{
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

		#region Override

		private IEnumerator _DoInit()
		{
			yield return DoInit();
			AfterInit();
		}

		protected abstract IEnumerator DoInit();

		protected virtual void BeforeInit() { }

		protected virtual void AfterInit() { }

		protected abstract void DoTrigger(string eventName, int type, Dictionary<string, object> args);

		protected virtual void UpdateDatas(Dictionary<string, object> datas, bool once = false) { }

		protected virtual void InitPropertys() { }

		protected virtual void InitListening() { }

		/// <summary>
		/// 填充事件额外属性
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="properties"></param>
		protected virtual void DoFillEventProperties(string eventName, ref Dictionary<string, object> properties)
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

		#region Private

		protected Dictionary<string, object> ArgsToDic(params object[] args)
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
				throw new Exception("args为空或者数量不是成对的");
			}
		}

		protected PItem RegisterProperty(string name, object val = null, System.Func<string, object> get = null, bool once = false, bool upload = true, bool replace = false)
		{
			if (string.IsNullOrEmpty(name)) return default;
			if (!_properties.TryGetValue(name, out var item))
				item = _properties[name] = new PItem() { name = name, val = val, get = get, once = once, upload = upload };
			else if (replace)
			{
				item.val = val;
				item.get = get;
				item.once = once; item.upload = upload;
			}
			return item;
		}

		protected object GetPropertyVal(string name)
		{
			if (!string.IsNullOrEmpty(name) && _properties.TryGetValue(name, out var p))
			{
				return p.GetVal();
			}
			return default;
		}

		protected object[] GetProperty(string name)
		{
			return new object[] { name, GetPropertyVal(name) };
		}

		protected T GetCfg<T>(string name, T def = default)
		{
			if (_configProxy != null && !string.IsNullOrEmpty(name))
			{
				var val = _configProxy(name);
				if (!string.IsNullOrEmpty(val))
				{
					try
					{
						if (typeof(T) == typeof(bool))
							return (T)(object)(val != null && val.ToLower() != "false");
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


	}

	public class RecordDataBind<TEnum> : RecordDataBind where TEnum : struct, Enum
	{
		private static Dictionary<string, List<BindAttr>> _bindattrs;

		public RecordDataBind()
		{
			if (_bindattrs == null)
			{
				_bindattrs = new Dictionary<string, List<BindAttr>>();
				var fields = typeof(TEnum).GetFields();
				if (fields?.Length > 0)
				{
					for (int i = 0; i < fields.Length; i++)
					{
						var f = fields[i];
						var attrs = Attribute.GetCustomAttributes(f, typeof(BindAttr)) as BindAttr[];
						if (attrs != null)
							_bindattrs[f.Name] = attrs.ToList();
					}
				}
			}

			if (_bindattrs.Count > 0)
			{
				foreach (var item in _bindattrs)
					item.Value.ForEach(value => RegisterProperty(item.Key, value));
			}

		}

		protected override IEnumerator DoInit()
		{
			yield return null;
		}

		protected override void DoTrigger(string eventName, int type, Dictionary<string, object> args)
		{

		}

	}
}
