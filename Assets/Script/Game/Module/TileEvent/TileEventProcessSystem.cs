using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using UnityEngine;
namespace SGame
{
    [DisableAutoCreation]
    public partial class TileEventProcessSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.game");
        
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity e, in DynamicBuffer<TileEventTrigger> triggers) =>
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].state == TileEventTrigger.State.FINISH)
                    {
                        log.Info("Finish Trigger=" + triggers[i].titleId);
                    }
                }
            }).WithoutBurst().Run();
        }
    }
}