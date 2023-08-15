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
			public long     num;
			public ItemType type;

			public override string ToString()
			{
				return string.Format("{0}_{1}_{2}", type, id, num);
			}
		}

		public List<Value> Values = new List<Value>();
	}
}