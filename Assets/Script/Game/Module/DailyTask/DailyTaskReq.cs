using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public partial class RequestExcuteSystem 
    {
        [InitCall]
        static void InitDailyTask()
        {
            DataCenter.DailyTaskUtil.Init();
        }

        public static void DailyTaskFinsih(int cfgId) 
        {
            var dailyTaskData = DataCenter.Instance.dailyTaskData;
            var task = dailyTaskData.tasks.Find((v) => v.cfgId == cfgId);
            if (task != null) 
            {
                task.isFinish = true;
                dailyTaskData.dayLiveness += task.GetConfig().TaskReward;
                dailyTaskData.weekLiveness += task.GetConfig().TaskReward;
                EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.DIALY_TASK, 1);
                EventManager.Instance.Trigger((int)GameEvent.DAILY_TASK_UPDATE);
            }
        }

        public static void DailyGiftGet(int cfgId) 
        {
            var dailyTaskData = DataCenter.Instance.dailyTaskData;
            var reward = dailyTaskData.dayRewards.Find((v) => v.cfgId == cfgId);
            if (reward == null) reward = dailyTaskData.weekRewards.Find((v) => v.cfgId == cfgId);

            if (reward != null) 
            {
                reward.isGet = true;
                var config = reward.GetConfig();
                var list = Utils.GetArrayList(
                        true,
                        config.GetReward1Array,
                        config.GetReward2Array,
                        config.GetReward3Array);

                Utils.ShowRewards(list);
                EventManager.Instance.Trigger((int)GameEvent.DAILY_TASK_UPDATE);
            }
        }
    }
}

