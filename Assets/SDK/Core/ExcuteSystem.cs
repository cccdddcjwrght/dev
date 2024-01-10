using System;
using System.Collections.Generic;
using System.Linq;

namespace LibExcute
{

	#region RequestExcuter

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ExcuterAttribute : Attribute
	{
		public string group { get; set; }
		public string id { get; set; }

		public ExcuterAttribute(string id) { this.id = id; }
	}

	public interface IExcuter
	{
		void Excute(string id, Action<bool, string> completed, params object[] args);
	}

	public class ExcuterSystem
	{

		private Dictionary<string, object> _excutors;

		public ExcuterSystem()
		{
			var ty = typeof(ExcuterAttribute);
			var attrs = this
				.GetType()
				.GetNestedTypes(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
				.Where(t => typeof(IExcuter).IsAssignableFrom(t) && Attribute.IsDefined(t, ty))
				.ToDictionary(t => t, t => t.GetCustomAttributes(ty, true));

			_excutors = new Dictionary<string, object>();

			if (attrs != null && attrs.Count > 0)
			{

				foreach (var item in attrs)
				{
					if (item.Value != null && item.Value.Length > 0)
					{
						foreach (var attr in item.Value)
						{
							var a = attr as ExcuterAttribute;
							if (!string.IsNullOrEmpty(a.id))
							{
								var k = Convert(a.id, a.group);
								if (!_excutors.ContainsKey(k))
									_excutors[k] = item.Key;
								else
									throw new Exception($"重复定义的处理：{k}");
							}
						}
					}
				}
			}
		}

		public IExcuter GetExcuter(string id, string group = null)
		{
			if (!string.IsNullOrEmpty(id))
			{

				var k = Convert(id, group);
				if (_excutors.TryGetValue(k, out var excuter) && excuter != null)
				{
					if (excuter is IExcuter e) return e;
					else if (excuter is Type ty)
					{
						try
						{
							e = System.Activator.CreateInstance(ty) as IExcuter;
						}
						catch { e = null; }
						_excutors[k] = e;
						return e;
					}
				}
			}
			return default;
		}

		public void Excute(string id, string pid, Action<bool, string> complete, string groupid = null, params object[] args)
		{
			var ex = GetExcuter(id, groupid);
			if (ex != null)
			{
				if (string.IsNullOrEmpty(pid))
				{
					complete?.Invoke(false, $"执行器业务ID为空 {groupid}_{id}:{pid}");
					return;
				}
				ex.Excute(pid, complete, args);
			}
			else
				complete?.Invoke(false, $"没有定义执行器 {groupid}_{id}:{pid}");
		}

		public static string Convert(string id, string group = null)
		{
			return string.Format("{0}_{1}", group, id);
		}
	}
	#endregion

}