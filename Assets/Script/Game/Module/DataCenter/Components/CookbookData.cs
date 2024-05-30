using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class DataCenter
	{
		public CookbookData cookbook = new CookbookData();

		public static class CookbookUtils
		{
			private static CookbookData data { get { return Instance.cookbook; } }

			public static void Init()
			{
				data.Refresh();
			}

			public static CookBookItem GetBook(int id, bool missadd = true)
			{
				if (data.bookDics == null)
					data.Refresh();
				if (!data.bookDics.TryGetValue(id, out var book) && missadd && data.bookIDs.Contains(id))
				{
					book = new CookBookItem() { id = id };
					book.Refresh();
					data.bookDics[id] = book;
					data.books.Add(book);
				}
				return book;
			}

			public static List<CookBookItem> GetBooks()
			{
				if (data.bookDics == null)
					data.Refresh();
				return data.books;
			}

			public static int[] GetBookIDs()
			{
				if (data.bookDics == null)
					data.Refresh();
				return data.bookIDs.ToArray();
			}

			public static void UpLv(int id)
			{
				var book = GetBook(id);
				if (book != null)
				{
					var cost = book.lvCfg.GetCostArray();
					PropertyManager.Instance.UpdateByArgs( true , cost);
					book.level++;
					book.Refresh();
					EventManager.Instance.Trigger(((int)GameEvent.COOKBOOK_UP_LV), id, book.level);
				}
			}

		}

	}

	[Serializable]
	public class CookbookData
	{
		public List<CookBookItem> books = new List<CookBookItem>();


		public Dictionary<int, CookBookItem> bookDics { get; private set; }
		public List<int> bookIDs { get; private set; }


		public void Refresh()
		{
			if (bookIDs == null)
				bookIDs = ConfigSystem.Instance
					.Finds<CookBookRowData>(c => c.Level == 1)
					.Select(c => c.CookId)
					.ToList();
			books.ForEach(b => b.Refresh());
			bookDics = books.ToDictionary(b => b.id);
		}

	}

	[Serializable]
	public class CookBookItem
	{
		public int id;
		public int level;

		[NonSerialized]
		public int maxLv;
		[NonSerialized]
		public int maxStar;
		[NonSerialized]
		public ItemRowData cfg;
		[NonSerialized]
		public CookBookRowData lvCfg;
		[NonSerialized]
		public CookBookRowData nextLvCfg;

		private List<CookBookRowData> cfgLists;

		public bool IsMaxLv()
		{
			return level >= maxLv;
		}

		public double GetCost(out int type, out int id)
		{
			type = id = 0;
			if (lvCfg.IsValid())
			{
				var cost = lvCfg.GetCostArray();
				type = (int)cost[0];
				id = (int)cost[1];
				return cost[2];
			}
			return 0;
		}

		public bool CanUpLv(out bool scenelimit)
		{
			scenelimit = false;
			if (IsMaxLv()) return false;
			if (lvCfg.IsValid())
			{
				var scene = DataCenter.Instance.roomData.roomID;
				if (lvCfg.Map == scene)
				{
					switch (lvCfg.ConditionType)
					{
						case 1:
							return DataCenter.Instance.roomData.tables.Contains(lvCfg.ConditionValue);
						case 2:
							return DataCenter.MachineUtil.IsAreaEnable(lvCfg.ConditionValue);
						default: return true;
					}
				}
				scenelimit = true;
				return scene > lvCfg.Map;
			}
			return false;
		}

		public CookBookItem Refresh()
		{
			if (id > 0)
			{
				if (!cfg.IsValid() && ConfigSystem.Instance.TryGet(id, out cfg))
				{
					if (cfgLists == null)
					{
						cfgLists = ConfigSystem.Instance.Finds<CookBookRowData>(c => c.CookId == id);
						if (cfgLists?.Count > 0)
						{
							maxLv = cfgLists.Count;
							maxStar = cfgLists.Last().Star;
						}
						else { maxStar = maxLv = 1; }
					}
				}

				if (cfgLists?.Count > 0)
				{
					level = Math.Clamp(level, 1, maxLv);
					lvCfg = cfgLists[level - 1];
					nextLvCfg = level >= cfgLists.Count ? default : cfgLists[level];
				}
			}
			return this;
		}

	}

}
