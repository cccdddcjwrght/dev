
using System.Collections.Generic;
using System.Linq;

namespace SGame
{
	public class PropertyManager : Singleton<PropertyManager>
	{
		// 数据存储
		private Dictionary<int, ItemGroup> m_Values;
		private ItemData m_caches;

		public void Initalize()
		{
			m_Values = new Dictionary<int, ItemGroup>(32);
		}

		public void InitCache(ItemData itemData)
		{
			m_caches = itemData;
		}

		public bool IsInitalize { get { return m_Values != null; } }

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

		public ItemData.Value GetItem(int id, int type = 1)
		{
			var g = GetGroup(type);
			if (g != null) return g.GetItem(id);
			return default;
		}

		public bool CheckCount(int id, double num, PropertyGroup type)
		{
			return CheckCount(id, num, (int)type);
		}

		public bool CheckCountByArgs(params float[] args)
		{
			if (args != null && args.Length > 2)
				return CheckCount((int)args[1], args[2], (int)args[0]);
			return args == null || args.Length == 0;
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

		public void UpdateByArgs(bool iscost, params float[] args)
		{
			if (args.Length > 2)
				Update((int)args[0], (int)args[1], args[2], iscost);
			else if (args.Length == 2)
				Update(PropertyGroup.ITEM, (int)args[0], args[1], iscost);
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

		public void Insert2Cache(List<int[]> items)
		{

			if (items == null) return;
			for (int i = 0; i < items.Count; i++)
			{
				var d = items[i];
				if (d != null && d.Length >= 2)
				{
					if (d.Length > 2)
						m_caches.Values.Add(new ItemData.Value() { id = d[1], num = d[2], type = (PropertyGroup)d[0] });
					else
						m_caches.Values.Add(new ItemData.Value() { id = d[0], num = d[1], type = PropertyGroup.ITEM });
				}
			}

		}

		public void Insert2Cache(List<double[]> items)
		{

			if (items == null) return;
			for (int i = 0; i < items.Count; i++)
			{
				var d = items[i];
				if (d != null && d.Length >= 2)
				{
					if (d.Length > 2)
						m_caches.Values.Add(new ItemData.Value() { id = (int)d[1], num = d[2], type = (PropertyGroup)d[0] });
					else
						m_caches.Values.Add(new ItemData.Value() { id = (int)d[0], num = d[1], type = PropertyGroup.ITEM });
				}
			}

		}

		/// <summary>
		/// 合并Cache物品数据到Item
		/// </summary>
		public void CombineCache2Items()
		{
			var caches = m_caches.Values;
			if (caches == null || caches.Count == 0) return;
			var items = caches.GroupBy(v => v.type)
				.ToDictionary(g => g.Key, g => g.GroupBy(v => v.id).Select(v => new ItemData.Value() { id = v.Key, num = v.Sum(i => i.num) })
				.ToList());
			caches.Clear();
			foreach (var item in items)
			{
				if (item.Value.Count > 0)
				{
					var group = GetGroup(item.Key);
					for (int i = 0; i < item.Value.Count; i++)
					{
						var d = item.Value[i];
						group.AddNum(d.id, d.num);
					}
				}
			}
		}

	}
}