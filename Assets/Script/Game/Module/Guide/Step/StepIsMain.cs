using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    //等待返回主界面并且没有锁定
    public class StepIsMain : Step
    {
        UIWindow m_MainUI;
        public override IEnumerator Excute()
        {
            yield return WaitMainUI();
            m_MainUI = UIUtils.GetUIView("mainui");
            m_MainUI.Value.touchable = false;
            Debug.Log("mainui lock ---------" + Time.realtimeSinceStartupAsDouble);
            yield return Wait();
            m_MainUI.Value.touchable = true;
            Debug.Log("mainui unlock ---------" + Time.realtimeSinceStartupAsDouble);
            Finish();
        }

        IEnumerator WaitMainUI()
        {
            while (true)
            {
                bool isOpen = UIUtils.CheckUIIsOpen("mainui");
                if (isOpen) yield break;
                yield return null;
            }
        }

        IEnumerator Wait() 
        {
            while (true) 
            {
                if (UIUtils.CheckIsOnlyMainUI() && !UILockManager.Instance.isLocked) yield break;
                yield return null;
            }
        }

        public override void Dispose()
        {
            if(m_MainUI != null) m_MainUI.Value.touchable = true;
        }
    }
}

