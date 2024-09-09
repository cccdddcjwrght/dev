using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    //打开指定UI界面
    public class StepOpenUI : Step
    {
        public override IEnumerator Excute()
        {
            var uiName = m_Config.StringParam;
            var isOpen = UIUtils.CheckUIIsOpen(uiName);
            if (!isOpen) UIUtils.OpenUI(uiName);
            Finish();
            yield break;
        }
    }
}

