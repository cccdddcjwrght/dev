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
            Entities.WithNone<DespawningEntity>().WithAll<GameObjectSyncTag>().ForEach((Entity e, Transform trans, in LocalToWorld lt ) =>
            {
                if (trans != null)
                {
                    trans.position = lt.Position;
                    trans.rotation = lt.Rotation;
                }
            }).WithoutBurst().Run();
        }
    }
}