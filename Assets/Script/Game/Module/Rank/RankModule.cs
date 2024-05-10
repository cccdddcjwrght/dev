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
            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.ENTER_GAME, () =>
            {
                FiberCtrl.Pool.Run(ReqRankList());
                FiberCtrl.Pool.Run(ReqRankData());
            });
        }

        IEnumerator ReqRankList() 
        {
            HttpPackage pkg = new HttpPackage();
            pkg.data = DataCenter.Instance.accountData.playerID.ToString();
            var result = HttpSystem.Instance.Post("ranks", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("ranks data fail=" + result.error);
                yield break;
            }
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);

            rankPanelData = JsonUtility.FromJson<RankPanelData>(pkg.data);
            //Debug.Log("json:" + result.data);
            if (rankPanelData.rewards?.Length > 0) 
            {
                var rewardData = rankPanelData.rewards[0];
                if(ConfigSystem.Instance.TryGet<GameConfigs.RankConfigRowData>((r)=> r.RankingId == rewardData.id, out var data))
                {
                    var rankConfig = GetRankConfig(data.RankingMarker, rewardData.rank);
                    var items = Utils.GetArrayList(rankConfig.GetReward1Array, rankConfig.GetReward2Array, rankConfig.GetReward3Array);
                    for (int i = 0; i < items.Count; i++)
                        PropertyManager.Instance.Update(items[i][0], items[i][1], items[i][2]);
                    //弹出排行奖励
                    OpenResultView(data.RankingMarker, rewardData.rank);
                    ClearRankScore();
                }
            }

            EventManager.Instance.Trigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        public IEnumerator ReqRankData(bool cancelReddot = false)
        {
            if (cancelReddot) rankPanelData.reddot = false;

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
                Debug.LogError("rank data fail=" + result.error);
                yield break;
            }
            pkg = JsonUtility.FromJson<HttpPackage>(result.data);
            DataCenter.Instance.rankData = JsonUtility.FromJson<RankData>(pkg.data);
            //Debug.Log("json:" + result.data);
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
            var rankData = rankPanelData.ids[0];
            if (ConfigSystem.Instance.TryGet<RankConfigRowData>((r) => (r.RankingMarker == rankData.marker
            && r.RankingId == rankData.id), out var data))
            {
                return data;
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
            return rankPanelData.reddot;
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
            rankScore.tips      = 0;
            rankScore.boxs      = 0;
            rankScore.workers   = 0;
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


