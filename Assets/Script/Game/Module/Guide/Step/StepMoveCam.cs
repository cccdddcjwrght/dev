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
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true);

            float duration;
            int grid_x, grid_z, to_gridx, to_gridz;
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);
            var start_pos = Vector3.zero;
            Vector3 end_pos = Vector3.zero;

            if (m_Config.FloatParamLength <= 3)
            {
                SceneCameraSystem.Instance.Cache(out start_pos, out float rot, out int fov);
                grid_x = (int)m_Config.FloatParam(0);
                grid_z = (int)m_Config.FloatParam(1);
                duration = m_Config.FloatParam(2);

                end_pos.x = Mathf.Clamp(grid_x, xMin, xMax);
                end_pos.z = Mathf.Clamp(grid_z, zMin, zMax);
            }
            else 
            {
                start_pos.x = (int)m_Config.FloatParam(0);
                start_pos.z = (int)m_Config.FloatParam(1);
                to_gridx = (int)m_Config.FloatParam(2);
                to_gridz = (int)m_Config.FloatParam(3);
                duration = m_Config.FloatParam(4);

                start_pos.x = Mathf.Clamp(start_pos.x, xMin, xMax);
                start_pos.z = Mathf.Clamp(start_pos.z, zMin, zMax);

                end_pos.x = Mathf.Clamp(to_gridx, xMin, xMax);
                end_pos.z = Mathf.Clamp(to_gridz, zMin, zMax);
            }

            m_Timer = GTween.To(start_pos, end_pos, duration).OnUpdate((t)=> 
            {
                SceneCameraSystem.Instance.Focus(t.value.vec3);
            }).OnComplete(Finish);
            yield break;
        }

        public override void Dispose()
        {
            m_Handler.DisableControl(false);
            m_Timer?.Kill();
            m_Timer = null;
        }
    }
}

