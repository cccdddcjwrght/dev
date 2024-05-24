using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using Http;
using SGame.Http;
using System;
using System.Linq;

namespace SGame 
{
    public enum RankScoreEnum 
    {
        CHAPTER     = 1,    //完成章节次数
        LEVEL       = 2,    //完成关卡次数
        BOX         = 3,    //打开场景箱子数量
        WORKER      = 4,    //雇佣工人数量
        SELL        = 5,    //出售商品数量
        SERVE       = 6,    //服务客户人数
        TIP         = 7,    //收集客人小费次数
        EQUIP_BOX   = 8,    //打开装备宝箱数量
        AD          = 9,    //观看广告次数
        EQUIP_LEVEL = 10,   //升级装备次数
        EQUIP_STAGE = 11,   //进阶装备次数
        PET         = 12    //宠物进化次数
    }

    public partial class DataCenter 
    {
        //自己的排行标识�?
        public RankScore rankScore = new RankScore();

        //排行榜数�?
        public RankData rankData = new RankData();

        public RankCacheData rankCacheData = new RankCacheData();
    }

    public class RankModule : Singleton<RankModule>
    {
        //对应活动�?
        public const int RANK_ACTIVE_ID = 3;

        public RankData rankData { get { return DataCenter.Instance.rankData;}}
        RankScore rankScore = DataCenter.Instance.rankScore;

        public RankPanelData rankPanelData = new RankPanelData();
        EventHandleContainer m_EventHandle = new EventHandleContainer();

        bool isLoop = false;

