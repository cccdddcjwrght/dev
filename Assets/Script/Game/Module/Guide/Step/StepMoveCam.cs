using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepMoveCam : Step
    {
        public override IEnumerator Excute()
        {
            int girdX = (int)m_Config.FloatParam(0);
            int gridZ = (int)m_Config.FloatParam(1);

            var xMin = SceneCameraSystem.Instance.xMove.minValue;
            var xMax = SceneCameraSystem.Instance.xMove.maxValue;
            var zMin = SceneCameraSystem.Instance.zMove.minValue;
            var zMax = SceneCameraSystem.Instance.zMove.maxValue;

            var pos = GameTools.MapAgent.CellToVector(girdX, gridZ);
            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            pos.z = Mathf.Clamp(pos.z, zMin, zMax);
            SceneCameraSystem.Instance.Focus(pos);
            Finish();
            yield break;
        }
    }
}

