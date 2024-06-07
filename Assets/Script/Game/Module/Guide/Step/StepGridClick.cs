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
            Debug.Log("<color=yellow>GridClick wait</color>");

            m_Handler.DisableCameraDrag(true);
            yield return m_Handler.WaitFingerClose();

            Debug.Log("<color=yellow>GridClick runing</color>");
            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.WORK_REGION_CLICK, Finish);
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

