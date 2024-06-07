using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepFov : Step
    {
        GTweener m_Timer;
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true);

            float target = m_Config.FloatParam(0);
            float time = m_Config.FloatParam(1);

            float cur = SceneCameraSystem.Instance.GetOrthoSize();
            m_Timer = GTween.To(cur, target, time).OnUpdate((t)=> 
            {
                SceneCameraSystem.Instance.SetOrthoSize((float)t.value.d);
            }).OnComplete(Finish);
            yield break;
        }

        public override void Dispose()
        {
            m_Handler.DisableControl(false);
            m_Timer.Kill();
            m_Timer = null;
        }
    }
}

