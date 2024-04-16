using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// 成长礼包数据存储
namespace SGame
{
    /// <summary>
    /// 每个活动的数据存储
    /// </summary>
    [Serializable]
    public class GrowGiftItem
    {
        /// <summary>
        /// 已经领取的ID, 该ID对应配置表上的ID
        /// </summary>
        public List<int> takedID = new List<int>();

        /// <summary>
        /// 保存红点状态, ID对应的是配置表上的ID
        /// </summary>
        //public List<int> redotID = new List<int>();

        /// <summary>
        /// 活动用的道具ID
        /// </summary>
        public int goodsID;

        /// <summary>
        /// 记录是否购买
        /// </summary>
        public bool isBuy = false;

        /// <summary>
        /// 活动ID
        /// </summary>
        public int  activlityID;

        /// <summary>
        /// 统计星星的
        /// </summary>
        public ItemData itemData = new ItemData();

        [NonSerialized]
        private ItemGroup itemGroup = null;
        

        public ItemGroup GetOrCrateGroup()
        {
            if (itemGroup == null)
            {
                itemGroup = new ItemGroup();
                itemGroup.Initalize(itemData);
            }

            return itemGroup;
        }

        /// <summary>
        /// 是否已领取
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public bool IsTaked(int configID)
        {
            return takedID.Contains(configID);
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="goodsID"></param>
        /// <param name="activlity_id"></param>
        /// <returns></returns>
        public static GrowGiftItem  Create(int goodsID, int activlity_id)
        {
            var ret = new GrowGiftItem()
            {
                goodsID = goodsID,
                activlityID = activlity_id,
                isBuy = false
            };
            return ret;
        }
    }
    
    /// <summary>
    /// 成长礼包数据
    /// </summary>
    [Serializable]
    public class GrowGiftRecord
    {
        public List<GrowGiftItem> Values = new List<GrowGiftItem>();
        
        public GrowGiftItem GetItem(int goodsID)
        {
            foreach (var item in Values)
            {
                if (item.goodsID == goodsID)
                    return item;
            }

            return null;
        }

        public GrowGiftItem AddNewItem(int goodsID, int activityID)
        {
            GrowGiftItem item = GrowGiftItem.Create(goodsID, activityID);
            Values.Add(item);
            return item;
        }

        public GrowGiftItem GetItemFromActivityID(int activityID)
        {
            foreach (var item in Values)
            {
                if (item.activlityID == activityID)
                    return item;
            }

            return null;
        }
    }
}