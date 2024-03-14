using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGame
{
	partial class DataCenter
	{
		public EquipData equipData = new EquipData();


		public static class EquipUtil
		{
			private static EquipData _data { get { return Instance.equipData; } }

			static public void Init()
			{
				_data.items?.ForEach(e => e.Refresh());
			}


			static public void AddEquips(bool isnew, params int[] ids)
			{
				if (ids?.Length > 0)
				{

					foreach (var item in ids)
						AddEquip(item, isnew: isnew, triggerevent: false);
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_ADD));
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
				}
			}

			static public void AddEquips(bool isnew, params GameConfigs.EquipmentRowData[] eqs)
			{
				if (eqs?.Length > 0)
				{

					foreach (var item in eqs)
						AddEquip(item.Id, isnew: isnew, triggerevent: false, cfg: item);
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_ADD));
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
				}
			}

			static public void AddEquip(int eq, int count = 1, bool isnew = true, bool triggerevent = true, GameConfigs.EquipmentRowData cfg = default)
			{
				if (count > 0 && eq > 0 && (cfg.IsValid() || ConfigSystem.Instance.TryGet<GameConfigs.EquipmentRowData>(eq, out cfg)))
				{
					for (int i = 0; i < count; i++)
					{
						var e = new EquipItem() { cfgID = eq, cfg = cfg, level = cfg.Level, isnew = isnew ? (byte)1 : (byte)0 };
						_data.items.Add(e);
					}
					if (triggerevent)
					{
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_ADD));
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					}
				}
			}

			static public List<EquipItem> GetEquipDataByType(int type)
			{

				if (_data.items?.Count > 0)
					return _data.items.FindAll(e => e.type == type);
				return default;

			}

			static public List<EquipItem> GetEquipDataByID(int id)
			{

				if (_data.items?.Count > 0)
					return _data.items.FindAll(e => e.cfgID == id);
				return default;

			}

			static public List<EquipItem> GetEquipDataByCondition(Predicate<EquipItem> condition)
			{

				if (_data.items?.Count > 0 && condition != null)
					return _data.items.FindAll(condition);
				return default;

			}

		}

	}

	[Serializable]
	public class EquipData
	{
		public List<EquipItem> items = new List<EquipItem>();
	}

	[Serializable]
	public class EquipItem
	{
		public int cfgID;
		public int level;
		public byte isnew;

		public int type { get { return cfg.Type; } }
		public int quality { get { return cfg.Quality; } }

		private string _attrStr;

		[NonSerialized]
		public GameConfigs.EquipmentRowData cfg;

		public EquipItem Refresh()
		{
			ConfigSystem.Instance.TryGet<GameConfigs.EquipmentRowData>(cfgID, out cfg);
			return this;
		}

		public string GetAttrStr()
		{
			return _attrStr;
		}

	}

}
