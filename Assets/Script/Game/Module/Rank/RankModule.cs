using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using Http;
using SGame.Http;

namespace SGame 
{
    public partial class DataCenter 
    {
        //自己的所有的排行类型数据
        public RankTotalOwnData rankOwnData;

        //排行榜数据
        public RankData rankData;
    }

    public class RankModule : Singleton<RankModule>
    {
        public RankData rankData { get { return DataCenter.Instance.rankData;}}
        RankTotalOwnData totalOwnRankData = DataCenter.Instance.rankOwnData;

        EventHandleContainer m_EventHandle = new EventHandleContainer();
        public void Initalize() 
        {
            //m_EventHandle += EventManager.Instance.Reg((int)GameEvent.ENTER_NEW_ROOM, () => AddRankTypeData(1));
            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GAME_START, () =>
            {
                FiberCtrl.Pool.Run(Run());
            });
            
        }

        IEnumerator Run() 
        {
            HttpPackage pkg = new HttpPackage();
            pkg.data = DataCenter.Instance.accountData.playerID.ToString();
            var result = HttpSystem.Instance.Post("http://192.168.10.109:8082/ranks", pkg.ToJson());
            yield return result;
            if (!string.IsNullOrEmpty(result.error))
            {
                Debug.LogError("ranks data fail=" + result.error);
                yield break;
            }
        }

        public void AddRankTypeData(int rankType, int value = 1) 
        {
            RankOwnData data = totalOwnRankData.rankOwnDatas.Find((r) => r.type == rankType);
            if (data == null) data = new RankOwnData();
            data.value += value;
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
            int index = rankData.rankDatas.FindIndex((r) => r.player_id == playerID);
            if (index != -1) 
            {
                rank = index + 1;
                rankItemData = rankData.rankDatas[index];
            }
        }
    }
}


