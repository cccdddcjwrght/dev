using System;
using System.Collections.Generic;

namespace SGame
{
    [Serializable]
    public class GuideData
    {
        public int guideStep;
        [NonSerialized]
        public bool isGuide=false;
    }

    public class GuideModule :Singleton<GuideModule>
    {
        //是否第一次进入指引
        public const string GUIDE_FIRST = "guide.first";
    }
}

