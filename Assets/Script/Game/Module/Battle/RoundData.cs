using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// ս���غ���Ϣ
    /// </summary>
    public class RoundData
    {
        public int roundIndex;     //��ǰ�غ�
        public BaseBattleCharacter attacker;
        public BaseBattleCharacter defender;

        //����Ч��
        public List<AttackEffect> attackList = new List<AttackEffect>();
        //״̬����
        public List<BaseState> stateList;

        public void Excute()
        {
            attacker.state.UpdateState();
            RecordAttack(false);
        }

        /// <summary>
        /// ��¼�������غ���Ϣ��
        /// </summary>
        /// <param name="isCombo">��ǰ�����ǲ��Ǵ���������</param>
        public void RecordAttack(bool isCombo)
        {
            if (defender.attributes.GetBaseAttribute(EnumAttribute.Hp) <= 0) return;
            //����Ƿ���ѣ��״̬
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

                //�Ƿ�����
                triggerDodge = BattleUtil.TriggerProbability(rate);
                if (triggerDodge)
                {
                    attackList.Add(new AttackEffect());
                    Debug.Log(string.Format("����������:{0}, �ܻ�������:{1} ��������", attacker.roleType.ToString(), defender.roleType.ToString()));
                }
                else
                {
                    damage = attacker.attributes.CalculateDamage();
                    //�Ƿ񱩻�
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

                    //��Ѫ
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

                //��������
                rate = (int)Mathf.Max((attacker.attributes.GetBaseAttribute(EnumAttribute.Combo) - defender.attributes.GetBaseAttribute(EnumAttribute.AntiCombo))
                     * (Mathf.Pow(BattleConst.doublehit_ratio, attacker.attributes.batterCount)), 0);
                triggerCombo = BattleUtil.TriggerProbability(rate);
                if (triggerCombo)
                {
                    attacker.attributes.batterCount++;
                    RecordAttack(triggerCombo);
                }

                Debug.Log(string.Format("Battle- ����������:{0}\n����˺�:{1}\n��Ѫ:{2}\nʣ��Ѫ��:{3}\n�ܻ�������:{4}\nʣ��Ѫ��:{5}\n�Ƿ񴥷�����{6}\n�Ƿ񴥷�����:{7}\n�Ƿ񴥷�ѣ��{8}",
                attacker.roleType.ToString(), damage, steal, attacker.attributes.GetBaseAttribute(EnumAttribute.Hp),
                defender.roleType.ToString(), defender.attributes.GetBaseAttribute(EnumAttribute.Hp),
                triggerCrit, triggerCombo, triggerDizz));
            }
            else
            {
                Debug.Log(string.Format("����������:{0}, ��ǰ����ѣ��״̬, ������ǰ�غ�", attacker.roleType.ToString()));
            }
        }
    }
}

