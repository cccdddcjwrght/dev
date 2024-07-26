using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using Unity.Mathematics;

namespace SGame
{
	partial class DataCenter
	{
		public EquipData equipData = new EquipData();


		public static class EquipUtil
		{
			readonly static int EQ_FROM_ID = (int)EnumFrom.Equipment;
			static public int c_max_auto_merge_quality = 0;

			private static EquipData _data { get { return Instance.equipData; } }
			private static StringBuilder _sb = new StringBuilder();
			private static List<int> _unlockQualityDic;

			static public void Init()
			{

				_data.items?.ForEach(e => e.Refresh());
				_data.equipeds.Foreach(e => e?.Refresh());
				c_max_auto_merge_quality = GlobalDesginConfig.GetInt("equip_automerge_max", 0);
				StaticDefine.EQUIP_MAX_LEVEL = ConfigSystem.Instance.GetConfigCount(typeof(EquipUpLevelCost));
				CheckCanMerge();
			}

			static public void InitQuality()
			{
				if (_unlockQualityDic == null)
				{
					_unlockQualityDic = new List<int>();
					var ls = ConfigSystem.Instance.Finds<EquipQualityRowData>(q => q.BuffNum > 0);
					if (ls?.Count > 0)
					{
						var num = 0;
						foreach (var item in ls)
						{
							if (item.BuffNum > num)
							{
								_unlockQualityDic.Add(item.Id);
								num = item.BuffNum;
							}
						}
					}
				}
			}

			static public void InitEquipEffects()
			{
				OnRoleEquipChange();
			}

			static public string GetRoleEquipString() => GetRoleEquipString(0);

			static public string GetRoleEquipString(int roleType, IList<BaseEquip> eqs = null, bool needpet = true)
			{
				eqs = eqs ?? _data.equipeds;
				var rt = roleType;
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
					if (rt == 0)
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
				if (rt == 0 && needpet && Instance.petData.pet != null)
					_sb.AppendFormat("|pet|{0}", Instance.petData.pet.cfgID);

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

			static public List<int[]> GetEquipEffects(IList<BaseEquip> equips, bool needmain = false, bool valid = true, bool pack = false)
			{
				equips = equips ?? _data.equipeds;
				if (equips?.Count > 0)
				{
					var list = new List<int[]>();
					equips.Foreach(e => GetEquipEffects(e, list, needmain, valid));
					if (pack)
					{
						return list
							.GroupBy(v => v[0])
							.ToDictionary(v => v.Key, v => v.Sum(i => i[1]))
							.Select(v => new int[] { v.Key, v.Value })
							.ToList();
					}
					return list;
				}
				return default;
			}

			static public List<int[]> GetEquipEffects(BaseEquip equip, List<int[]> rets = null, bool needmain = true, bool valid = false)
			{
				if (equip != null && equip.cfg.IsValid())
				{
					rets = rets ?? new List<int[]>();
					var es = equip.GetEffects(valid);
					if (es?.Count > 0) rets.AddRange(es);
					if (needmain && equip.attrID > 0)
						rets.Add(new int[] { equip.attrID, equip.attrVal });
				}
				return rets;
			}

			static public List<int[]> ConvertId2Effects(int part, ulong id)
			{
				if (part > 0 && id > 0)
				{
					if (ConfigSystem.Instance.TryGet<EquipBuffRowData>(part, out var cfg))
					{
						var effects = Utils.GetArrayList(
							cfg.GetBuff1Array,
							cfg.GetBuff2Array,
							cfg.GetBuff3Array,
							cfg.GetBuff4Array,
							cfg.GetBuff5Array,
							cfg.GetBuff6Array
						);
						var list = new List<int[]>();
						for (int i = effects.Count - 1; i >= 0 && id > 0; i--)
						{
							var index = id;
							id = id / 100;
							index = index - id * 100 - 1;
							if (index >= 0)
								list.Insert(0, new int[] { effects[i][index * 2], effects[i][index * 2 + 1] });
						}
						return list;
					}
				}
				return default;
			}

			static public ulong RandomEffects(int part, out List<int[]> rets)
			{
				rets = new List<int[]>();
				return RandomEffects(part, rets);
			}

			static public ulong RandomEffects(int part, List<int[]> rets = null)
			{
				if (part > 0)
				{
					if (ConfigSystem.Instance.TryGet<EquipBuffRowData>(part, out var cfg))
					{
						ulong id = 0;
						var effects = Utils.GetArrayList(
							cfg.GetBuff1Array,
							cfg.GetBuff2Array,
							cfg.GetBuff3Array,
							cfg.GetBuff4Array,
							cfg.GetBuff5Array,
							cfg.GetBuff6Array
						);

						var ws = Utils.GetArrayList(
							cfg.GetWeight1Array,
							cfg.GetWeight2Array,
							cfg.GetWeight3Array,
							cfg.GetWeight4Array,
							cfg.GetWeight5Array,
							cfg.GetWeight6Array
						);

						for (int i = 0; i < effects.Count; i++)
						{
							var bs = effects[i];
							var w = ws[i];
							var index = SGame.Randoms.Random._R.NextWeight(w);
							if (bs.Length > index * 2)
							{
								id = (id * 100) + ((ulong)index + 1);
								if (rets != null)
									rets.Add(new int[] { bs[index * 2], bs[index * 2 + 1] });
							}
						}

						return id;

					}
				}
				return 0;
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
					CheckCanMerge();
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
							key = GID,
							cfgID = eq,
							cfg = cfg,
							level = Math.Max(cfg.Level, 1),
							isnew = isnew ? (byte)1 : (byte)0
						};
						e.Refresh();
						_data.items.Add(e);
					}
					EventManager.Instance.Trigger((int)GameEvent.EQUIP_NUM_UPDATE, eq, count);

