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
            if (m_Config.Force == 0)
            {
                GuideManager.Instance.SetCoerceGuideState(true);
                m_Handler.DisableCameraDrag(true);
            }

            Debug.Log("<color=yellow>GridClick wait</color>");
            yield return m_Handler.WaitFingerClose();

            Debug.Log("<color=yellow>GridClick runing</color>");
            m_EventHandle += EventManager.Instance.Reg((int)GameEvent.WORK_REGION_CLICK, Finish);
            m_Handler.InitConfig(m_Config);

            if (m_Config.Force == 0)
            {
                UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
                UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });
            }
            else
            {
                if (m_Config.GuideType == 0) m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Finish);
                else m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Stop);
                UIUtils.OpenUI("guidefingerscene", new UIParam() { Value = m_Handler });
            }

            yield break;
        }

        public void Stop()
        {
            GuideManager.Instance.StopGuide(m_Config.GuideId);
        }

        public override void Dispose()
        {
            //Ç¿Ö¸Òý
            if (m_Config.Force == 0)
            {
                UIUtils.CloseUIByName("guideback");
                GuideManager.Instance.SetCoerceGuideState(false);
                m_Handler.DisableCameraDrag(false);
            }
            UIUtils.CloseUIByName("fingerui");
            UIUtils.CloseUIByName("guidefingerscene");
            UIUtils.CloseUIByName("dialogue");
            m_EventHandle?.Close();
            m_EventHandle = null;
        }
    }
}

