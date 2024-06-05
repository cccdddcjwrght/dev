using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepCheck : Step
    {
        public override IEnumerator Excute()
        {
            yield return WaitCheckItem();
            Finish();
        }

        //等待对应item数量充足
        public IEnumerator WaitCheckItem() 
        {
            while (true) 
            {
                int type = (int)m_Config.FloatParam(0); 
                int itemId = (int)m_Config.FloatParam(1);
                int count = (int)m_Config.FloatParam(2);
                if (PropertyManager.Instance.CheckCount(itemId, count, type)) 
                {
                    yield break;
                }
                yield return null;
            }
        }
    }
}

