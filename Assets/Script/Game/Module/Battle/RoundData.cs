using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 战斗回合信息
    /// </summary>
    public class RoundData
    {
        public int roundIndex;     //当前回合
        public BaseBattleCharacter attacker;
        public BaseBattleCharacter defender;

        //攻击效果
        public List<AttackEffect> attackList = new List<AttackEffect>();
        //状态生成
        public List<BaseState> stateList;

        public void Excute()
        {
            attacker.state.UpdateState();
            RecordAttack(false);
        }

        /// <summary>
        /// 记录攻击到回合信息中
        /// </summary>
        /// <param name="isCombo">当前攻击是不是触发的连击</param>
        public void RecordAttack(bool isCombo)
        {
            if (defender.attributes.GetBaseAttribute(EnumAttribute.Hp) <= 0) return;
            //检测是否有眩晕状态
            if (!attacker.state.IsHasState(BattleStateType.DIZZ))
            {
                bool triggerDodge = false;
                bool triggerCrit = false;
                bool triggerDizz = false;
                bool triggerCombo = false;
                int damage = 0;
                int steal = 0;

                var rate = Mathf.Max(defender.attributes.GetBaseAttribute(EnumAttribute.Dodge) -
                    attacker.attributes.GetBaseAttribute(EnumAttribute.AntiDodge), 0);
                if (defender.state.IsHasState(BattleStateType.DIZZ)) rate = 0;

                //是否闪避
                triggerDodge = BattleUtil.TriggerProbability(rate);
                if (triggerDodge)
                {
                    attackList.Add(new AttackEffect());
                    Debug.Log(string.Format("攻击方类型:{0}, 受击方类型:{1} 被闪避了", attacker.roleType.ToString(), defender.roleType.ToString()));
                }
                else
                {
                    damage = attacker.attributes.CalculateDamage();
                    //是否暴击
                    rate = Mathf.Max(attacker.attributes.GetBaseAttribute(EnumAttribute.Crit) -
                        defender.attributes.GetBaseAttribute(EnumAttribute.AntiCrit), 0);
                    triggerCrit = BattleUtil.TriggerProbability(rate);
                    if (triggerCrit)
                    {
                        damage = Mathf.CeilToInt(damage * BattleConst.criticalhit_ratio);
                    }

                    rate = Mathf.Max(attacker.attributes.GetBaseAttribute(EnumAttribute.Stun)
                        - defender.attributes.GetBaseAttribute(EnumAttribute.AntiStun), 0);
                    triggerDizz = BattleUtil.TriggerProbability(rate);
                    if (triggerDizz)
                    {
                        defender.state.ApplyState(new DizzState());
                        stateList = new List<BaseState>();
                        stateList.Add(new DizzState() { stateShow = true });
                    }

                    //吸血
                    steal = Mathf.CeilToInt(damage * Mathf.Max(attacker.attributes.GetBaseAttribute(EnumAttribute.Steal)
                        - defender.attributes.GetBaseAttribute(EnumAttribute.AntiSteal), 0) * 0.0001f);

                    steal = Mathf.Min(steal, attacker.attributes.GetBaseAttributeUpperLimit(EnumAttribute.Hp) -
                        attacker.attributes.GetBaseAttribute(EnumAttribute.Hp));
                    damage = Mathf.Min(damage, defender.attributes.GetBaseAttribute(EnumAttribute.Hp));
                    attackList.Add(new AttackEffect()
                    {
                        damage = damage,
                        steal = steal,
                        isCritical = triggerCrit,
                        isCombo = isCombo,
                        stateList = stateList,
                    });
                    attacker.attributes.ChangeAttribute(EnumAttribute.Hp, steal);
                    defender.attributes.ChangeAttribute(EnumAttribute.Hp, -damage);
                }

                //连击概率
                rate = (int)Mathf.Max((attacker.attributes.GetBaseAttribute(EnumAttribute.Combo) - defender.attributes.GetBaseAttribute(EnumAttribute.AntiCombo))
                     * (Mathf.Pow(BattleConst.doublehit_ratio, attacker.attributes.batterCount)), 0);
                triggerCombo = BattleUtil.TriggerProbability(rate);
                if (triggerCombo)
                {
                    attacker.attributes.batterCount++;
                    RecordAttack(triggerCombo);
                }

                Debug.Log(string.Format("Battle- 攻击方类型:{0}\n造成伤害:{1}\n吸血:{2}\n剩余血量:{3}\n受击方类型:{4}\n剩余血量:{5}\n是否触发暴击{6}\n是否触发连击:{7}\n是否触发眩晕{8}",
                attacker.roleType.ToString(), damage, steal, attacker.attributes.GetBaseAttribute(EnumAttribute.Hp),
                defender.roleType.ToString(), defender.attributes.GetBaseAttribute(EnumAttribute.Hp),
                triggerCrit, triggerCombo, triggerDizz));
            }
            else
            {
                Debug.Log(string.Format("攻击方类型:{0}, 当前处于眩晕状态, 跳过当前回合", attacker.roleType.ToString()));
            }
        }
    }
}

