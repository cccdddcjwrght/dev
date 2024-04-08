using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{

	public enum Operator
	{
		Ass = 0,
		Add,
		Sub,
		Mul,
		Div,
		Mod,
	}

	public class AttributeUnit
	{
		public int id;
		public int from;
		public int type;
		public double val;
		public int deadtime;


		public double modifiy;
		public GameAttribute attribute;
		public int count;


		public void Excute(GameAttribute attribute)
		{
			count = 0;
			this.attribute = attribute;
			switch ((EnumCaluType)type)
			{
				case EnumCaluType.Value:
					modifiy = val;
					break;
				case EnumCaluType.Percentage:
					var v = attribute.value * val;
					modifiy = (v * ConstDefine.C_PER_SCALE).Round();
					break;
			}
			Readd();
		}

		public void Readd()
		{
			attribute.value += modifiy;
			count++;
		}

		public void Reset(GameAttribute attribute = null)
		{
			attribute = attribute ?? this.attribute;
			attribute.value -= (modifiy * count);
			this.attribute = null;
			count = 0;
		}

		public bool IsCompleted(int time)
		{
			if (deadtime <= 0) return false;
			return time >= deadtime;
		}

		public bool CheckAndReset(int time, GameAttribute attribute = null)
		{
			if (IsCompleted(time))
			{
				Reset(attribute);
				return true;
			}
			return false;
		}

	}

	public class GameAttribute
	{
		private double _val;

		public int id;
		public double origin;
		public double value { get { return _val; } set { _val = value; } }
		public double modify { get { return value - origin; } }

		public GameAttribute(int id) => this.id = id;

		public GameAttribute Break()
		{
			origin = value;
			return this;
		}

		public GameAttribute CopyTo(GameAttribute attribute)
		{
			if (attribute != null)
			{
				attribute.id = id;
				attribute.origin = origin;
				attribute.value = value;
			}
			return attribute;
		}

		public GameAttribute Clone()
		{
			return new GameAttribute(id)
			{
				value = value,
				origin = origin
			};
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}_{2}", (EnumAttribute)id, origin, value).ToString();
		}

		public override bool Equals(object obj)
		{
			return obj is GameAttribute attribute && id == attribute.id;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}


		#region Operator

		static public implicit operator double(GameAttribute p)
		{
			return p.value;
		}

		static public double operator +(GameAttribute p, GameAttribute v)
		{
			return p.value + v;
		}

		static public double operator -(GameAttribute p, GameAttribute v)
		{
			return p.value - v;
		}

		static public double operator *(GameAttribute p, GameAttribute v)
		{
			return p.value * v;
		}

		static public double operator /(GameAttribute p, GameAttribute v)
		{
			return v == 0 ? 0 : p.value / v;
		}

		static public double operator %(GameAttribute p, GameAttribute v)
		{
			return p.value % v;
		}

		static public bool operator ==(GameAttribute p, double v)
		{
			return p.value == v;
		}

		static public bool operator !=(GameAttribute p, double v)
		{
			return p.value != v;
		}

		static public double operator +(GameAttribute p, double v)
		{
			return p.value + v;
		}

		static public double operator -(GameAttribute p, double v)
		{
			return p.value - v;
		}

		static public double operator *(GameAttribute p, double v)
		{
			return p.value * v;
		}

		static public double operator /(GameAttribute p, double v)
		{
			return v == 0 ? 0 : p.value / v;
		}

		static public double operator %(GameAttribute p, double v)
		{
			return p.value % v;
		}

		static public double operator +(double v, GameAttribute p)
		{
			return p.value + v;
		}

		static public double operator -(double v, GameAttribute p)
		{
			return v - p.value;
		}

		static public double operator *(double v, GameAttribute p)
		{
			return p.value * v;
		}

		static public double operator /(double v, GameAttribute p)
		{
			return p.value == 0 ? 0 : v / p.value;
		}

		static public double operator %(double v, GameAttribute p)
		{
			return v % p.value;
		}

		#endregion

	}


	/// <summary>
	/// 属性列表
	/// </summary>
	[Serializable]
	public class AttributeList
	{
		private static Dictionary<int, int> _indexs;

		private double[] _array;

		private List<AttributeUnit> _units = new List<AttributeUnit>();
		private GameAttribute[] _values = new GameAttribute[0];
		public int Count { get { return _values.Length; } }

		public int entityID;
		public string key;

		private int _ctime;

		public double this[int id]
		{
			get
			{
				var a = GetAttribute(id);
				return a == null ? 0 : a.value;
			}
			set
			{
				var a = GetAttribute(id);
				if (a != null)
					a.value = value;
			}
		}

		static AttributeList()
		{
			var array = Enum.GetValues(typeof(EnumAttribute));
			_indexs = new Dictionary<int, int>();
			for (int i = 0; i < array.Length; i++)
				_indexs[(int)array.GetValue(i)] = i;
		}

		public AttributeList()
		{
			if (_indexs.Count > 0)
			{
				_values = _indexs.Keys.Select(i => new GameAttribute(i)).ToArray();
				_array = new double[Count];
			}
		}

		public AttributeList SetTime(int time)
		{
			_ctime = time;
			return this;
		}

		public AttributeList Change(EnumAttribute id, double val, EnumCaluType addtype, int deadline = 0, int from = 0, int repeatType = 0)
		{
			return Change((int)id, val, (int)addtype, deadline, from, repeatType);
		}

		public AttributeList Change(int id, double val, int addtype, int deadline = 0, int from = 0, int repeatType = 0)
		{
			var a = GetAttribute(id);
			if (a != null)
			{
				var flag = true;
				if (from != 0)
				{
					var u = GetUnit(id, from);
					if (u != null)
					{
						switch (repeatType)
						{
							case 0://时间刷新
								u.deadtime = deadline;
								u.Reset();
								u.val = val;
								u.Excute(a);
								break;
							case 1://时间叠加
								if (deadline > 0)
									u.deadtime += deadline - _ctime;
								break;
							case 2: //数值叠加
								u.Readd();
								break;
							case 3://都叠加
								if (deadline > 0) u.deadtime += deadline - _ctime;
								u.Readd();
								break;
						}
						flag = false;
					}
				}

				if (flag)
				{
					var unit = new AttributeUnit() { id = id, from = from, type = addtype, val = val, deadtime = deadline };
					unit.Excute(a);
					if (deadline != 0 || from != 0)
						_units.Add(unit);
				}
#if DEBUG
				GameDebug.Log($"<color='green'>[BUFF]</color>{key} -> ::attribute {a} change: {a.modify} - deadtime {deadline} ");
#endif

			}
			return this;
		}

		public AttributeList ResetByFrom(int from, int id = 0)
		{
			if (from != 0)
			{
				for (int i = _units.Count - 1; i >= 0; i--)
				{
					var u = _units[i];
					if (u.from == from && (id == 0 || u.id == id))
					{
#if DEBUG
						GameDebug.Log($"<color='red'>[BUFF]</color>{key} -> ::attribute {u.attribute} change : {u.modifiy}, val-> {u.attribute.value - u.modifiy}  ");
#endif
						u.Reset();
						_units.RemoveAt(i);
					}
				}
			}
			return this;
		}

		public AttributeUnit GetUnit(int id, int from)
		{
			if (_units?.Count > 0)
			{
				return _units.Find(u => u.id == id && u.from == from);
			}
			return default;
		}

		public AttributeList FromArray(double[] array)
		{
			if (array?.Length > 0)
			{
				for (int i = 0; i < Math.Min(array.Length, Count); i++)
				{
					_values[i].value = array[i];
				}
			}
			return this;
		}

		public double[] ToArray()
		{
			for (int i = 0; i < Count; i++)
				_array[i] = _values[i];
			return _array;
		}

		public void Refresh(int time)
		{
			for (int i = _units.Count - 1; i >= 0; i--)
			{
				var item = _units[i];
				var a = item.attribute;
				if (item.CheckAndReset(time))
				{
					_units.RemoveAt(i);
					GameDebug.Log($" {key}->reset attribute {a} : {item.modifiy} ");

				}
			}
		}

		public void Break()
		{
			_values?.Foreach(a => a.Break());
		}

		public void Reset()
		{
			foreach (var item in _units)
				item.Reset();
			_units?.Clear();
		}

		public void Clear()
		{
			_values = null;
		}

		public AttributeList Copy()
		{
			return ToArray();
		}

		private GameAttribute GetAttribute(int id)
		{
			if (_indexs.TryGetValue(id, out var index) && _values.Length > index)
				return _values[index];
			return default;
		}


		#region Static

		public static implicit operator AttributeList(double[] doubles)
		{
			return new AttributeList().FromArray(doubles);
		}

		public static double[] operator +(AttributeList main, AttributeList add)
		{
			return ComputeUtil.Op(main.ToArray(), add.ToArray(), Operator.Add);
		}

		public static double[] operator -(AttributeList main, AttributeList add)
		{
			return ComputeUtil.Op(main.ToArray(), add.ToArray(), Operator.Sub);
		}

		public static double[] operator *(AttributeList main, AttributeList add)
		{
			return ComputeUtil.Op(main.ToArray(), add.ToArray(), Operator.Mul);
		}

		public static double[] operator /(AttributeList main, AttributeList add)
		{
			return ComputeUtil.Op(main.ToArray(), add.ToArray(), Operator.Div);
		}

		public static double[] operator %(AttributeList main, AttributeList add)
		{
			return ComputeUtil.Op(main.ToArray(), add.ToArray(), Operator.Mod);
		}

		public static double[] operator +(AttributeList main, double[] add)
		{
			return ComputeUtil.Op(main.ToArray(), add, Operator.Add);
		}

		public static double[] operator -(AttributeList main, double[] add)
		{
			return ComputeUtil.Op(main.ToArray(), add, Operator.Sub);
		}

		public static double[] operator *(AttributeList main, double[] add)
		{
			return ComputeUtil.Op(main.ToArray(), add, Operator.Mul);
		}

		public static double[] operator /(AttributeList main, double[] add)
		{
			return ComputeUtil.Op(main.ToArray(), add, Operator.Div);
		}

		public static double[] operator %(AttributeList main, double[] add)
		{
			return ComputeUtil.Op(main.ToArray(), add, Operator.Mod);
		}

		#endregion

	}

	public static class ComputeUtil
	{
		static public double Op(Operator type, double left, double right)
		{
			double v = left;
			switch (type)
			{
				case Operator.Ass: v = right; break;
				case Operator.Add: v = left + right; break;
				case Operator.Sub: v = left - right; break;
				case Operator.Mul: v = left * right; break;
				case Operator.Div: v = right == 0 ? 0 : left / right; break;
				case Operator.Mod: v = left % right; break;
			}
			return v;
		}

		static public double[] Op(double[] left, double[] right, Operator op, double[] ret = null)
		{
			if (left == null && right == null) return ret ?? Array.Empty<double>();
			int l = Math.Max(right == null ? 0 : right.Length, left == null ? 0 : left.Length);
			ret = ret ?? new double[l];
			if (ret.Length == 0) return ret;

			l = Math.Min(ret.Length, l);
			for (int i = 0; i < l; i++)
			{
				if (left == null) ret[i] = right[i];
				else if (right == null) ret[i] = left[i];
				else ret[i] = Op(op, left.Length > i ? left[i] : 0, right.Length > i ? right[i] : 0);
			}

			return ret;
		}

		static public double[] Op(double[] left, double right, Operator op, double[] ret = null)
		{
			if (left == null || left.Length == 0) return Array.Empty<double>();
			ret = ret ?? new double[left.Length];
			if (ret.Length == 0) return ret;
			var l = Math.Min(ret.Length, left.Length);
			for (int i = 0; i < l; i++)
				ret[i] = Op(op, left[i], right);
			return ret;
		}

	}
}
