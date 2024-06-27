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
            SceneCameraSystem.Instance.GetLimitXZ(out float xMin, out float xMax, out float zMin, out float zMax);

            var pet_pos = PetLogic.Instance.transform.position;
            pet_pos.x = Mathf.Clamp(pet_pos.x, xMin, xMax);
            pet_pos.y = Mathf.Clamp(pet_pos.z, zMin, zMax);
            SceneCameraSystem.Instance.Focus(pet_pos);
            Finish();
            yield break;
        }
    }
}

