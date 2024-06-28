using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class StepPetBorn : Step
    {
        public override IEnumerator Excute()
        {
            PetLogic.Instance.LoadEffect();
            PetLogic.Instance.Stop();
            PetLogic.Instance.StopWalkAnim();
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);

            var pet_pos = PetLogic.Instance.transform.position;
            pet_pos.x = Mathf.Clamp(pet_pos.x, xMin, xMax);
            pet_pos.y = Mathf.Clamp(pet_pos.z, zMin, zMax);
            SceneCameraSystem.Instance.Focus(pet_pos);
            PetLogic.Instance.enabled = false;
            GTween.To(0, 1, 1).OnComplete(()=>
            {
                PetLogic.Instance.enabled = true;
            });
            Finish();
            yield break;
        }

        public override void Dispose()
        {
            
        }
    }
}

