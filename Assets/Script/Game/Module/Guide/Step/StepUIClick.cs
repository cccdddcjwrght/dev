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

            if (m_Config.Force == 0) 
            {
                GuideManager.Instance.SetCoerceGuideState(true);
                //锁定UI
                UILockManager.Instance.Require("guide");
                //GRoot.inst.touchable = false;
                Debug.Log("<color=white> ui lock-------------</color>");
                m_Handler.DisableControl(true);
                m_Handler.DisableCameraDrag(true);
            }

            yield return m_Handler.WaitFingerClose();
            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();
            clickTarget = m_Handler.GetTarget();
            clickTarget.onClick.Add(Finish);

            if (m_Config.Force == 0)
            {
                //解开UI
                UILockManager.Instance.Release("guide");
                Debug.Log("<color=bule> ui unlock-------------</color>");
                m_Handler.DisableControl(false);
                UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
            }
            else 
            {
                clickTarget.onFocusOut.Add(Stop);
            }
            UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });

        }

        public void Stop() 
        {
            GuideManager.Instance.StopGuide(m_Config.GuideId);
        }


        public override void Dispose()
        {
            if(clickTarget != null)
                clickTarget.onClick.Remove(Finish);

            UIUtils.CloseUIByName("fingerui");

            if (m_Config.Force == 0)
            {
                UIUtils.CloseUIByName("guideback");
                GuideManager.Instance.SetCoerceGuideState(false);
                m_Handler.DisableCameraDrag(false);
            }
            else 
            {
                clickTarget.onFocusOut.Remove(Stop);
            }
       
        }
    }
}

