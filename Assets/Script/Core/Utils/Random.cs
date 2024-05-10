using System;
using System.Collections.Generic;
using System.Linq;

namespace SGame.Randoms
{
	public class Random
	{
		public readonly static Random _R = new Random();
		public static long Seed { get { return _R.seed; } set { _R.seed = value; } }

		public long seed;

		public Random() : this(System.DateTime.Now.Ticks) { }

		public Random(long seed)
		{
			this.seed = seed;
		}

		public int Next()
		{
			var v = (int)System.DateTime.Now.Ticks;
			return Next(-v, v);
		}

		public int Next(int min, int max)
		{
			return Next(min, max, ref seed);
		}

		public void Next(int min, int max, int count, ref List<int> rets)
		{
			count = Math.Max(1, count);
			rets = rets ?? new List<int>();
			for (int i = 0; i < count; i++)
				rets.Add(Next(min, max));
		}

		public float NextFloat(float min, float max)
		{
			return Next((int)Math.Floor(min * 100), (int)Math.Ceiling(max * 100), ref seed) * 0.01f;
		}


		/// <summary>
		/// 列表随机
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		public T NextItem<T>(IList<T> args, out int index)
		{
			index = -1;
			if (args != null && args.Count > 0)
			{
				var l = args.Count;
				index = 0;
				if (l == 1) return args[0];
				index = Math.Min(Next(0, l), l - 1);
				return args[index];
			}
			return default;
		}

		public T NextItem<T>(IList<T> args)
		{
			return NextItem(args, out _);
		}

		public void NextItem<T>(IList<T> args, int count, ref List<T> rets, bool unique = false)
		{
			if (args != null && args.Count > 0)
			{
				count = Math.Max(1, count);
				for (int i = 0; i < count; i++)
				{
					var idx = args.Count > 0 ? Next(0, args.Count - 1) : default;
					rets.Add(args[idx]);
					if (unique)
						args.RemoveAt(idx);
				}
			}

		}

		/// <summary>
		/// 权重随机
		/// </summary>
		/// <param name="weights"></param>
		/// <returns></returns>
		public int NextWeight(IList<int> weights)
		{
			if (weights != null && weights.Count > 0)
			{
				var sum = weights.Sum((w) => { return Math.Max(0, w); });
				var val = Next(0, sum);
				for (int i = 0; i < weights.Count; i++)
				{
					var w = weights[i];
					if (w > 0)
					{
						if (w >= val) return i;
						val -= w;
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// 权重列表随机一定数量
		/// </summary>
		/// <param name="weights">权重列表</param>
		/// <param name="count">数量</param>
		/// <param name="unique">是否不放回抽取</param>
		/// <param name="afterrandom">每次随机后的处理</param>
		/// <returns></returns>
		public int[] NextWeights(IList<int> weights, int count = 1, bool unique = true, Action<int, IList<int>> afterrandom = null)
		{
			if (count > 0 && weights != null && weights.Count > 0)
			{
				weights = weights.ToArray();
				count = unique ? count > weights.Count ? weights.Count : count : count;
				var rets = new int[count];
				for (int i = 0; i < count; i++)
				{
					rets[i] = NextWeight(weights);
					afterrandom?.Invoke(rets[i], weights);
					if (unique) weights[rets[i]] = 0;
				}
				return rets;
			}
			return default;
		}

		/// <summary>
		/// 几率判断
		/// </summary>
		/// <param name="rate"></param>
		/// <returns></returns>
		public bool Rate(int rate)
		{
			return Next(0, 100) <= rate;
		}

		static public int Next(int min, int max, ref long val)
		{
			if (min >= max) return min;
			val = Math.Abs((48271 * val + 13) % 2147483647);
			int ret = min + (int)(val % (max - min + 1));
			return ret;
		}
	}
}
