using System.Collections.Generic;
using System;
using SGame.Firend;

namespace SGame 
{

    [Serializable]
    public class RankData
    {
        public List<RankItemData> rankDatas;
        public int rankType;                    //排行类型
    }


    [Serializable]
    public class RankItemData
    {
        public int roleID;                  //人物造型
        public int player_id;               //玩家id
        public int icon_id;                 //头像id
        public int frame_id;                //头像框id
        public string name;                 //角色名称
        public Score score;                 //标识值

        public List<FirendEquip> equips;
    }

    [Serializable]
    public class Score 
    {
        public int tips;
    }

    [Serializable]
    public class RankOwnData 
    {
        public int type;    //排行类型
        public int value;   //标识值
    }

    [Serializable]
    public class RankTotalOwnData 
    {
        public List<RankOwnData> rankOwnDatas;
    }
}


