using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// ָ��id����
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

