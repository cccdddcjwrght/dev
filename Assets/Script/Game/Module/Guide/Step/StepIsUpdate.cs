using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// 是否刷新客人
    /// </summary>
    public class StepIsUpdate : Step
    {
        public override IEnumerator Excute()
        {
            var isUpdate = m_Config.FloatParam(0);
            DataCenter.Instance.guideData.isStopCreate = isUpdate < 0;
            Finish();
            yield break;
        }
    }
}

