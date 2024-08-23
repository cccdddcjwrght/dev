using System.Collections;
using System.Collections.Generic;
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
            ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.showLevel, out var config);
            if (config.IsValid()) 
            {
                var rewards = DataCenter.BattleLevelUtil.GetReward(config.GetRewardId1Array(), config.GetRewardNum1Array());
                Utils.ShowRewards(rewards);
            }
            DataCenter.BattleLevelUtil.UpdateShowLevel();
        }

        /// <summary>
        /// 领取广告奖励
        /// </summary>
        public static void BattleAdAward() 
        {
            ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(DataCenter.Instance.battleLevelData.showLevel, out var config);
            if (config.IsValid())
            {
                var rewards = DataCenter.BattleLevelUtil.GetReward(config.GetRewardId2Array(), config.GetRewardNum2Array());
                Utils.ShowRewards(rewards);
            }
            DataCenter.BattleLevelUtil.UpdateShowLevel();
        }
    }
}

