using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepSelect : Step
    {
        public override IEnumerator Excute()
        {
            int type = (int)m_Config.FloatParam(0);
            int itemId = (int)m_Config.FloatParam(1);
            int count = (int)m_Config.FloatParam(2);
            if (PropertyManager.Instance.CheckCount(itemId, count, type))
            {
                Debug.Log(string.Format("<color=green>guide finish id: {0} </color>", m_Config.GuideId));
                EventManager.Instance.Trigger((int)GameEvent.GUIDE_FINISH, m_Config.GuideId);
            }
            else Finish();
            yield break;
        }
    }
}

