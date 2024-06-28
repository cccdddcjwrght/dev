using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepWaiter : Step
    {
        GTweener m_Timer;
        GameObject m_Arrow;
        List<Character> characters = new List<Character>();
        public override IEnumerator Excute()
        {
            m_Handler.DisableControl(true); //���õ��
            yield return m_Handler.WaitGuideMaskClose();
            m_Handler.DisableControl(false); //���õ��
            UIUtils.OpenUI("guidemask");

            yield return WaitCharacter();

            Character character = characters[0];
            var path = "Assets/BuildAsset/Prefabs/Other/guideArrow";
            var wait = SpawnSystem.Instance.SpawnAndWait(path);
            yield return wait;
            m_Arrow = wait.Current as GameObject;
            m_Arrow.transform.SetParent(character.pos);
            m_Arrow.transform.localPosition = new Vector3(0, 2, -0.2f);
            var duration = m_Config.FloatParam(0);
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);
            m_Timer = GTween.To(0, 1, duration).OnUpdate(() =>
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
        }

        IEnumerator WaitCharacter()
        {
            while (true)
            {
                CharacterModule.Instance.FindCharacters(characters, (character) => character.roleType == 1 || character.roleType == 2);
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

