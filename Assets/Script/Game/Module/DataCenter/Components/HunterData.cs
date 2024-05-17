using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;

namespace SGame
{

	partial class DataCenter
	{
		public HunterData hunterData = new HunterData();

		static public class HunterUtil
		{
			static HunterData _data { get { return Instance.hunterData; } }

			static public void Init()
			{
				_data.hunters.ForEach(h => CheckActivityIsClose(h));
				_data.hunters.RemoveAll(h => h == null || h.isClosed);
				EventManager.Instance.Reg<int>(((int)GameEvent.ACTIVITY_CLOSE), OnActivityClose);
			}

			static public Hunter GetHunter(int actID, bool create = true)
			{
				if (actID > 0)
				{
					var hunter = _data.hunters.Find(h => h.id == actID);
					if (hunter == null && create)
					{
						hunter = new Hunter() { id = actID };
						hunter.Refresh();
						_data.hunters.Add(hunter);
					}
					return hunter;
				}
				return default;
			}

			static public List<int[]> GetRewards(MonsterHunterRowData cfg, int step = 3)
			{
				if (cfg.IsValid())
				{
					switch (step)
					{
						case 1:
							return Utils.GetArrayList(cfg.GetMidReward1Array);
						case 2:
							return Utils.GetArrayList(cfg.GetMidReward2Array);
						case 3:
							return Utils.GetArrayList(
								true,
								cfg.GetKillReward1Array,
								cfg.GetKillReward2Array,
								cfg.GetKillReward3Array,
								cfg.GetKillReward4Array
							);
					}
				}
				return default;
			}

			static public void OnActivityClose(int id)
			{
				var hunter = GetHunter(id, false);
				if (hunter != null)
				{
					hunter.isClosed = true;
					var type = hunter.subID;
					var h = _data.hunters.Find(h => h.id != id && h.subID == type);
					if (h == null)
					{
						var count = PropertyManager.Instance.GetItem(hunter.itemID).num;
						if (count > 0)
							PropertyManager.Instance.Update(1, hunter.itemID, count, true);
					}
				}
			}

			static private void CheckActivityIsClose(Hunter hunter)
			{
				if (hunter != null)
				{
					hunter.Refresh();
					if (hunter.isClosed)
					{
						var type = hunter.subID;
						var h = _data.hunters.Find(h => h.id != hunter.id && h.subID == type);
						if (h == null)
						{
							var count = PropertyManager.Instance.GetItem(hunter.itemID).num;
							if (count > 0)
								PropertyManager.Instance.Update(1, hunter.itemID, count, true);
						}
					}
				}
			}

		}

	}

	[System.Serializable]
	public class HunterData
	{
		public List<Hunter> hunters = new List<Hunter>();
	}

	[System.Serializable]
	public class Hunter
	{

		public int id;
		public int index;
		public List<HunterMonster> monsters;

		[System.NonSerialized]
		public int subID;
		[System.NonSerialized]
		public GameConfigs.ActivityTimeRowData actCfg;
		[System.NonSerialized]
		public HunterMonster monster;
		[System.NonSerialized]
		public int power;
		[System.NonSerialized]
		public int itemID;
		[System.NonSerialized]
		public ItemRowData item;

		[System.NonSerialized]
		public bool isClosed;

		public void Refresh()
		{
			if (id > 0)
			{
				isClosed = !ActiveTimeSystem.Instance.IsActive(id, GameServerTime.Instance.serverTime);
				if (subID == 0)
				{
					if (actCfg.IsValid() || ConfigSystem.Instance.TryGet(id, out actCfg))
					{
						subID = actCfg.Value;
						if (monsters == null || monsters.Count == 0)
							monsters = ConfigSystem.Instance.Finds<MonsterHunterRowData>(c => c.Group == subID)
								.Select(c => new HunterMonster() { id = c.Id, cfg = c }).ToList();
						monsters?.ForEach(m => m.Refresh());
					}
				}
				if (index < monsters.Count)
					monster = monsters[index];
				if (itemID == 0)
				{
					var item = ConfigSystem.Instance.Find<ItemRowData>(c => c.Type == ((int)EnumItemType.Act) && c.TypeId == subID);
					if (item.IsValid())
					{
						this.item = item;
						itemID = item.ItemId;
					}
					else
					{
						GameDebug.LogError($"[hunter]{subID}=>没有配道具");
						itemID = -1;
					}
				}
			}
		}

		public bool Next()
		{
			index++;
			monster = index < monsters.Count ? monsters[index] : null;
			return monster != null;
		}


		public bool Attack(int val, out List<int> steps, bool reward = true)
		{
			steps = default;
			if (monster != null)
			{
				val = monster.progress = Math.Clamp(monster.progress + val, 0, monster.cfg.MonsterHP);
				var ratio = (val * 100f) / monster.cfg.MonsterHP;
				var step = monster.step;
				steps = new List<int>();

				if (step < 1 && monster.cfg.MidHP1 > 0 && ratio >= monster.cfg.MidHP1)
				{ step = 1; steps.Add(step); }

				if (step < 2 && monster.cfg.MidHP2 > 0 && ratio >= monster.cfg.MidHP2)
				{ step = 2; steps.Add(step); }

				if (step < 3 && val >= monster.cfg.MonsterHP)
				{ step = 3; steps.Add(step); }

				if (steps.Count > 0)
				{
					monster.step = step;
					if (reward)
					{
						var rw = new List<int[]>();
						steps.ForEach(s => rw.AddRange(DataCenter.HunterUtil.GetRewards(monster.cfg, s)));
						PropertyManager.Instance.Insert2Cache(rw);
					}
					if (monster.IsKilled())
					{
						Next();
						DataCenter.Instance.SavePlayerData();
					}
					return true;
				}
			}
			return false;
		}



	}

	[System.Serializable]
	public class HunterMonster
	{
		public int id;
		public int progress;
		public int step;

		public int max { get { return cfg.MonsterHP; } }

		[System.NonSerialized]
		public GameConfigs.MonsterHunterRowData cfg;

		public HunterMonster Refresh()
		{
			if (cfg.IsValid() || ConfigSystem.Instance.TryGet(id, out cfg))
			{

			}
			return this;
		}

		public bool IsKilled()
		{
			return progress >= cfg.MonsterHP;
		}



	}



}