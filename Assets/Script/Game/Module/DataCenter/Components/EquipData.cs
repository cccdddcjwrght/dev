using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	partial class DataCenter
	{
		public EquipData equipData = new EquipData();


		public static class EquipUtil
		{
			readonly static int EQ_FROM_ID = nameof(EquipUtil).GetHashCode();

			private static EquipData _data { get { return Instance.equipData; } }
			private static StringBuilder _sb = new StringBuilder();

			static public void Init()
			{
				_data.items?.ForEach(e => e.Refresh());
				_data.equipeds.Foreach(e => e?.Refresh());
			}

			static public void InitEquipEffects()
			{
				OnRoleEquipChange();
			}

			static public string GetRoleEquipString()
			{
				#region 当前场景角色基模数据
				if (_data.defaultEquipPart == null)
				{
					_data.defaultEquipPart = new Dictionary<string, string>();
					if (ConfigSystem.Instance.TryGet<GameConfigs.LevelRowData>(DataCenter.Instance.roomData.current.id, out var level))
					{
						if (ConfigSystem.Instance.TryGet<GameConfigs.RoleDataRowData>(level.PlayerId, out var role))
						{
							if (ConfigSystem.Instance.TryGet<GameConfigs.roleRowData>(role.Model, out var model))
							{
								var ss = model.Part.Split('|');
								if (ss.Length % 2 == 1)
								{
									for (int i = 1; i < ss.Length - 1; i += 2)
										_data.defaultEquipPart[ss[i].ToLower()] = ss[i + 1];
								}
							}
						}
					}
				}
				#endregion

				var d = new Dictionary<string, string>(_data.defaultEquipPart);
				_data.equipeds.Foreach(e =>
				{
					if (e != null && e.cfgID > 0)
						e.ReplacePart(d);
				});
				_sb.Clear();
				_sb.Append("role");
				d.ToList().ForEach(kv => _sb.AppendFormat("|{0}|{1}", kv.Key, kv.Value));
				return _sb.ToString();
			}

			static public int GetRoleEquipAddValue()
			{
				var v = 0;
				for (int i = 0; i < _data.equipeds.Length; i++)
				{
					var e = _data.equipeds[i];
					if (e != null && e.cfgID > 0)
						v += e.cfg.MainBuff(1);
				}
				return v;
			}

			static public List<int[]> GetEquipEffects(GameConfigs.EquipmentRowData equipment, bool needMainBuff = false, List<int[]> rets = null)
			{
				if (equipment.IsValid())
				{
					var list = rets ?? new List<int[]>();
					if (equipment.Buff1Length > 0)
						list.Add(equipment.GetBuff1Array());
					if (equipment.Buff2Length > 0)
						list.Add(equipment.GetBuff2Array());
					if (equipment.Buff3Length > 0)
						list.Add(equipment.GetBuff3Array());
					if (equipment.Buff4Length > 0)
						list.Add(equipment.GetBuff4Array());
					if (equipment.Buff5Length > 0)
						list.Add(equipment.GetBuff5Array());

					if (needMainBuff)
						list.Add(equipment.GetMainBuffArray());

					return list;
				}
				return rets;
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
						var e = new EquipItem()
						{
							cfgID = eq,
							cfg = cfg,
							level = cfg.Level,
							isnew = isnew ? (byte)1 : (byte)0
						};
						_data.items.Add(e);
					}
					if (triggerevent)
					{
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_ADD));
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					}
				}
			}

			static public void RemoveEquips(params EquipItem[] equips)
			{
				if (equips?.Length > 0)
				{
					foreach (var item in equips)
						RemoveEquip(item, false);
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
				}
			}

			static public void PutOn(EquipItem equip, int pos = 0)
			{
				if (equip != null)
				{
					pos = pos == 0 ? equip.cfg.Type : pos;
					if (pos > _data.equipeds.Length)
					{ log.Error("装备格子不对，无法装备"); return; }

					PutOff(_data.equipeds[pos], false);
					RemoveEquip(equip, false);

					equip.pos = pos;
					_data.equipeds[pos] = equip;

					OnRoleEquipChange();
					EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					EventManager.Instance.Trigger(((int)GameEvent.ROLE_EQUIP_CHANGE));
				}
			}

			static public void PutOff(EquipItem equip, bool triggerevent = true)
			{
				if (equip != null && equip.cfgID > 0 && equip.pos > 0)
				{
					_data.equipeds[equip.pos] = default;
					_data.items.Add(equip);
					equip.pos = 0;
					if (triggerevent)
					{
						OnRoleEquipChange();
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
						EventManager.Instance.Trigger(((int)GameEvent.ROLE_EQUIP_CHANGE));
					}
				}
			}

			static public void RemoveEquip(EquipItem equip, bool triggerevent = true)
			{

				var index = _data.items.IndexOf(equip);
				if (index >= 0)
				{
					_data.items.RemoveAt(index);
					if (triggerevent)
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
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

			static public void CancelAllNewFlag()
			{
				_data.items?.ForEach(e => e.isnew = 0);
			}

			static private void OnRoleEquipChange(bool remove = false)
			{
				EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = EQ_FROM_ID });
				if (!remove)
				{
					var list = new List<int[]>();
					_data.equipeds.Foreach(e => GetEquipEffects(e != null ? e.cfg : default, true, list));

					if (list.Count > 0)
					{
						var buffs = list.GroupBy(v => v[0]).ToDictionary(v => v.Key, v => v.Sum(i => i[1])).Select(kv => new BuffData()
						{
							id = kv.Key,
							val = kv.Value,
							from = EQ_FROM_ID,
						}).All(buff =>
						{
							EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), buff);
							return true;
						});
					}
				}
			}

		}

	}

	[Serializable]
	public class EquipData
	{
		public EquipItem[] equipeds = new EquipItem[16];
		public List<EquipItem> items = new List<EquipItem>();

		[NonSerialized]
		public Dictionary<string, string> defaultEquipPart;

	}

	[Serializable]
	public class EquipItem
	{
		public int key;
		public int cfgID;
		public int level;
		public int pos;
		public byte isnew;

		public int type { get { return cfg.Type; } }
		public int quality { get { return cfg.Quality; } }

		private string _attrStr;

		private Dictionary<string, string> _partData;

		[NonSerialized]
		public GameConfigs.EquipmentRowData cfg;


		public EquipItem()
		{
			key = (int)System.DateTime.Now.Ticks;
		}

		public EquipItem Refresh()
		{
			ConfigSystem.Instance.TryGet<GameConfigs.EquipmentRowData>(cfgID, out cfg);
			return this;
		}

		public Dictionary<string, string> GetPartData()
		{
			if (_partData == null && cfg.IsValid() && !string.IsNullOrEmpty(cfg.Resource))
			{
				_partData = new Dictionary<string, string>();
				var ss = cfg.Resource.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				if (ss.Length > 1)
				{
					for (int i = 0; i < ss.Length - 1; i += 2)
					{
						try
						{
							var k = ss[i].ToLower();
							//if (k.StartsWith("weapon")) continue;
							_partData[k] = ss[i + 1];
						}
						catch (Exception)
						{
							GameDebug.LogError($"装备 {cfg.Id} 配置 {cfg.Resource} error!!!");
						}
					}
				}
			}
			return _partData;
		}

		public void ReplacePart(in Dictionary<string, string> rets)
		{
			if (rets != null)
			{
				var parts = GetPartData();
				if (parts?.Count > 0)
				{
					foreach (var item in parts)
						rets[item.Key] = item.Value;
				}
			}
		}

		public string GetAttrStr()
		{
			return _attrStr;
		}

	}

}
