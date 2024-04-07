using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.Firend
{
    public enum FIREND_STATE : int
    {
        RECOMMEND = 0, // 可邀请
        HIRING    = 1, // 雇佣中
        CAN_HIRE  = 2, // 可雇佣
        HIRE_CD   = 3, // 雇佣CD中
        HIRED     = 4, // 已雇佣
    }
    
    /// <summary>
    /// 还有装备
    /// </summary>
    [Serializable]
    public class FirendEquip
    {
        public int id;
        public int quality;
        public int level;
    }
    
    /// <summary>
    /// 好友数据
    /// </summary>
    [Serializable]
    public class FirendItemData
    {
        public int                player_id;     // 玩家ID
        public int                icon_id;       // 头像ID
        public int                frame_id;      // 头像框ID
        public int                roleID;        // 角色ID
        public string             name;          // 角色名称
        public int                hireTime = 0;  // 下次可招募时间(秒)
        public int                hiringTime = 0;// 雇佣生效时间
        public int                passLevel = 0; // 通关数量
        public int                state = 0;     // 雇佣状态
        public List<FirendEquip>  equips;        // 装备信息

        /// <summary>
        /// 还有多少秒可以雇佣
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public int GetDisableTime(int currentTime)
        {
            return currentTime >= hireTime ? 0 : hireTime - currentTime;
        }

        /// <summary>
        /// 获取激活剩余时间
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public int GetActiveTime(int currentTime)
        {
            return currentTime >= hiringTime ? 0 : hiringTime - currentTime;
        }
    }
    
    /// <summary>
    /// 好友数据
    /// </summary>
    [Serializable]
    public class FriendData
    {
        public List<FirendItemData>     Friends;
        public List<FirendItemData>     RecommendFriends;
        public int                      nextHireTime;          // 下次可雇佣时间
        public int                      hiringTime;            //  好友雇佣到时间
    }
    
    /// <summary>
    /// 好友网络数据
    /// </summary>
    [Serializable]
    public class NetFriendData
    {
        public int      code;
        public string   msg;
        public string   data;
        public int      version;
    }
}