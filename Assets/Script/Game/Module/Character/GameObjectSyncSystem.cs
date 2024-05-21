using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicBefore))]
    public class GameObjectSyncSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithNone<DespawningEntity>().WithAll<GameObjectSyncTag>().ForEach((Entity e, Transform trans, ref Translation t, ref Rotation rot ) =>
            {
                if (trans != null)
                {
                    trans.position = t.Value;
                    trans.rotation = rot.Value;
                }
            });
        }
    }
}