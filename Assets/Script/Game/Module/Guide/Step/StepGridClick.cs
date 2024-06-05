using SGame.Dining;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepGridClick : Step
    {
        public EventHandleContainer m_EventHandle = new EventHandleContainer();
        public override IEnumerator Excute()
        {
            GuideManager.Instance.SetCoerceGuideState(true);

            m_Handler.DisableControl(true);
            m_Handler.DisableCameraDrag(true);
            yield return m_Handler.WaitFingerClose();
            m_Handler.DisableControl(false);

            m_EventHandle += EventManager.Instance.Reg<Build, int>((int)GameEvent.WORK_TABLE_CLICK, (b,index)=> Finish());
            m_Handler.InitConfig(m_Config);
            UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
            UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });
            yield break;
        }

        public override void Dispose() 
        {
            UIUtils.CloseUIByName("guideback");
            UIUtils.CloseUIByName("fingerui");
            GuideManager.Instance.SetCoerceGuideState(false);
            m_Handler.DisableCameraDrag(false);

            m_EventHandle.Close();
            m_EventHandle = null;
        }
    }
}

