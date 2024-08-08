using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepPiercing : Step
    {
        public override IEnumerator Excute()
        {
            UILockManager.Instance.Require("guide_piercing");
            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();
            UIUtils.OpenUI("piercing", new UIParam() { Value = m_Handler });
            yield return WaitOpen();
            UILockManager.Instance.Release("guide_piercing");

            yield return WaitClick();
            Finish();
        }

        IEnumerator WaitOpen() 
        {
            while (true) 
            {
                bool isOpen = UIUtils.CheckUIIsOpen("piercing");
                if (isOpen) yield break;
                yield return null;
            }
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
            UILockManager.Instance.Clear("guide_piercing");
            UIUtils.CloseUIByName("dialogue");
            UIUtils.CloseUIByName("piercing");
        }
    }
}

