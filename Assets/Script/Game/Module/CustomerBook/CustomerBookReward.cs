using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGame
{
    /// <summary>
    /// 图鉴存储数据
    /// </summary>
    [Serializable]
    public class CustomerBookReward
    {
        [Serializable]
        public class Record
        {
            public int  roleID;                                              // 角色ID
            public bool isOpened = false;                                    // 是否已打开
            public bool isUnlock = false; // 角色是否已出现
            public bool isRewared = false;                                   // 奖励是否已领取
        }

        [SerializeField]
        private List<Record> Values = new List<Record>();
        
        public void Clear()
        {
            Values.Clear();
        }

        public Record GetOrCreate(int roleID)
        {
            foreach (var item in Values)
            {
                if (item.roleID == roleID)
                    return item;
            }

            Record newRecord = new Record()
            {
                roleID = roleID,
                isOpened = false,
                isUnlock = false,
                isRewared = false
            };
            Values.Add(newRecord);
            return newRecord;
        }
    }
}