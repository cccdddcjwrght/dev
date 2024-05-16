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

	}

	public partial class DataCenter
	{
		public class MachineUtil
		{
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
							if (!worktable.isTable) DataCenter.Instance.roomData.current.worktableCount++;
						}
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_MACHINE_ENABLE), worktable.id, id);
						if (worktable.level == 0) UpdateLevel(worktable.id, worktable.scene);

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
						if (m == null && cfg.Enable == 1)
						{
							m = CreateMachine(id, ref worktable.stations);
							worktable.level = System.Math.Max(1, worktable.level);
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
					for (int i = 0; i < ls.Count; i++)
					{
						var w = ls[i];
						if (w != null && w.id == id)
						{ val = w; break; }
					}
				}
				if (ifMissAdd && (val == null || val.id == 0))
				{ val = AddWorktable(id, scene); }
				if (!val.cfg.IsValid())
					val.Refresh();
				return val;
			}

			public static List<Worktable> GetWorktables(Func<Worktable, bool> condition)
			{
				if (condition != null)
				{
					var ws = GetWorktables();
					if (ws?.Count > 0)
						return ws.FindAll(w => condition(w));
				}
				return default;
			}

			public static Worktable UpdateLevel(int id, int scene, int val = 1, bool set = false)
			{
				var w = GetWorktable(id, scene);
				if (w != null)
				{
					if (!set)
						w.level += val;
					else
						w.level = Math.Max(0, val);

					var pmac = 0;
					var prp = 0;
					var cost = w.GetUpCost();
					if (w.lvcfg.IsValid())
					{
						pmac = w.lvcfg.Num;
						prp = w.lvcfg.ShopPriceStarRatio;
					}
					var srws = w.starRewards;
					w.Refresh();
					w.addMachine = Math.Min(w.max, w.lvcfg.Num) - pmac;
					w.addProfit = (w.lvcfg.ShopPriceStarRatio - prp) / 100;

					//升级消耗
					PropertyManager.Instance.Update((int)w.lvcfg.UpgradePrice(0), (int)w.lvcfg.UpgradePrice(1), cost, true);
					EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UPLEVEL), id, w.level);

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
									ConfigSystem.Instance.TryGet(item[0] , out ItemRowData cfg) 
									&& ActiveTimeSystem.Instance.IsActiveBySubID(cfg.TypeId , GameServerTime.Instance.serverTime , out _)
								)
								{
									PropertyManager.Instance.UpdateByArgs(false, item);
								}
							}
						}
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UP_STAR), id, w.lvcfg.MachineStar);
					}

					if (!w.isTable)
					{
						if (CheckAllWorktableIsMaxLv())
							EventManager.Instance.AsyncTrigger(((int)GameEvent.WORK_TABLE_ALL_MAX_LV));
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
				if (w != null && !w.isTable)
					return CalcuStarList(GetWorkertableMaxStar(w.maxlv), w.lvcfg.MachineStar);
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
					if (!ignoreCost && w.cfg.IsValid() && !PropertyManager.Instance.CheckCountByArgs(w.cfg.GetUnlockPriceArray()))
						return Error_Code.ITEM_NOT_ENOUGH;
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
				if (worktable != null && worktable.cfg.IsValid())
				{
					var cost = worktable.GetUpCost(out var type, out var id);
					if (worktable.level >= worktable.maxlv) return Error_Code.LV_MAX;
					if (worktable.level > 0 && !PropertyManager.Instance.CheckCount(id, cost, type))
						return Error_Code.ITEM_NOT_ENOUGH;
					return 0;
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
				var ws = GetWorktables(w => !w.isTable);
				if (ws?.Count > 0)
					return ws.All(w => w.level >= w.maxlv);
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
	public class WorktableData
	{
		public List<Worktable> machines = new List<Worktable>();
	}

	[System.Serializable]
	public class Worktable
	{
		public int id;
		public int scene;

		public int level;
		public int star;

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
		public MachineRowData cfg;
		[NonSerialized]
		public MachineUpgradeRowData lvcfg;
		[NonSerialized]
		public int addProfit;
		[NonSerialized]
		public int addMachine;
		[NonReorderable]
		public int reward;

		public List<int[]> starRewards;

		public int item { get { return cfg.IsValid() ? cfg.ItemId : 0; } }

		public int price { get { return cfg.IsValid() ? cfg.ItemId : 0; } }

		public double GetPrice()
		{
			if (!isTable && lvcfg.IsValid())
				return (1L * AttributeSystem.Instance.GetValue(EnumTarget.Machine, EnumAttribute.Price, id) * lvcfg.ShopPriceRatio * lvcfg.ShopPriceStarRatio * 0.0001).ToInt();
			return 0;
		}

		public double GetWorkTime()
		{
			if (!isTable && lvcfg.IsValid())
			{
				var t = cfg.Time * lvcfg.TimeRatio * 0.01d;
				return (t / AttributeSystem.Instance.GetValue(EnumTarget.Machine, EnumAttribute.WorkSpeed, id)).Round();
			}
			return 0;
		}


		public double GetUpCost()
		{
			return GetUpCost(out _, out _);
		}

		public double GetUpCost(out int type, out int id)
		{
			type = id = 0;
			if (!isTable && lvcfg.IsValid())
			{
				type = (int)lvcfg.UpgradePrice(0);
				id = (int)lvcfg.UpgradePrice(1);
				return (0.01d * lvcfg.UpgradePrice(2) * cfg.UpgradeRatio).ToInt();
			}
			return 0;
		}

		public void Refresh()
		{
			if (lvcfg.IsValid()) star = lvcfg.MachineStar;
			if (!ConfigSystem.Instance.TryGet<MachineRowData>(id, out cfg)) isTable = true;
			else if (max <= 0)
			{
				var ls = ConfigSystem.Instance.Finds<RoomMachineRowData>(c => c.Machine == id);
				maxlv = ls.Count > 0 ? ls[0].MachineLevelMax : 10;
				max = ls.Count - 1;
			}
			if (ConfigSystem.Instance.TryGet<MachineUpgradeRowData>(level, out lvcfg))
			{
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
						starRewards = new List<int[]>() ;
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

		[NonSerialized]
		public RoomMachineRowData cfg;

		public void Refresh()
		{
			ConfigSystem.Instance.TryGet(id, out cfg);
		}

	}



}