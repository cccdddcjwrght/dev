using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepMoveCam : Step
    {
        GTweener m_Timer;
        bool isDispose = false;
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true);

            float duration;
            int girdX, gridZ, toGridX, toGridZ;
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);

            if (m_Config.FloatParamLength <= 3)
            {
                girdX = (int)m_Config.FloatParam(0);
                gridZ = (int)m_Config.FloatParam(1);
                duration = m_Config.FloatParam(2);

                var pos = GameTools.MapAgent.CellToVector(girdX, gridZ);
                pos.x = Mathf.Clamp(pos.x, xMin, xMax);
                pos.z = Mathf.Clamp(pos.z, zMin, zMax);
                SceneCameraSystem.Instance.Focus(pos, time:duration);
            }
            else 
            {
                girdX = (int)m_Config.FloatParam(0);
                gridZ = (int)m_Config.FloatParam(1);
                toGridX = (int)m_Config.FloatParam(2);
                toGridZ = (int)m_Config.FloatParam(3);
                duration = m_Config.FloatParam(4);

                var pos = GameTools.MapAgent.CellToVector(girdX, gridZ);
                pos.x = Mathf.Clamp(pos.x, xMin, xMax);
                pos.z = Mathf.Clamp(pos.z, zMin, zMax);
                SceneCameraSystem.Instance.Focus(pos);
                yield return new WaitForSeconds(0.02f);

                if (isDispose) yield break;

                var toPos = GameTools.MapAgent.CellToVector(toGridX, toGridZ);
                toPos.x = Mathf.Clamp(toPos.x, xMin, xMax);
                toPos.z = Mathf.Clamp(toPos.z, zMin, zMax);
                SceneCameraSystem.Instance.Focus(toPos, time:duration);
            }

            m_Timer = GTween.To(0, 1, duration).OnComplete(Finish);
            yield break;
        }

        public override void Dispose()
        {
            isDispose = true;
            m_Handler.DisableControl(false);
            m_Timer?.Kill();
            m_Timer = null;
        }
    }
}

