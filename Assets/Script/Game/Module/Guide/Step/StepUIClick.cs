using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// UI点击步骤
    /// </summary>
    public class StepUIClick : Step
    {
        GObject clickTarget;
        public override IEnumerator Excute() 
        {
            GuideManager.Instance.SetCoerceGuideState(true);
            //锁定UI
            GRoot.inst.touchable = false;
            Debug.Log("<color=white> ui lock-------------</color>");
            m_Handler.DisableControl(true);
            m_Handler.DisableCameraDrag(true);

            yield return m_Handler.WaitFingerClose();
            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();

            //解开UI
            GRoot.inst.touchable = true;
            Debug.Log("<color=bule> ui unlock-------------</color>");
            m_Handler.DisableControl(false);

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

