using GameConfigs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame 
{

    [Serializable]
    public class DailyTaskData 
    {
        public int dayLiveness;
        public int weekLiveness;

        public int nextDayRefreshTime;
        public int nextWeekRefreshTime;

        public List<DailyTaskItem> tasks = new List<DailyTaskItem>();
        public List<DailyReward> dayRewards = new List<DailyReward>();
        public List<DailyReward> weekRewards = new List<DailyReward>();
    }

    [Serializable]
    public class DailyTaskItem 
    {
        public int cfgId;
        public bool isFinish;
        public DailyTaskRowData GetConfig()
        {
            ConfigSystem.Instance.TryGet<DailyTaskRowData>(cfgId, out var config);
            return config;
        }

        public int GetValue() 
        {
            return RecordModule.Instance.GetValue(GetConfig().TaskType, (int)RecordFunctionId.DAILY_TASK);
        }

        public bool IsGet() 
        {
            return !isFinish && GetValue() >= GetConfig().TaskValue(1);
        }
    }

    [Serializable]
    public class DailyReward 
    {
        public int cfgId;
        public int needLiveness;
        public bool isGet;

        public DailyRewardRowData GetConfig() 
        {
            ConfigSystem.Instance.TryGet<DailyRewardRowData>(cfgId, out var config);
            return config;
        }

        public bool IsCanGet() 
        {
            if (isGet) return false;
            if (GetConfig().Type == 1) return DataCenter.Instance.dailyTaskData.dayLiveness >= needLiveness;
            else return DataCenter.Instance.dailyTaskData.weekLiveness >= needLiveness;
        }
    }

    public partial class DataCenter 
    {
        public DailyTaskData dailyTaskData = new DailyTaskData();

        public static class DailyTaskUtil 
        {
            public static DailyTaskData m_Data { get { return Instance.dailyTaskData; } }
            private static int daily_task_num = GlobalDesginConfig.GetInt("daily_task_num", 3);
            public static void Init() 
            {
                GenerateTask();
                EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) => { GenerateTask(); });
            }

            public static int nextWeekInterval 
            {
                get { return m_Data.nextWeekRefreshTime - GameServerTime.Instance.serverTime; }
            }

            public static void GenerateTask() 
            {
                var serverTime = GameServerTime.Instance.serverTime;
                if (serverTime >= m_Data.nextDayRefreshTime) 
                {
                    m_Data.tasks.Clear();
                    m_Data.dayLiveness = 0;
                    var t = GetRandomDayTasks(daily_task_num);
                    m_Data.tasks = t.Select(v => new DailyTaskItem() 
                    { 
                        cfgId = v,
                        isFinish =false,
                    }).ToList();
                    m_Data.tasks.ForEach((v) =>
                    {
                        var config = v.GetConfig();
                        if (config.CountType == 1) RecordModule.Instance.ClearValue(config.TaskType, (int)RecordFunctionId.DAILY_TASK);
                    });
                    m_Data.dayRewards = GetDailyRewards(1);
                    m_Data.nextDayRefreshTime = GameServerTime.Instance.nextDayTime;
                }

                if (serverTime >= m_Data.nextWeekRefreshTime) 
                {
                    m_Data.weekLiveness = 0;
                    m_Data.weekRewards = GetDailyRewards(2);
                    m_Data.nextWeekRefreshTime = GetNextWeekDay();
                }
                EventManager.Instance.Trigger((int)GameEvent.DAILY_TASK_UPDATE);
            }

            public static List<DailyReward> GetDailyRewards(int type) 
            {
                var roomId = Instance.roomData.roomID;
                var list = ConfigSystem.Instance.Finds<DailyRewardRowData>((v) => v.Type == type 
                && roomId >= v.LevelUnlock(0) && roomId <= v.LevelUnlock(1));
                return list.Select(v => new DailyReward() 
                { 
                    cfgId = v.Id, 
                    needLiveness = v.Num, 
                    isGet = false 
                }).ToList();
            }

            public static int[] GetRandomDayTasks(int num) 
            {
                List<int> weights = new List<int>();
                var configs = ConfigSystem.Instance.LoadConfig<DailyTask>();

                int weight = 0;
                for (int i = 0; i < configs.DatalistLength; i++)
                {
                    var config = configs.Datalist(i).Value;
                    var min = config.TaskUnlock(0);
                    var max = config.TaskUnlock(1);
                    var cur = Instance.roomData.roomID;
                    if (cur >= min && cur <= max) weight = config.Weight;
                    weights.Add(weight);
                }
                return Randoms.Random._R.NextWeights(weights, num).Select(v => configs.Datalist(v).Value.Id).ToArray();
            }


            public static int GetNextWeekDay() 
            {
                var d = DateTimeOffset.FromUnixTimeSeconds(GameServerTime.Instance.serverTime);
                d = new DateTimeOffset(d.Year, d.Month, d.Day, 0, 0, 0, default);
                var nextMonday = ((int)DayOfWeek.Monday - (int)d.DayOfWeek + 7) % 7;
                if (nextMonday == 0) nextMonday = 7;

                d = d.AddDays(nextMonday);
                return (int)d.ToUnixTimeSeconds();
            }

            public static bool CheckRed() 
            {
                foreach (var v in m_Data.tasks)
                    if (v.IsGet()) return true;
                foreach (var v in m_Data.dayRewards)
                    if (v.IsCanGet()) return true;
                foreach (var v in m_Data.weekRewards)
                    if (v.IsCanGet()) return true;
                return false;
            }
        }
    }

}

