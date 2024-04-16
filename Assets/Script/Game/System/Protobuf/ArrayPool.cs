using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// 数组对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ArrayPool<T>
{
	private readonly static Dictionary<int, List<T[]>> _queues = new Dictionary<int, List<T[]>>();
	private static volatile int state;

	public static T[] Pop(int size)
	{
		if (size <= 0)
			return System.Array.Empty<T>();
		else
		{
			T[] ret;
			while (true)
			{
				if (0 == Interlocked.Exchange(ref state, 1))
				{
					List<T[]> list = GetList(size);
					if (list == null || list.Count == 0)
						ret = new T[size];
					else
					{
						ret = list[0];
						list.RemoveAt(0);
					}
					Interlocked.Exchange(ref state, 0);
					break;
				}
			}
			if (ret != null)
				ret.Initialize();
			return ret;
		}
	}

	public static void Push(T[] array)
	{
		if (array != null && array.Length > 0)
		{
			Array.Clear(array, 0, array.Length);
			while (true)
			{
				if (0 == Interlocked.Exchange(ref state, 1))
				{
					var list = GetList(array.Length, true);
					list.Add(array);
					Interlocked.Exchange(ref state, 0);
					break;
				}
			}
		}
	}

	private static List<T[]> GetList(int size, bool create = false)
	{
		List<T[]> list = null;
		if (!_queues.TryGetValue(size, out list) && create)
		{
			list = new List<T[]>();
			_queues[size] = list;
		}
		return list;
	}

	public static void Clear()
	{
		if (_queues.Count > 0)
		{
			foreach (var item in _queues)
				item.Value.Clear();
			_queues?.Clear();
		}
	}
}

public static class ArrayArgsExtend
{
	static public object GetArg(this IList args, string key , object def = null)
	{
		if (!string.IsNullOrEmpty(key) && args != null && args.Count > 0 && args.Count % 2 == 0)
		{
			var idx = args.IndexOf(key);
			if (idx % 2 == 0)
				return args[idx + 1];
		}
		return def;
	}

	static public bool GetBool(this IList args, string key)
	{
		if (!string.IsNullOrEmpty(key) && args != null && args.Count > 0 && args.Count % 2 == 0)
		{
			var idx = args.IndexOf(key);
			if (idx % 2 == 0)
				return args[idx + 1] != null;
		}
		return default;
	}

}