using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame 
{
    partial class RequestExcuteSystem
    {
        [InitCall]
        static void InitBattle() 
        {
            DataCenter.BattleLevelUtil.Init();
        }

        public static void BattleBegin() 
        {
            EventManager.Instance.Trigger((int)GameEvent.BATTLE_START);
        }


        /// <summary>
        /// ��ȡ��ͨ����
        /// </summary>
        public static void BattleAward() 
        {
            Utils.ShowFightRewards(DataCenter.Instance.battleLevelData.award.Select((v) => v.getArray()).ToList(), type: 0);
            DataCenter.BattleLevelUtil.UpdateCacheResult();
        }

        /// <summary>
        /// ��ȡ��潱��
        /// </summary>
        public static void BattleAdAward() 
        {
            Utils.ShowFightRewards(DataCenter.Instance.battleLevelData.adAward.Select((v) => v.getArray()).ToList(), type: 1);
            DataCenter.BattleLevelUtil.UpdateCacheResult();
        }
    }
}

