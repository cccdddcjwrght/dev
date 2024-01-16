using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SGame
{
	public static partial class CLSExtend
	{

		static public T Val<T>(this IList<T> list, int index, T def = default)
		{
			if (list != null && list.Count > index && index >= 0)
				return list[index];
			return def;
		}

		static public T Val<T>(this IList list, int index, T def = default)
		{
			if (list != null && list.Count > 0 && index >= 0 && index < list.Count)
			{
				var v = list[index];
				try
				{
					return (T)Convert.ChangeType(v, typeof(T));
				}
				catch { }
			}
			return def;
		}

		static public int GetVal(this IList<int> array, int offset = 0, int def = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return array[offset];
			return def;
		}

		static public Vector2 GetVector2(this IList<int> array, int offset = 0, Vector2 vector = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return new Vector2(array[offset++], array.Count > offset ? array[offset++] : 0);
			return vector;
		}

		static public Vector3 GetVector3(this IList<int> array, int offset = 0, Vector3 vector = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return new Vector3(array[offset++], array.Count > offset ? array[offset++] : 0, array.Count > offset ? array[offset] : 0);
			return vector;
		}

		static public float GetVal(this IList<float> array, int offset = 0, int def = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return array[offset];
			return def;
		}

		static public Vector2 GetVector2(this IList<float> array, int offset = 0, Vector2 vector = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return new Vector2(array[offset++], array.Count > offset ? array[offset++] : 0);
			return vector;
		}

		static public Vector3 GetVector3(this IList<float> array, int offset = 0, Vector3 vector = default)
		{
			offset = Math.Max(0, offset);
			if (array != null && array.Count > offset)
				return new Vector3(array[offset++], array.Count > offset ? array[offset++] : 0, array.Count > offset ? array[offset] : 0);
			return vector;
		}

		static public bool IsInState<T>(this int val, T state) where T : struct, IConvertible
		{
			var v = val;
			var s = state.ToInt32(null);
			return (s & v) == v;
		}

		static public bool IsInState<T>(this T val, T state) where T : struct, IConvertible
		{
			var v = val.ToInt32(null);
			var s = state.ToInt32(null);
			return (s & v) == v;
		}

		static public void Foreach<T>(this IList<T> list, Action<T> excute)
		{
			if (excute != null && list != null && list.Count > 0)
			{

				foreach (var item in list)
					excute(item);
			}
		}

		static public void CForeach<T>(this IList list, Action<T> excute)
		{
			if (excute != null && list != null && list.Count > 0)
			{

				foreach (var item in list)
				{
					if (item is T v)
						excute(v);
				}
			}
		}

		static public T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
		{
			if (gameObject != null)
			{
				return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
			}
			return default;
		}
	}
}
