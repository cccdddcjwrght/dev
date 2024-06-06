using System;
using System.Collections.Generic;

namespace SGame
{
    [Serializable]
    public class GuideData
    {
        public int guideStep;

        public int guideId = 1; //当前指引id
        [NonSerialized]
        public bool isGuide=false;

        [NonSerialized]
        public bool isStopCreate = false;   //停止创建顾客
    }

    public class GuideModule :Singleton<GuideModule>
    {
        //是否第一次进入指引
        public const string GUIDE_FIRST = "guide.first";

        //指引最高步骤
        public const int MAX_SETP = 7; 

        /// <summary>
        /// 指引是否全部完成
        /// </summary>
        /// <returns></returns>
        public bool IsGuideFinsih() 
        {
            return DataCenter.Instance.guideData.guideStep > MAX_SETP;
        }
    }
}

