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
        public int               player_id;
        public int               roleID;
        public string            name;
        public List<FirendEquip>  equips;
        public int                hireTime;
    }

    [Serializable]
    public class RecommendData
    {
        public int               player_id;
        public int               roleID;
        public string            name;
        public List<FirendEquip>      equips;
    }

    [Serializable]
    public class FriendBody
    {
        public List<FirendData>     Friends;
        public List<FirendData>     RecommendFriends;
        public List<FirendData>     HireFriends;
    }
    
    [Serializable]
    public class NetFriendData
    {
        public int      code;
        public string   msg;
        public string   data;
        public int      version;
    }
}