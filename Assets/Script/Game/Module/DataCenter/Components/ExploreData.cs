using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using UnityEngine;

namespace SGame
{
	public partial class DataCenter
	{
		public ExploreData exploreData = new ExploreData();


		public static class ExploreUtil
		{
			#region const

			public static float c_exploretool_time_cost { get; private set; }
			
			public readonly static IReadOnlyList<EnumAttribute> c_fight_attrs = new List<EnumAttribute>()
			{
				EnumAttribute.Hp,
				EnumAttribute.Attack,
				//EnumAttribute.AtkSpeed,

				EnumAttribute.Dodge,
				EnumAttribute.Combo,
				EnumAttribute.Crit,
				EnumAttribute.Stun,
				EnumAttribute.Steal,

				EnumAttribute.AntiDodge,
				EnumAttribute.AntiCombo,
				EnumAttribute.AntiCrit,
				EnumAttribute.AntiStun,
				EnumAttribute.AntiSteal,
			};

			public readonly static IReadOnlyList<EnumAttribute> c_fight_attrs_1 = new List<EnumAttribute>()
			{
				EnumAttribute.Dodge,
				EnumAttribute.Combo,
				EnumAttribute.Crit,
				EnumAttribute.Stun,
				EnumAttribute.Steal,
			};

			public readonly static IReadOnlyList<EnumAttribute> c_fight_attrs_2 = new List<EnumAttribute>()
			{
				EnumAttribute.AntiDodge,
				EnumAttribute.AntiCombo,
				EnumAttribute.AntiCrit,
				EnumAttribute.AntiStun,
				EnumAttribute.AntiSteal,
			};
			#endregion

			public static float c_strong_power { get; private set; }
			public static Dictionary<int, float> c_attr_power_ratios { get; private set; } = new Dictionary<int, float>();
			public static IReadOnlyList<int[]> c_cache_attr { get; private set; }


			private static ExploreData data { get { return Instance.exploreData; } }

			public static void Init()
			{
				var ls = new List<int[]>();
				foreach (var i in c_fight_attrs)
				{
					c_attr_power_ratios[(int)i] = GlobalDesginConfig.GetFloat(i.ToString().ToLower() + "_ratio", 0f);
					ls.Add(new int[] { ((int)i), 0 });
				}
				c_cache_attr = ls;
				c_strong_power = GlobalDesginConfig.GetFloat("battle_equip_fortify", 2f);
				c_exploretool_time_cost = GlobalDesginConfig.GetInt("battle_explore_gem", 100); 

				data.Refresh();
			}

			static public EqAttrInfo GetBattleInfo(EnumAttribute attribute, FightEquip cfg, Func<int, int> getVal, double power = 1)
			{

				if (getVal == null)
					return new EqAttrInfo(((int)attribute), 0);

				var min = getVal(0);
				var max = getVal(1);
#if !SVR_RELEASE || CHECK
				if (min > max)
					GameDebug.LogError($"等级{cfg.cfg.Id}-战斗装备的属性{getVal.GetType().Name}的范围异常");
#endif
				var rate = 1;
				var id = ((int)attribute);
				switch (attribute)
				{
					case EnumAttribute.Hp: rate = cfg.qcfg.HpRatio; break;
					case EnumAttribute.Attack: rate = cfg.qcfg.AtkRatio; break;
					case EnumAttribute.AtkSpeed: rate = 1; break;
					default:
						if (id <= 10200) rate = cfg.qcfg.AttribRatio;
						else rate = cfg.qcfg.AnitAttribRatio;
						break;

				}
				var v = ConstDefine.C_PER_SCALE * SGame.Randoms.Random._R.Next(min * rate * (int)power, max * rate * (int)power)  ;
				return new EqAttrInfo(((int)attribute), (int)v.ToInt());
			}

			static public Func<int, int> GetAttrFunc(EnumAttribute attribute, EquipBattleLevelRowData cfg)
			{
				if (cfg.IsValid())
				{
					switch (attribute)
					{
						case EnumAttribute.Hp: return cfg.HpRange;
						case EnumAttribute.Attack: return cfg.AtkRange;
						case EnumAttribute.AtkSpeed: return null;

						case EnumAttribute.Dodge: return cfg.Dodge;
						case EnumAttribute.Combo: return cfg.Combo;
						case EnumAttribute.Crit: return cfg.Crit;
						case EnumAttribute.Stun: return cfg.Stun;
						case EnumAttribute.Steal: return cfg.Steal;

						case EnumAttribute.AntiDodge: return cfg.AntiDodge;
						case EnumAttribute.AntiCombo: return cfg.AntiCombo;
						case EnumAttribute.AntiCrit: return cfg.AntiCrit;
						case EnumAttribute.AntiStun: return cfg.AntiStun;
						case EnumAttribute.AntiSteal: return cfg.AntiSteal;

					}
				}
				return default;
			}

