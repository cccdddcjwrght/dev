using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    /// <summary>
    /// µÈ´ýÊ±¼ä
    /// </summary>
    public class StepWait : Step
    {
        GTweener tween;
        public override IEnumerator Excute()
        {
            float time = m_Config.FloatParam(0);
            UILockManager.Instance.Require("guide");
            m_Handler.DisableControl(true);
            tween = GTween.To(0, 1, time).OnComplete(Finish);
            yield break;
        }

        public override void Dispose()
        {
            UILockManager.Instance.Release("guide");
            m_Handler.DisableControl(false);
            tween?.Kill();
        }
    }
}

