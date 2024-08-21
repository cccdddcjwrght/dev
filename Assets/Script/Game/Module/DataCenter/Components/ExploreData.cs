using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;

namespace SGame
{
	public partial class DataCenter
	{
		public ExploreData exploreData = new ExploreData();


		public static class ExploreUtil
		{
			public static float c_strong_power { get; private set; }
			public static Dictionary<int, float> c_attr_power_ratios { get; private set; } = new Dictionary<int, float>();

			private static ExploreData data { get { return Instance.exploreData; } }

			public static void Init()
			{
				for (var i = EnumAttribute.Hp; i <= EnumAttribute.AntiSteal; i++)
					c_attr_power_ratios[(int)i] = GlobalDesginConfig.GetFloat(i.ToString().ToLower() + "_ratio", 2f);
				c_strong_power = GlobalDesginConfig.GetFloat("battle_equip_fortify", 2f);
			}

			static public EqAttrInfo GetBattleInfo(EnumAttribute attribute, FightEquip cfg, Func<int, int> getVal, double power = 1)
			{
				var min = getVal(0);
				var max = getVal(1);
#if !SVR_RELEASE || CHECK
				if (min > max)
					GameDebug.LogError($"等级{cfg.cfg.Id}-战斗装备的属性{getVal.GetType().Name}的范围异常");
#endif
				var rate = 1f;
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
				var v = SGame.Randoms.Random._R.Next(min, max) * power * rate;
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
				return ratio;
			}

			static public double CaluPower(params EqAttrInfo[] attrs)
			{
				if (attrs != null && attrs.Length > 0)
				{
					var p = 0d;
					for (int i = 0; i < attrs.Length; i++)
						p += GetPowerRatio(attrs[i].id);
					return p.ToInt();
				}
				return 0;
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

		[NonSerialized]
		public ExploreLevelRowData exploreLevel;
		[NonSerialized]
		public ExploreLevelRowData exploreNextLevel;
		[NonSerialized]
		public ExploreToolLevelRowData exploreToolLevel;
		[NonSerialized]
		public ExploreToolLevelRowData exploreToolNextLevel;

		public void Refresh()
		{
			explorer.Refresh();
			RefreshCfg();
		}

		public void RefreshCfg()
		{
			level = Math.Clamp(level, 1, 999);
			toolLevel = Math.Clamp(toolLevel, 1, 999);
			ConfigSystem.Instance.TryGet(level, out exploreLevel);
			ConfigSystem.Instance.TryGet(level+1, out exploreNextLevel);
			ConfigSystem.Instance.TryGet(toolLevel, out exploreToolLevel);
			ConfigSystem.Instance.TryGet(toolLevel+1, out exploreToolNextLevel);
		}

		public bool IsExploreMaxLv()
		{
			return !exploreNextLevel.IsValid();
		}

		public bool IsExploreToolMaxLv()
		{
			return !exploreToolNextLevel.IsValid();
		}

	}

	[Serializable]
	public class ExploreRoleData
	{
		public FightEquip[] equips = new FightEquip[8];

		private double power;

		public ExploreRoleData Refresh()
		{
			equips.Foreach(e => e.Refresh());
			return this;
		}

		public double GetPower(bool recalu = false)
		{
			if (recalu || power == 0)
			{
				power = equips.Sum(e => e.power);
			}
			return power;
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
				if (!battleLvCfg.IsValid() || level != battleLvCfg.Id)
				{
					if (ConfigSystem.Instance.TryGet(level, out battleLvCfg))
					{
						ConvertBuff();
					}
				}
			}
			return this;
		}

		public override int GetAttrVal(bool needlv = true, int lv = 0, int id = 0)
		{
			id = id == 0 ? ((int)EnumAttribute.Hp) : id;
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
					var anitnum = qcfg.AttribNum(1);
					var rnd = SGame.Randoms.Random._R;
					attrs.Add(GetBattleInfo(EnumAttribute.Hp, this, power));
					attrs.Add(GetBattleInfo(EnumAttribute.Attack, this, power));

					for (int i = 0; i < addnum; i++)
					{
						var id = rnd.Next(((int)EnumAttribute.Dodge), ((int)EnumAttribute.Steal));
						attrs.Add(GetBattleInfo((EnumAttribute)id, this, power));
					}

					for (int i = 0; i < anitnum; i++)
					{
						var id = rnd.Next(((int)EnumAttribute.AntiDodge), ((int)EnumAttribute.AntiSteal));
						attrs.Add(GetBattleInfo((EnumAttribute)id, this, power));
					}
				}

				if (attrDic == null)
				{
					attrDic = attrs.ToDictionary(v => v.id);
					power = DataCenter.ExploreUtil.CaluPower(attrs.ToArray());
				}
			}
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