			static public float GetPowerRatio(int attribute)
			{
				c_attr_power_ratios.TryGetValue(attribute, out float ratio);
				return ratio == 0 ? 1 : ratio;
			}

			static public double CaluPower(params EqAttrInfo[] attrs)
			{
				if (attrs != null && attrs.Length > 0)
				{
					var p = 0d;
					for (int i = 0; i < attrs.Length; i++)
						p += GetPowerRatio(attrs[i].id) * attrs[i].val;
					return p.ToInt();
				}
				return 0;
			}

			static public double CaluPower(params int[][] attrs)
			{

				if (attrs != null && attrs.Length > 0)
				{
					var p = 0d;
					for (int i = 0; i < attrs.Length; i++)
						p += GetPowerRatio(attrs[i][0]) * attrs[i][1];
					return p.ToInt();
				}
				return 0;

			}


			static public List<int[]> GetAttrList(bool all, params FightEquip[] eqs)
			{
				if (eqs?.Length > 0)
				{
					var v = eqs
						.Where(e => e?.attrs?.Count > 0)
						.SelectMany(e => e.attrs)
						.Select(e => e.ToArray());

					if (all)
						v = c_cache_attr.Concat(v);

					return v.GroupBy(v => v[0])
						.ToDictionary(v => v.Key, v => v.Sum(i => i[1]))
						.Select(v => new int[] { v.Key, v.Value })
						.ToList();
				}
				return default;
			}

			static public List<EqAttrInfo> GetRoleAttr(int battleRoleID)
			{
				if (battleRoleID > 0)
				{
					if (ConfigSystem.Instance.TryGet<BattleRoleRowData>(battleRoleID, out var cfg))
					{
						return new List<EqAttrInfo>() {
							new EqAttrInfo(((int)EnumAttribute.Hp), cfg.Hp),
							new EqAttrInfo(((int)EnumAttribute.Attack), cfg.Atk),
							//new EqAttrInfo(((int)EnumAttribute.AtkSpeed), cfg.Speed),

							new EqAttrInfo(((int)EnumAttribute.Dodge), cfg.Dodge),
							new EqAttrInfo(((int)EnumAttribute.AntiDodge), cfg.AntiDodge),
							new EqAttrInfo(((int)EnumAttribute.Combo), cfg.Combo),
							new EqAttrInfo(((int)EnumAttribute.AntiCombo), cfg.AntiCombo),
							new EqAttrInfo(((int)EnumAttribute.Crit), cfg.Crit),
							new EqAttrInfo(((int)EnumAttribute.AntiCrit), cfg.AntiCrit),
							new EqAttrInfo(((int)EnumAttribute.Stun), cfg.Stun),
							new EqAttrInfo(((int)EnumAttribute.AntiStun), cfg.AntiStun),
							new EqAttrInfo(((int)EnumAttribute.Steal), cfg.Steal),
							new EqAttrInfo(((int)EnumAttribute.AntiSteal), cfg.AntiSteal),
						};
					}
				}
				return default;
			}

			static public FightEquip RandomFightEquip(ExploreToolLevelRowData cfg, int count = 1)
			{
				if (cfg.IsValid() && count > 0)
				{
					var pw = cfg.GetTypeWeightArray();
					var qw = cfg.GetQualityWeightArray();

					var eqs = EquipUtil.RandomEquip(pw, count, 11);
					if (eqs.Count > 0)
					{
						var rnd = SGame.Randoms.Random._R;
						var e = eqs[0];
						var q = EquipUtil.Type2Quality(((EnumQualityType)rnd.NextWeight(qw) + 1));
						return new FightEquip()
						{
							level = Math.Max(1, rnd.Next(data.level - 4, data.level + 1)),//等级
							cfg = e,
							cfgID = e.Id,
							quality = (int)q,//品质
							strong = rnd.Rate(data.exploreToolLevel.FortifyChance) ? 1 : 0,//强化
						}.Refresh() as FightEquip;
					}
				}
				return default;
			}

			static public int GetCurrentMaxExploreLv()
			{
				var r = Instance.roomData.current.roomCfg;
				var a = Instance.roomData.current.GetNewAreaCfg();
				if (a.IsValid()) return a.ExploreLevelMax;
				else return r.ExploreLevelMax;
			}
		}

	}

	[Serializable]
	public class ExploreData
	{
		public ExploreRoleData explorer = new ExploreRoleData();

		public int level;//探索等级
		public int exp;//探索经验

