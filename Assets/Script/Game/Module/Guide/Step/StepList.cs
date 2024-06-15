using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepList : Step
    {
        public override IEnumerator Excute()
        {
            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();

            var target = m_Handler.GetTarget();
            if (target.parent is GList)
            {
                var list = target.parent.asList;
                int index = list.GetChildIndex(target);
                list.ScrollToView(index);
            }
            Finish();
        }
    }
}

