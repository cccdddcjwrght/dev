using System.Collections.Generic;
using System;
using SGame.Firend;

namespace SGame 
{

    [Serializable]
    public class RankData
    {
        public List<RankItemData> list;

        //public int rankType;                    //排行类型
    }

    [Serializable]
    public class RankCacheData 
    {
        public int startTime;
        public bool reddot;
        public List<RankReward> rewards = new List<RankReward>();
    }

    [Serializable]
    public class RankItemData
    {
        public int roleID;                  //人物造型
        public long player_id;               //玩家id
        public int icon_id;                 //头像id
        public int frame_id;                //头像框id
        public string name;                 //角色名称
        public RankScore score;             //标识值

        public List<FirendEquip> equips;
    }

    //[Serializable]
    //public class Score 
    //{
    //    public int tips;
    //}

    public class RankUIParam 
    {
        public int marker;
        public int rank;
    }

    [Serializable]
    public class RankScore 
    {
        public int type;        //排行类型

        public double tips;     //小费
        public int boxs;        //打开场景箱子数量
        public int workers;     //雇佣工人数量
    }

    public class RankScoreEx 
    {
        public int type;        //排行类型

        public double tips;     //小费
        public int boxs;        //打开场景箱子数量
        public int workers;     //雇佣工人数量
        public long player_id;
    }


    [Serializable]
    public class RankPanelData 
    {
        public RanksData[] ids;
        public RankReward[] rewards;
    }

    [Serializable]
    public class RankReward 
    {
        public int id;          //排行id
        public int rank;        //排名
        public RankItem items;  //物品列表
    }

    [Serializable]
    public class RankItem 
    {
        public int item_id;          //物品id
        public int amount;      //数量
    }

    [Serializable]
    public class RanksData
    {
        public int id;          //排行id
        public int marker;      //排行榜标识
        public int begin_time;  //开始时间(秒)
        public int end_time;    //结束时间(秒)
    }
}


