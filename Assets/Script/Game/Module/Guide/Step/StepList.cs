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
            if (target.parent is GComponent)
            {
                var com = target.asCom;
                com.scrollPane.SetPercY(1, false);
            }
            Finish();
        }
    }
}

