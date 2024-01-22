
using System.Collections.Generic;
namespace SGame
{
	public class PropertyManager : Singleton<PropertyManager>
	{
		// 数据存储
		private Dictionary<int, ItemGroup> m_Values;

		public void Initalize()
		{
			m_Values = new Dictionary<int, ItemGroup>(32);
		}

		// 获取道具管理器
		public ItemGroup GetGroup(ItemType type)
		{
			return GetGroup((int)type);
		}

		public ItemGroup GetGroup(int type)
		{
			if (m_Values.TryGetValue(type, out ItemGroup group))
			{
				return group;
			}

			group = new ItemGroup();
			m_Values.Add(type, group);
			return group;
		}

		public ItemGroup GetUserGroup(int playerId)
		{
			return GetGroup(ItemType.USER);
		}

		public bool CheckCount(int id, int num, ItemType type)
		{
			return CheckCount(id, num, (int)type);
		}

		public bool CheckCountByArgs(params int[] args)
		{
			if (args.Length > 2)
				return CheckCount(args[1], args[2], args[0]);
			return args == null || args.Length == 0;
		}

		public bool CheckCount(int id, int num, int type = 0)
		{
			var g = GetGroup(type);
			if (g != null)
				return g.GetNum(id) >= num;
			return false;
		}

	}
}