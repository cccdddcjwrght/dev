using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using System;
using System.Linq;

namespace SGame
{

    [Serializable]
    public class ClubCreateData
    {
        public string title;
        public int icon_id;
        public int frame_id;
        public long player_id;
    }

    [Serializable]
    public class ClubJoinData
    {
        public long player_id;   //玩家id
        public int id;          //俱乐部id
    }

    [Serializable]
    public class ClubGetData
    {
        public long playerId;
        public int score;
    }

    [Serializable]
    public class ClubList
    {
        public List<ClubData> list = new List<ClubData>();     //俱乐部列表
        public int id;                                          //自己俱乐部id
    }

    [Serializable]
    public class ClubData 
    {
        public int id;
        public string title;
        public int icon_id;
        public int frame_id;
        public int limit;
        public int member;
    }

    [Serializable]
    public class ClubCurrentData 
    {
        public int id;
        public string title;
        public int icon_id;
        public int frame_id;
        public int limit;
        public long creator_id;
        public List<MemberData> member_list = new List<MemberData>();

        public int acvitivy_id;
        public int ranking_id;
        public int start_time;
        public int end_time;
    }

    [Serializable]
    public class MemberData 
    {
        public int roleID;                  //人物造型
        public long player_id;               //玩家id
        public int icon_id;                 //头像id
        public int frame_id;                //头像框id
        public string name;                 //角色名称
        public int score;

        public List<Firend.FirendEquip> equips; //装备信息
    }

    [Serializable]
    public class ClubTaskDataList 
    {
        public List<ClubTaskData> taskList = new List<ClubTaskData>();
        public List<ClubRewardData> rewardList = new List<ClubRewardData>();

        public int clubId;      //当前俱乐部id
        public int currencyId;
        public int start_time;
        public int end_time;
    }

    [Serializable]
    public class ClubTaskData
    {
        public int id;        //任务id
        public int type;      //任务类型
        public int value;     //进度
        public int max;       //目标值
        public int score;     //积分

        public int finishNum; //当前任务完成次数
        public int limitNum;  //完成上限次数
    }

    [Serializable]
    public class ClubRewardData 
    {
        public int id;
        public int target;  //需要兑换的奖励的值
        public bool isGet;  //是否领取过
        public bool isBuff; //是否有buff奖励
    }


    public partial class DataCenter
    {
        public ClubTaskDataList clubTaskList = new ClubTaskDataList();

        public static class ClubUtil
        {
            //创建俱乐部需要的钻石
            public static readonly int CREATE_DIAMOND = GlobalDesginConfig.GetInt("club_create_value");
            //俱乐部任务数量
            public static readonly int TASK_MAXNUM = GlobalDesginConfig.GetInt("club_task_num");

            public static ClubTaskDataList m_taskDataList { get { return Instance.clubTaskList; } }

            public static ClubList clubList;
            public static ClubCurrentData currentData;

