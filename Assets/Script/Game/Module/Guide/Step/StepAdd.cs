using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 指引id增加
    /// </summary>
    public class StepAdd : Step
    {
        public override IEnumerator Excute()
        {
            DataCenter.Instance.guideData.guideId++;
            Finish();
            yield break;
        }
    }
}

