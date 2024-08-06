using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class RequestExcuteSystem
	{

		[InitCall]
		static void InitItem()
		{
			ChestItemUtil.Init();

			var group = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
			if (group != null)
				group.onValueUpdate += Group_onValueUpdate;
		}

		private static void Group_onValueUpdate(int arg1, double arg2, double arg3)
		{
			ChestItemUtil.ChestCheck(arg1, arg2, arg3);
		}



	}

	public partial class ChestItemUtil
	{
		const string c_best_chest = "chest.best";

		private static EventManager _eMgr = EventManager.Instance;

		private static double _chestcount;
		private static List<int> _chestIds;
		private static Queue<ItemData.Value> _chestQueues = new Queue<ItemData.Value>();

		private static List<int> _chestKeyIds;

		private static ChestRowData _chest;
		private static ItemRowData _item;

		static public void Init()
		{
			InitChestIDs();
			InitChestKeyIDs();

			ReputationModule.Instance.AddLikeRewardData();
			RefreshChestNums();
			OpenChestKey();

			if (_chestcount > 0)
			{
				var cid = DataCenter.GetIntValue(c_best_chest, 0);
				if (cid > 0)
				{
					ConfigSystem.Instance.TryGet(cid, out _item);
					ConfigSystem.Instance.TryGet(_item.TypeId, out _chest);
				}
			}
		}

		static public void InitChestIDs()
		{
			_chestIds = ConfigSystem
			   .Instance
			   .Finds<ItemRowData>(i => i.Type == ((int)EnumItemType.Chest))
			   .Select(i => i.ItemId)
			   .ToList();
		}

		static public void InitChestKeyIDs() 
		{
			_chestKeyIds = ConfigSystem
			   .Instance
			   .Finds<ItemRowData>(i => i.Type == ((int)EnumItemType.ChestKey))
			   .Select(i => i.ItemId)
			   .ToList();
		}

		static void OpenChestKey() 
		{
			_chestKeyIds?.ForEach((id)=> 
			{
				var num = PropertyManager.Instance.GetItem(id).num;
				if (num > 0) 
				{
					ConfigSystem.Instance.TryGet<ItemRowData>(id, out var cfg);
					var list = DataCenter.LikeUtil.GetItemDrop(cfg.TypeId, (int)num);
                    list.Foreach((i)=> PropertyManager.Instance.Update(1, i.id, i.num)) ;
					PropertyManager.Instance.Update(1, id, num, true);
				}
			});
		}

		static public double GetChestCount()
		{
			return _chestcount;
		}

		static public string GetIcon()
		{
			if (_chest.IsValid()) return _chest.Icon;
			return default;
		}

		static public void RefreshChestNums()
		{
			_chestcount = _chestIds
				.Select(i => PropertyManager.Instance.GetItem(i))
				.Sum(i => i.num);
		}

		static public bool CheckEqGiftBag()
		{
			return _chestcount > 0;
		}

		static public void ChestCheck(int id, double num, double old)
		{

			if (_chestIds.Contains(id))
			{
				if (ConfigSystem.Instance.TryGet<ItemRowData>(id, out var cfg))
				{
					var c = num - old;
					_chestcount = Math.Max(0, _chestcount + c);
					if (num > old)
					{
						ConfigSystem.Instance.TryGet<ChestRowData>(cfg.TypeId, out var chest);
						if (cfg.SubType == 0)
							DoExcuteEqGift(cfg, (int)num, chest);
						else
						{
							if (!_chest.IsValid() || _chest.ChestQuality < chest.ChestQuality)
							{
								_chest = chest;
								_item = cfg;
								DataCenter.SetIntValue(c_best_chest, cfg.ItemId);
							}
							_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
						}
					}
					else if (num == 0 && (cfg.SubType == 1 || !_item.IsValid() || cfg.ItemId == _item.ItemId))
					{
						_chest = default;
						_item = default;
						DataCenter.SetIntValue(c_best_chest, 0);
						_eMgr.Trigger(((int)GameEvent.GAME_MAIN_REFRESH));
					}
				}
			}

			if (_chestKeyIds.Contains(id))
			{
				if (ConfigSystem.Instance.TryGet<ItemRowData>(id, out var cfg))
				{
					var c = num - old;
					var dropId = cfg.TypeId;
					if (num > old) 
					{
						DoExcuteChestKey(dropId, (int)c);
						PropertyManager.Instance.Update(1, id, c, true);
					}
				}
			}

		}


		private static void DoExcuteEqGift(ItemRowData item, int count, ChestRowData chest)
		{
			if (count > 0 && chest.IsValid())
			{
				var val = _chestQueues.FirstOrDefault(v => v.id == item.ItemId);
				if (val.id == 0)
				{
					_chestQueues.Enqueue(new ItemData.Value() { id = item.ItemId, num = item.TypeId });
					if (!UIUtils.CheckUIIsOpen("eqgiftui"))
						UIUtils.OpenUI("eqgiftui", _chestQueues);
				}
			}
		}

		private static void DoExcuteChestKey(int dropId, int count) 
		{
			if (count > 0) 
			{
				var list = DataCenter.LikeUtil.GetItemDrop(dropId, count);
				list.Foreach((i) => PropertyManager.Instance.Update(i.type, i.id, i.num));
				if(!UIUtils.CheckUIIsOpen("frament"))
					UIUtils.OpenUI("frament", list);
			}
		}

	}


}
