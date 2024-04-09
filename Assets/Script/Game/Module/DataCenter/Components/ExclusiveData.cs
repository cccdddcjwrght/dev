using GameConfigs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame {
    partial class DataCenter
    {
        public ExclusiveData exclusiveData = new ExclusiveData();

        public static class ExclusiveUtils 
        {
            public readonly static int EXCLUSIVE_FORM = (int)EnumFrom.Exclusive;
            public static ExclusiveData _data = Instance.exclusiveData;

            public static void Init() 
            {

            }

            /// <summary>
            /// 选择开局奖励
            /// </summary>
            /// <param name="exclusiveId"></param>
            public static void SelectRewardBuff(int exclusiveId) 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(exclusiveId, out var exclusive))
                {
                    var scene = DataCenter.Instance.GetUserData().scene;
                    if (!_data.rooms.Contains(scene))
                        _data.rooms.Add(scene);
                    _data.cfgId = exclusiveId;
                    _data.endTime = GameServerTime.Instance.serverTime + exclusive.BuffDuration;

                    if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(exclusive.BuffId, out var buffData))
                    {
                        if (buffData.Attribute == (int)EnumAttribute.LevelGold)
                            PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.GOLD, exclusive.BuffValue);
                        else
                            EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(exclusive.BuffId, exclusive.BuffValue, 0, exclusive.BuffDuration) { from = EXCLUSIVE_FORM });

                        EventManager.Instance.Trigger((int)GameEvent.ROOM_START_BUFF);
                    }
                }
            }

            /// <summary>
            /// 获取随机三个开局奖励
            /// </summary>
            public static List<int> GetRandomRewardList() 
            {
                var exclusiveConfigs = ConfigSystem.Instance.LoadConfig<GameConfigs.RoomExclusive>();
                int len = exclusiveConfigs.DatalistLength;
                var weights = new List<int>();
                for (int i = 0; i < len; i++)
                    weights.Add(exclusiveConfigs.Datalist(i).Value.Weight);
                var rand = new Randoms.Random();
                var ws = rand.NextWeights(weights, 3, true).GroupBy(v => v);
                var rets = new List<RoomExclusiveRowData>();
                foreach (var item in ws)
                {
                    var exs = ConfigSystem.Instance.Finds<GameConfigs.RoomExclusiveRowData>(r => r.ExclusiveId == item.Key + 1);
                    rand.NextItem(exs, item.Count(), ref rets);
                }
                return rets.Select(s => s.ExclusiveId).ToList();
            }


            /// <summary>
            /// 检测buff是否生效中
            /// </summary>
            /// <returns></returns>
            public static bool CheckBuffTakeEffect() 
            {
                bool isTakeEffect = false;
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(_data.cfgId, out var data)) 
                {
                    double endTime = _data.endTime;
                    isTakeEffect = endTime > GameServerTime.Instance.serverTime;
                }
                return isTakeEffect;
            }

            /// <summary>
            /// 获取buff剩余时间
            /// </summary>
            /// <returns></returns>
            public static int GetBuffResiduTime() 
            {
                if (!CheckBuffTakeEffect()) return -1;
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(_data.cfgId, out var data)) 
                {
                    int endTime = (int)_data.endTime;
                    return Math.Max(endTime - GameServerTime.Instance.serverTime, 0);
                }
                return -1;
            }

            public static void Clear() 
            {
                _data.randomBuffs.Clear();
                _data.cfgId = 0;
            }
        }
    }

    [Serializable]
    public class ExclusiveData
    {
        //已选择奖励的关卡id
        public List<int> rooms = new List<int>();

        //随机的奖励buff
        public List<int> randomBuffs = new List<int>();

        //选择的buff
        public int cfgId;

        //结束时间
        public int endTime;
    }
} 

