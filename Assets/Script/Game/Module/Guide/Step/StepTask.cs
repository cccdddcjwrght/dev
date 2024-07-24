using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepTask : Step
    {
        public override IEnumerator Excute()
        {
            yield return WaitTask();
            Finish();
        }

        IEnumerator WaitTask() 
        {
            int targetTaskId = (int)m_Config.FloatParam(0);
            while (true)
            {
                if (DataCenter.Instance.taskMainData.cfgId == targetTaskId &&
                    DataCenter.TaskMainUtil.CheckIsGet()) yield break;
                yield return null;
            }
        }
    }
}

