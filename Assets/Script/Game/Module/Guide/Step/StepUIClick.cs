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
        EventHandleContainer m_EventHandle = new EventHandleContainer();
        bool isLock = false;
        public override IEnumerator Excute() 
        {

            if (m_Config.Force == 0) 
            {
                GuideManager.Instance.SetCoerceGuideState(true);
                //锁定UI
                UILockManager.Instance.Require("guide");
                isLock = true;
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
                UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
                //等待遮罩打开
                yield return m_Handler.WaitGuideMaskOpen();
                isLock = false;
                //解开UI
                UILockManager.Instance.Release("guide");
                m_Handler.DisableControl(false);
                Debug.Log("<color=bule> ui unlock-------------</color>");
            }
            else 
            {
                //如果是主线指引，弱指引点击其他地方相当于完成该指引
                if (m_Config.GuideType == 0)
                    m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Finish);
                else
                    m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Stop);
            }
            UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });

        }

        //public override void Stop()
        //{
        //    GuideManager.Instance.StopGuide(m_Config.GuideId);
        //    base.Stop();
        //}


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
            if (isLock) 
            {
                UILockManager.Instance.Release("guide");
                m_Handler.DisableControl(false);
                Debug.Log("<color=bule> ui unlock-------------</color>");
            }

            UIUtils.CloseUIByName("dialogue");

            m_EventHandle.Close();
            m_EventHandle = null;
        }
    }
}

