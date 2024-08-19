using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;

namespace SGame
{

	public static partial class Error_Code
	{
		/// <summary>
		/// 成功
		/// </summary>
		public const int SUCCESS = 0;

		/// <summary>
		/// 等级上限
		/// </summary>
		public const int LV_MAX = 1;
		/// <summary>
		/// 道具不足
		/// </summary>
		public const int ITEM_NOT_ENOUGH = 2;
		/// <summary>
		/// 金币不足
		/// </summary>
		public const int ITEM_GOLD_NOT_ENOUGH = 3;
		/// <summary>
		/// 钻石不足
		/// </summary>
		public const int ITEM_DIAMOND_NOT_ENOUGH = 4;

		//===============================================================
		/// <summary>
		/// 依赖点位没有激活
		/// </summary>
		public const int MACHINE_DEPENDS_NOT_ENABLE = 100;
		public const int AREA_IS_LOCK = 101;
		/// <summary>
		/// 依赖工作台等级不足
		/// </summary>
		public const int MACHINE_DEPENDS_LEVEL_ERROR = 102;


	}

	public partial class DataCenter
	{
		public class MachineUtil
		{
			static private int _tempIndex = 0;

			private static Dictionary<string, int> _areas = new Dictionary<string, int>();

			public static Machine AddMachine(int id)
			{
				if (id > 0)
				{
					var machine = GetMachine(id, out var worktable);
					if (machine == null)
					{
						if (worktable == null && ConfigSystem.Instance.TryGet<RoomMachineRowData>(id, out var cfg))
							worktable = AddWorktable(cfg.Machine, cfg.Scene);
						machine = CreateMachine(id, ref worktable.stations);

						if (worktable.stations.Count == 1)
						{
							EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_ENABLE), worktable.id);
							if (!worktable.isTable)
							{
								DataCenter.Instance.roomData.current.worktableCount++;
								if (!DataCenter.Instance.roomData.tables.Contains(worktable.id))
									DataCenter.Instance.roomData.tables.Add(worktable.id);
							}
						}
					}
					if (machine != null && !machine.active)
					{
						machine.active = true;
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_MACHINE_ENABLE), worktable.id, id);
						if (worktable.lv == 0) UpdateLevel(worktable.id, worktable.scene);
						return machine;
					}
				}
				return default;
			}

			public static Machine GetMachine(int id, out Worktable worktable)
			{
				worktable = default;
				if (id > 0 && ConfigSystem.Instance.TryGet<RoomMachineRowData>(id, out var cfg))
				{
					worktable = GetWorktable(cfg.Machine, cfg.Scene, false);
					if (worktable != null)
					{
						var m = FindMachine(worktable, id);
						if (m == null && cfg.Enable == 1 /*&& IsAreaEnable(cfg.RoomArea)*/)
						{
							m = CreateMachine(id, ref worktable.stations);
							worktable.lv = System.Math.Max(1, worktable.lv);
							worktable.Refresh();
						}
						else if (m != null && !m.cfg.IsValid())
							m.Refresh();
						return m;
					}
				}
				return default;
			}

			public static Machine FindMachine(Worktable worktable, int id)
			{
				if (worktable != null)
				{
					for (int i = 0; i < worktable.stations.Count; i++)
					{
						var s = worktable.stations[i];
						if (s.id == id) return s;
					}
				}
				return default;
			}

			public static Worktable GetWorktable(int id, int scene = 0, bool ifMissAdd = false)
			{
				var ls = GetWorktables();
				var val = default(Worktable);
				if (ls != null && ls.Count > 0)
				{
					_tempIndex = 0;
					for (_tempIndex = 0; _tempIndex < ls.Count; _tempIndex++)
					{
						var w = ls[_tempIndex];
						if (w != null && w.id == id)
						{ val = w; break; }
					}
				}
				if (ifMissAdd && (val == null || val.id == 0))
				{ val = AddWorktable(id, scene); }
				if (val != null && val.type == -1)
					val.Refresh();
				return val;
			}

			public static List<Worktable> GetWorktables(Func<Worktable, bool> condition)
			{
				if (condition != null)
				{
					var ws = GetWorktables();
					if (ws?.Count > 0)
					{
						var ls = default(List<Worktable>);
						foreach (var w in ws)
						{
							if (condition(w))
							{
								ls = ls ?? new List<Worktable>();
								ls.Add(w);
							}
						}
						return ls;
					}
				}
				return default;
			}

			public static Worktable UpdateLevel(int id, int scene, int val = 1, bool set = false, int select = 0)
			{
				var w = GetWorktable(id, scene);
				if (w != null)
				{
					if (!set) w.lv += val;
					else w.lv = Math.Max(0, val);

					var cost = w.GetUpCost(out var ct, out var cid);
					var plv = w.lvcfg;
					var pobjlv = w.objLvCfg;
					var srws = w.starRewards;
					w.addCooker = w.addMachine = w.addProfit = w.addRole = w.addWaiter = 0;
					w.Refresh();

					if (w.lvcfg.IsValid())
					{
						w.addMachine = Math.Min(w.max, w.lvcfg.Num) - (plv.IsValid() ? plv.Num : 0);
						w.addProfit = (w.lvcfg.ShopPriceStarRatio - (plv.IsValid() ? plv.ShopPriceStarRatio : 0)) / 100;
					}
					if (w.objLvCfg.IsValid())
					{
						var f = pobjlv.IsValid();

						if (w.type == ((int)EnumMachineType.JOB) && f)
						{
							select = select == 0 ? pobjlv.ShowType : select;
							if (select == 1) w.addWaiter = 1;
							else if (select == 2) w.addCooker = 1;
						}
						w.addRole = w.objLvCfg.CustomerNum - (f ? pobjlv.CustomerNum : 0);
						w.addMachine = w.objLvCfg.SetNum - (f ? pobjlv.SetNum : 0);
					}

					//升级消耗
					PropertyManager.Instance.Update(ct, cid, cost, true);
					EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UPLEVEL), id, w.level);
					EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.TABEL_LEVEL, 1);

					if (!w.isTable)
					{
						if (w.lvcfg.MachineStar > w.star)//升星奖励
						{
							if (srws?.Count > 0)
							{
								for (int i = 0; i < srws.Count; i++)
								{
									var item = srws[i];
									if (i == 0)
										PropertyManager.Instance.UpdateByArgs(false, item);
									else if (
										ConfigSystem.Instance.TryGet(item[0], out ItemRowData cfg)
										&& ActiveTimeSystem.Instance.IsActiveBySubID(cfg.TypeId, GameServerTime.Instance.serverTime, out var act)
									)
									{
										if (("act" + cfg.TypeId).IsOpend(false) || ("act" + act.configID).IsOpend(false))
											PropertyManager.Instance.UpdateByArgs(false, item);
									}
								}
							}
							EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UP_STAR), id, w.lvcfg.MachineStar);
						}
						if (CheckAllWorktableIsMaxLv())
							EventManager.Instance.AsyncTrigger(((int)GameEvent.WORK_TABLE_ALL_MAX_LV));
					}
					else if (w.objLvCfg.IsValid())
					{
						if (w.addCooker > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Cook), w.addCooker, 0, 0);
						if (w.addWaiter > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Waiter), w.addWaiter, 0, 0);
						if (w.addRole > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Customer), w.addRole, 0, 0);
					}

					return w;
				}
				return default;
			}

			/// <summary>
			/// 工作时间
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static double GetWorkTime(int id)
			{
				var w = GetWorktable(id);
				if (w != null)
					return w.GetWorkTime();
				return 0;
			}

			/// <summary>
			/// 商品价格
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static double GetWorkItemPrice(int id)
			{
				var w = GetWorktable(id);
				if (w != null) return w.GetPrice();
				return 0;
			}

			public static double GetWorkPriceByItem(int item)
			{
				var w = GetWorktables()?.Find(w => w.item == item);
				return w == null ? 0 : w.GetPrice();
			}

			/// <summary>
			/// 星级进度
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static int GetStarProgress(int id)
			{
				var w = GetWorktable(id);
				if (w != null && !w.isTable && w.level > 0)
				{
					var s = w.lvcfg.MachineStar;
					if (ConfigSystem.Instance.TryGet<MachineStarRowData>(s, out var cfg))
					{
						var range = cfg.GetMachineStarArray();
						var len = range[1] + 1 - range[0];
						return Mathf.CeilToInt((w.level - range[0]) * 100f / len);
					}
				}
				return 0;
			}

			/// <summary>
			/// 获取工作台星星信息
			/// 返回显示列表
			/// 列表长度是多少就显示多少个星星
			/// 数据代表星星类型 <see cref="EnumStar"/>
			/// </summary>
			/// <param name="id">工作台id</param>
			/// <returns></returns>
			public static int[] GetWorktableStarInfo(int id)
			{
				var w = GetWorktable(id);
				if (w != null)
				{
					if (!w.isTable)
						return CalcuStarList(GetWorkertableMaxStar(w.maxlv), w.lvcfg.MachineStar);
					else if (w.objCfg.IsValid())
						return CalcuStarList(w.maxlv - w.lvStart, w.lv);
				}
				return default;
			}

			public static int[] CalcuStarList(int max, int cur)
			{

				if (max > 0)
				{
					var cstar = cur;
					//几阶
					var step = Mathf.CeilToInt(max / 5f);
					//当前处于哪一阶 
					var type = cstar == 0 ? 1 : Mathf.CeilToInt(cstar / 5f);
					//星星长度
					var size = type < step ? 5 : max % 5;
					size = size == 0 ? 5 : size;

					var stars = new int[size];
					if (cstar > 0)
					{
						var l = cstar % 5 == 0 ? size : cstar % 5;
						for (int i = 0; i < l; i++)
							stars[i] = type;
					}
					return stars;
				}
				return default;
			}

			public static bool IsActiveds(bool isAll, params int[] machines)
			{
				if (machines?.Length > 0)
				{
					for (int i = 0; i < machines.Length; i++)
					{
						if (isAll && !IsActived(machines[i])) return false;
						else if (!isAll && IsActived(machines[i])) return true;
					}
					return isAll;
				}
				return true;
			}

			public static bool IsActived(int machine)
			{
				var m = GetMachine(machine, out Worktable worktable);
				if (m != null && m.enable) return true;
				return false;
			}

			public static bool CheckCanAddMachine(int id, int scene)
			{
				return CheckCanAddMachine(GetWorktable(id, scene));
			}

			public static bool CheckCanAddMachine(Worktable worktable)
			{
				if (worktable != null && worktable.lvcfg.IsValid())
				{
					return worktable.lvcfg.Num + 1 > worktable.stations?.Count;
				}
				return false;
			}

			public static int CheckCanActiveMachine(int id, bool ignoreCost = false)
			{
				if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(id, out var m))
				{
					var w = GetWorktable(m.Machine, m.Scene);
					var ds = m.GetDependsArray();
					if (!IsActiveds(true, ds))
						return Error_Code.MACHINE_DEPENDS_NOT_ENABLE;
					if (!ignoreCost && !PropertyManager.Instance.CheckCountByArgs(w.GetUnlockPrice()))
						return Error_Code.ITEM_NOT_ENOUGH;
					if (m.DependsLevelLength > 1)//工作台等级依赖
					{
						var dw = GetWorktable(m.DependsLevel(0));
						if (dw != null && dw.level > 0)
						{
							if (dw.level < m.DependsLevel(1))
								return Error_Code.MACHINE_DEPENDS_LEVEL_ERROR;
						}
					}
					return 0;
				}
				return -1;
			}

			public static bool CheckDontAutoActive(int id)
			{
				if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(id, out var m))
				{
					if (m.Enable == 1 || m.DependsLength > 0)
						return false;
					return true;
				}
				return false;
			}

			public static int CheckCanUpLevel(int id, int scene)
			{
				return CheckCanUpLevel(GetWorktable(id, scene));
			}

			public static int CheckCanUpLevel(Worktable worktable)
			{
				if (worktable != null && worktable.notNormalTable && worktable.type != -1)
				{
					if (worktable.level >= worktable.maxlv) return Error_Code.LV_MAX;
					if (worktable.level > 0 && !PropertyManager.Instance.CheckCount(1, worktable.GetCostVal(),1))
						return Error_Code.ITEM_NOT_ENOUGH;
					if (worktable.condition > 0)
					{
						if (!IsAreaEnable(worktable.condition)) return Error_Code.AREA_IS_LOCK;
					}
					return Error_Code.SUCCESS;
				}
				return -1;
			}

			/// <summary>
			/// 获取上限星星
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static int GetWorkertableMaxStar(int lvmax)
			{
				if (ConfigSystem.Instance.TryGet(lvmax, out MachineUpgradeRowData lv))
					return lv.MachineStar;
				return 0;
			}

			public static bool CheckAllWorktableIsMaxLv()
			{
				return GetWorktables(CheckMaxLv) == null;
			}

			private static bool CheckMaxLv(Worktable worktable)
			{
				return !worktable.isTable && worktable.level < worktable.maxlv;
			}

			/// <summary>
			/// 区域是否激活
			/// </summary>
			/// <param name="area"></param>
			/// <returns></returns>
			public static bool IsAreaEnable(int area)
			{
				if (area > 1)
				{
					return RoomUtil.IsAreaEnable(area);
				}
				return true;
			}

			public static bool IsAreaEnable(string key)
			{
				if (!_areas.TryGetValue(key, out var id))
				{
					int.TryParse(key.Split('_').Last(), out id);
					_areas[key] = id;
				}
				return IsAreaEnable(id);
			}

			public static bool IsAreaEnable(Machine machine)
			{
				if (machine != null && machine.cfg.IsValid())
					return IsAreaEnable(machine.cfg.RoomArea);
				return false;
			}

			public static bool IsAreaEnableByMachine(int machineID)
			{
				if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(machineID, out var m))
				{
					return IsAreaEnable(m.RoomArea);
				}
				return false;
			}

			public static (int, int) GetRoomLvState()
			{
				var ws = GetWorktables(w => !w.isTable);
				if (ws?.Count > 0)
					return (ws.Sum(w => w.maxlv), ws.Sum(w => w.level));
				return (0, 0);
			}

			private static Worktable AddWorktable(int id, int scene)
			{
				var val = new Worktable()
				{
					id = id,
					scene = scene,
				};
				val.Refresh();

				GetWorktables()?.Add(val);
				return val;
			}

			private static Machine CreateMachine(int id, ref List<Machine> machines, int lv = 0)
			{
				var m = new Machine() { id = id, enable = true };
				machines = machines ?? new List<Machine>();
				machines.Add(m);
				m.Refresh();
				return m;
			}

			private static List<Worktable> GetWorktables(int scene = 0)
			{
				return Instance.roomData.current.worktables;
			}
		}
	}


	[System.Serializable]
	public class Worktable
	{
		public int id;
		public int scene;

		public int lv;
		public int star;
		public int food;

		[NonSerialized]
		public bool isTable;
		[NonSerialized]
		public int max;
		[NonSerialized]
		public int maxlv;
		[NonSerialized]
		public int maxStar;

		public List<Machine> stations = new List<Machine>();

		[NonSerialized]
		public int type = -1;
		[NonSerialized]
		public MachineRowData cfg;
		[NonSerialized]
		public RoomObjRowData objCfg;
		[NonSerialized]
		public ItemRowData foodCfg;
		[NonSerialized]
		public MachineUpgradeRowData lvcfg;
		[NonSerialized]
		public RoomObjLevelRowData objLvCfg;
		[NonSerialized]
		public RoomObjLevelRowData objNextLvCfg;
		[NonSerialized]
		public int addProfit;
		[NonSerialized]
		public int addMachine;
		[NonSerialized]
		public int reward;
		[NonSerialized]
		public List<int[]> starRewards;
		[NonSerialized]
		public int addCooker;
		[NonSerialized]
		public int addRole;
		[NonSerialized]
		public int addWaiter;
		[NonSerialized]
		public int lvStart;
		[NonSerialized]
		public float[] upcost;
		[NonSerialized]
		public float[] unlockcost;
		[NonSerialized]
		public int condition;

		static Dictionary<int, List<RoomMachineRowData>> ALL_TABLE_CFGS = null;


		public int item { get { return cfg.IsValid() && food <= 0 ? cfg.ItemId(0) : food; } }

		public int level { get { return lvStart + lv; } }

		public string name { get { return foodCfg.IsValid() ? foodCfg.Name : cfg.IsValid() ? cfg.MachineName : objCfg.IsValid() ? objCfg.Name : null; } }

		public string foodName { get { return foodCfg.IsValid() ? "ui_worktable_name".Local(null, foodCfg.Name.Local()) : name.Local(); } }

		public bool notNormalTable { get { return type != 1; } }

		public bool IsMaxLv()
		{
			return level >= maxlv;
		}

		public int GetMaxStar()
		{
			if (objCfg.IsValid()) return maxlv - lvStart;
			return DataCenter.MachineUtil.GetWorkertableMaxStar(maxlv);
		}

		public float[] GetUnlockPrice()
		{
			return unlockcost;
		}

		public double GetPrice(int bookid = 0, bool baseval = false)
		{
			if (!isTable)
			{
				var book = DataCenter.CookbookUtils.GetBook(bookid == 0 ? item : bookid);
				if (book != null)
				{
					if (!lvcfg.IsValid())
						ConfigSystem.Instance.TryGet<MachineUpgradeRowData>(1, out lvcfg);

					var price = baseval ? book.cfg.Price(2) : AttributeSystem.Instance.GetValue(EnumTarget.Machine, EnumAttribute.Price, id);
					return (1D * book.lvCfg.Price * 0.01 * price * lvcfg.ShopPriceRatio * lvcfg.ShopPriceStarRatio * 0.0001).ToInt();
				}
			}
			return 0;
		}

		public double GetWorkTime(int bookid = 0)
		{
			if (!isTable)
			{
				var book = DataCenter.CookbookUtils.GetBook(bookid == 0 ? item : bookid);
				if (book != null)
				{
					if (!lvcfg.IsValid())
						ConfigSystem.Instance.TryGet<MachineUpgradeRowData>(1, out lvcfg);
					var t = book.lvCfg.Time * lvcfg.TimeRatio * 0.01d;
					return (t / AttributeSystem.Instance.GetValue(EnumTarget.Machine, EnumAttribute.WorkSpeed, id)).Round();
				}
			}
			return 0;
		}

		public double GetUpCost()
		{
			return GetUpCost(out _, out _);
		}

		public double GetCostVal()
		{
			if (upcost == null) return 0;
			return !isTable ? (0.01d * upcost[2] * cfg.UpgradeRatio).ToInt() : upcost[2];
		}

		public double GetUpCost(out int type, out int id)
		{
			type = id = 0;
			if (!isTable)
			{
				if (lvcfg.IsValid())
				{
					if (upcost == null) upcost = lvcfg.GetUpgradePriceArray();
					type = (int)upcost[0];
					id = (int)upcost[1];
					return (0.01d * upcost[2] * cfg.UpgradeRatio).ToInt();
				}
			}
			else if (objLvCfg.IsValid())
			{
				if (upcost == null) return default;
				type = (int)upcost[0];
				id = (int)upcost[1];
				return upcost[2];
			}
			return default;
		}

		public bool CanUpLv()
		{
			return (maxlv - 1) > lvStart;
		}

		public int GetSeats()
		{
			return addRole > 0 ? addRole : objLvCfg.IsValid() ? objLvCfg.CustomerNum : 0;
		}

		public void SetFood(int foodindex)
		{
			if (isTable) return;
			this.food = cfg.ItemId(foodindex);
		}

		public string GetFoodAsset()
		{
			return "machine_icon_item_" + item + ".png";
		}

		public void Refresh()
		{
			if (lvcfg.IsValid()) star = lvcfg.MachineStar;
			if (type == -1)
			{
				if (ALL_TABLE_CFGS == null)
					ALL_TABLE_CFGS = ConfigSystem.Instance.Finds<RoomMachineRowData>(c => true).GroupBy(v => v.Machine).ToDictionary(c => c.Key, c => c.ToList());

				ALL_TABLE_CFGS.TryGetValue(id, out var ls);
				var first = ls.FirstOrDefault();
				lvStart = 0;
				if (first.IsValid())
				{
					maxlv = first.MachineLevelMax;
					max = ls.Count - 1;
					type = first.Type;
					if (type > 3 && ConfigSystem.Instance.TryGet(first.Machine, out objCfg))
					{
						max = 999;
						lvStart = objCfg.LevelId(0) - 1;
						maxlv = objCfg.LevelIdLength > 1 ? objCfg.LevelId(1) : objCfg.LevelId(0);
					}
				}
				else
					type = -9999;

				if (!ConfigSystem.Instance.TryGet<MachineRowData>(id, out cfg))
					isTable = true;
				else
					unlockcost = cfg.GetUnlockPriceArray();
			}

			if (type > 3)
			{
				if (lv > 0)
				{

					if (ConfigSystem.Instance.TryGet<RoomObjLevelRowData>(level, out objLvCfg))
					{
						upcost = objLvCfg.GetCostArray();
						condition = objLvCfg.Condition;
					}
				}
				ConfigSystem.Instance.TryGet<RoomObjLevelRowData>(level + 1, out objNextLvCfg);
				unlockcost = objLvCfg.IsValid() && objLvCfg.CostLength > 0 ? objLvCfg.GetCostArray() : objCfg.GetCostArray();
			}
			else if (!isTable && ConfigSystem.Instance.TryGet<MachineUpgradeRowData>(level, out lvcfg))
			{
				upcost = lvcfg.GetUpgradePriceArray();
				if (food == 0) food = cfg.ItemId(0);
				ConfigSystem.Instance.TryGet(item, out foodCfg);
				if ((starRewards == null || lvcfg.MachineStar > star))
				{
					if (ConfigSystem.Instance.TryGet(lvcfg.MachineStar + 1, out MachineStarRowData scfg))
					{
						starRewards = new List<int[]>() { scfg.GetStarRewardArray() };
						if (scfg.ActivityRewardLength > 1)
						{
							for (int i = 0; i < scfg.ActivityRewardLength; i += 2)
								starRewards.Add(new int[] { scfg.ActivityReward(i), scfg.ActivityReward(i + 1) });
						}
					}
					else
						starRewards = new List<int[]>();
				}
			}
		}

		public void RefreshStarRewards()
		{

		}

	}

	[System.Serializable]
	public class Machine
	{
		public int id;
		public bool enable;
		public bool active;

		[NonSerialized]
		public RoomMachineRowData cfg;

		public void Refresh()
		{
			ConfigSystem.Instance.TryGet(id, out cfg);
		}

	}



}