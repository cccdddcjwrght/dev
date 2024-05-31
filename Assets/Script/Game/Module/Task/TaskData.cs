using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace SGame
{
    public enum TaskState 
    {
        WAIT_GET        = 0,//待领取
        IN_PROGRESS     = 1,//进行中
        GET_REWARD      = 2,//已领取
    }

    [Serializable]
    public class TaskData
    {
        public List<TaskItem> taskItems = new List<TaskItem>();
        public List<int> taskGoods = new List<int>();

        public int startTime;

        [System.NonSerialized]
        public Dictionary<int, TaskItem> taskDict = new Dictionary<int, TaskItem>();
        [System.NonSerialized]
        public Dictionary<int, List<int>> taskRewardDict = new Dictionary<int, List<int>>();
    }
    
    [Serializable]
    public class TaskItem 
    {
        public int taskId;      //任务id
        public int taskType;    //任务类型
        public int value;       //任务当前进度
        public int maxValue;    //任务总进度
        public int state;       //任务状态
        public bool isGet;      //奖励是否领取
    }

    [Serializable]
    public class TaskGood 
    {
        public int id;
    }

    public partial class DataCenter 
    {
        public TaskData taskData = new TaskData();

        public static class TaskUtil
        {
            public const int TASK_TYPE = 3;         //兑换任务活动类型
            //public const int TASK_CURRENCY = 7;     //兑换任务货币

            static EventHandleContainer m_EventHandle = new EventHandleContainer();

            static TaskData m_Data { get { return Instance.taskData; } }

            public static GameConfigs.ActivityTimeRowData taskActivityConfig 
            {
                get 
                {
                    if (ConfigSystem.Instance.TryGet<GameConfigs.ActivityTimeRowData>(GetTaskActivityId(), out var data))
                        return data;
                    return default;
                }
            }

            public static void InitData()
            {
                InitTaskData();
                InitTaskReward();
                m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, RefreshTaskProgress);
                m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ACTIVITY_OPEN, (id) =>
                {
                    if (ConfigSystem.Instance.TryGet<GameConfigs.ActivityTimeRowData>(id, out var cfg))
                    {
                        if (cfg.Type == TASK_TYPE)
                        {
                            InitTaskData();
                            InitTaskReward();
                        }
                    }
                });
                
                m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ORDER_FINSIH, (orderId) =>
                {
                    EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RecordDataEnum.SELL, 1);
                });
            }

            public static void InitTaskData()
            {
                var activityConfig = taskActivityConfig;
                if (!activityConfig.IsValid()) return;

                var timeRange = ActiveTimeSystem.Instance.GetTimeRange(activityConfig.Id);
                if (m_Data.startTime != timeRange.tMin) 
                {
                    m_Data.startTime = timeRange.tMin;
                    ClearData();    //清除数据
                }

                var olds = m_Data.taskItems.ToDictionary(t => t.taskId);
                var list = new List<TaskItem>();
                if (ConfigSystem.Instance.TryGets<GameConfigs.MerchantMissionRowData>((c) => c.TypeValue == activityConfig.Value, out var cfgs))
                {
                    cfgs.ForEach((cfg) =>
                    {
                        if (!olds.TryGetValue(cfg.Id, out var taskItem))
                        {
                            taskItem = new TaskItem()
                            {
                                taskId = cfg.Id,
                                taskType = cfg.TaskType,
                                maxValue = cfg.TaskValue,
                                state = (int)TaskState.IN_PROGRESS,
                                value = 0,
                                isGet = false,
                            };
                            //taskItem.state = taskItem.value >= taskItem.maxValue ? (int)TaskState.WAIT_GET : (int)TaskState.IN_PROGRESS;
                        }
                        list.Add(taskItem);
                    });
                    m_Data.taskItems = list;
                    TaskStateSort();

                    m_Data.taskDict?.Clear();
                    m_Data.taskDict = list.ToDictionary((t) => t.taskId);
                }
            }

            public static void InitTaskReward() 
            {
                var activityConfig = taskActivityConfig;
                if (!activityConfig.IsValid()) return;

                var dict = new Dictionary<int, List<int>>();
                if (ConfigSystem.Instance.TryGets<GameConfigs.MerchantRewardRowData>((c) => c.TypeValue == activityConfig.Value, out var cfgs)) 
                {
                    cfgs.ForEach((c)=> 
                    {
                        if (!dict.TryGetValue(c.Group, out var list)) 
                        {
                            list = new List<int>();
                            dict.Add(c.Group, list);
                        }
                        if(!m_Data.taskGoods.Contains(c.Id))
                            list.Add(c.Id);
                    });
                }
                m_Data.taskRewardDict = dict;

                foreach (var item in dict)
                {
                    int group = item.Key;
                    int max = GetTaskRewardGroupCount(activityConfig.Value, group);
                    int count = m_Data.taskGoods.FindAll((id) =>
                    {
                        if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>(id, out var cfg)) return cfg.Group == group;
                        return false;
                    }).Count;

                    while (count < max) 
                    {
                        RandomTaskGood(group);
                        count++;
                    }
                }
                TaskRewardSort();
            }

            public static void RandomTaskGood(int group, int lastGoodId = 0) 
            {
                if (m_Data.taskRewardDict.TryGetValue(group, out var list))
                {
                    var weights = new List<int>();
                    list.ForEach((id) =>
                    {
                        if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>(id, out var cfg))
                        {
                            weights.Add(cfg.Weight);
                        }
                    });

                    var randomId = SGame.Randoms.Random._R.NextWeight(weights);//RandomSystem.Instance.GetRandomID(list, weights);
                    //Debug.Log("-----------randomId:" + list[randomId]);

                    m_Data.taskGoods.Add(list[randomId]);
                    list.Remove(list[randomId]);
     
                    //list.ForEach((i) => Debug.Log("-----------剩余:" + i));

                    if (lastGoodId > 0)
                    {
                        list.Add(lastGoodId);
                        m_Data.taskGoods.Remove(lastGoodId);
                    }
                    TaskRewardSort();
                }
            }

            public static int GetTaskRewardGroupCount(int typeValue, int group) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>((c) => c.TypeValue == typeValue && c.Group == group, out var cfg))
                    return cfg.GroupShowMax;
                return 0;
            }

            public static void TaskStateSort()
            {
                m_Data.taskItems.Sort((t1, t2) => 
                {
                    if (t1.state < t2.state)
                        return -1;

                    if (t1.state == t2.state) 
                        if (t1.taskId < t2.taskId) return -1;

                    return 1;
                });
            }

            public static void TaskRewardSort() 
            {
                m_Data.taskGoods.Sort((t1, t2) =>
                {
                    return t1 < t2 ? -1 : 1;
                });
            }

            public static void RefreshTaskProgress(int taskType, int value)
            {
                if (!IsOpen()) return;
                var taskItem = m_Data.taskItems.Find((t) => t.taskType == taskType);
                if (taskItem != null)
                {
                    if (taskItem.value < taskItem.maxValue) taskItem.value += value;

                    if(!taskItem.isGet && taskItem.value >= taskItem.maxValue) 
                        taskItem.state = (int)TaskState.WAIT_GET;

                }
                TaskStateSort();
                EventManager.Instance.Trigger((int)GameEvent.TASK_UPDATE);
            }

            public static int GetCurCurrencyId() 
            {
                if(ConfigSystem.Instance.TryGet<GameConfigs.MerchantMissionRowData>(m_Data.taskItems[0].taskId, out var cfg))
                    return cfg.TaskReward(1);
                return 0;
            }

            /// <summary>
            /// 获取开启的兑换活动id
            /// </summary>
            /// <returns></returns>
            public static int GetTaskActivityId()
            {
                if (ConfigSystem.Instance.TryGets<GameConfigs.ActivityTimeRowData>((c) => c.Type == TASK_TYPE, out var list))
                {
                    foreach (var config in list)
                    {
                        if (ActiveTimeSystem.Instance.IsActive(config.Id, GameServerTime.Instance.serverTime))
                            return config.Id;
                    }
                }
                return 0;
            }


            /// <summary>
            /// 获取兑换任务活动剩余时间
            /// </summary>
            public static int GetTaskActiveTime()
            {
                int activityId = GetTaskActivityId();
                if (ConfigSystem.Instance.TryGet<GameConfigs.ActivityTimeRowData>(activityId, out var data))
                {
                    return ActiveTimeSystem.Instance.GetLeftTime(activityId, GameServerTime.Instance.serverTime);
                }
                return 0;
            }

            /// <summary>
            /// 任务是否可以领取奖励
            /// </summary>
            /// <returns></returns>
            public static bool CheckTaskIsGetReward(int id) 
            {
                if (m_Data.taskDict.TryGetValue(id, out var data)) 
                    return !data.isGet && data.value >= data.maxValue;
                
                return false;
            }

            /// <summary>
            /// 检测是否有任务可领取
            /// </summary>
            /// <returns></returns>
            public static bool CheckHasTaskIsGet() 
            {
                foreach (var taskItem in m_Data.taskItems)
                {
                    if (CheckTaskIsGetReward(taskItem.taskId)) 
                        return true;
                }
                return false;
            }

            /// <summary>
            /// 检测是否有商品可以兑换
            /// </summary>
            /// <returns></returns>
            public static bool CheckIsHasExchange() 
            {
                foreach (var id in m_Data.taskGoods)
                {
                    if (ConfigSystem.Instance.TryGet<GameConfigs.MerchantRewardRowData>(id, out var cfg)) 
                    {
                        var val = cfg.GetCostArray();
                        if (PropertyManager.Instance.CheckCount(val[1], val[2], val[0]))
                            return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// 跳转到某个格子
            /// </summary>
            /// <param name="x"></param>
            /// <param name="z"></param>
            public static void GotoGridXZ(int x, int z) 
            {
                var xMin = SceneCameraSystem.Instance.xMove.minValue;
                var xMax = SceneCameraSystem.Instance.xMove.maxValue;
                var zMin = SceneCameraSystem.Instance.zMove.minValue;
                var zMax = SceneCameraSystem.Instance.zMove.maxValue;

                var pos = GameTools.MapAgent.CellToVector(x, z);
                pos.x = Mathf.Clamp(pos.x, xMin, xMax);
                pos.z = Mathf.Clamp(pos.z, zMin, zMax);
                SceneCameraSystem.Instance.Focus(pos);
            }


            public static bool IsOpen() 
            {
                return 28.IsOpend(false) && GetTaskActiveTime() > 0;
            }

            public static void ClearData() 
            {
                m_Data.taskItems.Clear();
                m_Data.taskGoods.Clear();
                DataCenter.Instance.m_foodTipsCount = 0;
            }
        }
    }   
}