            public static void InitData() 
            {
                EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) =>
                {
                    RequestExcuteSystem.Instance.ClubListDataReq().Start();
                });
                EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, RefreshTaskProgress);
            }


            /// <summary>
            /// 重置数据
            /// </summary>
            public static void ResetData() 
            {
                //更换俱乐部或者活动换期时|重置数据
                if (m_taskDataList.clubId != currentData.id ||
                    m_taskDataList.end_time != currentData.end_time) 
                {
                    m_taskDataList.taskList.Clear();
                    m_taskDataList.rewardList.Clear();
                    m_taskDataList.clubId = currentData.id;
                    m_taskDataList.currencyId = FindClubCurrenyId();
                    //m_taskDataList.score = 0;
                    m_taskDataList.start_time = currentData.start_time;
                    m_taskDataList.end_time = currentData.end_time;

                    InitClubTaskData();
                    InitClubRewardData();
                }
            }

            /// <summary>
            /// 初始化俱乐部任务
            /// </summary>
            public static void InitClubTaskData() 
            {
                List<int> taskIdList = new List<int>();
                var list = ConfigSystem.Instance.Finds<ClubTaskRowData>((c) => c.Weight == -1);
                taskIdList = list.Select((c) => c.Id).ToList();

                var randomCfgs = ConfigSystem.Instance.Finds<ClubTaskRowData>((c) => c.Weight > 0);
                var randomIds = randomCfgs.Select((c) => c.Id).ToList();
                var weights = randomCfgs.Select((c) => c.Weight).ToList();

                //需要随机的任务数量
                int randomCount = TASK_MAXNUM - list.Count;
                var rand = new Randoms.Random();
                var ws = rand.NextWeights(weights, randomCount, true);
                for (int i = 0; i < ws.Length; i++)
                    taskIdList.Add(randomIds[ws[i]]);

                taskIdList.ForEach((id) =>
                {
                    if (ConfigSystem.Instance.TryGet<ClubTaskRowData>(id, out var cfg)) 
                    {
                        m_taskDataList.taskList.Add(new ClubTaskData()
                        {
                            id = id,
                            value = 0,
                            max = cfg.Task,
                            type =cfg.Marker,
                            score = cfg.Reward(1),
                            finishNum = 0,
                            limitNum = cfg.Repeat
                        });
                    }
                });
            }

            /// <summary>
            /// 初始化俱乐部任务奖励
            /// </summary>
            public static void InitClubRewardData() 
            {
                int periods = GetClubPeriods();
                var list = GetCurClubActivityRewards(periods);
                if (list != null) 
                {
                    list.ForEach((cfg) => m_taskDataList.rewardList.Add(new ClubRewardData()
                    {
                        id = cfg.Id,
                        target = cfg.Target(1),
                        isGet = false,
                        isBuff = cfg.BuffLength > 0,
                    }));
                }
            }

            public static void RefreshTaskProgress(int type, int value) 
            {
                var taskData = m_taskDataList.taskList.Find((t) => t.type == type);
                if (taskData == null) return; 
                if (taskData.limitNum > 0 && taskData.finishNum >= taskData.limitNum)
                    return;
                
                taskData.value += value;
                if (taskData.value >= taskData.max) 
                {
                    taskData.finishNum++;
                    taskData.value %= taskData.max;
                    PropertyManager.Instance.Update(1, m_taskDataList.currencyId, taskData.score);
                    //m_taskDataList.score += taskData.score;
                }
            }

            public static ClubRewardData GetClubReward(int rewardId) 
            {
                return m_taskDataList.rewardList.Find((r) => r.id == rewardId);
            }

            /// <summary>
            /// 检测是否在俱乐部中
            /// </summary>
            public static bool CheckIsInClub() 
            {
                return clubList?.id > 0;
            }

            /// <summary>
            /// 获取当前俱乐部列表
            /// </summary>
            /// <returns></returns>
            public static List<ClubData> GetClubList() 
            {
                return clubList?.list;
            }

            /// <summary>
            /// 获取当前俱乐部信息
            /// </summary>
            public static ClubCurrentData GetClubCurrentData() 
            {
                return currentData;
            }

            public static long GetCreatePlayerId() 
            {
                return currentData.creator_id;
            }

            public static List<MemberData> GetClubMemberList() 
            {
                return currentData?.member_list;
            }

            public static int GetClubPeriods() 
            {
                if (currentData != null)
                    return currentData.ranking_id;
                return 0;
            }

            public static List<ClubTaskData> GetClubTaskData() 
            {
                return m_taskDataList.taskList;
            }

            /// <summary>
            /// 查找对应名称俱乐部
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public static List<ClubData> GetFindClubList(string name) 
            {
                var list = clubList.list?.FindAll((s) => s.title.Contains(name));
                return list;
            }

            public static List<ClubRewardRowData> GetCurClubActivityRewards(int periods) 
            {
                if (ConfigSystem.Instance.TryGets<ClubRewardRowData>((cfg)=> cfg.Periods == periods, out var list))
                    return list;
                return default;
            }

            /// <summary>
            /// 获取普通奖励
            /// </summary>
            /// <param name="cfg"></param>
            /// <returns></returns>
            public static List<int[]> GetItemReward(ClubRewardRowData cfg) 
            {
                var list = new List<int[]>();
                if (cfg.Item1Length > 0)
                    list.Add(cfg.GetItem1Array());
                if (cfg.Item2Length > 0)
                    list.Add(cfg.GetItem2Array());
                return list;
            }

            /// <summary>
            /// 获取当前俱乐部活动结束时间
            /// </summary>
            public static int GetResidueTime() 
            {
                var time = currentData.end_time - GameServerTime.Instance.serverTime;
                if (time > 0) return time;
                return 0;
            }

            /// <summary>
            /// 查找当前活动货币
            /// </summary>
            /// <returns></returns>
            public static int FindClubCurrenyId() 
            {
                var cfg = ConfigSystem.Instance.Find<ClubRewardRowData>((c) => c.Periods == GetClubPeriods());
                return cfg.Target(1);
            }

            public static int GetClubCurrencyId() 
            {
                return m_taskDataList.currencyId;
            }

            public static int GetClubTotalProgress() 
            {
                var list = GetCurClubActivityRewards(GetClubPeriods());
                return list[list.Count - 1].Target(1);
            }

            /// <summary>
            /// 获取俱乐部头像配置
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static ClubHeadRowData GetClubHeadCfg(int id) 
            {
                if (ConfigSystem.Instance.TryGet<ClubHeadRowData>(id, out var cfg))
                    return cfg;
                return default;
            }


            /// <summary>
            /// 获取俱乐部头像框配置
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static ClubFrameRowData GetClubFrameCfg(int id) 
            {
                if (ConfigSystem.Instance.TryGet<ClubFrameRowData>(id, out var cfg))
                    return cfg;
                return default;
            }
        }
    }
}

