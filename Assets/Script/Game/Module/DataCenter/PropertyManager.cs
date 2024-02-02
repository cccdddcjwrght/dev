
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
		public ItemGroup GetGroup(PropertyGroup type)
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

		public bool CheckCount(int id, double num, PropertyGroup type)
		{
			return CheckCount(id, num, (int)type);
		}

		public bool CheckCountByArgs(params int[] args)
		{
			if (args != null && args.Length > 2)
				return CheckCount(args[1], args[2], args[0]);
			return args == null || args.Length == 0;
		}

		public bool CheckCount(int id, double num, int type = 0)
		{
			var g = GetGroup(type);
			if (g != null)
				return g.GetNum(id) >= num;
			return false;
		}

		public void UpdateByArgs(bool iscost, params int[] args)
		{
			if (args.Length > 2)
				Update((PropertyGroup)args[0], args[1], args[2], iscost);
			else if (args.Length == 2)
				Update(PropertyGroup.ITEM, args[0], args[1], iscost);
		}

		public void Update(int type, int id, double count, bool iscost = false) => Update((PropertyGroup)type, id, count, iscost);

		public void Update(PropertyGroup type, int id, double count, bool iscost = false)
		{
			if (id > 0)
			{
				var group = GetGroup(type);
				if (group != null)
				{
					group.AddNum(id, iscost ? -count : count);
				}
			}
		}

	}
}