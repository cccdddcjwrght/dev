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
        GameObject m_Arrow;
        List<Character> characters = new List<Character>();
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true); //禁用点击
            yield return m_Handler.WaitGuideMaskClose();
            m_Handler.DisableControl(false); //启用点击
            UIUtils.OpenUI("guidemask");

            yield return WaitCharacter();
            Character character = characters[0];

            var path = "Assets/BuildAsset/Prefabs/Other/guideArrow";
            var wait = SpawnSystem.Instance.SpawnAndWait(path);
            yield return wait;
            m_Arrow = wait.Current as GameObject;
            m_Arrow.transform.SetParent(character.pos);
            m_Arrow.transform.localPosition = new Vector3(0, 2, 0);

            var duration = m_Config.FloatParam(0);
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);
            m_Timer = GTween.To(0, 1, duration).OnUpdate(()=> 
            {
                if (character.pos != null) 
                {
                    var pos = character.pos.position;
                    pos.x = Mathf.Clamp(pos.x, xMin, xMax);
                    pos.z = Mathf.Clamp(pos.z, zMin, zMax);
                    SceneCameraSystem.Instance.PosX(pos.x);
                    SceneCameraSystem.Instance.PosZ(pos.z);
                }
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
            UIUtils.CloseUIByName("dialogue");
            GTween.Kill(m_Timer);
            GameObject.Destroy(m_Arrow);
            m_Arrow = null;
            m_Timer = null;
            characters = null;
        }
    }
}

