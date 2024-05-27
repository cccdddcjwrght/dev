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
        public long player_id;   //���id
        public int id;          //���ֲ�id
    }

    [Serializable]
    public class ClubGetData
    {
        public long playerId;
        public int score;
    }

    public class ClubKickData 
    {
        public long player_id;
        public long user_id;
    }

    [Serializable]
    public class ClubList
    {
        public List<ClubData> list = new List<ClubData>();     //���ֲ��б�
        public int id;                                          //�Լ����ֲ�id
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
        public int roleID;                  //��������
        public long player_id;               //���id
        public int icon_id;                 //ͷ��id
        public int frame_id;                //ͷ���id
        public string name;                 //��ɫ����
        public int score;

        public List<Firend.FirendEquip> equips; //װ����Ϣ
    }

    [Serializable]
    public class ClubTaskDataList 
    {
        public List<ClubTaskData> taskList = new List<ClubTaskData>();
        public List<ClubRewardData> rewardList = new List<ClubRewardData>();

        public int clubId;      //��ǰ���ֲ�id
        public int currencyId;
        public int oldValue;    //��¼���ϴδ�Ŀ�����Ļ���
        public int start_time;
        public int end_time;
    }

    [Serializable]
    public class ClubTaskData
    {
        public int id;        //����id
        public int type;      //��������
        public int value;     //����
        public int max;       //Ŀ��ֵ
        public int score;     //����

        public int finishNum; //��ǰ������ɴ���
        public int limitNum;  //������޴���
    }

    [Serializable]
    public class ClubRewardData 
    {
        public int id;
        public int target;  //��Ҫ�һ��Ľ�����ֵ
        public bool isGet;  //�Ƿ���ȡ��
        public bool isBuff; //�Ƿ���buff����
    }


    public partial class DataCenter
    {
        public ClubTaskDataList clubTaskList = new ClubTaskDataList();

        public static class ClubUtil
        {
            //�������ֲ���Ҫ����ʯ
            public static readonly int CREATE_DIAMOND = GlobalDesginConfig.GetInt("club_create_value");
            //���ֲ���������
            public static readonly int TASK_MAXNUM = GlobalDesginConfig.GetInt("club_task_num");

            public static ClubTaskDataList m_taskDataList { get { return Instance.clubTaskList; } }

            public static ClubList clubList;
            public static ClubCurrentData currentData;

            public static void InitData() 
            {
                EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) =>
                {
                    RequestExcuteSystem.Instance.ClubListDataReq().Start();
                    ResetFirstLogin();
                    LoadGetRewardBuff();
                });

                //��¼������ɴ���
                EventManager.Instance.Reg<int>((int)GameEvent.ORDER_PERFECT, (id) =>
                {
                    EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RankScoreEnum.PERFECT, 1);
                });

                //��¼������ɴ���
                EventManager.Instance.Reg<int>((int)GameEvent.ORDER_INSTANT, (id) =>
                {
                    EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RankScoreEnum.IMMEDIATE, 1);
                });

                EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, RefreshTaskProgress);
            }


            /// <summary>
            /// �������
            /// </summary>
            public static void ClearData() 
            {
                //�������ֲ����߻����ʱ|�������
                if (m_taskDataList.end_time != currentData.end_time) 
                {
                    m_taskDataList.taskList.Clear();
                    m_taskDataList.rewardList.Clear();
                    m_taskDataList.clubId = currentData.id;
                    PropertyManager.Instance.GetGroup(1).SetNum(m_taskDataList.currencyId, 0);
                    m_taskDataList.currencyId = FindClubCurrenyId();
                    m_taskDataList.oldValue = 0;
                    m_taskDataList.start_time = currentData.start_time;
                    m_taskDataList.end_time = currentData.end_time;

                    InitClubTaskData();
                    InitClubRewardData();
                }
            }

            /// <summary>
            /// ��������
            /// </summary>
            public static void ResetData() 
            {
                m_taskDataList.taskList.ForEach((t) => 
                {
                    if(t.type != (int)RankScoreEnum.FIRST_LOGIN) t.value = 0;
                });
                m_taskDataList.rewardList.ForEach((t) => t.isGet = false);
                m_taskDataList.oldValue = 0;
                PropertyManager.Instance.GetGroup(1).SetNum(m_taskDataList.currencyId, 0);
                EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData() { from = (int)EnumFrom.Club });
            }

            public static void ResetFirstLogin() 
            {
                bool isFirst = Utils.IsFirstLoginInDay("club.firstLogin");
                if (isFirst) 
                {
                    m_taskDataList.taskList.ForEach((t) =>
                    {
                        if (t.type == (int)RankScoreEnum.FIRST_LOGIN)
                            t.value = 0;
                    });
                }
            }

            /// <summary>
            /// ��ʼ�����ֲ�����
            /// </summary>
            public static void InitClubTaskData() 
            {
                List<int> taskIdList = new List<int>();
                var list = ConfigSystem.Instance.Finds<ClubTaskRowData>((c) => c.Weight == -1);
                taskIdList = list.Select((c) => c.Id).ToList();

                var randomCfgs = ConfigSystem.Instance.Finds<ClubTaskRowData>((c) => c.Weight > 0);
                var randomIds = randomCfgs.Select((c) => c.Id).ToList();
                var weights = randomCfgs.Select((c) => c.Weight).ToList();

                //��Ҫ�������������
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
            /// ��ʼ�����ֲ�������
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
                //�ھ��ֲ��ڼ�¼����
                if (!CheckIsInClub()) return;

                var taskData = m_taskDataList.taskList.Find((t) => t.type == type);
                if (taskData == null) return; 
                if (taskData.limitNum > 0 && taskData.finishNum >= taskData.limitNum)
                    return;
                
                taskData.value += value;
                if (taskData.value >= taskData.max) 
                {
                    taskData.finishNum++;
                    PropertyManager.Instance.Update(1, m_taskDataList.currencyId, taskData.score);
                    if (taskData.limitNum > 0 && taskData.finishNum >= taskData.limitNum)
                        return;
                    taskData.value %= taskData.max;
                }
            }

            public static ClubRewardData GetClubReward(int rewardId) 
            {
                return m_taskDataList.rewardList.Find((r) => r.id == rewardId);
            }

            /// <summary>
            /// ����Ƿ��ھ��ֲ���
            /// </summary>
            public static bool CheckIsInClub() 
            {
                return clubList?.id > 0;
            }

            /// <summary>
            /// ��ȡ��ǰ���ֲ��б�
            /// </summary>
            /// <returns></returns>
            public static List<ClubData> GetClubList() 
            {
                return clubList?.list;
            }

            /// <summary>
            /// ��ȡ��ǰ���ֲ���Ϣ
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
            /// ���Ҷ�Ӧ���ƾ��ֲ�
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
            /// ��ȡ��ͨ����
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
            /// ��ȡ��ǰ���ֲ������ʱ��
            /// </summary>
            public static int GetResidueTime() 
            {
                var time = currentData.end_time - GameServerTime.Instance.serverTime;
                if (time > 0) return time;
                return 0;
            }

            /// <summary>
            /// ���ҵ�ǰ�����
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
            /// ������Ƿ��ھ��ֲ���û�������������ݣ����ⱻ�ߵ�����buff����
            /// </summary>
            public static void DetectionResetData() 
            {
                if (!CheckIsInClub()) ResetData();
            }

            /// <summary>
            /// ��������ȡ�Ľ���buff
            /// </summary>
            public static void LoadGetRewardBuff() 
            {
                m_taskDataList.rewardList.ForEach((r) =>
                {
                    if (r.isGet && r.isBuff && ConfigSystem.Instance.TryGet<GameConfigs.ClubRewardRowData>(r.id, out var cfg))
                    {
                        EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(cfg.Buff(0), cfg.Buff(1), 0, GetResidueTime()) { from = (int)EnumFrom.Club });
                    }
                });
            }

            /// <summary>
            /// ��ȡ���ֲ�ͷ������
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
            /// ��ȡ���ֲ�ͷ�������
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static ClubFrameRowData GetClubFrameCfg(int id) 
            {
                if (ConfigSystem.Instance.TryGet<ClubFrameRowData>(id, out var cfg))
                    return cfg;
                return default;
            }


            /// <summary>
            /// �����ֲ��Ƿ��н�������ȡ
            /// </summary>
            /// <returns></returns>
            public static bool CheckIsGetReward() 
            {
                foreach (var data in m_taskDataList.rewardList)
                {
                    if (!data.isGet && PropertyManager.Instance.CheckCount(GetClubCurrencyId(), data.target, 1))
                        return true;
                }
                return false;
            }

            public static MemberData GetMemberData(long playerID) 
            {
                return currentData.member_list.Find((m) => m.player_id == playerID);
            }

            public static void RemoveMember(long playerID) 
            {
                foreach (var data in currentData.member_list)
                {
                    if (data.player_id == playerID) 
                    {
                        currentData.member_list.Remove(data);
                        break;
                    }
                }
                EventManager.Instance.Trigger((int)GameEvent.CLUB_MEMBER_REMOVE);
            }

            public static void RecordValue(int value) 
            {
                m_taskDataList.oldValue = value;
            }


            public static RoleData GetRoleData(long playerID)
            {
                var item = GetMemberData(playerID);
                List<BaseEquip> equips = new List<BaseEquip>();

                foreach (var e in item.equips)
                {
                    BaseEquip equip = new BaseEquip()
                    {
                        cfgID = e.id,
                        level = e.level,
                        quality = e.quality,
                    };
                    equip.Refresh();
                    equips.Add(equip);
                }

                RoleData roleData = new RoleData()
                {
                    roleTypeID = item.roleID,
                    isEmployee = true,
                    equips = equips
                };
                return roleData;
            }
        }
    }
}

