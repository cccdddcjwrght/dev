using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{

    /// <summary>
    /// 聚焦到一个顾客
    /// </summary>
    public class StepFocus : Step
    {
        GTweener m_Timer;
        List<Character> characters = new List<Character>();
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true); //禁用点击
            yield return m_Handler.WaitGuideMaskClose();
            m_Handler.DisableControl(false); //启用点击
            UIUtils.OpenUI("guidemask");

            yield return WaitCharacter();
            Character character = characters[0];
            var duration = m_Config.FloatParam(0);
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);
            m_Timer = GTween.To(0, 1, duration).OnUpdate(()=> 
            {
                var pos = character.pos.position;
                pos.x = Mathf.Clamp(pos.x, xMin, xMax);
                pos.z = Mathf.Clamp(pos.z, zMin, zMax);
                SceneCameraSystem.Instance.PosX(pos.x);
                SceneCameraSystem.Instance.PosZ(pos.z);
            }).OnComplete(Finish);

            //yield break;
        }

        public IEnumerator WaitCharacter() 
        {
            while (true) 
            {
                CharacterModule.Instance.FindCharacters(characters, (character) => character.roleType == 3);
                if (characters.Count > 0)
                    yield break;
                yield return null;
            }
        }

        public override void Dispose()
        {
            UIUtils.CloseUIByName("guidemask");
            GTween.Kill(m_Timer);
            m_Timer = null;
            characters = null;
        }
    }
}

