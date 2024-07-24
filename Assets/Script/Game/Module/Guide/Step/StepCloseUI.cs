using SGame.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame 
{
    public class StepCloseUI : Step
    {
        public override IEnumerator Excute()
        {
            UILockManager.Instance.Require("guide_close_ui");
            m_Handler.DisableControl(true);

            UIUtils.CloseAllUI("mainui", "flight", "lockred", "SystemTip",
            "Redpoint", "ordertip", "progress", "FoodTip");

            EventManager.Instance.Trigger((int)GameEvent.GUIDE_CLOSE);
            WorktableHud.Instance.Close();

            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(UIRequest));
            var uiRequest = entityQuery.ToEntityArray(Unity.Collections.Allocator.Temp);
            for (int i = 0; i < uiRequest.Length; i++)
            {
                var entity = uiRequest[i];
                UIModule.Instance.CloseUI(entity);
            }
            uiRequest.Dispose();

            Finish();
            yield break;
        }

        public override void Dispose()
        {
            UILockManager.Instance.Release("guide_close_ui");
            m_Handler.DisableControl(false);
        }
    }
}