					if (triggerevent)
					{
						CheckCanMerge();
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_ADD));
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					}
				}
			}

			static public void RemoveEquips(bool trigger = true, params EquipItem[] equips)
			{
				if (equips?.Length > 0)
				{
					foreach (var item in equips)
						RemoveEquip(item, false);
					if (trigger)
					{
						CheckCanMerge();
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
					}
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
					RemoveEquip(equip, false, true);

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
					CheckCanMerge();
					if (triggerevent)
					{
						OnRoleEquipChange();
						EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
						EventManager.Instance.Trigger(((int)GameEvent.ROLE_EQUIP_CHANGE));
					}
					EventManager.Instance.Trigger<int>((int)GameEvent.ROLE_EQUIP_PUTON, -equip.cfgID);
				}
			}

			static public void RemoveEquip(EquipItem equip, bool triggerevent = true, bool isputon = false)
			{
				if (equip != null)
				{

					if (equip.pos != 0) PutOff(equip, false);
					var index = _data.items.IndexOf(equip);
					if (index >= 0)
					{
						_data.items.RemoveAt(index);
						if (triggerevent)
							EventManager.Instance.Trigger(((int)GameEvent.EQUIP_REFRESH));
						if (!isputon)
							EventManager.Instance.Trigger((int)GameEvent.EQUIP_NUM_UPDATE, equip.cfgID, -1);
						if (isputon || triggerevent) CheckCanMerge();
					}
				}
			}

			static public double RecycleEquips(bool remove = false, bool triggerevent = true, params EquipItem[] equips)
			{
				if (equips?.Length > 0)
				{
					double count = 0;
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

			static public double RecycleEquip(EquipItem equip, bool remove = false, bool triggerevent = true)
			{
				if (equip != null)
				{
					if (_data.items.Contains(equip) || equip.pos != 0)
					{
						double count = equip.progress;

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
						case 8: count = cfg.Quality8Need; break;
						case 9: count = cfg.Quality9Need; break;
						case 10: count = cfg.Quality10Need; break;
						case 11: count = cfg.Quality11Need; break;
						case 12: count = cfg.Quality12Need; break;
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
					var list = GetEquipEffects(equips, true, true, true);
					if (list.Count > 0)
					{
						return list.Select(kv => new BuffData()
						{
							id = kv[0],
							val = kv[1],
							from = from == 0 ? EQ_FROM_ID : from,
						}).ToList();
					}
				}
				return default;
			}

			static public int GetEquipNum(int id)
			{
				int num = 0;
				num += _data.items.Count((e) => e?.cfgID == id);
				num += _data.equipeds.Count((e) => e?.cfgID == id);
				return num;
			}

			static public List<List<EquipItem>> GetCombineList(int qualitymask = 0, bool ischeck = false, bool ignoreselected = false, bool checkauto = false)
			{
				if (_data.items.Count > 1)
				{
					//_data.items.ForEach(i => i.upflag = 0);
					List<List<EquipItem>> rets = new List<List<EquipItem>>();
					var flag = false;
					for (int i = (int)EnumQuality.Max - 1; i > 0; i--)
					{
						if (qualitymask == 0 || (qualitymask < 0 && i <= -qualitymask) || (qualitymask > 0 && i.IsInState(qualitymask)))
						{
							var ret = GetCombineListByQuality(i, ref rets, ignoreselected);
							if (ischeck && rets.Count > 0) return rets;
							if (!ischeck && checkauto && ret && !flag && c_max_auto_merge_quality >= i)
								flag = true;
						}
					}
					if (flag) rets.Add(null);
					return rets;
				}
				return default;
			}

			static public bool FindEquip(List<EquipItem> equips, int count, ref List<EquipItem> rets,
				int start = 0, int quality = 0, int type = 0, int id = 0, EquipItem self = null, bool ignoreselected = false)
			{
				start = ignoreselected ? 0 : start;
				if (count > 0 && equips?.Count > start && equips.Count - start >= count)
				{
					rets = rets ?? new List<EquipItem>();
					rets.Clear();
					var s = type == 0 && id == 0 ? 0 : start;
					for (int i = s; i < equips.Count && count > 0; i++)
					{
						var eq = equips[i];

						if (self == eq || (!ignoreselected && eq.upflag != 0)) continue;
						if (quality != 0 && quality != eq.quality) continue;
						//由于已经按照部位和Group排序，所以如果一个不符合就直接结束
						if (type != 0 && type != eq.cfg.Type)
						{
							if (ignoreselected) continue;
							break;
						}
						if (id != 0 && id != eq.cfg.Group)
						{
							if (ignoreselected) continue;
							break;
						}
						rets.Add(eq);
						count--;
					}
					if (count <= 0)
					{
						foreach (var item in rets) item.upflag = 1;
						return true;
					}
				}
				return false;
			}

			static public bool GetCombineListByQuality(int quality, ref List<List<EquipItem>> list, bool ignoreselected = false, bool onlycheck = true)
			{
				bool FindConditon(BaseEquip equip)
				{
					return equip.quality == quality;
				}
				if (quality > 0 && quality < ((int)EnumQuality.Max))
				{
					var eqs = _data.items.FindAll(FindConditon);
					var ret = false;
					if (eqs?.Count > 0)
					{
						if (eqs.Count > 1) eqs.Sort(SortEqLevel);
						var allmatcount = PropertyManager.Instance.GetItem(ConstDefine.EQUIP_UPQUALITY_MAT).num;

						list = list ?? new List<List<EquipItem>>();
						for (int i = 0; i < eqs.Count; i++)
						{
							var eq = eqs[i]; var cid = 0; var ctype = 0; var cq = quality;
							var val = eq.qcfg.AdvanceValue;
							if (eq.upflag != 0) continue;
							var packs = new List<EquipItem>();
							if (eq.qcfg.AdvanceType == 3)
							{
								if (allmatcount >= val)
								{ eq.upflag = 1; allmatcount -= val; }
							}
							else
							{
								switch (eq.qcfg.AdvanceType)
								{
									case 2:
										ctype = eq.type;
										break;
									case 4:
										cid = eq.cfg.Group;
										break;
								}

								eq.upflag = 1;
								if (eqs.Count - 1 < val || !FindEquip(eqs, val, ref packs, i + 1, quality: quality, type: ctype, id: cid, self: eq, ignoreselected: ignoreselected))
								{
									eq.upflag = 0;
								}
							}
							if (eq.upflag != 0)
							{
								packs.Insert(0, eq);
								list.Add(packs);
								ret = true;
							}
						}
						return ret;
					}
				}
				return false;
			}

			static public void ConvertQuality(int quality, out int qType, out int qStep)
			{
				qType = quality;
				qStep = 0;
				var q = (EnumQuality)quality;
				var mq = EnumQuality.Red;

				if (q >= EnumQuality.Purple && q < mq)
				{
					if (q < EnumQuality.Orange)
					{ qType = ((int)EnumQualityType.Purple); q = EnumQuality.Purple; }
					else if (q < EnumQuality.Red)
					{ qType = ((int)EnumQualityType.Orange); q = EnumQuality.Orange; }
					qStep = quality - (int)q;
				}
				else if (q >= mq)
					qType -= 5;
			}

			static public int[] GetQualityUnlockBuff(EquipmentRowData equipment, int quality)
			{

				if (equipment.IsValid() && quality > 0)
				{
					InitQuality();
					var index = _unlockQualityDic.IndexOf(quality);
					if (index >= 0)
					{
						var buff = new int[2];
						Array.Copy(equipment.GetBuffArray(), index * 2, buff, 0, 2);
						return buff;
					}
				}
				return default;

			}

			static public int GetBuffUnlockQuality(int index)
			{
				InitQuality();
				if (index >= 0 && index < _unlockQualityDic.Count)
					return _unlockQualityDic[index];
				return 0;
			}

			static private void OnRoleEquipChange(bool remove = false)
			{
				EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = EQ_FROM_ID });
				if (!remove)
					GetEquipBuffList(_data.equipeds)?.ForEach(buff => EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), buff));
			}


			static private int SortEqLevel(BaseEquip a, BaseEquip b)
			{
				var ret = a.type.CompareTo(b.type);
				if (ret == 0)
				{
					ret = a.cfg.Group.CompareTo(b.cfg.Group);
					if (ret == 0)
						ret = a.level.CompareTo(b.level);
				}
				return ret;
			}

			static public void CheckCanMerge()
			{
				var rets = GetCombineList(ignoreselected: true , checkauto:true);
				_data.canMerge = rets?.Count > 0;
				if (_data.canMerge)
					_data.canAutoMerge = c_max_auto_merge_quality > 0 ? rets[rets.Count - 1] == null : true;
				else
					_data.canAutoMerge = false;
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
		[NonSerialized]
		public bool canMerge;
		[NonSerialized]
		public bool canAutoMerge;
	}

	public class BaseEquip
	{
		public int cfgID;
		public int level;
		public int quality;
		public ulong effectID;

		public byte isnew;

		[NonSerialized]
		public string icon;
		[NonSerialized]
		public double count;

		[NonSerialized]
		public EquipmentRowData cfg;
		[NonSerialized]
		public EquipUpLevelCostRowData lvcfg;
		[NonSerialized]
		public EquipUpLevelCostRowData nextlvcfg;
		[NonSerialized]
		public EquipQualityRowData qcfg;
		[NonSerialized]
		public List<int[]> mats;
		/// <summary>
		/// 品质类型
		/// </summary>
		[NonSerialized]
		public int qType;
		/// <summary>
		/// 品质阶段
		/// </summary>
		[NonSerialized]
		public int qStep;

		[NonSerialized]
		public int upflag;

		public int upLvCost { get; private set; }
		public int type { get { return cfg.IsValid() ? cfg.Type : _type; } }
		public string name { get { return cfg.IsValid() && _name == null ? cfg.Name : _name; } }

		public int attrID { get; private set; }
		public int attrVal { get { return GetAttrVal(); } }
		public int realType { get { return _type > 0 ? _type : type; } }


		protected int _type;
		protected string _name;


		private Dictionary<string, string> _partData;
		private int _baseAttrVal;
		private List<int[]> _effects;
		private List<int[]> _vEffects;

		public virtual BaseEquip UpQuality(int nextquality = -1)
		{
			if (quality < (int)EnumQuality.Max)
			{
				if (nextquality < 0) quality++;
				else quality = nextquality;
				level = 1;
				isnew = 1;
				Refresh();
			}
			return this;
		}

		public BaseEquip UpLevel()
		{
			if (qcfg.IsValid() && level < qcfg.LevelMax)
			{
				level++;
				Refresh();
			}
			return this;
		}

		public virtual BaseEquip Refresh()
		{
			if (this.cfgID == 0) return this;
			if (!this.cfg.IsValid()) ConfigSystem.Instance.TryGet(cfgID, out cfg);
			if (quality <= 0) quality = this.cfg.Quality;

			ConfigSystem.Instance.TryGet(level, out lvcfg);
			ConfigSystem.Instance.TryGet(level + 1, out nextlvcfg);
			ConfigSystem.Instance.TryGet(quality, out qcfg);

			upLvCost = DataCenter.EquipUtil.GetUplevelConst(level, quality, lvcfg);
			attrID = qcfg.IsValid() ? qcfg.MainBuff(0) : 0;
			_baseAttrVal = qcfg.IsValid() ? qcfg.MainBuff(1) : 0;
			if (type > 0)
			{
				if (type < 5)
				{
					DataCenter.EquipUtil.ConvertQuality(quality, out qType, out qStep);
					this.level = Math.Max(1, this.level);
					ConvertBuff();
				}
			}
			return this;
		}

		public int GetAttrVal(bool needlv = true, int lv = 0)
		{
			lv = (lv > 0 ? lv : level) - 1;
			return _baseAttrVal + (needlv && qcfg.IsValid() ? qcfg.MainBuffAdd * lv : 0);

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

		public List<int[]> GetEffects(bool valid = false)
		{
			ConvertBuff();
			if (valid)
				return _vEffects;
			return _effects;
		}

		public bool CheckMats()
		{
			if (mats?.Count > 0)
			{
				foreach (var item in mats)
				{
					if (!PropertyManager.Instance.CheckCountByArgs(item))
						return false;
				}
				return true;
			}
			return false;
		}

		public int RedState(bool checkup = false)
		{
			if (checkup && upflag > 0) return 2;
			return isnew;
		}

		public virtual BaseEquip Clone()
		{
			return new BaseEquip()
			{
				cfgID = cfgID,
				level = level,
				quality = quality,
				cfg = cfg,
				effectID = effectID,
				_effects = _effects
			};
		}

		private void ConvertBuff()
		{
			if (cfg.IsValid() && cfg.BuffLength > 1)
			{
				if (_effects?.Count > 0) return;
				_effects = new List<int[]>();
				for (int i = 0; i < cfg.BuffLength; i += 2)
					_effects.Add(new int[] { cfg.Buff(i), cfg.Buff(i + 1) });
			}
			else
				_effects = _effects ?? new List<int[]>();

			if (_vEffects == null || _vEffects.Count != qcfg.BuffNum)
			{
				if (_effects.Count < qcfg.BuffNum)
				{
					UnityEngine.Debug.LogError($"装备{cfgID}: buff数量不满足品质设置数量");
					return;
				}
				_vEffects = _effects.Take(qcfg.BuffNum).ToList();
			}
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


		public override BaseEquip Refresh()
		{
			if (realType > 100)
			{
				if (cfgID > 0)
				{
					count = PropertyManager.Instance.GetItem(cfgID).num;
					if (icon == null && ConfigSystem.Instance.TryGet<ItemRowData>(cfgID, out var c))
					{
						icon = c.Icon;
						_name = c.Name;
						_type = _type * 100 + c.Type;
						if (c.Type == 6 && c.TypeId > 0)
						{
							if (ConfigSystem.Instance.TryGet<EquipmentRowData>(c.TypeId, out cfg))
							{
								quality = cfg.Quality;
								mats = Utils.GetArrayList(
									cfg.GetPart1Array,
									cfg.GetPart2Array,
									cfg.GetPart3Array,
									cfg.GetPart4Array,
									cfg.GetPart5Array
								);
							}
						}
					}
				}
			}
			else base.Refresh();
			return this;
		}

		public EquipItem Convert(int id, double num, int type)
		{
			cfgID = id;
			_type = 1000 + type;
			Refresh();
			if (num != 0)
				count = num;
			return this;
		}

		public EquipItem Convert(ItemData.Value item)
		{
			Convert(item.id, item.num, (int)item.type);
			return this;
		}

	}

}
