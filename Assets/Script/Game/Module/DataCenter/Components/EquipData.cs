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
			readonly static int EQ_FROM_ID = (int)EnumFrom.Equipment;

			private static EquipData _data { get { return Instance.equipData; } }
			private static StringBuilder _sb = new StringBuilder();

			static public void Init()
			{
				_data.items?.ForEach(e => e.Refresh());
				_data.equipeds.Foreach(e => e?.Refresh());
				StaticDefine.EQUIP_MAX_LEVEL = ConfigSystem.Instance.GetConfigCount(typeof(EquipUpLevelCost));
			}

			static public void InitEquipEffects()
			{
				OnRoleEquipChange();
			}

			static public string GetRoleEquipString() => GetRoleEquipString(0);

			static public string GetRoleEquipString(int roleType, IList<BaseEquip> eqs = null)
			{
				eqs = eqs ?? _data.equipeds;

				#region 当前场景角色基模数据
				var parts = _data.defaultEquipPart;
				if (_data.defaultEquipPart == null || roleType != 0)
				{
					parts = new Dictionary<string, string>();
					if (roleType == 0)
					{
						if (ConfigSystem.Instance.TryGet<GameConfigs.LevelRowData>(DataCenter.Instance.roomData.current.id, out var level))
							roleType = level.PlayerId;
					}

					if (ConfigSystem.Instance.TryGet<GameConfigs.RoleDataRowData>(roleType, out var role))
					{
						if (ConfigSystem.Instance.TryGet<GameConfigs.roleRowData>(role.Model, out var model))
						{
							var ss = model.Part.Split('|');
							if (ss.Length % 2 == 1)
							{
								for (int i = 1; i < ss.Length - 1; i += 2)
									parts[ss[i].ToLower()] = ss[i + 1];
							}
						}
					}
					if (roleType == 0)
						_data.defaultEquipPart = parts;

				}
				#endregion

				var d = new Dictionary<string, string>(parts);
				eqs.Foreach(e =>
				{
					if (e != null && e.cfgID > 0)
						e.ReplacePart(d);
				});
				_sb.Clear();
				_sb.Append("role");
				d.ToList().ForEach(kv => _sb.AppendFormat("|{0}|{1}", kv.Key, kv.Value));
				return _sb.ToString();
			}

			static public int GetRoleEquipAddValue(IList<BaseEquip> eqs = null)
			{
				var v = 0;
				eqs = eqs ?? _data.equipeds;
				for (int i = 0; i < eqs.Count; i++)
				{
					var e = eqs[i];
					if (e != null && e.cfgID > 0)
						v += e.attrVal;
				}
				return v;
			}

			static public List<int[]> GetEquipEffects(IList<BaseEquip> equips, bool needmain = false)
			{
				equips = equips ?? _data.equipeds;
				if (equips?.Count > 0)
				{
					var list = new List<int[]>();
					equips.Foreach(e => GetEquipEffects(e, list, needmain));
					return list;
				}
				return default;
			}


			static public List<int[]> GetEquipEffects(BaseEquip equip, List<int[]> rets = null, bool needmain = true)
			{
				if (equip != null && equip.cfg.IsValid())
				{
					rets = GetEquipEffects(equip.cfg, equip.quality, rets: rets);
					if (rets != null && needmain)
						rets.Add(new int[] { equip.attrID, equip.attrVal });
				}
				return rets;
			}


			static public List<int[]> GetEquipEffects(GameConfigs.EquipmentRowData equipment, int quality = 0, bool needMainBuff = false, List<int[]> rets = null)
			{
				if (equipment.IsValid())
				{
					quality = quality > 0 ? quality : equipment.Quality;

					var list = rets ?? new List<int[]>();
					if (quality > 1 && equipment.Buff1Length > 0)
						list.Add(equipment.GetBuff1Array());
					if (quality > 2 && equipment.Buff2Length > 0)
						list.Add(equipment.GetBuff2Array());
					if (quality > 3 && equipment.Buff3Length > 0)
						list.Add(equipment.GetBuff3Array());
					if (quality > 4 && equipment.Buff4Length > 0)
						list.Add(equipment.GetBuff4Array());
					if (quality > 5 && equipment.Buff5Length > 0)
						list.Add(equipment.GetBuff5Array());
					/*
					if (quality > 6 && equipment.Buff6Length > 0)
						list.Add(equipment.GetBuff6Array());*/

					if (needMainBuff)
					{
						if (ConfigSystem.Instance.TryGet<EquipQualityRowData>(quality, out var cfg))
							list.Add(cfg.GetMainBuffArray());
					}
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
						e.Refresh();
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
					EventManager.Instance.Trigger<int>((int)GameEvent.ROLE_EQUIP_PUTON, equip.cfgID);

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
					EventManager.Instance.Trigger<int>((int)GameEvent.ROLE_EQUIP_PUTON, -equip.cfgID);
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

			static public int RecycleEquips(bool remove = false, bool triggerevent = true, params EquipItem[] equips)
			{
				if (equips?.Length > 0)
				{
					var count = 0;
					equips.Foreach(e => count += RecycleEquip(e, remove, false));
					if (count > 0)
					{
						PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPLV_MAT, count);
						if (triggerevent)
							EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					}
					return count;
				}
				return 0;
			}

			static public int RecycleEquip(EquipItem equip, bool remove = false, bool triggerevent = true)
			{
				if (equip != null)
				{
					if (_data.items.Contains(equip))
					{
						var count = equip.progress;

						if (equip.level > 1)
						{
							for (int i = 1; i < equip.level; i++)
								count += GetUplevelConst(i, equip.quality);
						}
						if (triggerevent && count > 0)
							PropertyManager.Instance.Update(1, ConstDefine.EQUIP_UPLV_MAT, count);
						if (remove)
							RemoveEquip(equip, triggerevent);
						return count;
					}
				}
				return 0;
			}

			static public int GetUplevelConst(int level, int quality, EquipUpLevelCostRowData cfg = default)
			{
				var count = 0;
				if (cfg.IsValid() || ConfigSystem.Instance.TryGet<EquipUpLevelCostRowData>(level, out cfg))
				{
					switch (quality)
					{
						case 1: count = cfg.Quality1Need; break;
						case 2: count = cfg.Quality2Need; break;
						case 3: count = cfg.Quality3Need; break;
						case 4: count = cfg.Quality4Need; break;
						case 5: count = cfg.Quality5Need; break;
						case 6: count = cfg.Quality6Need; break;
						case 7: count = cfg.Quality7Need; break;
					}
				}
				return count;
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

			/// <summary>
			/// 获取装备buff列表
			/// </summary>
			/// <param name="equips"></param>
			/// <param name="from"></param>
			/// <returns></returns>
			static public List<BuffData> GetEquipBuffList(IList<BaseEquip> equips, int from = 0)
			{
				if (equips?.Count > 0)
				{
					var list = new List<int[]>();
					equips.Foreach(e => GetEquipEffects(e, list));

					if (list.Count > 0)
					{
						return list.GroupBy(v => v[0]).ToDictionary(v => v.Key, v => v.Sum(i => i[1])).Select(kv => new BuffData()
						{
							id = kv.Key,
							val = kv.Value,
							from = from == 0 ? EQ_FROM_ID : from,
						}).ToList();
					}
				}
				return default;
			}

			static private void OnRoleEquipChange(bool remove = false)
			{
				EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = EQ_FROM_ID });
				if (!remove)
					GetEquipBuffList(_data.equipeds)?.ForEach(buff => EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), buff));
			}

		}

	}

	[Serializable]
	public class RoleData
	{
		/// <summary>
		/// 实例ID
		/// </summary>
		public int entityID;
		/// <summary>
		/// 角色类型ID
		/// </summary>
		public int roleTypeID;
		/// <summary>
		/// 是否雇佣兵
		/// </summary>
		public bool isEmployee;
		/// <summary>
		/// 装备列表
		/// </summary>
		public List<BaseEquip> equips;
	}

	[Serializable]
	public class EquipData
	{
		public EquipItem[] equipeds = new EquipItem[16];
		public List<EquipItem> items = new List<EquipItem>();

		[NonSerialized]
		public Dictionary<string, string> defaultEquipPart;
	}

	public class BaseEquip
	{
		public int cfgID;
		public int level;
		public int quality;

		public byte isnew;


		public EquipmentRowData cfg { get; set; }
		public EquipUpLevelCostRowData lvcfg { get; set; }
		public EquipUpLevelCostRowData nextlvcfg { get; set; }
		public EquipQualityRowData qcfg { get; set; }

		public int upLvCost { get; private set; }
		public int type { get { return cfg.Type; } }
		public int attrID { get; private set; }
		public int attrVal { get { return GetAttrVal(); } }


		private Dictionary<string, string> _partData;
		private int _baseAttrVal;

		public BaseEquip Refresh()
		{
			if (this.cfgID == 0) return this;
			if (!this.cfg.IsValid() && ConfigSystem.Instance.TryGet<GameConfigs.EquipmentRowData>(cfgID, out var cfg))
				this.cfg = cfg;
			if (quality <= 0)
				quality = this.cfg.Quality;
			ConfigSystem.Instance.TryGet<EquipUpLevelCostRowData>(level, out var lv);
			ConfigSystem.Instance.TryGet<EquipUpLevelCostRowData>(level + 1, out var nlv);
			ConfigSystem.Instance.TryGet<EquipQualityRowData>(quality, out var qcfg);

			this.lvcfg = lv;
			this.nextlvcfg = nlv;
			this.qcfg = qcfg;
			upLvCost = DataCenter.EquipUtil.GetUplevelConst(level, quality, lvcfg);
			attrID = qcfg.IsValid() ? qcfg.MainBuff(0) : 0;
			_baseAttrVal = qcfg.IsValid() ? qcfg.MainBuff(1) : 0;
			return this;
		}

		public int GetAttrVal(bool needlv = true)
		{
			return _baseAttrVal + (needlv && qcfg.IsValid() ? qcfg.MainBuffAdd * (level - 1) : 0);

		}

		public int GetNextAttrVal()
		{
			return _baseAttrVal + (qcfg.IsValid() ? qcfg.MainBuffAdd * level : 0);
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

		public bool IsMaxLv()
		{
			return level >= qcfg.LevelMax;
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

		public virtual BaseEquip Clone()
		{
			return new BaseEquip() { cfgID = cfgID, level = level, quality = quality, cfg = cfg };
		}
	}

	[Serializable]
	public class EquipItem : BaseEquip
	{
		public int key;
		public int pos;
		/// <summary>
		/// 升级进度
		/// </summary>
		public int progress;

		[NonSerialized]
		public bool selected;

		public EquipItem()
		{
			key = (int)System.DateTime.Now.Ticks;
		}
	}

}
