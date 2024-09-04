﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class DataCenter
	{
		public WorkerData workerdata = new WorkerData();

		public static class WorkerDataUtils
		{
			const string enable_str_cook = "enable_collect_cook";
			const string enable_str_wait = "enable_collect_wait";

			static public int c_worker_maxlv = 0;
			static public int c_waiter_maxlv = 0;


			private static WorkerData data { get { return Instance.workerdata; } }

			public static void Init()
			{
				if (c_worker_maxlv == 0)
				{
					c_worker_maxlv = ConfigSystem.Instance.Finds<WorkerLevelRowData>(c => c.WorkerId == 1).Count;
					c_waiter_maxlv = ConfigSystem.Instance.Finds<WorkerLevelRowData>(c => c.WorkerId == 2).Count;
				}
				data.Refresh();
				EventManager.Instance.Reg(((int)GameEvent.BUFF_RESET), BuffTrigger);
				EventManager.Instance.Reg<int>(((int)GameEvent.WORK_AREA_UNLOCK), (a) => RefreshBuff());


			}

			public static WorkerDataItem GetData(int id, bool missadd = true)
			{
				if (data.dataDics == null)
					data.Refresh();
				if (!data.dataDics.TryGetValue(id, out var val) && missadd && data.dataIDs.Contains(id))
				{
					val = new WorkerDataItem() { id = id };
					val.Refresh();
					data.dataDics[id] = val;
					data.datas.Add(val);
				}
				return val;
			}

			public static List<WorkerDataItem> GetDatas(Func<WorkerDataItem, bool> condition = null)
			{
				if (data.dataDics == null)
					data.Refresh();
				if (condition != null)
					return data.datas.FindAll(x => condition(x));
				return data.datas;
			}

			public static List<int[]> GetBuffList(int type, List<WorkerDataItem> datas = null)
			{
				if (datas == null && type > 0)
					datas = GetDatas((w) => w.cfg.RoleType == type);
				if (datas?.Count > 0)
				{
					/*var ws = "waiter".IsOpend(false);
					var cs = "cooker".IsOpend(false);*/

					return datas
						//.Where(d => (d.cfg.RoleType == ((int)EnumRole.Cook) && cs) || (d.cfg.RoleType == ((int)EnumRole.Waiter) && ws))
						.Select(d => new int[] { d.cfg.Buff, d.level > 0 ? d.GetBuffVal() : 0 })
						.GroupBy(v => v[0])
						.ToDictionary(v => v.Key, v => v.Sum(i => i[1]))
						.Select(v => new int[] { v.Key, v.Value })
						.ToList();
				}

				return default;

			}

			public static int[] GetDataIDs()
			{
				if (data.dataDics == null)
					data.Refresh();
				return data.dataIDs.ToArray();
			}

			public static bool Select(WorkerDataItem item)
			{
				if (item != null)
				{
					if (item.IsSelected()) return false;
					switch (item.cfg.RoleType)
					{
						case 1:
							data.selectID = item.id;
							DataCenter.Instance.cookerModel = item.cfg.RoleId;
							break;
						case 2:
							data.selectWaiterID = item.id;
							DataCenter.Instance.waiterModel = item.cfg.RoleId;
							break;
					}
					EventManager.Instance.Trigger(((int)GameEvent.WORKER_SELECTED), item.cfg.RoleType, item.cfg.RoleId);
					EventManager.Instance.Trigger(((int)GameEvent.WORKER_UPDAETE), item.id);
					return true;
				}
				return false;
			}

			public static bool GetReward(WorkerDataItem item)
			{
				if (item != null && item.reward > 0)
				{
					item.reward = 0;
					Utils.ShowRewards(new List<int[]>() { item.cfg.GetRewardArray() });
					EventManager.Instance.Trigger(((int)GameEvent.WORKER_UPDAETE), item.id);
					return true;
				}
				return false;
			}

			public static bool UpLv(int id)
			{
				var book = GetData(id);
				if (book != null)
				{
					var state = false;
					var old = book.GetBuffVal();
					var lv = book.level;
					while (book.CanUpLv())
					{
						state = true;
						var cost = book.GetCost(out var ty, out var item);
						PropertyManager.Instance.Update(ty, item, cost, true);
						book.UpLv();

					}
					if (state)
					{
						book.lastLv = lv;
						book.lastVal = old;
						EventManager.Instance.Trigger(((int)GameEvent.WORKER_UP_LV), id, book.level);
						EventManager.Instance.Trigger(((int)GameEvent.WORKER_UPDAETE), book.id);
						BuffTrigger();
						return true;
					}
				}
				return false;
			}

			public static void RefreshBuff(bool focus = false)
			{
				return;
				var s = focus;
				if (s)
				{
					var cs = "cooker".IsOpend(false);
					var ws = "watier".IsOpend(false);
					if (cs && GetIntValue(enable_str_cook) == 0)
					{
						s = true;
						SetIntValue(enable_str_cook, 1);
					}
					if (ws && GetIntValue(enable_str_wait) == 0)
					{
						s = true;
						SetIntValue(enable_str_wait, 1);
					}
				}
				if (s) BuffTrigger();
			}

			public static void BuffTrigger()
			{
				CheckDefaultSelect();
				EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = (int)EnumFrom.WorkerCollect });
				GetBuffList(0, data.datas)
				   .Where(s => s[1] != 0)
				   .Select(s => new BuffData()
				   {
					   from = ((int)EnumFrom.WorkerCollect),
					   id = s[0],
					   val = s[1]
				   }
				   ).ToList()
				   .ForEach(b => EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), b));
			}

			public static bool Check(int type)
			{
				foreach (var item in data.datas)
				{
					if ((type == 0 || type == item.cfg.RoleType) && item.Check()) return true;
				}
				return false;
			}

			public static void CheckDefaultSelect()
			{
				if (data.selectID == 0 && "cooker".IsOpend(false)) Select(data.datas.Find(d => d.cfg.RoleType == 1 && d.level == 1));
				if (data.selectWaiterID == 0 && "waiter".IsOpend(false)) Select(data.datas.Find(d => d.cfg.RoleType == 2 && d.level == 1));
			}

		}

	}

	[Serializable]
	public class WorkerData
	{
		public List<WorkerDataItem> datas = new List<WorkerDataItem>();
		public int selectID;
		public int selectWaiterID;

		public Dictionary<int, WorkerDataItem> dataDics { get; private set; }
		public List<int> dataIDs { get; private set; }

		public void Refresh()
		{
			if (dataIDs == null)
			{
				dataIDs = ConfigSystem.Instance
					.Finds<WorkerRowData>(c => true)
					.Select(c => c.Id)
					.ToList();
			}
			datas.ForEach(b => b.Refresh());
			dataDics = datas.ToDictionary(b => b.id);
			dataIDs.ForEach(b => DataCenter.WorkerDataUtils.GetData(b));

		}

	}

	[Serializable]
	public class WorkerDataItem
	{

		public int id;
		public int level;
		public int reward;
		public int val;

		[NonSerialized]
		public WorkerRowData cfg;
		[NonSerialized]
		public RoleDataRowData roleCfg;
		[NonSerialized]
		public WorkerLevelRowData lvCfg;
		[NonSerialized]
		public int lastVal;
		[NonSerialized]
		public int lastLv;

		public bool IsMaxLv()
		{
			if (cfg.ByteBuffer!= null)
			{
				return level >= (cfg.RoleType == 1 ? DataCenter.WorkerDataUtils.c_worker_maxlv : DataCenter.WorkerDataUtils.c_waiter_maxlv);
			}
			return false;
		}

		public bool IsEnable()
		{
			return level >= 1;
		}

		public double GetCost(out int type, out int id)
		{
			type = 0;
			id = this.id;
			if (lvCfg.ByteBuffer!=null)
			{
				type = 1;
				return Math.Ceiling(lvCfg.UpgradePrice * cfg.LevelRatio);
			}
			return 0;
		}

		public bool CanUpLv()
		{
			var f = false;
			if (IsMaxLv()) return f;
			if (lvCfg.ByteBuffer != null)
			{
				var need = GetCost(out _, out var item);
				return need == 0 || PropertyManager.Instance.CheckCount(item, need, 1);
			}
			return f;
		}

		public bool IsSelected()
		{
			if (cfg.RoleType == 1)
				return DataCenter.Instance.workerdata.selectID == this.id;
			else
				return DataCenter.Instance.workerdata.selectWaiterID == this.id;
		}

		public int GetBuffVal(bool usedefault = false)
		{
			if (usedefault || level <= 1)
				return cfg.IsValid() ? cfg.Default : 0;
			return cfg.IsValid() && level > 0 ? (cfg.Default + val) : 0;
		}

		public WorkerDataItem UpLv()
		{
			if (!IsMaxLv())
			{
				lastVal = GetBuffVal();
				lastLv = level;
				level += 1;
				if (level == 1)
					reward = 1;
				if (level > 1)
					val += SGame.Randoms.Random._R.Next(cfg.Range(0), cfg.Range(1));
				Refresh();
			}
			return this;
		}

		public WorkerDataItem Refresh()
		{
			if (id > 0)
			{

				if (!cfg.IsValid() && !ConfigSystem.Instance.TryGet(id, out cfg))
					return this;
				if (!roleCfg.IsValid()) ConfigSystem.Instance.TryGet(cfg.RoleId, out roleCfg);
				if (IsMaxLv()) lvCfg = default;
				else if (!lvCfg.IsValid() || lvCfg.Level <= level)
				{
					if (level == 0 && cfg.Unlock > 0)
					{
						level = 1; reward = 1;
					}
					lvCfg = ConfigSystem.Instance.Find<WorkerLevelRowData>((c) => c.WorkerId == cfg.RoleType && c.Level == level + 1);
				}
			}
			return this;
		}

		public bool Check()
		{
			return reward > 0 || CanUpLv();
		}

	}

}
