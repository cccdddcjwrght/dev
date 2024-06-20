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
        public int likeNum;     //好评数量

        //好评抽到的大奖
        public List<LikeRewardData> likeRewardDatas = new List<LikeRewardData>();    
    }

    [Serializable]
    public class LikeRewardData 
    {
        public int id;
        public int num;

        public int itemType;
        public int typeId;
    }

    public partial class DataCenter 
    {
        public LikeData likeData = new LikeData();

        public static class LikeUtil 
        {
            private static List<ItemData.Value> _dropItems = new List<ItemData.Value>();

            /// <summary>
            /// 获取随机到的奖励配置Id
            /// </summary>
            /// <returns></returns>
            public static int GetLotteryIndex() 
            {
                List<int> weights = new List<int>();
                var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.Likes_Rewards>();
                var len = configs.DatalistLength;
                for (int i = 0; i < len; i++)
                {
                    var config = configs.Datalist(i).Value;
                    var weight = GetRewardWeight(config.ConditionType, config.ConditionValue, config.Weight);
                    weights.Add(weight);
                }
                var index = SGame.Randoms.Random._R.NextWeight(weights) + 1;
                return index;
            }

            /// <summary>
            /// 获取随机到的隐藏奖励id
            /// </summary>
            /// <returns></returns>
            public static int GetHiddenRewardIndex() 
            {
                List<int> weights = new List<int>();
                var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.Likes_Jackpot>();
                var len = configs.DatalistLength;
                for (int i = 0; i < len; i++)
                {
                    var config = configs.Datalist(i).Value;
                    var weight = GetRewardWeight(config.ConditionType, config.ConditionValue, config.Weight);
                    weights.Add(weight);
                }
                var index = SGame.Randoms.Random._R.NextWeight(weights) + 1;
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
                            _dropItems.Add(new ItemData.Value()
                            {
                                id = id,
                                num = num,
                                type = PropertyGroup.ITEM
                            });
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
                    typeId = config.TypeId,
                });
            }
        }
    }
}

