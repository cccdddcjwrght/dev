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
		[SerializeField]
		public WorktableData worktable = new WorktableData();

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
							if (worktable.level == 0) UpdateLevel(worktable.id, worktable.scene);
						}
						EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_MACHINE_ENABLE), worktable.id, id);
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
				var val = Instance.worktable.machines?.Find(m => m.id == id && (scene == 0 || m.scene == scene));
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
					w.Refresh();

					EventManager.Instance.Trigger(((int)GameEvent.WORK_TABLE_UPLEVEL), id, w.level);
				}
				return default;
			}

			/// <summary>
			/// 空闲点位
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
				if (worktable != null)
				{
					if (worktable.lvcfg.ByteBuffer != null)
						return worktable.lvcfg.Num + 1 > worktable.stations?.Count;
				}
				return false;
			}

			public static int CheckCanActiveMachine(int id, bool ignoreCost = false)
			{
				if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(id, out var m))
				{
					var w = GetWorktable(m.Machine, m.Scene);
					if (!IsActiveds(true, m.GetDependsArray()))
						return Error_Code.MACHINE_DEPENDS_NOT_ENABLE;
					/*if (!ignoreCost &&!PropertyManager.Instance.CheckCountByArgs(w.cfg.GetUnlockPriceArray()))
						return Error_Code.ITEM_NOT_ENOUGH;*/
					return 0;
				}
				return -1;

			}

			public static int CheckCanUpLevel(int id, int scene)
			{
				return CheckCanUpLevel(GetWorktable(id, scene));
			}

			public static int CheckCanUpLevel(Worktable worktable)
			{
				if (worktable != null && worktable.cfg.ByteBuffer != null)
				{
					if (worktable.level >= worktable.cfg.MachineLevelMax) return Error_Code.LV_MAX;
					//if (worktable.level>0 && !PropertyManager.Instance.CheckCountByArgs(worktable.lvcfg.GetUpgradePriceArray()))
					//	return Error_Code.ITEM_NOT_ENOUGH;
					return 0;
				}
				return -1;
			}

			/// <summary>
			/// 获取上限星星
			/// </summary>
			/// <param name="id"></param>
			/// <returns></returns>
			public static int GetWorkertableMaxStar(int id)
			{
				if (ConfigSystem.Instance.TryGet(id, out MachineRowData cfg))
				{
					if (ConfigSystem.Instance.TryGet(cfg.MachineLevelMax, out MachineUpgradeRowData lv))
					{
						return lv.MachineStar;
					}
				}
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
				Instance.worktable.machines.Add(val);
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

		public List<Machine> stations = new List<Machine>();

		[NonSerialized]
		public MachineRowData cfg;
		[NonSerialized]
		public MachineUpgradeRowData lvcfg;

		public bool isTable;

		public void Refresh()
		{
			if (!ConfigSystem.Instance.TryGet<MachineRowData>(id, out cfg)) isTable = true;
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

		public List<Seat> seats = new List<Seat>();

		[NonSerialized]
		public RoomMachineRowData cfg;

		public void Refresh()
		{
			ConfigSystem.Instance.TryGet(id, out cfg);
		}

	}

	[System.Serializable]
	public class Seat
	{
		public string tag;
		public int index;
		public int state;
		public int holder;
		public int time;
	}


}