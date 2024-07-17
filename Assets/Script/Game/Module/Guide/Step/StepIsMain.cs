using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    //等待返回主界面并且没有锁定
    public class StepIsMain : Step
    {
        public override IEnumerator Excute()
        {
            //yield return new WaitForSeconds(0.5f);
            yield return Wait();
            Finish();
        }

        IEnumerator Wait() 
        {
            while (true) 
            {
                if (UIUtils.CheckIsOnlyMainUI() && !UILockManager.Instance.isLocked) yield break;
                yield return null;
            }
        }
    }
}

