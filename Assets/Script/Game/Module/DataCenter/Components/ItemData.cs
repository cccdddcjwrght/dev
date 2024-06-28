using System;
using System.Collections.Generic;
using Unity.Mathematics;
namespace SGame
{
	[Serializable]
	public class ItemData
	{
		// Item数值
		[Serializable]
		public struct Value
		{
			public int      id;
			public double     num;
			public PropertyGroup type;

			public double change;

			public Value SetNum(double num)
			{
				this.num = num;
				return this;
			}

			public override string ToString()
			{
				return string.Format("{0}_{1}_{2}", type, id, num);
			}
		}

		public List<Value> Values = new List<Value>();
	}
}