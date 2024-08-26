using SGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame 
{
    public class FightAttrInfo
    {
        public int id;
        public int val;

        public int upperLimit; //属性值上限
    }

    /// <summary>
    /// 角色战斗属性
    /// </summary>
    public class BattleAttritube
    {
        private List<FightAttrInfo> fightAttrList = new List<FightAttrInfo>();
        private Dictionary<int, FightAttrInfo> fightAttrDict;

        public int AtkRatio1;
        public int AtkRatio2;

        public int batterCount;         //当前角色连击次数

        //读取配置属性
        public void ReadAttribute(int cfgId)
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.BattleRoleRowData>(cfgId, out var config))
            {
                SetBaseAttribute((int)EnumAttribute.Hp, config.Hp);
                SetBaseAttribute((int)EnumAttribute.Attack, config.Atk);
                //SetBaseAttribute((int)EnumAttribute.AtkSpeed, config.Speed);
                SetBaseAttribute((int)EnumAttribute.Dodge, config.Dodge);
                SetBaseAttribute((int)EnumAttribute.Combo, config.Combo);
                SetBaseAttribute((int)EnumAttribute.Crit, config.Crit);
                SetBaseAttribute((int)EnumAttribute.Stun, config.Stun);
                SetBaseAttribute((int)EnumAttribute.Steal, config.Steal);
                SetBaseAttribute((int)EnumAttribute.AntiDodge, config.AntiDodge);
                SetBaseAttribute((int)EnumAttribute.AntiCombo, config.AntiCombo);
                SetBaseAttribute((int)EnumAttribute.AntiCrit, config.AntiCrit);
                SetBaseAttribute((int)EnumAttribute.AntiStun, config.AntiStun);
                SetBaseAttribute((int)EnumAttribute.AntiSteal, config.AntiSteal);

                AtkRatio1 = config.AtkRatio(0);
                AtkRatio2 = config.AtkRatio(1);
            }
            batterCount = 0;
            fightAttrDict = fightAttrList.ToDictionary(v => v.id);
        }

        //加载属性
        public void LoadAttribute(List<int[]> attrList)
        {
            attrList.ForEach((v) => SetAttribute(v[0], v[1]));
            fightAttrDict = fightAttrList.ToDictionary(v => v.id);
            batterCount = 0;
        }

        public void SetBaseAttribute(int attribute, int value)
        {
            fightAttrList.Add(new FightAttrInfo() { id = attribute, val = value, upperLimit = value });
        }

        public void SetAttribute(int attribute, int value)
        {
            if (fightAttrDict.TryGetValue(attribute, out var info)) 
            {
                info.val = value;
                info.upperLimit = value;
            }
        }

        public int GetBaseAttribute(EnumAttribute attribute)
        {
            if (fightAttrDict.TryGetValue((int)attribute, out var info))
                return info.val;
            return 0;
        }

        public int GetBaseAttributeUpperLimit(EnumAttribute attribute)
        {
            if (fightAttrDict.TryGetValue((int)attribute, out var info))
                return info.upperLimit;
            return 0;
        }

        public void ChangeAttribute(EnumAttribute attribute, int value)
        {
            if (fightAttrDict.TryGetValue((int)attribute, out var info))
                info.val += value;
        }

        public void ResetAttribute()
        {
            fightAttrList.Foreach((v) => v.val = v.upperLimit);
        }

        //计算伤害（无加成）
        public int CalculateDamage()
        {
            var undulate = Random.Range(AtkRatio1, AtkRatio2) * 0.01f;
            return Mathf.CeilToInt(undulate * GetBaseAttribute(EnumAttribute.Attack));
        }

        public List<int[]> GetFightAttr() 
        {
            return fightAttrList.Select(v => new int[] { v.id, v.upperLimit }).ToList();
        }
    }
}

