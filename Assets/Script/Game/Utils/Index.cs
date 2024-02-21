using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{
	public class Index<T>
	{
		static readonly public T Default = default(T);

		private Dictionary<uint, T> _values = new Dictionary<uint, T>();
		private Dictionary<T, uint> _indexs = new Dictionary<T, uint>();
		private uint _index = 1;

		public uint IndexOf(T item)
		{
			if (EqualityComparer<T>.Default.Equals(item, default)) return 0;
			if (!_indexs.TryGetValue(item, out var idx))
			{
				idx = _index++;
				_values[idx] = item;
				_indexs[item] = idx;
			}
			return idx;
		}

		public T Value(uint index)
		{
			if (index > 0)
			{
				if (_values.TryGetValue(index, out var val))
					return val;
			}
			return Default;
		}

		public T Pop(uint index)
		{
			if (index > 0)
			{
				if (_values.TryGetValue(index, out var val))
				{
					_values.Remove(index);
					_indexs.Remove(val);
					return val;
				}
			}
			return Default;
		}

		public void Remove(T val)
		{
			if (_indexs.TryGetValue(val, out var index))
			{
				_values.Remove(index);
				_indexs.Remove(val);
			}
		}

		public void RemoveAt(uint index)
		{
			if (_values.TryGetValue(index, out var val))
			{
				_values.Remove(index);
				_indexs.Remove(val);
			}
		}

		public void Clear()
		{
			_indexs.Clear();
			_values.Clear();
		}

	}

	public static class IndexExtend
	{
		static public Dictionary<Type, object> _indexs = new Dictionary<Type, object>();


		static public uint ToIndex<T>(this T item)
		{
			if (!_indexs.TryGetValue(typeof(T), out var o))
				o = _indexs[typeof(T)] = new Index<T>();
			if (o is Index<T> index)
				return index.IndexOf(item);
			return 0;
		}

		static public T FromIndex<T>(this uint index , bool pop = false)
		{
			if (_indexs.TryGetValue(typeof(T), out var o) && o is Index<T> items)
				return pop ? items.Pop(index) : items.Value(index);
			return default;
		}

	}

}