        public void Initalize() 
        {
            m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RECORD_PROGRESS, AddScoreTypeData);
            m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) =>
            {
                SetTimer();
                ReqRankList(true).Start();
                ReqRankData().Start();
            });


            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, () =>
            {
                var ws = DataCenter.MachineUtil.GetWorktables((w) => !w.isTable);
                int scene = DataCenter.Instance.roomData.current.id;
                if (ConfigSystem.Instance.TryGets<GameConfigs.RoomMachineRowData>((r) => r.Scene == scene && r.Type == 0 && r.Nowork == 0, out var list)) 
                {
                    int count = 0;
                    ws.ForEach((w) => count += w.stations.Count - 1);
                    count = list.Count - count;
                    EventManager.Instance.Trigger((int)GameEvent.RECORD_PROGRESS, (int)RankScoreEnum.BOX,  count);  
                }
            });
        }


        public IEnumerator ReqRankList(bool popReward = false) 
        {
            HttpPackage pkg = new HttpPackage();
            pkg.data = DataCenter.Instance.accountData.playerID.ToString();
            var result = HttpSystem.Instance.Post("ranks", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogWarning("ranks data fail=" + result.error);
                yield break;
            }
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);

            Debug.LogWarning("ranks data:" + result.data);
            rankPanelData = JsonUtility.FromJson<RankPanelData>(pkg.data);

            if (rankPanelData.ids?.Length > 0) 
            {
                Debug.Log("rank cache time:" + DataCenter.Instance.rankCacheData.startTime + "rank begin time:" + rankPanelData.ids[0].begin_time);
                rankPanelData.ids.Foreach((r) => Debug.Log(string.Format("cur rankId:{0},marker:{1}, startTime:{2}, endTime:{3}", r.id, r.marker, r.begin_time, r.end_time)));
            }
                
            if (rankPanelData.ids?.Length > 0 && DataCenter.Instance.rankCacheData.startTime != rankPanelData.ids[0].begin_time) 
            {
                DataCenter.Instance.rankCacheData.startTime = rankPanelData.ids[0].begin_time;
                ClearRankScore();//清除自己排行标识数据
                Debug.Log("rank clear data------------");
            }

            if (rankPanelData.rewards?.Length > 0) 
            {
                var list = rankPanelData.rewards.ToList();
                if (DataCenter.Instance.rankCacheData.rewards?.Length > 0)
                    list.AddRange(DataCenter.Instance.rankCacheData.rewards?.ToList());
                DataCenter.Instance.rankCacheData.rewards = list.ToArray();
                DataCenter.Instance.rankCacheData.rewards.Foreach((r) => Debug.Log(string.Format("------save rank reward----type��{0}rankindex:{1}", r.id, r.rank)));
            } 
 
            if (popReward && DataCenter.Instance.rankCacheData.rewards?.Length > 0) 
            {
                DataCenter.Instance.rankCacheData.rewards.Foreach((r) => Debug.Log(string.Format("------open rank reward----type��{0}rankindex:{1}", r.id, r.rank)));
                OpenResultView(DataCenter.Instance.rankCacheData.rewards.ToArray());
                DataCenter.Instance.rankCacheData.rewards = null;
            }

            EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        public IEnumerator ReqRankData(bool isTip = false)
        {
            DataCenter.Instance.rankCacheData.reddot = Utils.IsFirstLoginInDay("rank.rankfirst");
            HttpPackage pkg = new HttpPackage();
            RankScoreEx score = new RankScoreEx()
            {
                tips = rankScore.tips,
                boxs = rankScore.boxs,
                workers = rankScore.workers,
                player_id = DataCenter.Instance.accountData.playerID,
            };
            pkg.data = JsonUtility.ToJson(score);
            var result = HttpSystem.Instance.Post("rank", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                if(isTip) "tips_ranking_1".Tips();
                Debug.LogWarning("rank data fail=" + result.error);
                yield break;
            }

            Debug.LogWarning("rank data:" + result.data);
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(pkg.data);

            EventManager.Instance.Trigger((int)GameEvent.RANK_UPDATE);
        }


        public void AddScoreTypeData(int marker, int value) 
        {
            if (marker == (int)RankScoreEnum.BOX)
                rankScore.boxs += value;
            else if (marker == (int)RankScoreEnum.WORKER)
                rankScore.workers += value;
        }

        //获取当前排行对应配置
        public RankConfigRowData GetCurRankConfig()
        {
            if (rankPanelData.ids?.Length > 0) 
            {
                var rankData = rankPanelData.ids[0];
                if (ConfigSystem.Instance.TryGet<RankConfigRowData>((r) => (r.RankingMarker == rankData.marker
                && r.RankingId == rankData.id), out var data))
                {
                    return data;
                }
            }
            return default;
        }

        public RankConfigRowData GetRankConfig(int marker, int rank) 
        {
            if(ConfigSystem.Instance.TryGet<RankConfigRowData>((r) => (r.RankingMarker == marker 
            && (rank >= r.RankingRange(0) && rank <= r.RankingRange(1))), out var data))
            {
                return data;
            }
            return default;
        }

        public void GetSelfData(out RankItemData rankItemData, out int rank) 
        {
            rankItemData = default; rank = -1;
            var playerID = DataCenter.Instance.accountData.playerID;
            int index = rankData.list.FindIndex((r) => r.player_id == playerID);
            if (index != -1) 
            {
                rank = index + 1;
                rankItemData = rankData.list[index];
            }
        }

        public int GetScoreValue(RankScore rankScore) 
        {
            switch (rankScore.type)
            {
                case (int)RankScoreEnum.BOX:
                    return rankScore.boxs;
                case (int)RankScoreEnum.WORKER:
                    return rankScore.workers;
                default:
                    break;
            }
            return 0;
        }

        public bool IsRedDot() 
        {
            return DataCenter.Instance.rankCacheData.reddot;
        }

        public bool IsOpen() 
        {
            return GetRankTime() > 0;
        }

        public int GetRankTime() 
        {
            if(rankPanelData.ids == null) return 0;
            return rankPanelData.ids[0].end_time - GameServerTime.Instance.serverTime;
        }   

        public RankItemData GetRankData(long playerId) 
        {
            return rankData.list.Find((r) => r.player_id == playerId);
        }

        public void ClearRankScore() 
        {
            DataCenter.Instance.rankScore.tips      = 0;
            DataCenter.Instance.rankScore.boxs      = 0;
            DataCenter.Instance.rankScore.workers   = 0;
            DataCenter.Instance.rankCacheData.reddot = true;
        }


        public RoleData GetRoleData(long playerID) 
        {
            var item = GetRankData(playerID);
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

        public void OpenResultView(RankReward[] rewards) 
        {
            DelayExcuter.Instance.DelayOpen("rankresult", "mainui", args: new UIParam() { Value = rewards });
        }

        public void SetTimer()
        {
            if (isLoop) return;
            isLoop = true;

            new Action(() => ReqRankList().Start()).CallWhenQuit();
            0.Loop(() =>
            {
                ReqRankList().Start();
            }, () => true, 5000, 5000);
        }
    }
}


