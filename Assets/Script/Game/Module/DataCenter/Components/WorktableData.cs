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
							EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_ENABLE), worktable.id);
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
						var m = worktable.stations?.Find(s => s.id == id);
						if (m == null && cfg.Enable == 1)
						{
							m = CreateMachine(id, ref worktable.stations);
							worktable.level = System.Math.Max(1, worktable.level);
							worktable.Refresh();
						}
						return m;
					}
				}
				return default;
			}

			public static Worktable GetWorktable(int id, int scene = 0, bool ifMissAdd = false)
			{
				var val = GetWorktables()?.Find(m => m.id == id);
				if (ifMissAdd && (val == null || val.id == 0))
					val = AddWorktable(id, scene);
				return val;
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
					var star = 0;
					if (w.lvcfg.IsValid())
					{
					 	star = w.lvcfg.MachineStar;
						pmac = w.lvcfg.Num;
						prp = w.lvcfg.ShopPriceStarRatio;
					}

					w.Refresh();
					w.addMachine = w.lvcfg.Num - pmac;
					w.addProfit = (w.lvcfg.ShopPriceStarRatio - prp) / 100;

					//升级消耗
					PropertyManager.Instance.UpdateByArgs(true, w.lvcfg.GetUpgradePriceArray());
					EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UPLEVEL), id, w.level);

					if (w.lvcfg.MachineStar > star)//升星奖励
					{
						if (ConfigSystem.Instance.TryGet<MachineStarRowData>(w.lvcfg.MachineStar, out var cfg))
							PropertyManager.Instance.UpdateByArgs(false, cfg.GetStarRewardArray());
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UP_STAR), id, w.lvcfg.MachineStar);
					}
					return w;
				}
				return default;
			}

			/// <summary>
			/// 空闲点位【暂时没什么】
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static Machine GetFreeMachine(int id)
			{
				var w = GetWorktable(id);
				if (w != null && w.stations?.Count > 0)
					return w.stations.Find(s => s.state == 0 && s.cfg.Nowork != 1);
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
					for (int i = 0; i < cstar % 6; i++)
						stars[i] = type;

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
					if (worktable.level >= worktable.maxlv) return Error_Code.LV_MAX;
					if (worktable.level > 0 && !PropertyManager.Instance.CheckCountByArgs(worktable.lvcfg.GetUpgradePriceArray()))
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

		public bool isTable;
		public int max;
		public int maxlv;

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

		public int item { get { return cfg.IsValid() ? cfg.ItemId : 0; } }

		public int price { get { return cfg.IsValid() ? cfg.ItemId : 0; } }

		public double GetPrice()
		{
			if (!isTable && lvcfg.IsValid())
				return (1L * AttributeSystem.Instance.GetValue(EnumTarget.Machine, EnumAttribute.Price, id) * lvcfg.ShopPriceStarRatio * lvcfg.ShopPriceStarRatio * 0.0001).ToInt();
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

		public void Refresh()
		{
			if (lvcfg.IsValid()) star = lvcfg.MachineStar;
			if (!ConfigSystem.Instance.TryGet<MachineRowData>(id, out cfg)) isTable = true;
			else
			{
				var ls = ConfigSystem.Instance.Finds<RoomMachineRowData>(c => c.Machine == id);
				max = ls.Count;
				maxlv = max > 0 ? ls[0].MachineLevelMax : 10;
			}
			ConfigSystem.Instance.TryGet<MachineUpgradeRowData>(level, out lvcfg);
		}

	}

	[System.Serializable]
	public class Machine
	{
		public int id;
		public bool enable;
		public int state;
		public int holder;
		public int time;

		[NonSerialized]
		public RoomMachineRowData cfg;

		public void Refresh()
		{
			ConfigSystem.Instance.TryGet(id, out cfg);
		}

	}



}