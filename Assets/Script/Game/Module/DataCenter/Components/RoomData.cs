using System.Collections;
using System.Collections.Generic;
using GameConfigs;

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
					/*PropertyManager
						.Instance
						.GetGroup(PropertyGroup.ITEM)
						.SetNum(1, AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.LevelGold));*/

					if (iscurrent)
					{
						d.roomID = id;
						d.rooms.Insert(0, room);
					}
					else
						d.rooms.Add(room);
					return room;
				}
				return default;
			}

			public static Room GetRoom(int id, bool isnew = false)
			{
				var r = Instance.roomData.rooms.Find(x => x.id == id);
				return r ?? (isnew ? NewRoom(id) : null);
			}

			public static Room EnterRoom(int id, bool isnew = false)
			{

				return GetRoom(id, isnew);

			}

			/// <summary>
			/// 初始化所有关卡科技buff
			/// </summary>
			public static void InitTechBuffs()
			{
				var room = Instance.roomData.current;
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
						if (!UseTechBuff(cfg))//buff触发
						{
							//添加奖励
							EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_REWARD), id);
							if (cfg.RoleId == ((int)EnumRole.Customer))//添加顾客相当于解锁桌子
								EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_TABLE), cfg.TableId(0));
							//添加角色
							EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_ROLE), cfg.RoleId, cfg.Value, cfg.TableId(0));
						}
					}
				}
			}

			private static bool UseTechBuff(RoomTechRowData cfg)
			{
				if (cfg.IsValid() && cfg.Type == 1)
				{
					EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData(cfg.BuffId, cfg.Value, cfg.MachineId) { from = typeof(RoomData).GetHashCode() * 100 + cfg.Id });
					return true;
				}
				return false;
			}

		}
	}

	[System.Serializable]
	public class RoomData
	{
		public int roomID;
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
		public List<Worktable> worktables = new List<Worktable>();
		public List<int> techs = new List<int>();
	}


}