using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepCloseUI : Step
    {
        public override IEnumerator Excute()
        {
            UIUtils.CloseAllUI("mainui", "flight", "lockred", "SystemTip",
            "Redpoint", "ordertip", "progress", "FoodTip");

            Finish();
            yield break;
        }
    }
}

