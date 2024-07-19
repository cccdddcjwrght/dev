using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepFireClick : Step
    {
        public override IEnumerator Excute()
        {
            m_Handler.InitConfig(m_Config);

            UILockManager.Instance.Require("guide_fire_click");
            m_Handler.DisableControl(true);
            yield return m_Handler.FindTarget();

            var target = m_Handler.GetTarget();
            if (target is GButton) 
            {
                var btn = target as GButton;
                btn.FireClick(false, false);
            }
            Finish();
        }

        public override void Dispose()
        {
            UILockManager.Instance.Release("guide_fire_click");
            m_Handler.DisableControl(false);
        }
    }
}

