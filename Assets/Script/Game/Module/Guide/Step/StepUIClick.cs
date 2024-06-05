using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// UIµã»÷²½Öè
    /// </summary>
    public class StepUIClick : Step
    {
        GObject clickTarget;
        public override IEnumerator Excute() 
        {
            GuideManager.Instance.SetCoerceGuideState(true);

            m_Handler.DisableControl(true);
            m_Handler.DisableCameraDrag(true);
            yield return m_Handler.WaitFingerClose();
            m_Handler.DisableControl(false);

            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();
            clickTarget = m_Handler.GetTarget();
            clickTarget.onClick.Add(Finish);
            UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
            UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });
        }

        public override void Dispose()
        {
            if(clickTarget != null)
                clickTarget.onClick.Remove(Finish);

            UIUtils.CloseUIByName("fingerui");
            UIUtils.CloseUIByName("guideback");

            GuideManager.Instance.SetCoerceGuideState(false);
            m_Handler.DisableCameraDrag(false);
        }
    }
}

