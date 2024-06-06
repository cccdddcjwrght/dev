using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepFov : Step
    {
        Action<bool> m_Timer;
        public override IEnumerator Excute()
        {
            yield return m_Handler.WaitGuideMaskClose();
            UIUtils.OpenUI("guidemask");

            float target = m_Config.FloatParam(0);
            float time = m_Config.FloatParam(1);

            float cur = SceneCameraSystem.Instance.GetOrthoSize();
            GTween.To(cur, target, time).OnUpdate((t)=> 
            {
                SceneCameraSystem.Instance.SetOrthoSize((float)t.value.d);
            }).OnComplete(Finish);
            yield break;
        }

        public override void Dispose()
        {
            UIUtils.CloseUIByName("guidemask");
            m_Timer?.Invoke(false);
            m_Timer = null;
        }
    }
}

