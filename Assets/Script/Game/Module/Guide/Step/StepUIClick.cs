using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using SGame.UI;
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


        static bool NeedWait()
        {
            List<UIWindow> windows = UIModule.Instance.GetVisibleUI();
            foreach (var w in windows)
            {
                if (w.name == "eqgiftui" ||
                    w.name == "rewardlist" || 
                    w.name == "task")
                {
                    UILockManager.Instance.Release("guide_uiclick");
                    return true;
                }
            }
            
            return false;
        }

        // 等待安全环境 
        static IEnumerator WaitSafeState()
        {
            //UIUtils.CloseAllUI("mainui", "flight", "lockred", "SystemTip",
            //    "Redpoint", "ordertip", "progress", "FoodTip");
            //Debug.Log("<color=white> ui lock-------------</color>");
            UILockManager.Instance.Require("guide_uiclick");
            do
            {
                yield return null;
            } while (NeedWait());
            UILockManager.Instance.Release("guide_uiclick");
            //Debug.Log("<color=white> ui unlock-------------</color>");
        }
        
        public override IEnumerator Excute()
        {
            yield return WaitSafeState();

            //锁定UI(查找到UI解锁)
            UILockManager.Instance.Require("guide_uiclick");
            Debug.Log("<color=white> ui lock-------------</color>");
            m_Handler.DisableControl(true);
            m_Handler.DisableCameraDrag(true);

            if (m_Config.Force == 0)
            {
                GuideManager.Instance.SetCoerceGuideState(true);
            }
            else 
            {
                //如果是主线指引，弱指引点击其他地方相当于完成该指引
                if (m_Config.GuideType == 0)
                    m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Finish);
                else
                    m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Stop);
            }

            yield return m_Handler.WaitFingerClose();
            yield return m_Handler.WaitGuideMaskClose();

            m_Handler.InitConfig(m_Config);
            yield return m_Handler.FindTarget();
  
            clickTarget = m_Handler.GetTarget();
            clickTarget.onClick.Add(Finish);
            Debug.Log(string.Format("<color=yellow>{0} add guide click event</color>", clickTarget.name));
            clickTarget.onTouchBegin.Clear();

            if (m_Config.Force == 0)
            {
                UIUtils.OpenUI("guideback", new UIParam() { Value = m_Handler });
                //等待遮罩打开
                yield return m_Handler.WaitGuideMaskOpen();
            }
            UIUtils.OpenUI("fingerui", new UIParam() { Value = m_Handler });
            yield return m_Handler.WaitFingerOpen();

            //解开UI
            UILockManager.Instance.Release("guide_uiclick");
            m_Handler.DisableControl(false);
            Debug.Log("<color=bule> ui unlock-------------</color>");
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
            }
            m_Handler.DisableCameraDrag(false);
            UILockManager.Instance.Clear("guide_uiclick");
            m_Handler.DisableControl(false);
            Debug.Log("<color=bule> ui unlock-------------</color>");

            UIUtils.CloseUIByName("dialogue");

            m_EventHandle.Close();
            m_EventHandle = null;
        }
    }
}

