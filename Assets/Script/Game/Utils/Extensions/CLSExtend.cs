using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FairyGUI;
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

		static public List<T> StateToList<T>(this T type) where T : struct, IConvertible
		{
			var es = type.ToString().Split(',');
			if (es.Length > 1)
			{
				var ls = new List<T>();
				for (int i = 0; i < es.Length; i++)
				{
					if (Enum.TryParse<T>(es[i], out var e))
						ls.Add(e);
				}
				return ls;
			}
			return default;
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

		static public void SetLayer(this object go, string layer)
		{
			if (go != null)
			{
				Transform t = null;
				if (go is Transform v) t = v;
				else if (go is GameObject g) t = g.transform;
				else if (go is Component c) t = c.transform;
				else if (go is GObject f) t = f.displayObject.gameObject.transform;

				if (t)
				{
					int id = LayerMask.NameToLayer(layer);
					if (t.gameObject.layer == id) return;
					t.gameObject.layer = id;
					foreach (Transform c in t) SetLayer(c, layer);
				}
			}
		}

		static public T[] GetArray<T>(this FlatBuffers.IFlatbufferObject flatbuffer , System.Func<int,T> call ,int len ,int start = 0)
		{
			if (len > 0)
			{
				var a = new T[len - start];
				for (int i = start; i < len; i++)
					a[i] = call(i);
				return a;
			}
			return default;
		}

	}
}
