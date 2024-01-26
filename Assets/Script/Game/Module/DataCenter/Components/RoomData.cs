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
			public static Room GetRoom(int id)
			{
				return Instance.roomData.rooms.Find(x => x.id == id);
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
						if (cfg.Type == 1)//buff触发
							EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData(cfg.BuffId, cfg.Value, cfg.MachineId));
						else if (cfg.Type == 2)//添加奖励
							EventManager.Instance.Trigger(((int)GameEvent.TECH_ADD_REWARD), id);
					}
				}
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
					rooms = new List<Room> { new Room() { id = 1 } };
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