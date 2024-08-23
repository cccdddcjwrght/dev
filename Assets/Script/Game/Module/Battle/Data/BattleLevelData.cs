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

        //暂时存储的奖励，避免玩家未领取
        public List<int[]> battleRewards = new List<int[]>();
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
                Instance.battleLevelData.battleRewards.ForEach((v)
                    => PropertyManager.Instance.Update(v[0], v[1], v[2]));

                UpdateShowLevel();
            }


            public static void NextLevel() 
            {
                ConfigSystem.Instance.TryGet<GameConfigs.BattleLevelRowData>(m_Data.level, out var config);
                if (config.IsValid()) 
                {
                    Instance.battleLevelData.battleRewards =
                        GetReward(config.GetRewardId1Array(), config.GetRewardId2Array());
                }
                m_Data.level++;
            }

            public static List<int[]> GetReward(int[] rewardIds, int[] rewardNums) 
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

            public static void UpdateShowLevel() 
            {
                Instance.battleLevelData.showLevel = Instance.battleLevelData.level;
                Instance.battleLevelData.battleRewards?.Clear();
            }
        }
    }
}


