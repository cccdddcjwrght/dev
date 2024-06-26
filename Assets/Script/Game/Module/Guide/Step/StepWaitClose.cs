using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepWaitClose : Step
    {
        string m_uiname;
        public override IEnumerator Excute()
        {
            m_uiname = m_Config.StringParam;
            yield return WaitUIClose();
            Finish();
        }

        IEnumerator WaitUIClose()
        {
            while (true)
            {
                bool isOpen = UIUtils.CheckUIIsOpen(m_uiname);
                if (!isOpen) yield break;
                yield return null;
            }
        }
    }
}

