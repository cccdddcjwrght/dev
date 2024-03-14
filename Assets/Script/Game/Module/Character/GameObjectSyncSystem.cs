using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicBefore))]
    public partial class GameObjectSyncSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithNone<DespawningEntity>().WithAll<GameObjectSyncTag>().ForEach((Entity e, Transform trans, in Translation t, in Rotation rot ) =>
            {
                if (trans != null)
                {
                    trans.position = t.Value;
                    trans.rotation = rot.Value;
                }
            }).WithoutBurst().Run();
        }
    }
}