		public int toolLevel;//工具等级
		public int uplvtime;//升级结束时间

		public FightEquip cacheEquip;

		[NonSerialized]
		public ExploreLevelRowData exploreLevel;
		[NonSerialized]
		public ExploreLevelRowData exploreNextLevel;
		[NonSerialized]
		public ExploreToolLevelRowData exploreToolLevel;
		[NonSerialized]
		public ExploreToolLevelRowData exploreToolNextLevel;

		[NonSerialized]
		public int addExp;
		[NonSerialized]
		public bool showgoldfly = true;
		[NonSerialized]
		public float price;
		[NonSerialized]
		public bool waitFlag;

		public int exploreMaxLv { get; private set; }
		public int toolMaxLv { get; private set; }
		public bool lvPause { get; private set; }

		public void Refresh()
		{
			explorer.Refresh();
			cacheEquip?.Refresh();
			RefreshCfg();
			exploreMaxLv = ConfigSystem.Instance.LoadConfig<ExploreLevel>().DatalistLength;
			toolMaxLv = ConfigSystem.Instance.LoadConfig<ExploreToolLevel>().DatalistLength;
		}

		public void RefreshCfg()
		{
			level = Math.Clamp(level, 1, 999);
			toolLevel = Math.Clamp(toolLevel, 1, 999);
			ConfigSystem.Instance.TryGet(level, out exploreLevel);
			ConfigSystem.Instance.TryGet(level + 1, out exploreNextLevel);
			ConfigSystem.Instance.TryGet(toolLevel, out exploreToolLevel);
			ConfigSystem.Instance.TryGet(toolLevel + 1, out exploreToolNextLevel);

			price = DataCenter.ExploreUtil.c_exploretool_time_cost;


		}

		public bool IsExploreMaxLv()
		{
			return level >= exploreMaxLv;
		}

		public bool IsExploreToolMaxLv()
		{
			return toolLevel >= toolMaxLv;
		}

		public int ToolUpRemaining()
		{
			if (uplvtime > 0)
				return uplvtime - GameServerTime.Instance.serverTime;
			return 0;
		}

		public void ToolUpLv(int add = 1)
		{
			if (IsExploreToolMaxLv()) return;
			uplvtime = 0;
			toolLevel += add;
			RefreshCfg();
		}

		public bool AddExp(int exp)
		{
			if (IsExploreMaxLv()) return false;
			this.exp += exp;
			addExp = exp;
			var f = false;
			while (this.exp >= this.exploreLevel.Exp && CheckCanUpLv())
			{
				this.exp -= this.exploreLevel.Exp;
				level++;
				RefreshCfg();
				f = true;
			}
			return f;
		}

		public bool CheckCanUpLv()
		{
			lvPause = false;
			if (IsExploreMaxLv()) return false;
			if (exploreLevel.AreaLength > 0)
			{
				var s = exploreLevel.Area(0);
				var a = exploreLevel.Area(1);
				if (DataCenter.Instance.roomData.roomID > s) return true;
				if (!DataCenter.RoomUtil.IsAreaEnable(a))
				{
					lvPause = true;
					return false;
				}
			}
			return true;
		}

	}

	[Serializable]
	public class ExploreRoleData
	{

		public int roleID;
		public FightEquip[] equips = new FightEquip[8];

		[NonSerialized]
		public BattleRoleRowData cfg;

		private double power;
		private List<int[]> _attrs;

		public ExploreRoleData Refresh()
		{
			if (roleID == 0)
				roleID = GlobalDesginConfig.GetInt("battle_role_id", 1);

			if (!cfg.IsValid() || cfg.Id != roleID)
			{
				if (ConfigSystem.Instance.TryGet(roleID, out cfg))
					equips[0] = new FightEquip() { attrs = DataCenter.ExploreUtil.GetRoleAttr(roleID) };
				else GameDebug.LogError($"战斗角色数据配置错误{roleID}");
			}
			equips.Foreach(e => e?.Refresh());
			return this;
		}

		public FightEquip GetEquip(int pos)
		{
			var eq = equips.Length > pos && pos > 0 ? equips[pos] : default;
			return eq != null && eq.cfgID > 0 ? eq : default;
		}

		public bool Puton(FightEquip equip)
		{
			if (equip != null && equip.cfgID > 0 && equip.type > 10)
			{
				var pos = equip.type - 10;
				var old = equips[pos];
				if (old != null && old == equip) return false;
				equips[pos] = equip;
				equip.isnew = 0;
				if (old != null) old.Clear();
				GetAllAttr(true);
				return true;
			}
			return false;
		}

