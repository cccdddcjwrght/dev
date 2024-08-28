using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public enum RoleType
    {
        ROLE,      //玩家角色
        MONSTER,    //怪物
    }

    public class BattleManager : Singleton<BattleManager>
    {
        //战斗成员
        private List<BaseBattleCharacter> characterList = new List<BaseBattleCharacter>();
        private List<RoundData> roundList = new List<RoundData>();

        private UnityEngine.Coroutine _roundCorotine;

        //是否战斗中
        public bool isCombat;

        //当前第几回合
        private int roundIndex = 1;

        //最大回合
        private int _maxRound;

        //是否胜利
        public bool isWin = false;

        public void Initalize() 
        {
            BattleTimer.Instance.Start();
        }

        /// <summary>
        /// 战斗开始
        /// </summary>
        public IEnumerator BattleStart(BaseBattleCharacter role, BaseBattleCharacter monster, int maxRound, float waitTime)
        {
            isCombat = true;
            _maxRound = maxRound;
            characterList.Add(role);
            characterList.Add(monster);

            characterList.Sort((a, b) => b.attributes.GetBaseAttribute(EnumAttribute.AtkSpeed)
            .CompareTo(a.attributes.GetBaseAttribute(EnumAttribute.AtkSpeed)));

            GenerateRoundData();
            ResetCharacter();

            yield return new WaitForSeconds(waitTime);
            _roundCorotine = PlayRound().Start();
        }

        /// <summary>
        /// 重置角色属性
        /// </summary>
        public void ResetCharacter()
        {
            characterList.ForEach((v) => {
                v.attributes.ResetAttribute();
                v.state.Reset();
            });
        }

        /// <summary>
        /// 生成回合数据
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
            roundIndex = 1; //重置一下
            isWin = CheckVictory();

            DataCenter.BattleLevelUtil.CacheBattleResult(isWin);
        }

        /// <summary>
        /// 开始播放回合表现
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayRound()
        {
            int temp = 0;
            for (int i = 0; i < roundList.Count; i++)
            {
                var roundData = roundList[i];
                roundIndex = roundData.roundIndex;
                if (temp != roundIndex)
                {
                    temp = roundIndex;
                    EventManager.Instance.Trigger((int)GameEvent.BATTLE_ROUND);
                }

                roundData.attacker.state.UpdateState();
                //攻击效果
                for (int j = 0; j < roundData.attackList.Count; j++)
                {
                    var attackEffect = roundData.attackList[j];
                    yield return roundData.attacker.DoAttack(roundData.defender, attackEffect);
                }
            }
            BattleFinish();
        }

        /// <summary>
        /// 检测双方是否还存活对象
        /// </summary>
        /// <returns></returns>
        private bool IsCombatOngoing()
        {
            bool hasRoleAlive = false;
            bool hasMonsterAlive = false;
            bool isMaxTrunCount = roundIndex <= _maxRound;

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

        //获取当前战斗回合
        public int GetRoundIndex()
        {
            return roundIndex;
        }

        /// <summary>
        /// 检测是否胜利
        /// </summary>
        /// <returns></returns>
        bool CheckVictory()
        {
            return characterList.Find((v) => v.isAlive && v.roleType == RoleType.MONSTER) == null;
        }

        //回合表现中止
        public void DiscontinuePlayRound() 
        {
            if (isCombat) 
            {
                _roundCorotine?.Stop();
                BattleFinish(false);
            }
            
        }

        /// <summary>
        /// 战斗结束
        /// </summary>
        void BattleFinish(bool isTrigger = true)
        {
            //if (isWin) BattleWin();
            //else BattleLose();

            isCombat = false;
            characterList.Clear();
            roundList.Clear();

            if(isTrigger) EventManager.Instance.Trigger((int)GameEvent.BATTLE_OVER);
        }

        void BattleWin()
        {
            //SGame.UIUtils.OpenUI("fightwin");
            //Debug.Log("Win -");
        }

        void BattleLose()
        {
            //SGame.UIUtils.OpenUI("fightlose");
            //Debug.Log("Lose -");
        }
    }
}


