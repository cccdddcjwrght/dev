using System.Collections.Generic;
using System;
using SGame.Firend;

namespace SGame 
{

    [Serializable]
    public class RankData
    {
        public List<RankItemData> rankDatas;
        public int rankType;                    //��������
    }


    [Serializable]
    public class RankItemData
    {
        public int roleID;                  //��������
        public int player_id;               //���id
        public int icon_id;                 //ͷ��id
        public int frame_id;                //ͷ���id
        public string name;                 //��ɫ����
        public Score score;                 //��ʶֵ

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
        public int type;    //��������
        public int value;   //��ʶֵ
    }

    [Serializable]
    public class RankTotalOwnData 
    {
        public List<RankOwnData> rankOwnDatas;
    }
}


