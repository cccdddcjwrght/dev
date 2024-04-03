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
					var ud = Instance.GetUserData();
					ud.scene = id;
					Instance.roomData.roomID = id;
					Instance.SetUserData(ud);
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
								AddRoleReward(cfg.RoleId, cfg.Value, cfg.TableId(0), cfg.TableId(1));
						}
					}
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

			private static void AddRoleReward(int roleid, int count, int x, int y)
			{
				if (count > 0)
				{
					var pos = Vector2Int.zero;
					var tag = roleid == (int)EnumRole.Cook ? ConstDefine.TAG_BORN_COOK : ConstDefine.TAG_BORN_WAITER;
					for (int i = 0; i < count; i++)
					{
						Worker worker = default;
						if (x != 0 && y != 0)
							pos = new Vector2Int(x, y);
						else if (WorkQueueSystem.Instance.Random(tag, out worker))
							pos = worker.cell;
						DataCenter.Instance.m_gameRecord.RecordRole(roleid, 1, worker.index);
						EventManager.Instance.Trigger(((int)GameEvent.SCENE_REWARD), pos, new Action(() =>
						{
							EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_ROLE), roleid, 1, worker.index);
							WorkQueueSystem.Instance.Free(worker);
						}), string.Empty);
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
		public List<int> techs = new List<int>();

		[NonSerialized]
		public Dictionary<int, RoomTechRowData> roomTechs;

		[NonSerialized]
		public bool isnew;

	}


}