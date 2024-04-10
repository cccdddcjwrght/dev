using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGame 
{
    partial class DataCenter 
    {
        public ReputationData reputationData = new ReputationData();
        

        public static class ReputationUtils 
        {
            public static ReputationData _data = Instance.reputationData;

            /// <summary>
            /// 获取随机三个好评buff
            /// </summary>
            public static List<int> GetRandomBuffList()
            {
                var roomLikeConfigs = ConfigSystem.Instance.LoadConfig<GameConfigs.RoomLike>();
                int len = roomLikeConfigs.DatalistLength;
                var weights = new List<int>();
                for (int i = 0; i < len; i++)
                    weights.Add(roomLikeConfigs.Datalist(i).Value.Weight);
                var rand = new Randoms.Random();
                var ws = rand.NextWeights(weights, 3, true).GroupBy(v => v);
                var rets = new List<GameConfigs.RoomLikeRowData>();
                foreach (var item in ws)
                {
                    var exs = ConfigSystem.Instance.Finds<GameConfigs.RoomLikeRowData>(r => r.LikeId == item.Key + 1);
                    rand.NextItem(exs, item.Count(), ref rets);
                }
                return rets.Select(s => s.LikeId).ToList();
            }

            /// <summary>
            /// 好评进度满了随机选择一个buff
            /// </summary>
            public static void RandomSelect() 
            {
                if (_data.randomBuffs.Count <= 0)
                    _data.randomBuffs = GetRandomBuffList();

                int random = Random.Range(0, _data.randomBuffs.Count);
                _data.cfgId = _data.randomBuffs[random];
                int buffDuration = 0;
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(_data.cfgId, out var data))
                    buffDuration = data.BuffDuration;
                _data.endTime = GameServerTime.Instance.serverTime + buffDuration;
                AddBuff(_data.cfgId);
            }

            public static void AddBuff(int likeId) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(likeId, out var data)) 
                {
                    int validTime = GetBuffValidTime();
                    int duration = validTime > 0 ? validTime : data.BuffDuration;

                    EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(data.BuffId, data.BuffValue, 0, duration)
                    { from = (int)EnumFrom.RoomLike });
                }
                EventManager.Instance.Trigger((int)GameEvent.ROOM_BUFF_ADD);
            }

            public static int GetBuffValidTime() 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(_data.cfgId, out var roomLikeData)) 
                {
                    var endTime = _data.endTime;
                    if (endTime > GameServerTime.Instance.serverTime) return endTime - GameServerTime.Instance.serverTime;
                }
                return 0;
            }


            public static void Reset() 
            {
                _data.cfgId     = 0;
                _data.progress  = 0;
                _data.endTime   = 0;
            }

            public static void Clear() 
            {
                Reset();
                _data.randomBuffs.Clear();
            }
        }

    }

    [Serializable]
    public class ReputationData
    {
        //随机的奖励buff
        public List<int> randomBuffs = new List<int>();

        //选择的buff
        public int cfgId;

        //结束时间
        public int endTime;
        /// <summary>
        /// 好评进度
        /// </summary>
        public int progress;
    }

}


