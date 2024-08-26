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
                if(result) m_Data.level++;
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


