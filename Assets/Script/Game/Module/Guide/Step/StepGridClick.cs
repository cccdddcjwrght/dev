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
                m_Handler.DisableControl(true);
                m_Handler.DisableCameraDrag(true);
            }
            
            Debug.Log("<color=yellow>GridClick wait</color>");
            yield return m_Handler.WaitFingerClose();

            Debug.Log("<color=yellow>GridClick runing</color>");
            //监听界面打开后再完成该步骤
            m_EventHandle += EventManager.Instance.Reg<string>((int)GameEvent.GUIDE_UI_SHOW, (name)=> 
            {
                if (name == "worktable" || name == "getworker" || name == "unlocktable") Finish();
            });

            m_Handler.InitConfig(m_Config);

            if (m_Config.Force == 0)
            {
                UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
                UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });
                yield return m_Handler.WaitGuideMaskOpen();

                m_Handler.DisableControl(false);
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
            //强指引
            if (m_Config.Force == 0)
            {
                UIUtils.CloseUIByName("guideback");
                GuideManager.Instance.SetCoerceGuideState(false);
                m_Handler.DisableControl(false);
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