		public List<int[]> GetAllAttr(bool calu = false)
		{
			if (calu || _attrs == null)
				_attrs = DataCenter.ExploreUtil.GetAttrList(false, equips);
			return _attrs;
		}

		public int GetAttr(int id)
		{
			if (id >= 0)
			{
				var attr = GetAllAttr();
				for (int i = 0; i < attr.Count; i++)
					if (attr[i][0] == id) return attr[i][1];
			}
			return 0;
		}

		public double GetPower(bool recalu = false)
		{
			if (recalu || power == 0)
			{
				power = equips.Where(e => e != null && e.power != 0).Sum(e => e.power);
			}
			return power;
		}

		public string GetRoleModelString()
		{
			if (cfg.IsValid())
				return DataCenter.EquipUtil.GetRoleEquipString(-1, equips, false, cfg.Model);
			return default;
		}

	}

	[Serializable]
	public class FightEquip : EquipItem
	{
		public List<EqAttrInfo> attrs = new List<EqAttrInfo>();
		public int strong;

		[NonSerialized]
		public EquipBattleLevelRowData battleLvCfg;
		[NonSerialized]
		private Dictionary<int, EqAttrInfo> attrDic;

		public double power { get; private set; }


		public override BaseEquip Refresh()
		{
			base.Refresh();
			if (type > 10)
			{
				this.level = Math.Max(1, this.level);
				if (!battleLvCfg.IsValid() || level != battleLvCfg.Id)
				{
					if (ConfigSystem.Instance.TryGet(level, out battleLvCfg))
					{
						ConvertBuff();
					}
				}
			}
			else if (power == 0 && attrs?.Count > 0)
				power = DataCenter.ExploreUtil.CaluPower(attrs.ToArray());

			return this;
		}

		public override int GetAttrVal(bool needlv = true, int lv = 0, int id = 0)
		{
			id = id == 0 ? ((int)EnumAttribute.Hp) : id;
			if (attrDic == null)
				ConvertBuff();
			if (attrDic.TryGetValue(id, out var v))
				return v.val;
			return 0;
		}

		protected override void ConvertBuff()
		{
			if (battleLvCfg.IsValid())
			{
				if (attrs == null || attrs.Count == 0)
				{
					var power = strong == 1 ? DataCenter.ExploreUtil.c_strong_power : 1;
					var addnum = qcfg.AttribNum(0);
					var anitnum = qcfg.AttribNumLength > 1 ? qcfg.AttribNum(1) : 0;
					var rnd = SGame.Randoms.Random._R;
					attrs.Add(GetBattleInfo(EnumAttribute.Hp, this, power));
					attrs.Add(GetBattleInfo(EnumAttribute.Attack, this, power));
					//attrs.Add(GetBattleInfo(EnumAttribute.AtkSpeed, this, power));

					var ls = new List<EnumAttribute>();
					if (addnum > 0) rnd.NextItem(DataCenter.ExploreUtil.c_fight_attrs_1.ToList(), addnum, ref ls, true);
					if (anitnum > 0) rnd.NextItem(DataCenter.ExploreUtil.c_fight_attrs_2.ToList(), anitnum, ref ls, true);

					foreach (var id in ls)
						attrs.Add(GetBattleInfo((EnumAttribute)id, this, power));
					attrs.Sort((a, b) => a.id.CompareTo(b.id));
				}
				if (attrDic == null)
				{
					attrDic = attrs.ToDictionary(v => v.id);
					power = DataCenter.ExploreUtil.CaluPower(attrs.ToArray());
				}

				if (_effects == null)
					_effects = attrs.Select(v => v.ToArray()).ToList();

			}
			attrDic = attrDic ?? new Dictionary<int, EqAttrInfo>();
			_effects = _effects ?? new List<int[]>();
		}

		public override List<int[]> GetEffects(bool valid = false)
		{
			ConvertBuff();
			return _effects;
		}

		public override void Clear()
		{
			base.Clear();
			battleLvCfg = default;
			attrs?.Clear();
			attrs = default;
			attrDic?.Clear();
			attrDic = default;
		}

		static private EqAttrInfo GetBattleInfo(EnumAttribute attribute, FightEquip cfg, double power = 1)
		{
			return DataCenter.ExploreUtil.GetBattleInfo(
						attribute, cfg, DataCenter.ExploreUtil.GetAttrFunc(attribute, cfg.battleLvCfg),
						power
					);
		}

	}

	[Serializable]
	public class EqAttrInfo
	{
		public int id;
		public int val;

		public EqAttrInfo() { }

		public EqAttrInfo(int id, int val)
		{

			this.id = id;
			this.val = val;
		}

		public int[] ToArray()
		{
			return new int[] { id, val };
		}

	}

}

