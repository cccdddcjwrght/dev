using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepMoveCam : Step
    {
        Action<bool> m_Timer;

        public override IEnumerator Excute()
        {
            yield return m_Handler.WaitGuideMaskClose();
            UIUtils.OpenUI("guidemask");

            float duration;
            int girdX, gridZ, toGridX, toGridZ;
            var xMin = SceneCameraSystem.Instance.xMove.minValue;
            var xMax = SceneCameraSystem.Instance.xMove.maxValue;
            var zMin = SceneCameraSystem.Instance.zMove.minValue;
            var zMax = SceneCameraSystem.Instance.zMove.maxValue;

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

                var toPos = GameTools.MapAgent.CellToVector(toGridX, toGridZ);
                toPos.x = Mathf.Clamp(toPos.x, xMin, xMax);
                toPos.z = Mathf.Clamp(toPos.z, zMin, zMax);
                SceneCameraSystem.Instance.Focus(toPos, time:duration);
            }
            m_Timer = Utils.Timer(duration + 0.1f, null, completed: Finish);
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

