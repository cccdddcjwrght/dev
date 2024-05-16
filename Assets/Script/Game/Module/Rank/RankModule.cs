using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using Http;
using SGame.Http;

namespace SGame 
{
    public enum RankScoreEnum 
    {
        BOX     = 3,
        WORKER  = 4,
    }

    public partial class DataCenter 
    {
        //自己的排行标识值
        public RankScore rankScore = new RankScore();

        //排行榜数据
        public RankData rankData = new RankData();

        public RankCacheData rankCacheData = new RankCacheData();
    }

    public class RankModule : Singleton<RankModule>
    {
        //对应活动表
        public const int RANK_ACTIVE_ID = 3;

        public RankData rankData { get { return DataCenter.Instance.rankData;}}
        RankScore rankScore = DataCenter.Instance.rankScore;

        public RankPanelData rankPanelData = new RankPanelData();
        EventHandleContainer m_EventHandle = new EventHandleContainer();
        public void Initalize() 
        {
            m_EventHandle += EventManager.Instance.Reg<int, int>((int)GameEvent.RANK_ADD_SCORE, AddScoreTypeData);
            m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.BEFORE_ENTER_ROOM, (s) =>
            {
                DataCenter.Instance.rankCacheData.reddot = true;
                ReqRankList().Start();
            });
            
            m_EventHandle += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) =>
            {
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
                    EventManager.Instance.Trigger((int)GameEvent.RANK_ADD_SCORE, (int)RankScoreEnum.BOX,  count);  
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

            rankPanelData = JsonUtility.FromJson<RankPanelData>(pkg.data);
            if (DataCenter.Instance.rankCacheData.startTime != rankPanelData.ids[0].begin_time) 
            {
                DataCenter.Instance.rankCacheData.startTime = rankPanelData.ids[0].begin_time;
                ClearRankScore();//清除自己排行标识数据
            }

            Debug.Log("ranks data:" + result.data);
            if (rankPanelData.rewards?.Length > 0) 
            {
                DataCenter.Instance.rankCacheData.reddot = true;
                DataCenter.Instance.rankCacheData.rewards = rankPanelData.rewards;
            }

            if (popReward && DataCenter.Instance.rankCacheData.rewards?.Length > 0) 
            {
                for (int i = 0; i < DataCenter.Instance.rankCacheData.rewards.Length; i++)
                {
                    var rewardData = DataCenter.Instance.rankCacheData.rewards[i];
                    if (ConfigSystem.Instance.TryGet<GameConfigs.RankConfigRowData>((r) => r.RankingId == rewardData.id, out var data))
                        OpenResultView(data.RankingMarker, rewardData.rank);
                }
                DataCenter.Instance.rankCacheData.rewards = null;
            }
            //EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        public IEnumerator ReqRankData(bool cancelReddot = false)
        {
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
                if(cancelReddot) "tips_ranking_1".Tips();
                Debug.LogWarning("rank data fail=" + result.error);
                yield break;
            }

            Debug.Log("rank data:" + result.data);
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(pkg.data);

            if(cancelReddot)
                DataCenter.Instance.rankCacheData.reddot = false;

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

        public void OpenResultView(int marker, int rank) 
        {
            DelayExcuter.Instance.DelayOpen("rankresult", "mainui", args: new RankUIParam() { marker = marker, rank = rank });
        }
    }
}


