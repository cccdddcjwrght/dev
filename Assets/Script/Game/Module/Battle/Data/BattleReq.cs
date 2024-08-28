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
        /// 领取普通奖励
        /// </summary>
        public static void BattleAward() 
        {
            Utils.ShowRewards(DataCenter.Instance.battleLevelData.award.Select((v) => v.getArray()).ToList());
            DataCenter.BattleLevelUtil.UpdateCacheResult();
        }

        /// <summary>
        /// 领取广告奖励
        /// </summary>
        public static void BattleAdAward() 
        {
            Utils.ShowRewards(DataCenter.Instance.battleLevelData.adAward.Select((v) => v.getArray()).ToList());
            DataCenter.BattleLevelUtil.UpdateCacheResult();
        }
    }
}

