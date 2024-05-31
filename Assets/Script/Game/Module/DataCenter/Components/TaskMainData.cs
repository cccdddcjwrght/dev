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

                    EventManager.Instance.Trigger((int)GameEvent.MAIN_TASK_UPDATE);
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

            public static bool IsReddot() 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.MainTaskRowData>(GetCurTaskCfgId(), out var cfg)) 
                {
                    var max = cfg.TaskValue(1);
                    var value = GetTaskProgress(cfg.TaskType, cfg.GetTaskValueArray());
                    return value >= max;
                }
                return false;
            }
        }
    }



}
