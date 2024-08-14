using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SGame 
{
    public enum LikeUnLockType 
    {
        WORK_TABLE  = 1, //解锁加工台
        AREA        = 2, //解锁区域
    }

    [Serializable]
    public class LikeData 
    {
        public int likeNum = 1;         //好评数量
        public bool isFirst = true;    //是否第一次抽奖（第一次必中隐藏奖励）

        //好评抽到的大奖
        public List<LikeRewardData> likeRewardDatas = new List<LikeRewardData>();    
    }

    [Serializable]
    public class LikeRewardData 
    {
        public int id;
        public int num;

        public int itemType;
        public int subType;
        public int typeId;
    }

    public partial class DataCenter 
    {
        public LikeData likeData = new LikeData();

        public static class LikeUtil 
        {
            private static List<ItemData.Value> _dropItems = new List<ItemData.Value>();

            private static List<int> m_Weights = new List<int>();
            private static int m_BigPrizeId;        //大奖ID
            private static int m_SpecialId;         //特殊奖励ID

            static GameConfigs.Likes_Rewards m_LikesRewardConfigs;
            static GameConfigs.Likes_Jackpot m_LikesJackpotConfigs;
            

            public static void Init() 
            {
                m_BigPrizeId = ConfigSystem.Instance.Find<GameConfigs.Likes_RewardsRowData>((c) => c.ResultType == 3).Id;
                m_SpecialId = ConfigSystem.Instance.Find<GameConfigs.Likes_JackpotRowData>((c) => c.Id > 100).Id;
                m_LikesRewardConfigs = ConfigSystem.Instance.LoadConfig<GameConfigs.Likes_Rewards>();
                m_LikesJackpotConfigs = ConfigSystem.Instance.LoadConfig<GameConfigs.Likes_Jackpot>();
            }

            /// <summary>
            /// 获取随机到的奖励配置Id
            /// </summary>
            /// <returns></returns>
            public static int GetLotteryIndex() 
            {
                //第一次必中隐藏大奖
                if (Instance.likeData.isFirst)
                    return m_BigPrizeId;

                m_Weights.Clear();
                var len = m_LikesRewardConfigs.DatalistLength;
                for (int i = 0; i < len; i++)
                {
                    var config = m_LikesRewardConfigs.Datalist(i).Value;
                    var weight = GetRewardWeight(config.ConditionType, config.ConditionValue, config.Weight);
                    m_Weights.Add(weight);
                }
                var index = SGame.Randoms.Random._R.NextWeight(m_Weights) + 1;
                return index;
            }

            /// <summary>
            /// 获取随机到的隐藏奖励id
            /// </summary>
            /// <returns></returns>
            public static int GetHiddenRewardIndex() 
            {
                if (Instance.likeData.isFirst) 
                {
                    Instance.likeData.isFirst = false;
                    return m_SpecialId;
                }

                m_Weights.Clear();
                var len = m_LikesJackpotConfigs.DatalistLength;
                for (int i = 0; i < len; i++)
                {
                    var config = m_LikesJackpotConfigs.Datalist(i).Value;
                    if(config.Id <= 100) 
                    {
                        var weight = GetRewardWeight(config.ConditionType, config.ConditionValue, config.Weight);
                        m_Weights.Add(weight);
                    }
                }
                var index = SGame.Randoms.Random._R.NextWeight(m_Weights) + 1;
                return index;
            }

            /// <summary>
            /// 随机掉落物品
            /// </summary>
            /// <param name="dropId">掉落id</param>
            /// <param name="count">数量</param>
            /// <param name="isClear"></param>
            /// <returns></returns>
            public static List<ItemData.Value> GetItemDrop(int dropId, int count = 1 ,bool isClear = true) 
            {
                if(isClear) _dropItems.Clear();

                if (ConfigSystem.Instance.TryGet<GameConfigs.ItemDropRowData>(dropId, out var config)) 
                {
                    for (int i = 0; i < count; i++)
                    {
                        var weights = config.GetWeightArray();
                        var ret = Randoms.Random._R.NextWeights(weights, config.Num, config.Repeat == 1);

                        for (int j = 0; j < ret.Length; j++)
                        {
                            int id = config.ItemId(ret[j] * 2);
                            int num = config.ItemId(ret[j] * 2 + 1);
                            var index = _dropItems.FindIndex((i) => i.id == id);
                            if (index == -1)
                            {
                                _dropItems.Add(new ItemData.Value()
                                {
                                    id = id,
                                    num = num,
                                    type = PropertyGroup.ITEM
                                });
                            }
                            else 
                            {
                                _dropItems[index]= _dropItems[index].SetNum(_dropItems[index].num + num);
                            }
                            
                        }
                    }
                }
                return _dropItems;
            }


            /// <summary>
            /// 获取抽奖奖励权重，条件未达成的默认返回0
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
            public static int GetRewardWeight(int conditionType, int conditionValue,int weight) 
            {
                if (conditionType == (int)LikeUnLockType.WORK_TABLE)
                {
                    if (MachineUtil.GetWorktable(conditionValue) != null) return 0;
                }
                else if (conditionType == (int)LikeUnLockType.AREA)
                {
                    if(!RoomUtil.IsAreaEnable(conditionValue)) return 0;
                }
                return weight;
            }

            public static void AddRewardData(int id, int num) 
            {
                var list = DataCenter.Instance.likeData.likeRewardDatas;
                var index = list.FindIndex((r) => r.id == id);
                var config = ConfigSystem.Instance.Find<GameConfigs.ItemRowData>((i) => i.ItemId == id);

                if (index >= 0) list[index].num += num;
                else list.Add(new LikeRewardData()
                {
                    id = id,
                    num = num,
                    itemType = config.Type,
                    subType = config.SubType,
                    typeId = config.TypeId,
                });
            }
        }
    }
}

