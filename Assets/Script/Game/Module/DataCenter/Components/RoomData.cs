using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using GameTools;
using UnityEngine;

namespace SGame
{

	public partial class DataCenter
	{
		public RoomData roomData = new RoomData();


		public static class RoomUtil
		{

			public static Room NewRoom(int id, bool iscurrent = false)
			{
				var d = DataCenter.Instance.roomData;
				if (ConfigSystem.Instance.TryGet<RoomRowData>(id, out var cfg))
				{
					var room = new Room() { id = id };
					//初始金币
					PropertyManager
						.Instance
						.GetGroup(PropertyGroup.ITEM)
						.SetNum(1, AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.LevelGold));

					if (iscurrent)
					{
						d.roomID = id;
						d.rooms.Insert(0, room);
						room.isnew = true;
					}
					else
						d.rooms.Add(room);
					d.time = GameServerTime.Instance.serverTime;
					EventManager.Instance.Trigger(((int)GameEvent.ENTER_NEW_ROOM));
					return room;
				}
				return default;
			}

			public static Room GetRoom(int id, bool isnew = false)
			{
				var r = Instance.roomData.rooms.Find(x => x.id == id);
				return r ?? (isnew ? NewRoom(id, true) : null);
			}

			public static Room EnterRoom(int id, bool isnew = false)
			{

				var r = GetRoom(id, isnew);
				if (r != null)
				{
					var rd = Instance.roomData;
					var ud = Instance.GetUserData();
					rd.rooms.Remove(r);
					rd.rooms.Insert(0, r);
					ud.scene = id;
					r.roomAreas = ConfigSystem.Instance.Finds<RoomAreaRowData>(c => c.Scene == r.id).ToDictionary(c => c.ID);
					r.waitAreas = r.roomAreas.Keys.Where(v=>!r.areas.Contains(v)).ToList();
					r.worktables?.ForEach(w => w.Refresh());
					Instance.roomData.roomID = id;
					Instance.SetUserData(ud);
					DataCenter.Instance.SavePlayerData();
				}
				return r;
			}

			/// <summary>
			/// 初始化所有关卡科技buff
			/// </summary>
			public static void InitTechBuffs()
			{
				var room = Instance.roomData.current;

				if (room.roomTechs == null)
					room.roomTechs = ConfigSystem.Instance.Finds<RoomTechRowData>((c) => c.Room == room.id && !room.techs.Contains(c.Id)).ToDictionary(t => t.Id);

				if (room != null && room.techs?.Count > 0)
				{
					for (int i = 0; i < room.techs.Count; i++)
					{
						if (ConfigSystem.Instance.TryGet<RoomTechRowData>(room.techs[i], out var cfg))
							UseTechBuff(cfg);
					}
				}
			}

			/// <summary>
			/// 添加一个科技
			/// </summary>
			/// <param name="id"></param>
			public static void AddTech(int id)
			{
				if (ConfigSystem.Instance.TryGet<RoomTechRowData>(id, out var cfg))
				{
					var room = Instance.roomData.current;
					if (!room.techs.Contains(id))
					{
						room.techs.Add(id);
						room.roomTechs.Remove(id);
						//添加奖励
						EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_REWARD), id);
						EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.TECH_LEVEL, 1);
						if (!UseTechBuff(cfg))//buff触发
						{
							if (cfg.RoleId == ((int)EnumRole.Customer))//添加顾客相当于解锁桌子
							{

								EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_TABLE), cfg.TableId(0));
								var tid = 0;
								//添加角色
								if (cfg.TableIdLength > 1)
								{
									if (ConfigSystem.Instance.TryGet<RoomMachineRowData>(Math.Abs(cfg.TableId(1)), out var m))
									{
										tid = MapAgent.XYToCellIndex(m.ObjId(1), m.ObjId(2));
										EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_TABLE), cfg.TableId(1));
									}
								}
								EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_ROLE), cfg.RoleId, cfg.Value, tid);
							}
							else
							{
								AddRoleReward(cfg.RoleId, cfg.Value, cfg.TableId(0), cfg.TableId(1));
								EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.WORKER, 1);
							}
						}
					}
				}
			}

			public static void UnlockArea(int area)
			{
				var room = DataCenter.Instance.roomData.current;
				if (room != null)
				{
					room.waitAreas.Remove(area);
					room.areas.Add(area);
				}
			}

			private static bool UseTechBuff(RoomTechRowData cfg)
			{
				if (cfg.IsValid() && cfg.Type == 1)
				{
					EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData(cfg.BuffId, cfg.Value, cfg.MachineId)
					{
#if UNITY_EDITOR
						from = nameof(RoomData).GetHashCode() * 100 + cfg.Id
#endif
					});
					return true;
				}
				return false;
			}

			public static void AddRole(int roleid, int count, int x, int y, int tableid = 0)
			{
				if (roleid == ((int)EnumRole.Customer))//添加顾客相当于解锁桌子
					EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_ROLE), roleid, count, tableid);
				else
				{
					AddRoleReward(roleid, count, x, y);
					EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.WORKER, 1);
				}
			}

			public static void AddRoleReward(int roleid, int count, int x, int y)
			{
				if (count > 0)
				{
					var pos = Vector2Int.zero;
					var tag = roleid == (int)EnumRole.Cook ? ConstDefine.TAG_BORN_COOK : ConstDefine.TAG_BORN_WAITER;
					for (int i = 0; i < count; i++)
					{
						if (x != 0 && y != 0)
							pos = new Vector2Int(x, y);
						else 
							pos = GameTools.MapAgent.RandomPop(tag);

						var index = GameTools.MapAgent.GirdToRealIndex(pos);

						DataCenter.Instance.m_gameRecord.RecordRole(roleid, 1, index);
						EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_ROLE), roleid, 1, index);

						/*EventManager.Instance.Trigger(((int)GameEvent.SCENE_REWARD), pos, new Action(() =>
						{
						}), string.Empty);*/
					}
				}
			}

		}
	}

	[System.Serializable]
	public class RoomData
	{
		public int roomID;
		public int time;
		public List<Room> rooms = new List<Room>();
		public List<int> tables = new List<int>();

		public Room current
		{
			get
			{
				if (rooms?.Count == 0)
					DataCenter.RoomUtil.NewRoom(1, true);
				return rooms?.Count > 0 ? rooms[0] : default;
			}
		}
	}


	[System.Serializable]
	public class Room
	{
		public int id;
		public int worktableCount;
		public List<Worktable> worktables = new List<Worktable>();
		public List<int> areas = new List<int>();
		public List<int> techs = new List<int>();

		[NonSerialized]
		public List<int> waitAreas = new List<int>();
		[NonSerialized]
		public Dictionary<int, RoomTechRowData> roomTechs;
		[NonSerialized]
		public Dictionary<int, RoomAreaRowData> roomAreas;

		[NonSerialized]
		public bool isnew;

		public int GetAreaType(int area)
		{
			if(area > 1 && waitAreas.Count > 0)
			{
				if (waitAreas[0] == area) return 0;
				if (waitAreas.Count > 1 && waitAreas[1] == area) return 1;
			}
			return -1;
		}

	}


}