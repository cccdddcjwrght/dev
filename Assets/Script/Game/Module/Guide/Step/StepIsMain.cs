using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    //�ȴ����������沢��û������
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

