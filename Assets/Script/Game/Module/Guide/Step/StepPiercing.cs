using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepPiercing : Step
    {
        public override IEnumerator Excute()
        {
            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();
            UIUtils.OpenUI("piercing", new UIParam() { Value = m_Handler });
            yield return WaitClick();
            Finish();
        }

        IEnumerator WaitClick()
        {
            while (true) 
            {
                if (Input.GetMouseButtonDown(0)) 
                    yield break;
                yield return null;
            }
        }

        public override void Dispose()
        {
            UIUtils.CloseUIByName("dialogue");
            UIUtils.CloseUIByName("piercing");
        }
    }
}

