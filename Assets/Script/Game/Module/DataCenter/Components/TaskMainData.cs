using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SGame 
{
    [Serializable]
    public class TaskMainData 
    {
        public int cfgId;       //当前任务id
    }

    public partial class DataCenter 
    {
        public TaskMainData taskMainData = new TaskMainData();

        public static class TaskMainUtil
        {
            public static TaskMainData m_TaskMainData { get { return DataCenter.Instance.taskMainData; } }
            static bool isGet = false;

            //打开界面是否自动领取奖励
            //public static bool autoGet = false;

            /// <summary>
            /// 获取当前任务id
            /// </summary>
            public static int GetCurTaskCfgId() 
            {
                if (m_TaskMainData.cfgId == 0) 
                {
                    var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.MainTask>();
                    m_TaskMainData.cfgId = configs.Datalist(0).Value.Id;
                }
                return m_TaskMainData.cfgId;
            }

            public static int GetTaskProgress(int type, params int[] val)
            {
                switch (type)
                {
                    case (int)RecordDataEnum.MACHINE:
                        return GetMachineProgress(val[0], val[1]);
                    case (int)RecordDataEnum.TABLE:
                        return GetMachineProgress(val[0], val[1]);
                    case (int)RecordDataEnum.DECORATION:
                        return GetMachineProgress(val[0], val[1]);
                    case (int)RecordDataEnum.AREA:
                        return GetAreaProgress(val[0], val[1]);
                    case (int)RecordDataEnum.COOK:
                        return GetCookProgress(val[0], val[1]);
                    default:
                        return RecordModule.Instance.GetValue(type, (int)RecordFunctionId.TASK);
                }
            }

            /// <summary>
            /// 获取操作台对应任务进度
            /// </summary>
            /// <param name="machineId"></param>
            /// <param name="target"></param>
            /// <returns></returns>
            public static int GetMachineProgress(int id, int target)
            {
                int value = 0;
                var cfg = ConfigSystem.Instance.Find<GameConfigs.RoomMachineRowData>((c) => c.Machine == id);
                if (cfg.IsValid())
                {
                    if (Instance.roomData.current.id > cfg.Scene) return target;
                    MachineUtil.GetMachine(cfg.ID, out var worktable);
                    if (worktable != null) value = Math.Min(worktable.lv, target);
                }
                return value;
            }

            /// <summary>
            /// 获取区域解锁任务进度
            /// </summary>
            /// <returns></returns>
            public static int GetAreaProgress(int areaId, int target) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomAreaRowData>(areaId, out var cfg)) 
                {
                    if (Instance.roomData.current.id > cfg.Scene) return target;
                    if (MachineUtil.IsAreaEnable(areaId)) return target;
                }
                return 0;
            }

            public static int GetCookProgress(int cookId, int target) 
            {
                var data = CookbookUtils.GetBook(cookId);
                if (data.level >= target)
                    return target;
                return data.level;
            }

            /// <summary>
            /// 完成任务
            /// </summary>
            /// <param name="id"></param>
            public static void FinishTaskId(int id) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(id, out var cfg)) 
                {
                    var val = Utils.GetArrayList(true, cfg.GetTaskReward1Array, cfg.GetTaskReward2Array, cfg.GetTaskReward3Array);
                    val.ForEach((v) =>{ PropertyManager.Instance.Update(v[0], v[1], v[2]);});
                    m_TaskMainData.cfgId++;

                    isGet = false;
                    if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(m_TaskMainData.cfgId, out var newCfg)) 
                    {
                        if (newCfg.CountType == 1)
                            RecordModule.Instance.ClearValue(newCfg.TaskType, (int)RecordFunctionId.TASK);
                    }
                    EventManager.Instance.Trigger((int)GameEvent.MAIN_TASK_UPDATE, id);
                }
            }

            /// <summary>
            /// 判断当前任务是否显示
            /// </summary>
            /// <returns></returns>
            public static bool IsShow() 
            {
                if (32.IsOpend(false) && (m_TaskMainData.cfgId == 0 || ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(m_TaskMainData.cfgId, out var cfg)))
                    return true;                   
                return false;
            }

            static int oldTaskCfgId;
            static int oldValue;
            public static void RefreshTaskState() 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(GetCurTaskCfgId(), out var cfg))
                {
                    var max = cfg.TaskValue(1);
                    var value = GetTaskProgress(cfg.TaskType, cfg.GetTaskValueArray());
                    var state = value >= max;
                    if (isGet != state && state == true)
                    {
                        isGet = state;
                        EventManager.Instance.Trigger((int)GameEvent.MAIN_TASK_PROGRESS_CHANGE);
                    }
                    isGet = state;
                    if (oldTaskCfgId == GetCurTaskCfgId() && oldValue != value)
                    {
                        EventManager.Instance.Trigger((int)GameEvent.MAIN_TASK_PROGRESS_CHANGE);
                    }
                    oldTaskCfgId = GetCurTaskCfgId();
                    oldValue = value;
                }
                else isGet = false;
            }

            public static bool CheckIsGet() 
            {
                return isGet;
            }

            //获取当前关卡未完成的所有任务奖励
            public static List<int[]> GetRoomAllTaskReward() 
            {
                List<int[]> list = new List<int[]>();
                var roomID = DataCenter.Instance.roomData.roomID;
                var configs = ConfigSystem.Instance.Finds<GameConfigs.MainTaskRowData>((v) => v.LevelTag == roomID);
                var curCfgId = GetCurTaskCfgId();

                for (int i = 0; i < configs.Count; i++)
                {
                    var config = configs[i];
                    if (config.Id >= curCfgId) 
                    {
                        var rewardList = Utils.GetArrayList(true, config.GetTaskReward1Array, config.GetTaskReward2Array, config.GetTaskReward3Array);
                        rewardList.ForEach((r) =>
                        {
                            if (r[1] != 1) 
                            {
                                var index = list.FindIndex((v) => v[0] == r[0] && v[1] == r[1]);
                                if (index == -1) list.Add(r);
                                else list[index][2] += r[2];
                            }
                        });
                    }
                }
                return list;
            }

            public static void SkipNextRoomTask() 
            {
                var roomID = DataCenter.Instance.roomData.roomID;
                var configs = ConfigSystem.Instance.Finds<GameConfigs.MainTaskRowData>((v) => v.LevelTag == roomID);
                var nextCfgId = configs[configs.Count - 1].Id + 1;
                var curCfgId = DataCenter.Instance.taskMainData.cfgId;
                DataCenter.Instance.taskMainData.cfgId = nextCfgId;

                EventManager.Instance.Trigger((int)GameEvent.MAIN_TASK_UPDATE, -1);
            }

            public static void UpdateRoomTaskReward() 
            {
                if (DataCenter.MachineUtil.CheckAllWorktableIsMaxLv()) 
                {
                    var list = GetRoomAllTaskReward();
                    for (int i = 0; i < list.Count; i++)
                    {
                        var v = list[i];
                        PropertyManager.Instance.Update(v[0], v[1], v[2]);
                    }
                    SkipNextRoomTask();
                }
            }
        }
    }



}
