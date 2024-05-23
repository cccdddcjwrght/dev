using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{

	partial class RequestExcuteSystem
	{
		static public void UnlockArea(int area)
		{
			if (area > 0)
			{
				var room = DataCenter.Instance.roomData.current;
				if (room != null && ConfigSystem.Instance.TryGet(area, out RoomAreaRowData cfg))
				{
					var state = PropertyManager.Instance.CheckCountByArgs(cfg.GetCostArray());
					if (state)
					{
						DataCenter.RoomUtil.UnlockArea(area);
						if (cfg.ChefNum > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Cook), cfg.ChefNum, 0, 0);
						if (cfg.WaiterNum > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Waiter), cfg.WaiterNum, 0, 0);
						if (cfg.CustomerNum > 0)
							DataCenter.RoomUtil.AddRole(((int)EnumRole.Customer), cfg.CustomerNum, 0, 0);

						if (!string.IsNullOrEmpty(cfg.CustomerBorn))
							StaticDefine.CUSTOMER_TAG_BORN.Add(cfg.CustomerBorn);

						_eMgr.Trigger(((int)GameEvent.WORK_AREA_UNLOCK), area);
					}
				}

			}
		}
	}

}