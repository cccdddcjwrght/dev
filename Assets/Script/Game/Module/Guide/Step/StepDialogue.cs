using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepDialogue : Step
    {
        public EventHandleContainer m_EventHandle = new EventHandleContainer();
        public override IEnumerator Excute()
        {
            m_Handler.InitConfig(m_Config);
            UILockManager.Instance.Require("guide_dialogue");
            yield return WaitClose();
            UIUtils.OpenUI("dialogue", new UIParam() { Value = m_Handler });
            yield return WaitUI();
            UILockManager.Instance.Release("guide_dialogue");
            if (m_Config.Force == 0) //ǿָ��ֱ�ӽ������ȴ�UIClick�����رգ�
            {
                Finish();
            }
            else 
            {
                m_EventHandle += EventManager.Instance.Reg((int)GameEvent.GUIDE_CLICK, Finish);
            }
        }

        IEnumerator WaitClose() 
        {
            while (true)
            {
                bool isOpen = UIUtils.CheckUIIsOpen("dialogue");
                if (!isOpen) yield break;
                yield return null;
            }
        }

        IEnumerator WaitUI() 
        {
            while (true)
            {
                bool isOpen = UIUtils.CheckUIIsOpen("dialogue");
                if (isOpen) yield break;
                yield return null;
            }
        }

        public override void Dispose()
        {
            //��ָ��
            if (m_Config.Force == 1) 
            {
                UIUtils.CloseUIByName("dialogue");
            }
            m_EventHandle.Close();
            m_EventHandle = null;
        }

        public override void Stop()
        {
            UIUtils.CloseUIByName("dialogue");
            base.Stop();
        }
    }
}

