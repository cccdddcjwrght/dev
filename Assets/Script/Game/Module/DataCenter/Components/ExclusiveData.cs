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
            /// ѡ�񿪾ֽ���
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
                    _data.time = GameServerTime.Instance.serverTime;
                    EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(exclusive.BuffId, exclusive.BuffValue, 0, exclusive.BuffDuration) { from = EXCLUSIVE_FORM });
                    EventManager.Instance.Trigger((int)GameEvent.ROOM_START_BUFF);
                }
            }

            /// <summary>
            /// ��ȡ����������ֽ���
            /// </summary>
            public static List<int> GetRandomRewardList() 
            {
                var exclusiveConfigs = ConfigSystem.Instance.LoadConfig<GameConfigs.RoomExclusive>();
                int len = exclusiveConfigs.DatalistLength;
                var weights = new List<int>();
                for (int i = 0; i < len; i++)
                    weights.Add(exclusiveConfigs.Datalist(i).Value.Weight);
                var rand = new Randoms.Random();
                var ws = rand.NextWeights(weights, 3, false).GroupBy(v => v);
                var rets = new List<RoomExclusiveRowData>();
                foreach (var item in ws)
                {
                    var exs = ConfigSystem.Instance.Finds<GameConfigs.RoomExclusiveRowData>(r => r.ExclusiveId == item.Key + 1);
                    rand.NextItem(exs, item.Count(), ref rets);
                }
                return rets.Select(s => s.ExclusiveId).ToList();
            }


            /// <summary>
            /// ���buff�Ƿ���Ч��
            /// </summary>
            /// <returns></returns>
            public static bool CheckBuffTakeEffect() 
            {
                bool isTakeEffect = false;
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(_data.cfgId, out var data)) 
                {
                    double endTime = _data.time + data.BuffDuration;
                    isTakeEffect = endTime > GameServerTime.Instance.serverTime || data.BuffDuration <= 0;
                }
                return isTakeEffect;
            }

            /// <summary>
            /// ��ȡbuffʣ��ʱ��
            /// </summary>
            /// <returns></returns>
            public static int GetBuffResiduTime() 
            {
                if (!CheckBuffTakeEffect()) return -1;
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(_data.cfgId, out var data)) 
                {
                    int endTime = (int)(_data.time + data.BuffDuration);
                    return Math.Max(endTime - GameServerTime.Instance.serverTime, 0);
                }
                return -1;
            }

            public static void Clear() 
            {
                _data.rewardBuffs.Clear();
                _data.cfgId = 0;
            }
        }
    }

    [Serializable]
    public class ExclusiveData
    {
        //��ѡ�����Ĺؿ�id
        public List<int> rooms = new List<int>();

        //��������Ľ���buff
        public List<int> rewardBuffs = new List<int>();
        
        //��ǰѡ��Ŀ��ֽ���buff
        public int cfgId;

        //ѡ�񿪾�buff�Ŀ�ʼʱ��
        public double time;
    }

} 

