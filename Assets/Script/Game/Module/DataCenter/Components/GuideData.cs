using System;
using System.Collections.Generic;

namespace SGame
{
    [Serializable]
    public class GuideData
    {
        public int guideStep;

        public int guideId = 1; //��ǰָ��id
        [NonSerialized]
        public bool isGuide=false;

        [NonSerialized]
        public bool isStopCreate = false;   //ֹͣ�����˿�
    }

    public class GuideModule :Singleton<GuideModule>
    {
        //�Ƿ��һ�ν���ָ��
        public const string GUIDE_FIRST = "guide.first";

        //ָ����߲���
        public const int MAX_SETP = 7; 

        /// <summary>
        /// ָ���Ƿ�ȫ�����
        /// </summary>
        /// <returns></returns>
        public bool IsGuideFinsih() 
        {
            return DataCenter.Instance.guideData.guideStep > MAX_SETP;
        }
    }
}

