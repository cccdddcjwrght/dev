using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame.Firend
{
    [Serializable]
    public class FirendEquip
    {
        public int id;
        public int quality;
        public int level;
    }
    
    [Serializable]
    public class FirendData
    {
        public int                player_id;
        public int                roleID;
        public string             name;
        public List<FirendEquip>  equips;
        public int                hireTime;
    }
    
    [Serializable]
    public class FriendBody
    {
        public List<FirendData>     Friends;
        public List<FirendData>     RecommendFriends;
        public List<FirendData>     HireFriends;
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