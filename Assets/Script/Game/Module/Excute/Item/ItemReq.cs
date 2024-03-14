using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class RequestExcuteSystem
	{
		private static Queue<ItemData.Value> _chestQueues = new Queue<ItemData.Value>();

		[InitCall]
		static void InitItem()
		{

			var group = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			if (group != null)
				group.onValueUpdate += Group_onValueUpdate;
		}

		private static void Group_onValueUpdate(int arg1, double arg2, double arg3)
		{
			if (arg2 > arg3)
			{
				if (ConfigSystem.Instance.TryGet<ItemRowData>(arg1, out var cfg))
				{
					switch (cfg.Type)
					{
						case 3:
							DoExcuteEqGift(cfg, (int)arg2);
							break;
					}
				}
			}
		}

		private static void DoExcuteEqGift(ItemRowData item, int count)
		{
			if (count > 0 && ConfigSystem.Instance.TryGet<ChestRowData>(item.TypeId, out var chest))
			{
				_chestQueues.Enqueue(new ItemData.Value() { id = item.ItemId, num = item.TypeId });
				if (!UIUtils.CheckUIIsOpen("eqgiftui"))
					UIUtils.OpenUI("eqgiftui", _chestQueues);
			}
		}

	}
}
