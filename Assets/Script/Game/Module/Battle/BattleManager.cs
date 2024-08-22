using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public enum RoleType
    {
        ROLE,      //��ҽ�ɫ
        MONSTER,    //����
    }

    public class BattleManager : Singleton<BattleManager>
    {
        //ս����Ա
        private List<BaseBattleCharacter> characterList = new List<BaseBattleCharacter>();
        private List<RoundData> roundList = new List<RoundData>();

        private UnityEngine.Coroutine _roundCorotine;

        //�Ƿ�ս����
        public bool isCombat;

        //��ǰ�ڼ��غ�
        private int roundIndex = 1;

        //�Ƿ�ʤ��
        private bool isWin = false;

        public void Initalize() 
        {
            BattleTimer.Instance.Start();
        }

        /// <summary>
        /// ս����ʼ
        /// </summary>
        public void BattleStart(BaseBattleCharacter role, BaseBattleCharacter monster)
        {
            isCombat = true;
            characterList.Add(role);
            characterList.Add(monster);

            GenerateRoundData();
            ResetCharacter();

            _roundCorotine = PlayRound().Start();
        }

        /// <summary>
        /// ���ý�ɫ����
        /// </summary>
        public void ResetCharacter()
        {
            characterList.ForEach((v) => {
                v.attributes.ResetAttribute();
                v.state.Reset();
            });
        }

        /// <summary>
        /// ���ɻغ�����
        /// </summary>
        public void GenerateRoundData()
        {
            roundIndex = 1;
            while (IsCombatOngoing())
            {
                for (int i = 0; i < characterList.Count; i++)
                {
                    var attacker = characterList[i];
                    var defender = characterList[(i + 1) % characterList.Count];
                    if (attacker.isAlive)
                    {
                        RoundData data = new RoundData();
                        data.roundIndex = roundIndex;
                        data.attacker = attacker;
                        data.defender = defender;
                        data.Excute();
                        roundList.Add(data);
                    }
                }
                roundIndex++;
            }
            isWin = CheckVictory();
        }

        /// <summary>
        /// ��ʼ���Żغϱ���
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayRound()
        {
            for (int i = 0; i < roundList.Count; i++)
            {
                var roundData = roundList[i];
                roundIndex = roundData.roundIndex;
                roundData.attacker.state.UpdateState();
                //����Ч��
                for (int j = 0; j < roundData.attackList.Count; j++)
                {
                    var attackEffect = roundData.attackList[j];
                    yield return roundData.attacker.DoAttack(roundData.defender, attackEffect);
                }
            }
            BattleFinish();
        }

        /// <summary>
        /// ���˫���Ƿ񻹴�����
        /// </summary>
        /// <returns></returns>
        private bool IsCombatOngoing()
        {
            bool hasRoleAlive = false;
            bool hasMonsterAlive = false;
            bool isMaxTrunCount = roundIndex <= BattleConst.max_turncount;

            for (int i = 0; i < characterList.Count; i++)
            {
                var character = characterList[i];
                if (character.isAlive)
                {
                    if (character.roleType == RoleType.ROLE) hasRoleAlive = true;
                    else hasMonsterAlive = true;
                }
            }
            return hasRoleAlive && hasMonsterAlive && isMaxTrunCount;
        }

        //��ȡ��ǰս���غ�
        public int GetRoundIndex()
        {
            return roundIndex;
        }

        /// <summary>
        /// ����Ƿ�ʤ��
        /// </summary>
        /// <returns></returns>
        bool CheckVictory()
        {
            return characterList.Find((v) => v.isAlive && v.roleType == RoleType.MONSTER) == null;
        }

        //�غϱ�����ֹ
        public void DiscontinuePlayRound() 
        {
            if (isCombat) 
            {
                _roundCorotine?.Stop();
                BattleFinish();
            }
            
        }

        /// <summary>
        /// ս������
        /// </summary>
        void BattleFinish()
        {
            if (isWin) BattleWin();
            else BattleLose();

            EventManager.Instance.Trigger((int)GameEvent.BATTLE_OVER);

            isCombat = false;
            characterList.Clear();
            roundList.Clear();
        }

        void BattleWin()
        {
            Debug.Log("Win -");
        }

        void BattleLose()
        {
            Debug.Log("Lose -");
        }
    }
}


