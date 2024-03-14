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
}

