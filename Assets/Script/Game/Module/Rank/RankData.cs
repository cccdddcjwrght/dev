using System.Collections.Generic;
using System;
using SGame.Firend;

namespace SGame 
{

    [Serializable]
    public class RankData
    {
        public List<RankItemData> list;

        //public int rankType;                    //��������
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
        public int roleID;                  //��������
        public long player_id;               //���id
        public int icon_id;                 //ͷ��id
        public int frame_id;                //ͷ���id
        public string name;                 //��ɫ����
        public RankScore score;             //��ʶֵ

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
        public int type;        //��������

        public double tips;     //С��
        public int boxs;        //�򿪳�����������
        public int workers;     //��Ӷ��������
    }

    public class RankScoreEx 
    {
        public int type;        //��������

        public double tips;     //С��
        public int boxs;        //�򿪳�����������
        public int workers;     //��Ӷ��������
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
        public int id;          //����id
        public int rank;        //����
        public RankItem items;  //��Ʒ�б�
    }

    [Serializable]
    public class RankItem 
    {
        public int item_id;          //��Ʒid
        public int amount;      //����
    }

    [Serializable]
    public class RanksData
    {
        public int id;          //����id
        public int marker;      //���а��ʶ
        public int begin_time;  //��ʼʱ��(��)
        public int end_time;    //����ʱ��(��)
    }
}


