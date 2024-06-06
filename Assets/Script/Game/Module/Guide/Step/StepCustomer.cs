using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 控制顾客创建指引
    /// </summary>
    public class StepCustomer : Step
    {
        public override IEnumerator Excute()
        {
            float val = m_Config.FloatParam(0);

            if (val <= 0) DataCenter.Instance.guideData.isStopCreate = true;
            else DataCenter.Instance.guideData.isStopCreate = false;

            Finish();
            yield break;
        }
    }
}

