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
				EventManager.Instance.Reg<int, int>(((int)GameEvent.WORK_TABLE_MACHINE_ENABLE), (a, b) => data.Refresh());
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

			public static bool UpLv(int id)
			{
				var book = GetBook(id);
				if (book != null)
				{
					var enable = book.IsEnable();
					if (book.CanUpLv(out _, enable))
					{
						DataCenter.CostItem(false, book.GetCostArray());
						if (enable)
						{
							book.level++;
							book.Refresh();
						}
						else
							book.enable = true;
						EventManager.Instance.Trigger(((int)GameEvent.COOKBOOK_UP_LV), id, book.level);
						return true;
					}
				}
				return false;
			}

		}

		static public bool CostItem(bool onlycheck, params int[] cost)
		{
			if (cost != null && cost.Length>= 2)
			{
				for (int i = 0; i < cost.Length; i += 2)
				{
					var id = (int)cost[i];
					var val = cost[i + 1];
					if (!PropertyManager.Instance.CheckCount(id, val, 1)) return false;
				}

				if (!onlycheck)
				{
					for (int i = 0; i < cost.Length; i += 2)
					{
						var id = (int)cost[i];
						var val = cost[i + 1];
						PropertyManager.Instance.Update(1, id, val, true);
					}
				}
				return true;
			}
			return false;
		}

		static public bool CostItem(bool onlycheck, params double[] cost)
		{
			if (cost != null && cost.Length >= 2)
			{
				for (int i = 0; i < cost.Length; i += 2)
				{
					var id = (int)cost[i];
					var val = cost[i + 1];
					if (!PropertyManager.Instance.CheckCount(id, val, 1)) return false;
				}

				if (!onlycheck)
				{
					for (int i = 0; i < cost.Length; i += 2)
					{
						var id = (int)cost[i];
						var val = cost[i + 1];
						PropertyManager.Instance.Update(1, id, val, true);
					}
				}
				return true;
			}
			return false;
		}

	}

	[Serializable]
	public class CookbookData
	{
		public List<CookBookItem> books = new List<CookBookItem>();


		public Dictionary<int, CookBookItem> bookDics { get; private set; }
		public List<int> bookIDs { get; private set; }
		public static Dictionary<int, List<CookBookRowData>> cfgs;

		public void Refresh()
		{
			if (bookIDs == null)
			{
				cfgs = cfgs ?? ConfigSystem.Instance
					.Finds<CookBookRowData>(c => true)
					.GroupBy(v => v.CookId)
					.ToDictionary(v => v.Key, v => v.ToList());
				bookIDs = cfgs.Keys.ToList();
				bookDics = new Dictionary<int, CookBookItem>();
			}
			books.ForEach(b =>
			{
				b.Refresh();
				bookDics[b.id] = b;
			});
			bookIDs.ForEach(b => DataCenter.CookbookUtils.GetBook(b));

		}

	}

	[Serializable]
	public class CookBookItem
	{
		public int id;
		public int level;
		public bool enable;

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
		private int[] cost;
		private int[] condition;

		public bool IsMaxLv()
		{
			return level >= maxLv;
		}

		public bool IsEnable()
		{
			if (level > 1) return true;
			if (lvCfg.ConditionType == 3) return enable;
			else
			{
				if (!enable)
					enable = CanUpLv(out _, false);
				return enable;
			}
		}

		public double GetCost(out int type, out int id)
		{
			type = id = 0;

			if (lvCfg.IsValid())
			{
				if (lvCfg.ConditionType == 3 && !enable)
				{
					type = 1;
					id = lvCfg.ConditionValue(0);
					return lvCfg.ConditionValue(1);
				}
				else
				{
					var cost = lvCfg.GetCostArray();
					type = 1;
					id = (int)cost[0];
					return cost[1];
				}
			}
			return 0;
		}

		public int[] GetCostArray()
		{
			if (lvCfg.IsValid())
			{
				if (lvCfg.ConditionType == 3 && !enable)
					return lvCfg.GetConditionValueArray();
				else
					return lvCfg.GetCostArray();
			}
			return default;
		}

		public double GetPrice()
		{
			return 0.01D * cfg.Price(2) * lvCfg.Price;
		}

		public bool CanUpLv(out bool scenelimit, bool checkcost = true)
		{
			return CanUpLv(out scenelimit, out _, checkcost);
		}

		public bool CanUpLv(out bool scenelimit, out bool itemnot, bool checkcost = true)
		{
			scenelimit = false;
			itemnot = false;
			if (IsMaxLv()) return false;
			if (lvCfg.IsValid())
			{
				var scene = DataCenter.Instance.roomData.roomID;
				if (lvCfg.Map <= scene)
				{
					var f = true;
					switch (lvCfg.ConditionType)
					{
						case 1:
							f = DataCenter.Instance.roomData.tables.Contains(condition[0]);
							break;
						case 2:
							f = DataCenter.MachineUtil.IsAreaEnable(condition[0]);
							break;
						case 3:
							f = Utils.CheckItemCount(condition[0], condition[1], false);
							if (!f) itemnot = true;
							break;
					}
					if (!f) return false;
					if (checkcost && !DataCenter.CostItem( true, cost))
					{
						itemnot = true;
						return false;
					}
					return true;

				}
				scenelimit = true;
				return false;
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
						if (CookbookData.cfgs.TryGetValue(id, out cfgLists))
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
					lvCfg = level > 0 ? cfgLists[level - 1] : default;
					nextLvCfg = level >= cfgLists.Count ? default : cfgLists[level];
					cost = lvCfg.GetCostArray();
					condition = lvCfg.GetConditionValueArray();
				}
			}
			return this;
		}

	}

}
