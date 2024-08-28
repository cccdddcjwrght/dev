using System;
using System.Collections.Generic;
using GameConfigs;

namespace SGame 
{
    [Serializable]
    public class BattleLevelData
    {
        public int level = 1;           //战斗结果出现就更新
        public int showLevel = 1;       //(显示等级)等待战斗表现结束再更新

        public bool cacheResult;        //是否有缓存战斗结果
        public bool battlResult;        //战斗结果

        //当前关卡未领取奖励
        public List<FightReward> award = new List<FightReward>();   
        public List<FightReward> adAward = new List<FightReward>(); 
    }

    [Serializable]
    public class FightReward 
    {
        public int id;
        public int num;

        public int[] getArray() 
        {
            return new int[] { 1, id, num };
        }
    }


    public partial class DataCenter
    {
        public BattleLevelData battleLevelData = new BattleLevelData();
        public static class BattleLevelUtil
        {
            private static BattleLevelData m_Data { get { return Instance.battleLevelData; } }
            private static int _maxLevel;

            public static bool IsMax 
            {   
                get { return Instance.battleLevelData.level >= _maxLevel; } 
            }

            public static void Init() 
            {
                _maxLevel = ConfigSystem.Instance.GetConfigCount(typeof(BattleLevelRowData));
            }


            public static void CacheBattleResult(bool result) 
            {
                m_Data.cacheResult = true;
                m_Data.battlResult = result;
                if (result) 
                {
                    m_Data.level++;
                    ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(m_Data.showLevel, out var config);
                    m_Data.award = GetReward(config.GetRewardId1Array(), config.GetRewardNum1Array(), config.GetRewardodds1Array());
                    m_Data.adAward = GetReward(config.GetRewardId2Array(), config.GetRewardNum2Array(), config.GetRewardodds2Array());
                }
            }

            public static List<int[]> GetShowReward(int[] rewardIds, int[] rewardNums) 
            {
                if (rewardIds.Length != rewardNums.Length)
                    return default;
                List<int[]> list = new List<int[]>();
                for (int i = 0; i < rewardIds.Length; i++)
                {
                    int[] v = new int[] { 1, rewardIds[i], rewardNums[i] };
                    list.Add(v);
                }
                return list;
            }

            public static List<FightReward> GetReward(int[] rewardIds, int[] rewardNums, int[] odds)
            {
                if (rewardIds.Length != rewardNums.Length)
                    return default;
                List<FightReward> list = new List<FightReward>();
                for (int i = 0; i < rewardIds.Length; i++)
                {
                    if (SGame.Randoms.Random._R.Rate(odds[i])) 
                    {
                        list.Add(new FightReward() { id = rewardIds[i], num = rewardNums[i] });
                    }
                }
                return list;
            }


            public static void ShowBattleResult() 
            {
                if (m_Data.cacheResult) 
                {
                    if (m_Data.battlResult) SGame.UIUtils.OpenUI("fightwin");
                    else SGame.UIUtils.OpenUI("fightlose");
                }
            }

            public static void UpdateCacheResult() 
            {
                m_Data.showLevel = m_Data.level;
                m_Data.cacheResult = false;
            }
        }
    }
}


